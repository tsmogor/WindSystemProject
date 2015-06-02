using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BoxScript : MonoBehaviour {

    CrossCircleScript crossCircleScript;

    public string usernameProperty;
	// Use this for initialization
	void Start () 
    {
        crossCircleScript = GameObject.Find("Game(Clone)").GetComponent<CrossCircleScript>();
	}
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void OnMouseDown()
    {
        string username = "";
        if(gameObject.GetComponent<EventTrigger>().enabled == true)
            username = GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name;
        Debug.Log("Zmienam kolor");

        if (Network.isServer && crossCircleScript.adminRound == true)
        {
            Debug.Log("JESTEM SERWERM WSZECHSWIATA");
            GetComponent<NetworkView>().RPC("ChangeBoxColor", RPCMode.All, 1, username);
            crossCircleScript.GetComponent<NetworkView>().RPC("ChangeRound", RPCMode.All);
        }
        else if(Network.isClient && crossCircleScript.adminRound == false)
        {
            GetComponent<NetworkView>().RPC("ChangeBoxColor", RPCMode.All, 0, username);
            crossCircleScript.GetComponent<NetworkView>().RPC("ChangeRound", RPCMode.All);
        }

    }

    [RPC]
    public void ChangeBoxColor(int color,string username)
    {
        Debug.Log("ZMIENAM KOLOR");
        //Color boxColor = GetComponent<SpriteRenderer>().color;

        if (color == 1)
        {
            GetComponent<Text>().text = "X";
            gameObject.GetComponent<EventTrigger>().enabled = true;
        }
        else
        {
            GetComponent<Text>().text = "O";
            gameObject.GetComponent<EventTrigger>().enabled = false;
        }
        //Debug.Log("BoxColor:" + boxColor);
        //Debug.Log("BBCC: " + GetComponent<SpriteRenderer>().color);

        usernameProperty = username;
    }
}
