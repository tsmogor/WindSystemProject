using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;
public class PlayerShooting : MonoBehaviour {
	public float damage=25f;
	public float fireRate = 0.5f;
	// Use this for initializat
	float cooldown=0;
	private bool _shot;
	FXManager fxmanager;
	void Start(){
		fxmanager = GameObject.FindObjectOfType<FXManager> ();
	}
	// Update is called once per frame
	void Update () {
		cooldown -= Time.deltaTime;
		if (CrossPlatformInputManager.GetButtonDown("Fire")){
			Fire();
		}
	}
	void Fire()
	{
			if (cooldown > 0) {
				return;
			}
		Ray ray=new Ray(Camera.main.transform.position,Camera.main.transform.forward);
		Transform hitTransform;
		Vector3 hitPoint;

		hitTransform = FindClosestHitObject (ray,out hitPoint);
		if (hitTransform != null) 
		{
			Health h= hitTransform.transform.GetComponent<Health>();
			while(h==null && hitTransform.parent)
			{
				hitTransform=hitTransform.parent;
				h=hitTransform.GetComponent<Health>();
			}
			if(h!=null){
				h.GetComponent<PhotonView>().RPC("TakeDamage",PhotonTargets.AllBuffered,damage);
				//h.TakeDamage(damage);
			}
			if(fxmanager!=null)
			{
				fxmanager.GetComponent<PhotonView>().RPC("SniperBulletFX",PhotonTargets.All,Camera.main.transform.position,hitPoint);
			}

		}
		else{
			if(fxmanager!=null)
			{
				hitPoint=Camera.main.transform.position+(Camera.main.transform.forward*100f);
				fxmanager.GetComponent<PhotonView>().RPC("SniperBulletFX",PhotonTargets.All,Camera.main.transform.position,hitPoint);
			}
		}
		cooldown = fireRate;

		//if(Physics.Raycast(ray,out hitInfo)
	}
	Transform FindClosestHitObject(Ray ray,out Vector3 hitPoint)
	{

		RaycastHit[] hits = Physics.RaycastAll (ray);
		Transform closerHit = null;
		float distance = 0;
		hitPoint = Vector3.zero;
		foreach (RaycastHit hit in hits) {
			if(hit.transform != this.transform && (closerHit==null || hit.distance < distance)){
				closerHit=hit.transform;
				distance=hit.distance;
				hitPoint=hit.point;
			}
		}
		return closerHit;
	}
}
