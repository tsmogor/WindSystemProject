using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	float speed =8f;
	float jumpspeed = 3f;
	CharacterController cc;
	Animator anim;
	float verticalVelocity=0;
	Vector3 direction=Vector3.zero;
	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
		anim = GetComponent<Animator> ();

	}

		// Update is called once per frame
	void Update () {
				direction = transform.rotation * new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
				if (direction.magnitude > 1f) {
						direction=direction.normalized;
				}

		anim.SetFloat ("Speed", direction.magnitude);
		//jumping
		if (cc.isGrounded && Input.GetButton ("Jump")) {
						verticalVelocity = jumpspeed;
				}

	}
	void FixedUpdate()
	{
		Vector3 dist = direction * speed * Time.deltaTime;
		if (cc.isGrounded && verticalVelocity < 0) {
						anim.SetBool ("Jumping", false);
						verticalVelocity = Physics.gravity.y * Time.deltaTime;
				} 
		else {
						if (Mathf.Abs (verticalVelocity) > jumpspeed * 0.75f) {
								anim.SetBool ("Jumping", true);
						}
			verticalVelocity += Physics.gravity.y * Time.deltaTime;
		}
		dist.y = verticalVelocity * Time.deltaTime;
		cc.Move (dist);
	}
}
