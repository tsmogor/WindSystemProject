using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;



public class HostScript : MonoBehaviour 
{

    public string gameName = "Test game";
    public int maxPlayers = 7;
    private GameObject roomNameInputField;
    private GameObject passwordInputField;
    string registeredGameName = "TestServer";

    NetworkLevelLoaderScript networkLevelLoaderScript;


    private int lastLevelPrefix = 0;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

	// Use this for initialization

    void Start()
    {
        networkLevelLoaderScript = GameObject.Find("NetworkLoader").GetComponent<NetworkLevelLoaderScript>();
        roomNameInputField = GameObject.Find("RoomNameInputField");
        passwordInputField = GameObject.Find("PasswordInputField");
        InitializeRoomNameInputField();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartServer()
    {
        gameName = roomNameInputField.GetComponent<InputField>().text;
        bool useNat = !Network.HavePublicAddress();
        // Network.InitializeServer(32, 25000, useNat);
        Debug.Log("NAT SERVER:  " + useNat);

        Network.incomingPassword = passwordInputField.GetComponent<InputField>().text;
        Network.InitializeServer(maxPlayers, 25000 + Random.Range(0, 100), useNat);

        MasterServer.RegisterHost(registeredGameName, gameName, "EXTRA FLAGS");

        networkLevelLoaderScript.CreateGame(gameNameToLoad());
    }

    public void InitializeRoomNameInputField()
    {
        GameObject user = GameObject.Find("User");
        if (user != null)
            roomNameInputField.GetComponent<InputField>().text = user.GetComponent<UserScript>().name + "'s Room";
    }


    public string gameNameToLoad()
    {
        List<GameObject> gameList = new List<GameObject>(GameObject.FindGameObjectsWithTag("GameToggle"));
        return gameList.Find(x => x.GetComponent<Toggle>().isOn == true).GetComponentInParent<Text>().text;
    }

    
}
