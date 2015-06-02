using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class NetworkLevelLoaderScript : MonoBehaviour
{
    private int lastLevelPrefix = 0;

    public void CreateGame(string levelName)
    {

        if (Network.isServer)
            Application.LoadLevel(levelName);
        GetComponent<NetworkView>().RPC("LoadLevel", RPCMode.AllBuffered, levelName, lastLevelPrefix + 1);
    }

    public void Start()
    {
        DontDestroyOnLoad(this);
    }

    [RPC]
    public IEnumerator LoadLevel(string level, int levelPrefix)
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
        yield return new WaitForSeconds(0);

        // Allow receiving data again
        Network.isMessageQueueRunning = true;
        // Now the level has been loaded and we can start sending out data
        Network.SetSendingEnabled(0, true);

        // Notify our objects that the level and the network is ready
        foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
            go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
    }
}