using UnityEngine;
using System.Collections;

public class ServerObjectsInitializeScript : MonoBehaviour {

    public string[] objectsNameToInitialize;
	// Use this for initialization
	void Start () {

        if(Network.isClient)
        {
         //   Debug.LogError("CLIENT");
        }
        else if(Network.isServer)
        {
         //   Debug.LogError("SERVER");
        }

        if (objectsNameToInitialize.Length != 0)
        {
            if(Network.isServer)
                foreach (var obj in objectsNameToInitialize)
                {
                    GameObject.Instantiate(Resources.Load("Prefabs/" + obj));
                }
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
