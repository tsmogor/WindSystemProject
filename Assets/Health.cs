using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public float hitPoints=100f;
	float currentHitPoints;
	// Use this for initialization
	void Start () {
		currentHitPoints = hitPoints;
	}
	[RPC]
	public void TakeDamage(float amt){
		currentHitPoints -= amt;
		if (currentHitPoints <= 0) {
			Die();
		}
	}

	void Die(){
				if (GetComponent<PhotonView> ().instantiationId == 0) {
						Destroy (gameObject);
				} else {
					if(GetComponent<PhotonView> ().isMine){
						if(gameObject.tag=="Player"){
							NetworkManager2 nm2 = GameObject.FindObjectOfType<NetworkManager2>();
							nm2.standbyCamera.SetActive(true);
							nm2.respawnTimer=5f;
						}
						PhotonNetwork.Destroy (gameObject);
					}
				}
		}
}
