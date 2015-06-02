using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HostUIScript : MonoBehaviour {

    public bool restartGame;

    private void Start()
    {

    }
    public void StartGame(string gameObject)
    {
        if (restartGame == false)
        {
            
            Network.Instantiate(Resources.Load("Prefabs/" + gameObject), new Vector3(0, 0, 0), Quaternion.identity, 0);
            GetComponentInChildren<Text>().text = "Restart game";
            restartGame = true;
        }
        else
        {
            GameObject.Find(gameObject + "(Clone)").GetComponent<NetworkView>().RPC("RestartGame", RPCMode.All);

        }
    }

}
