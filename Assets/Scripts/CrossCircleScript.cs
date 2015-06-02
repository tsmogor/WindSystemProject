using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CrossCircleScript : MonoBehaviour 
{
    GameObject[] boxes;
   
    int[,] winningCombinations = new int[,] {{0,1,2},{3,4,5},{6,7,8},{0,3,6},{1,4,7},{2,5,8},{0,4,8},{6,4,2} };
    public bool gameOver = false;
    public bool adminRound = true;
    public bool gameRestart;

	// Use this for initialization
	void Start () 
    {
        transform.SetParent(GameObject.Find("Canvas RoundText").transform);
        boxes = GameObject.FindGameObjectsWithTag("Box");

        if (Network.isServer)
        {
        GetComponent<NetworkView>().RPC("ChangeRound", RPCMode.AllBuffered);
        int a = Random.Range(0, 2);
        Debug.LogError("A : " + a);

            if (a == 1)
            {
                GetComponent<NetworkView>().RPC("ChangeRound", RPCMode.AllBuffered);
                Debug.LogError("WYLOSOWALEM");
            }
        }
        Debug.LogError("ADMIN ROUND: " + adminRound);

        Debug.Log("CLIENT: " + Network.isClient);

        Debug.Log("SERVER: " + Network.isServer);
	}

    void checkScore()
    {
        if(boxes != null)
        for (int i = 0; i < winningCombinations.GetLength(0); i++)
        {
            bool case1 = false,
                case2 = false,
                case3 = false;
            if (boxes[winningCombinations[i, 0]].GetComponent<Text>().text != "" && boxes[winningCombinations[i, 1]].GetComponent<Text>().text != "" && boxes[winningCombinations[i, 2]].GetComponent<Text>().text != "")
            {
                case1 = boxes[winningCombinations[i, 0]].GetComponent<Text>().text == boxes[winningCombinations[i, 1]].GetComponent<Text>().text;
                case2 = boxes[winningCombinations[i, 1]].GetComponent<Text>().text == boxes[winningCombinations[i, 2]].GetComponent<Text>().text;
                case3 = boxes[winningCombinations[i, 0]].GetComponent<Text>().text == boxes[winningCombinations[i, 2]].GetComponent<Text>().text;

            }
            if (case1 && case2 && case3)
            {
                string winner = "";
                if (boxes[winningCombinations[i, 0]].GetComponent<BoxScript>().usernameProperty.Length >= winner.Length) winner = boxes[winningCombinations[i, 0]].GetComponent<BoxScript>().usernameProperty;
                if (boxes[winningCombinations[i, 1]].GetComponent<BoxScript>().usernameProperty.Length >= winner.Length) winner = boxes[winningCombinations[i, 1]].GetComponent<BoxScript>().usernameProperty;
                if (boxes[winningCombinations[i, 2]].GetComponent<BoxScript>().usernameProperty.Length >= winner.Length) winner = boxes[winningCombinations[i, 2]].GetComponent<BoxScript>().usernameProperty;
                Debug.LogError("1: " + boxes[winningCombinations[i, 0]].GetComponent<BoxScript>().usernameProperty);
                Debug.LogError("1: " + boxes[winningCombinations[i, 1]].GetComponent<BoxScript>().usernameProperty);
                Debug.LogError("1: " + boxes[winningCombinations[i, 2]].GetComponent<BoxScript>().usernameProperty);
                if (Network.isServer)
                {
                    Network.Instantiate(Resources.Load("Prefabs/Winning UI"), new Vector3(0, 0, 0), Quaternion.identity, 0);
                    GetComponent<NetworkView>().RPC("GameOverMessage", RPCMode.All, boxes[winningCombinations[i, 0]].GetComponent<BoxScript>().usernameProperty);
                    //GameObject.Destroy(GameObject.Find("Game(Clone)"));
                    if (GetComponent<NetworkView>().isMine)
                    {
                        gameOver = true;
                        //Network.RemoveRPCs(Network.player);
                        //Network.Destroy(GameObject.Find("Game(Clone)"));
                    }

                }
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
	}

    [RPC]
    void GameOverMessage(string winnerName)
    {
        var winningMessageText = GameObject.FindGameObjectWithTag("WinText");
        Debug.LogError("ZNALEZIONY:" +(winningMessageText != null));
        winningMessageText.GetComponent<Text>().text = winnerName + winningMessageText.GetComponent<Text>().text;

        foreach(var box in boxes)
        {
            box.gameObject.GetComponent<EventTrigger>().enabled = false;
        }
          
    }

    [RPC]
    public void ChangeRound()
    {
        adminRound = !adminRound;

        if (adminRound && Network.isServer)
            GameObject.Find("Round").GetComponent<Text>().text = "Twoja tura";
        else if (Network.isServer)
            GameObject.Find("Round").GetComponent<Text>().text = "Przeciwnik wybiera";

        if (!adminRound && Network.isClient)
            GameObject.Find("Round").GetComponent<Text>().text = "Twoja tura";
        else if (Network.isClient)
            GameObject.Find("Round").GetComponent<Text>().text = "Przeciwnik wybiera";
        
        Debug.LogError("po adminRound:" + adminRound);
        Debug.LogError("Find adminRound:" + GameObject.Find("Game(Clone)").GetComponent<CrossCircleScript>().adminRound);
        checkScore();

    }
    [RPC]
    void RestartGame()
    {
        foreach(var box in boxes)
        {
            box.GetComponent<EventTrigger>().enabled = true;
            box.gameObject.GetComponent<Text>().text = "";
            box.gameObject.GetComponent<BoxScript>().usernameProperty = "";
        }
        GetComponent<NetworkView>().RPC("ChangeRound", RPCMode.All);

        gameOver = false;

        if (GameObject.Find("Winning UI(Clone)") != null) // destroy winning UI if exist
            GameObject.Destroy(GameObject.Find("Winning UI(Clone)"));
        

    }
}
