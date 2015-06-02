using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class CountdownScript : MonoBehaviour {

    CountryScript countryScript;
    float timeStamp;
    public float coolDownPeriodInSeconds = 10;
	// Use this for initialization
	void Start () {
        countryScript = GameObject.Find("GameCountry(Clone)").GetComponent<CountryScript>();
        timeStamp = Time.time + coolDownPeriodInSeconds;

        GameObject.Find("EndRoundButton").GetComponent<Button>().interactable = false;

        Destroy(gameObject, coolDownPeriodInSeconds);
	}
	
    void OnDestroy()
    {
        countryScript.disableCountryUI();
        EventSystem.current.SetSelectedGameObject(null, null);
        string[] tempAnswers = countryScript.getAnswers();

        if(Network.isClient)
            GameObject.Find("GameCountry(Clone)").GetComponent<NetworkView>().RPC("sendAnswers", RPCMode.Server, GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name, Network.player, tempAnswers[0], tempAnswers[1], tempAnswers[2], tempAnswers[3]);
        else if(Network.isServer)
        {
            countryScript.sendAnswers(GameObject.FindGameObjectWithTag("User").GetComponent<UserScript>().name, Network.player, tempAnswers[0], tempAnswers[1], tempAnswers[2], tempAnswers[3]);
   
        }

    }

	// Update is called once per frame
	void Update () {
        GetComponentInChildren<Text>().text = ((int)(timeStamp - Time.time)).ToString();
	}
}
