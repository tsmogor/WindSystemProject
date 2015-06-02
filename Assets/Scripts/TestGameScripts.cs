using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

    public class Player
    {
        public string name;
        public NetworkPlayer networkPlayer;
        
        public Player(string nick,NetworkPlayer np)
        {
            name = nick;
            networkPlayer = np;
        }
        public string toString()
        {
            return name;
        }
        public bool Equal(Player player)
        {
            if (name == player.name && networkPlayer == player.networkPlayer)
                return true;
            else return false;
        }
    }


public class TestGameScripts : MonoBehaviour {

    private int lastLevelPrefix = 0;
    public List<Player> players = new List<Player>();
    public bool restartGame;
    private GameObject chatText;

    NetworkLevelLoaderScript networkLevelLoaderScript;

	// Use this for initialization
	void Start () {

        networkLevelLoaderScript = GameObject.Find("NetworkLoader").GetComponent<NetworkLevelLoaderScript>();
        chatText = GameObject.Find("TextChat");

           // addPlayerToList(GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name, Network.player);
            addPlayerToList(GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name, Network.player);



        Debug.Log("CLIENT: " + Network.isClient);

        Debug.Log("SERVER: " + Network.isServer);
	}
	
	// Update is called once per frame
	void Update () {

     //   foreach(var a in players)
    //    {
     //       Debug.Log(a.name + " " + a.networkPlayer);
    //    }
	
	}


    public void playersOnline()
    {
        Debug.LogError("PLAYA COUNT:    " + players.Count);
        foreach(var pl in players)
        {
            Debug.LogError(pl.name + " " + pl.networkPlayer);
        }
    }

    public void addPlayerButton()
    {
        GetComponent<NetworkView>().RPC("removePlayer", RPCMode.All, Network.player);

    }

    [RPC]
    void removePlayer(NetworkPlayer player)
    {
        players.Remove(players.Find(x => x.networkPlayer == player));
        GameObject playerOnlineTextBox = GameObject.Find("TextOnline");
        playerOnlineTextBox.GetComponent<Text>().text = "";
        foreach (var p in players)
            playerOnlineTextBox.GetComponent<Text>().text += p.name + "\n"; 
    }

    [RPC] void lastPlayer()
    {
        Debug.LogError("LAST PLAYER !!");
    }

    [RPC]
    void addPlayerToList(string nick,NetworkPlayer netPlayer)
    {
        Debug.LogError("DODAJE GRACZA: " +  nick + netPlayer);
        players.Add(new Player(nick, netPlayer));

        if (Network.isServer)
        {
            GetComponent<NetworkView>().RPC("addPlayerToList", netPlayer, GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name, Network.player);
        }
        GameObject playerOnlineTextBox = GameObject.Find("TextOnline");

        playerOnlineTextBox.GetComponent<Text>().text = "";
        foreach (var p in players)
            playerOnlineTextBox.GetComponent<Text>().text += p.name + "\n"; 
    }


    void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player " + player.guid + " connected from " + player.ipAddress + ":" + player.port);
        GetComponent<NetworkView>().RPC("sendInfoToServer", player);
    }

    [RPC]
    public void sendInfoToServer()
    {
        //addPlayerToList(GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name, Network.player);
        GetComponent<NetworkView>().RPC("addPlayerToList", RPCMode.OthersBuffered, GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name, Network.player);
     
    }

    public void shootFire()
    {
        Debug.LogError("shootFire()" + GetComponent<NetworkView>().viewID.owner + "isMess: " + Network.isMessageQueueRunning + "GROUP:" + GetComponent<NetworkView>().group);
        GetComponent<NetworkView>().RPC("printmess", RPCMode.All);
        
     //   networkView.RPC("addPlayerToList", RPCMode.Server, GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name, Network.player);
    }

    [RPC]
    private void printmess()
    {
        Debug.LogError("POZDROWIENIA");
    }
    void OnConnectedToServer()
    {
       // networkView.RPC("test2", RPCMode.Server, GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name);
       // networkView.RPC("addPlayerToList", RPCMode.Server, GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name, Network.player);
        Debug.Log("CONNECTED TO SERVER");

        //Network.Instantiate(Resources.Load("Prefabs/Player"), new Vector3(-3, 0, 0), Quaternion.identity, 0);
        //  Instantiate(Resources.Load("Prefabs/PlayerUI"), transform.position + new Vector3(0,0,0), transform.rotation);
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        removePlayer(player);
        GetComponent<NetworkView>().RPC("removePlayer", RPCMode.Others, player);
        // networkView.RPC("test2", RPCMode.Server, GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name);
      //  networkView.RPC("addPlayerToList", RPCMode.Server, GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name, Network.player);
       // Debug.Log("CONNECTED TO SERVER");

        //Network.Instantiate(Resources.Load("Prefabs/Player"), new Vector3(-3, 0, 0), Quaternion.identity, 0);
        //  Instantiate(Resources.Load("Prefabs/PlayerUI"), transform.position + new Vector3(0,0,0), transform.rotation);
    }

    void OnApplicationQuit()
    {
        Network.Disconnect();
    }

    public void buttonLoadLevel(string levelName)
    {
        networkLevelLoaderScript.CreateGame(levelName);
    }

    public void sendMessageButton()
    {
        GetComponent<NetworkView>().RPC("RPCTEST", RPCMode.All);
        GetComponent<NetworkView>().RPC("SendMessage", RPCMode.All, GameObject.Find("ChatInputText").GetComponent<Text>().text, GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name);
    }

    [RPC]
    public void RPCTEST()
    {
        Debug.LogError("TEST!!");
    }

    [RPC]
    public void SendMessage(string textMessage,string playerName)
    {
        if (chatText != null)
            Debug.Log("NOT NULL");

        Debug.LogError("COS TAM USTAWIAM:" + textMessage);
        chatText.GetComponent<Text>().text += playerName + ": " + textMessage + "\n";
    }

    [RPC]
    public void LoadLevel(string level, int levelPrefix)
    {
        Debug.Log("Loading level " + level + " with prefix " + levelPrefix);
        lastLevelPrefix = levelPrefix;

        // There is no reason to send any more data over the network on the default channel,
        // because we are about to load the level, thus all those objects will get deleted anyway
        Network.SetSendingEnabled(0, false);

        // We need to stop receiving because first the level must be loaded.
        // Once the level is loaded, RPC's and other state update attached to objects in the level are allowed to fire
        Network.isMessageQueueRunning = false;

        // All network views loaded from a level will get a prefix into their NetworkViewID.
        // This will prevent old updates from clients leaking into a newly created scene.
       Network.SetLevelPrefix(levelPrefix);
        Application.LoadLevel(level);
        //yield;
        //yield return new WaitForSeconds(2);
        
        
        // Allow receiving data again
       //Network.isMessageQueueRunning = true;
        // Now the level has been loaded and we can start sending out data
        Network.isMessageQueueRunning = true;
        Network.SetSendingEnabled(0, true);
        
        // Notify our objects that the level and the network is ready
        foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
            go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
    }

    public void goToRoomList()
    {
        Network.Disconnect();
        Application.LoadLevel("RoomList");
    }
}
