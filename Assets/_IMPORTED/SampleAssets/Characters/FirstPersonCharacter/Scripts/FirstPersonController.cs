using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;
using UnitySampleAssets.Utility;

namespace UnitySampleAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {

        //////////////////////// exposed privates ///////////////////////
		/// 
		/// 
		[SerializeField] private float lookSpeed=4;
        [SerializeField] private bool _isWalking;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] [Range(0f, 1f)] private float runstepLenghten;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float stickToGroundForce;
        [SerializeField] private float _gravityMultiplier;
        [SerializeField] private MouseLook _mouseLook;
        [SerializeField] private bool useFOVKick;
        [SerializeField] private FOVKick _fovKick = new FOVKick();
        [SerializeField] private bool useHeadBob;
        [SerializeField] private CurveControlledBob _headBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob _jumpBob = new LerpControlledBob();
        [SerializeField] private float _stepInterval;

       // [SerializeField] private AudioClip[] _footstepSounds;
                                             // an array of footstep sounds that will be randomly selected from.

       // [SerializeField] private AudioClip _jumpSound; // the sound played when character leaves the ground.
       //[SerializeField] private AudioClip _landSound; // the sound played when character touches back on ground.

        ///////////////// non exposed privates /////////////////////////
        private Camera _camera;
        private bool _jump;
        private float _yRotation;
        private CameraRefocus _cameraRefocus;
        private Vector2 _input;
        private Vector3 _moveDir = Vector3.zero;
        private CharacterController _characterController;
        private CollisionFlags _collisionFlags;
        private bool _previouslyGrounded;
        private Vector3 _originalCameraPosition;
        private float _stepCycle = 0f;
        private float _nextStep = 0f;
        private bool _jumping = false;
		Animator anim;
        // Use this for initialization
        private void Start()
        {
			anim = GetComponent<Animator> ();
            _characterController = GetComponent<CharacterController>();
            _camera = Camera.main;
            _originalCameraPosition = _camera.transform.localPosition;
            _cameraRefocus = new CameraRefocus(_camera, transform, _camera.transform.localPosition);
            _fovKick.Setup(_camera);
            _headBob.Setup(_camera, _stepInterval);
            _stepCycle = 0f;
            _nextStep = _stepCycle/2f;
            _jumping = false;
        }

        // Update is called once per frame
        private void Update()
        {
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!_jump)
                _jump = CrossPlatformInputManager.GetButtonDown("Jump");

            if (!_previouslyGrounded && _characterController.isGrounded)
            {
                StartCoroutine(_jumpBob.DoBobCycle());
              //  PlayLandingSound();
                _moveDir.y = 0f;
                _jumping = false;
            }
            if (!_characterController.isGrounded && !_jumping && _previouslyGrounded)
            {
                _moveDir.y = 0f;
            }

            _previouslyGrounded = _characterController.isGrounded;
        }

        private void PlayLandingSound()
        {
//            GetComponent<AudioSource>().clip = _landSound;
       //     GetComponent<AudioSource>().Play();
            _nextStep = _stepCycle + .5f;
        }

        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = _camera.transform.forward*_input.y + _camera.transform.right*_input.x;
			//direction = transform.rotation * new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, _characterController.radius, Vector3.down, out hitInfo,
                               _characterController.height/2f);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            _moveDir.x = desiredMove.x*speed;
            _moveDir.z = desiredMove.z*speed;


            if (_characterController.isGrounded)
            {
                _moveDir.y = -stickToGroundForce;
				//anim.SetFloat ("Speed", _moveDir.magnitude);

                if (_jump)
                {
                    _moveDir.y = jumpSpeed;
//                    PlayJumpSound();
                    _jump = false;
                    _jumping = true;
                }
            }
            else
            {
				//anim.SetFloat ("Speed", _moveDir.magnitude);
                _moveDir += Physics.gravity*_gravityMultiplier;
            }
			if (_moveDir.y <= -500) {
				if (GetComponent<PhotonView> ().instantiationId == 0) {
					Destroy (gameObject);
				} else {
					if(GetComponent<PhotonView> ().isMine){
						if(gameObject.tag=="Player"){
							NetworkManager2 nm2 = GameObject.FindObjectOfType<NetworkManager2>();
							nm2.standbyCamera.SetActive(true);
							nm2.respawnTimer=3f;
						}
						PhotonNetwork.Destroy (gameObject);
					}
				}
			}
            _collisionFlags = _characterController.Move(_moveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);
        }
		/*
        private void PlayJumpSound()
        {
            GetComponent<AudioSource>().clip = _jumpSound;
            GetComponent<AudioSource>().Play();
        }
*/
        private void ProgressStepCycle(float speed)
        {
			if (_characterController.velocity.sqrMagnitude > 0 && (_input.x != 0 || _input.y != 0)) {
				_stepCycle += (_characterController.velocity.magnitude + (speed * (_isWalking ? 1f : runstepLenghten))) *
					Time.fixedDeltaTime;
				anim.SetFloat ("Speed", _moveDir.magnitude);

			} else {
				anim.SetFloat ("Speed", 0);
			}
		
            if (!(_stepCycle > _nextStep)) return;

            _nextStep = _stepCycle + _stepInterval;

           // PlayFootStepAudio();
        }
		/*
        private void PlayFootStepAudio()
        {
            if (!_characterController.isGrounded) return;
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, _footstepSounds.Length);
//           GetComponent<AudioSource>().clip = _footstepSounds[n];
          // GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            // move picked sound to index 0 so it's not picked next time
            _footstepSounds[n] = _footstepSounds[0];
            _footstepSounds[0] = GetComponent<AudioSource>().clip;
        }*/

        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!useHeadBob) return;
            if (_characterController.velocity.magnitude > 0 && _characterController.isGrounded)
            {
                _camera.transform.localPosition =
                    _headBob.DoHeadBob(_characterController.velocity.magnitude +
                                       (speed*(_isWalking ? 1f : runstepLenghten)));
                newCameraPosition = _camera.transform.localPosition;
                newCameraPosition.y = _camera.transform.localPosition.y - _jumpBob.Offset();
            }
            else
            {
                newCameraPosition = _camera.transform.localPosition;
                newCameraPosition.y = _originalCameraPosition.y - _jumpBob.Offset();
            }
            _camera.transform.localPosition = newCameraPosition;

            _cameraRefocus.SetFocusPoint();
        }

        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxisRaw("Vertical");

            bool waswalking = _isWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            _isWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = _isWalking ? walkSpeed : runSpeed;
            _input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (_input.sqrMagnitude > 1) _input.Normalize();

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (_isWalking != waswalking && useFOVKick && _characterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!_isWalking ? _fovKick.FOVKickUp() : _fovKick.FOVKickDown());
            }
        }

        private void RotateView()
        {
			#if !MOBILE_INPUT
			Vector2 mouseInput = _mouseLook.Clamped(_yRotation, transform.localEulerAngles.y);
			_camera.transform.localEulerAngles = new Vector3(-mouseInput.y, _camera.transform.localEulerAngles.y,
			                                                 _camera.transform.localEulerAngles.z);
			transform.localEulerAngles = new Vector3(0, mouseInput.x, 0);
			#else
			Vector2 mouseInput = new Vector2(CrossPlatformInputManager.GetAxis("HorizontalLook"),
			                                 CrossPlatformInputManager.GetAxis("VerticalLook"));
			
			float camX = _camera.transform.localEulerAngles.x;
			if((camX > 280 && camX<= 360) ||
			   (camX >=0 && camX< 80) ||
			   (camX >=80 && camX< 180 && mouseInput.y>0) ||
			   (camX > 180 && camX<= 280 && mouseInput.y<0) )
			{
				_camera.transform.localEulerAngles += new Vector3(-mouseInput.y*lookSpeed,_camera.transform.localEulerAngles.y,
				                                                  _camera.transform.localEulerAngles.z);
			}
			transform.localEulerAngles += new Vector3(0,mouseInput.x*lookSpeed,0);
			#endif
			_yRotation = mouseInput.y;
			_cameraRefocus.GetFocusPoint ();
        }
    }
}