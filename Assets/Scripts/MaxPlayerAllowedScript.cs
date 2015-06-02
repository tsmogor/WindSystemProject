using UnityEngine;
using System.Collections;

public class MaxPlayerAllowedScript : MonoBehaviour {

    public int playersAllowed = 8;
	// Use this for initialization
	void Start () {
	
        if(Network.isServer)
        {
            Network.maxConnections = playersAllowed;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
