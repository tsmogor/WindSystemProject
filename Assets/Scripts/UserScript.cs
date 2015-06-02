using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UserScript : MonoBehaviour {

    public string name;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetUsername()
    {
        if (GameObject.FindGameObjectWithTag("NickInputField").GetComponent<Text>().text.Length != 0)
        {
            name = GameObject.FindGameObjectWithTag("NickInputField").GetComponent<Text>().text;
            Application.LoadLevel("RoomList");
        }
    }
}
