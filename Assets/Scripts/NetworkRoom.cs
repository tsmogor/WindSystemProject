using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkRoom : MonoBehaviour {


    bool listRefreshing = false;
    float refreshRequestLength = 5.0f;
    HostData[] hostData;
    string registeredGameName = "TestServer";


    Toggle togglePassword;
    InputField passwordInputField;
    public  IEnumerator RefreshHostList()
    {
   //     Debug.Log("Refreshing...");
        MasterServer.RequestHostList(registeredGameName);
        float timeStarted = Time.time;
        float timeEnd = Time.time + refreshRequestLength;

        while (Time.time < timeEnd)
        {
            hostData = MasterServer.PollHostList();
                yield return new WaitForEndOfFrame();
        }
        if (hostData == null || hostData.Length == 0)
        {
   //         Debug.Log("No active servers found.");
        }
        else
        {
            //Refresh();
   //         Debug.Log("Server found " + hostData.Length);
        }
    }
    public void Goo()
    {
        
        GameObject go = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/ButtonServer"));

        
    }

    public void Refresh()
    {
        int serverCountToShow = 0;
        if (hostData != null)
        {
            foreach (var a in GameObject.FindGameObjectsWithTag("ButtonServer"))
            {
                Destroy(a);
            }
            for (int i = 0; i < hostData.Length; i++)
            {
                if(hostData[i].passwordProtected && togglePassword.isOn == false)
                {

                }
                else
                {
                GameObject go = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/ButtonServer"));


             //   Debug.Log("1: " + go.GetComponent<RectTransform>().position);
             //   Debug.Log("2: " + go.GetComponent<RectTransform>().transform.position);
             //   Debug.Log("3: " + go.GetComponent<RectTransform>().transform.localPosition);
             //   Debug.Log("4: " + go.GetComponent<RectTransform>().localPosition);
                   
                go.transform.SetParent(GameObject.Find("Test22").transform);
            //    Debug.Log("11: " + go.GetComponent<RectTransform>().position);
            //    Debug.Log("22: " + go.GetComponent<RectTransform>().transform.position);
             //   Debug.Log("33: " + go.GetComponent<RectTransform>().transform.localPosition);
            //    Debug.Log("44: " + go.GetComponent<RectTransform>().localPosition);
                if (serverCountToShow == 0)
                    go.GetComponent<RectTransform>().anchoredPosition = new Vector2(-15, -20 * (serverCountToShow + 1));
                else
                    go.GetComponent<RectTransform>().anchoredPosition = new Vector2(-15, -25 * (serverCountToShow + 1));
                go.GetComponentInChildren<Text>().text = hostData[i].gameName + hostData[i].connectedPlayers + "/" + hostData[i].playerLimit;
                Debug.Log("IIII WYNOSI:   " + i);
                int serverNumber = i;
                go.GetComponent<Button>().onClick.AddListener(delegate { ConnectToServer(serverNumber); });
                //go.GetComponentInChildren<Text>().text = GameObject.FindGameObjectWithTag("HostGame").GetComponent<HostScript>().gameName;

                serverCountToShow++;
                }
            }
        }
    }
	// Use this for initialization
	void Start () {

        togglePassword = GameObject.Find("TogglePassword").GetComponent<Toggle>();
        passwordInputField = GameObject.Find("PasswordInputField").GetComponent<InputField>();
	}
	
    public void SET()
    {
        GameObject go = GameObject.Find("ButtonServer(Clone)");
        go.GetComponent<RectTransform>().anchoredPosition.Set(0, 30);

        Debug.Log("11: " + go.GetComponent<RectTransform>().position);
        Debug.Log("22: " + go.GetComponent<RectTransform>().transform.position);
        Debug.Log("33: " + go.GetComponent<RectTransform>().transform.localPosition);
        Debug.Log("44: " + go.GetComponent<RectTransform>().localPosition);
        Debug.Log("ANCHOR: " + go.GetComponent<RectTransform>().anchoredPosition);
        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);

    }

	// Update is called once per frame
	void Update () {
        StartCoroutine(RefreshHostList());
	}

    public void ConnectToServer(int i)
    {
        Debug.Log("I: " + i);
        if (hostData[i].connectedPlayers != hostData[i].playerLimit)
        {

        Debug.Log("PRAWDZIWE I: " + i);
        if(hostData[i] != null)
        {
            Debug.Log("LACZENIE Z  " + i);


            if (hostData[i].passwordProtected == true)
            {
                Debug.LogError("HASLO !!");
                Debug.LogError(Network.Connect(hostData[i], passwordInputField.text));

                if (Network.peerType == NetworkPeerType.Disconnected)
                    Debug.Log("Wrong password");
                else
                {
                    Network.Connect(hostData[i], passwordInputField.text);
                }
            }
            else
            {
                Network.Connect(hostData[i]);
            }
        }
        
        Debug.Log("LACZENIE SIE PRZEZ PRZYYCIK !!!");
        //Network.Connect(hostData[i]);
        }
    }

    void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to server: " + error);

    }

}
