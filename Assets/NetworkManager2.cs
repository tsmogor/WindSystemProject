using UnityEngine;
using System.Collections.Generic;

public class NetworkManager2 : MonoBehaviour {
	public GameObject standbyCamera;
	SpawnSpot[] spawnSpots;
	public bool offlineMode=false;
	bool connecting = false;
	public float respawnTimer=0;
	List<string> chatMessages;
	int maxChatMessages=3;
	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot> ();
		PhotonNetwork.player.name = PlayerPrefs.GetString ("Name", "SpaceWarrior");
		chatMessages = new List<string> ();
	}
	void Connect(){
		if (offlineMode) {
			PhotonNetwork.offlineMode=true;
			OnJoinedLobby();
		} 
		else {
				PhotonNetwork.ConnectUsingSettings ("MonGAMES v.1.0");
		}
		
	}
	void OnDestroy(){
		PlayerPrefs.SetString ("Name", PhotonNetwork.player.name);
	}


	void AddChatMessage(string m){
		GetComponent<PhotonView> ().RPC ("AddChatMessage_RPC", PhotonTargets.AllBuffered, m);
	}



	[RPC]
	void AddChatMessage_RPC(string m){
		while (chatMessages.Count >= maxChatMessages) {
			chatMessages.RemoveAt(0);
		}
		chatMessages.Add (m);
	}

	void OnGUI(){
				GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
				if (PhotonNetwork.connected == false && connecting == false)
				{
					GUILayout.BeginArea(new Rect(0,0,Screen.width,Screen.height));
					
					GUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace();
					GUILayout.BeginVertical();
					GUILayout.FlexibleSpace();
					GUILayout.BeginHorizontal();
				
					GUILayout.Label ("Name: ");
					PhotonNetwork.player.name=GUILayout.TextField(PhotonNetwork.player.name);
					GUILayout.EndHorizontal();
		
					if(GUILayout.Button("Join Server"))
			   		{
						connecting=true;
						Connect ();
					}
					GUILayout.FlexibleSpace();
					GUILayout.EndVertical();
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
					GUILayout.EndArea ();
				}
				if (PhotonNetwork.connected == true && connecting==false) {
					GUILayout.BeginArea(new Rect(0,0,Screen.width,Screen.height));
					GUILayout.BeginVertical();
					GUILayout.FlexibleSpace();
					
					foreach(string msg in chatMessages){
						GUILayout.Label(msg);
					}

					GUILayout.EndVertical();
					GUILayout.EndArea ();
				}
	}
	void OnJoinedLobby()
	{
		Debug.Log ("Waiting on Lobby");
		PhotonNetwork.JoinRandomRoom ();
	}
	void OnPhotonRandomJoinFailed(){
		Debug.Log ("OnPhotonRandomJoinFailed");
		PhotonNetwork.CreateRoom (null);
	}
	void OnJoinedRoom()
	{
		Debug.Log ("OnJoinedRoom");
		connecting = false;
		SpawnMyPlayer ();
	}
	void SpawnMyPlayer(){
		AddChatMessage ("Player " + PhotonNetwork.player.name + " connected.");
		if (spawnSpots == null) {
			Debug.LogError ("WTF?");
			return;
		}
		SpawnSpot mySpawnSpot=spawnSpots[Random.Range (0,spawnSpots.Length)];
		GameObject myPlayerGO = (GameObject) PhotonNetwork.Instantiate ("PlayerController",mySpawnSpot.transform.position,mySpawnSpot.transform.rotation,0);
		//PhotonNetwork.Instantiate ("PlayerController",mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		standbyCamera.SetActive(false);
		//((MonoBehaviour) myPlayerGO.GetComponent("FPSInputController")).enabled = true;
	//	((MonoBehaviour) myPlayerGO.GetComponent("MouseLook")).enabled = true;
		((MonoBehaviour) myPlayerGO.GetComponent("FirstPersonController")).enabled = true;
		//((MonoBehaviour) myPlayerGO.GetComponent("PlayerMovement")).enabled = true;
		((MonoBehaviour) myPlayerGO.GetComponent("PlayerShooting")).enabled = true;
		myPlayerGO.transform.FindChild ("Main Camera").gameObject.SetActive (true);
//		myPlayerGO.transform.FindChild ("FirstPersonController").gameObject.SetActive (true);
	}
	void Update(){

		if (respawnTimer > 0) {
			respawnTimer-=Time.deltaTime;
			if(respawnTimer<=0)
			{
				SpawnMyPlayer();
			}
		}
	}
}