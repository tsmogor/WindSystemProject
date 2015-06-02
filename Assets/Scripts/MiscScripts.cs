using UnityEngine;
using System.Collections;

public class MiscScripts : MonoBehaviour {

    public string escapeSceneName = "Login";

    public void LoadScene(string name) // load scene in offline mode
    {
        Application.LoadLevel(name);
  
    }

    public void Start() // for testing only to skip "Login scene"
    {
        if(GameObject.Find("User(Clone") == null && GameObject.Find("User") == null) 
        {
            GameObject.Instantiate(Resources.Load("Prefabs/User"));
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // go back when presssing back button
            if (escapeSceneName == "quit")
                Application.Quit();
            else
                LoadScene(escapeSceneName);
        
    }

}
