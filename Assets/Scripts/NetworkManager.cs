using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour {


    public int aa = 0;

    public void Start()
    {
        //if (Network.isServer && networkView.isMine)
        //{
          //  Debug.LogError("ODPALAM START");
        Network.Instantiate(Resources.Load("Prefabs/Player"), new Vector3(Random.Range(-5, 5), 2, Random.Range(-5, 5)), Quaternion.identity, 0);
         //   aa++;
       // }
            if (Network.isClient)
        {
            //Network.Instantiate(Resources.Load("Prefabs/Player"), new Vector3(Random.Range(-5, 5), 2, Random.Range(-5, 5)), Quaternion.identity, 1);
        }
        Debug.Log("STRZELAMY !!! NIEMCOM GOLA");
        //if(GameObject.FindGameObjectWithTag("HostGame") != null)
        //StartServer();
    }
    
    public void networkresp()
    {
        Network.Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 0, 0), Quaternion.identity, 0);
    }
    public void localresp()
    {
        GameObject.Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 0, 0), Quaternion.identity);
    }

    private bool connected = false; // connected flag
    
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Clean up after player " + player);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        if (Network.isServer)
            Debug.Log("Local server connection disconnected");
        else
            if (info == NetworkDisconnection.LostConnection)
                Debug.Log("Lost connection to the server");
            else
                Debug.Log("Successfully diconnected from the server");
        // TODO: remove objects + go back
        
    }
    

    void OnMasterServerEvent(MasterServerEvent masterServerEvent)
    {
        if(masterServerEvent == MasterServerEvent.RegistrationSucceeded)
        {
         //   Debug.Log("Registration succesful");
        }
    }
    void OnConnectedToServer()
    {
        connected = true;
        Debug.Log("CONNECTED TO SERVER");
        //Network.Instantiate(Resources.Load("Prefabs/Player"), new Vector3(-3, 0, 0), Quaternion.identity, 0);
        //  Instantiate(Resources.Load("Prefabs/PlayerUI"), transform.position + new Vector3(0,0,0), transform.rotation);
    }
    public void OnServerInitialized()
    {
        Debug.Log("Server initialized");
        connected = true;
        //Network.Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 0, 0), Quaternion.identity, 0);

    }
    private void OnDisconnectedFromServer()
    {
        connected = false;
    }
    
    void Update()
    {


    }
        

}
