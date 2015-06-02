using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ValidateFieldScript : MonoBehaviour {


    public GameObject inputField;
	// Use this for initialization

	// Update is called once per frame
	void Update () 
    {
        //Debug.Log("NICK LENGTH: " + inputNick.GetComponent<Text>().text.Length);
        if (inputField.GetComponent<Text>().text.Length == 0) // if user enter the nickname can join the game
            gameObject.GetComponent<Button>().interactable = false;
        else
            gameObject.GetComponent<Button>().interactable = true;

	
	}
}
