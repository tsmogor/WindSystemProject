using UnityEngine;
using System.Collections;

public class FXManager : MonoBehaviour {

	public GameObject SnipperBulletFXPrefab;
	//public AudioClip bulletHitFX;
	[RPC]
	void SniperBulletFX(Vector3 startPos,Vector3 endPos){
		GameObject sniperFX=(GameObject) Instantiate (SnipperBulletFXPrefab, startPos, Quaternion.LookRotation(endPos-startPos));
		LineRenderer lr = sniperFX.transform.Find("LineFX").GetComponent<LineRenderer>();
		lr.SetPosition (0, startPos);
		lr.SetPosition (1, endPos);
	}
}
