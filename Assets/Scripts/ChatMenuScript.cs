using UnityEngine;
using System.Collections;

public class ChatMenuScript : MonoBehaviour {

    float timeStamp;
    public float coolDownPeriodInSeconds = 0.3f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time >= timeStamp && Input.GetKey(KeyCode.Escape))
        {

            GetComponent<Canvas>().enabled = !GetComponent<Canvas>().enabled;
            timeStamp = Time.time + coolDownPeriodInSeconds;
            Input.ResetInputAxes();
        }
	}
}
