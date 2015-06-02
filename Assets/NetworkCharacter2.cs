﻿using UnityEngine;
using System.Collections;

public class NetworkCharacter2 : Photon.MonoBehaviour {
	Vector3 realPosition=Vector3.zero;
	Quaternion realRotation=Quaternion.identity;
	Animator anim;
	bool gotFirstUpdate=false;
	// Use this for initialization
	void Start () {
		InitAnim ();
	}
	void InitAnim()
	{
		if (anim == null) {
				
						anim = GetComponent<Animator> ();
			}
	}
	// Update is called once per frame
	void Update () {
		if(photonView.isMine)
	    {

		}
		else{
			transform.position=Vector3.Lerp (transform.position,realPosition,0.1f);
			transform.rotation=Quaternion.Lerp (transform.rotation,realRotation,0.1f);
		}
	}
	public void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info){
		InitAnim ();
		if (stream.isWriting) {
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(anim.GetFloat("Speed"));
			stream.SendNext(anim.GetBool("Jumping"));
		}
		else {
			realPosition=(Vector3) stream.ReceiveNext();
			realRotation=(Quaternion) stream.ReceiveNext();
			anim.SetFloat ("Speed",(float)stream.ReceiveNext());
			anim.SetBool ("Jumping",(bool)stream.ReceiveNext());
			//anim.SetFloat ("AimAngle",(float)stream.ReceiveNext());

			if(gotFirstUpdate==false)
			{
				transform.position=realPosition;
				transform.rotation=realRotation;
				gotFirstUpdate=true;
			}

		}
	}
}
