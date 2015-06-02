using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {


    public float health = 100; // test value
	int coolDownPeriodInSeconds = 3;
	float timeStamp;
	float timeStamp2;
	void Start () 
    {
	
	}



	// Update is called once per frame
	void Update () 
    {

		respawn ();
	//if(Input.GetKeyDown(KeyCode.Space))
   //     health -= 1; // test serialization

    if (GetComponent<NetworkView>().isMine) // check if other is mine
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            GetComponent<NetworkView>().RPC("movePlayer", RPCMode.All, 0);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            GetComponent<NetworkView>().RPC("movePlayer", RPCMode.All, 1);


        if (Input.GetKeyDown(KeyCode.LeftArrow))
            GetComponent<NetworkView>().RPC("movePlayer", RPCMode.All, 2);


        if (Input.GetKeyDown(KeyCode.RightArrow))
           GetComponent<NetworkView>().RPC("movePlayer", RPCMode.All, 3);

			if (Input.GetKeyDown(KeyCode.A))
				GetComponent<NetworkView>().RPC("movePlayer", RPCMode.All, 4);

			if (Input.GetKeyDown(KeyCode.D))
				GetComponent<NetworkView>().RPC("movePlayer", RPCMode.All, 5);

			if (Input.GetKeyDown(KeyCode.W))
				GetComponent<NetworkView>().RPC("movePlayer", RPCMode.All, 6);
			
			if (Input.GetKeyDown(KeyCode.S))
				GetComponent<NetworkView>().RPC("movePlayer", RPCMode.All, 7);
    }
    else
        Debug.Log("Access Denied !");

		//timeStamp = Time.time + coolDownPeriodInSeconds;
        if(GetComponent<NetworkView>().isMine)
			if (Input.GetKeyDown(KeyCode.X) && Time.time >= timeStamp)
        {
			timeStamp = Time.time + coolDownPeriodInSeconds;
			//timeStamp2 = Time.time + coolDownPeriodInSeconds;
            Debug.Log("STRZELAM UWAGA !");

            if (Network.isClient)
                GetComponent<NetworkView>().RPC("shoot", RPCMode.Server, transform.FindChild("Cannon").position);
            else if (Network.isServer)
                shoot(transform.FindChild("Cannon").position);
        }
    Input.ResetInputAxes();
	}
    /* // Custom serialization from youtube example 
     
    private void OnSerializeNetworkView(BitStream stream,NetworkMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.Serialize(ref health);
        }
        else
        {
            stream.Serialize(ref health);
        }
    }
    */
    [RPC]
    private void shoot(Vector3 pos)
    {
    //    Debug.Log("Pozycja kuli: " + transform.position);
     //   Debug.Log("Pozycja dzialo: " + transform.FindChild("Cannon").position);
     //   Debug.Log("POZYCJA: " + pos);
        //Network.Instantiate(Resources.Load("Prefabs/Pocisk"), pos, Quaternion.identity, 0);

       // if (networkView.isMine)
       // {
            GameObject pocisk = (GameObject)Network.Instantiate(Resources.Load("Prefabs/Bullet"), pos, Quaternion.identity, 0);
            pocisk.GetComponent<Rigidbody>().AddForce((transform.FindChild("Cannon").position - transform.position) * 10000);
       // }


    }



	public Vector3 eulerAngleVelocityLeft = new Vector3(0, 100, 0);
	public Vector3 eulerAngleVelocityRight = new Vector3(0, -100, 0);
	public Vector3 eulerAngleVelocityUp = new Vector3(0, 0, 100);
	public Vector3 eulerAngleVelocityDown = new Vector3(0, 0, -100);

    [RPC]
    private void movePlayer(int a)
    {
		Quaternion deltaRotationRight = Quaternion.Euler(eulerAngleVelocityRight * Time.deltaTime);
		Quaternion deltaRotationLeft = Quaternion.Euler(eulerAngleVelocityLeft * Time.deltaTime);
		Quaternion deltaRotationUp = Quaternion.Euler(eulerAngleVelocityUp * Time.deltaTime);
		Quaternion deltaRotationDown = Quaternion.Euler(eulerAngleVelocityDown * Time.deltaTime);
		Vector3 left = new Vector3(GetComponent<Rigidbody>().rotation.x,GetComponent<Rigidbody>().rotation.y + 2,GetComponent<Rigidbody>().rotation.z);

        if (a == 0)
						GetComponent<Rigidbody>().MovePosition (GetComponent<Rigidbody>().position + new Vector3 (0.1f, 0, 0));
            //rigidbody.AddForce(30, 0, 0, ForceMode.Force);
        else if (a == 1)
						GetComponent<Rigidbody>().MovePosition (GetComponent<Rigidbody>().position + new Vector3 (-0.1f, 0, 0));
            //rigidbody.AddForce(-30, 0, 0, ForceMode.Force);
        else if (a == 2)
						GetComponent<Rigidbody>().MovePosition (GetComponent<Rigidbody>().position + new Vector3 (0, 0, 0.1f));
			//rigidbody.MoveRotation(rigidbody.rotation * deltaRotationLeft);
           // rigidbody.AddForce(0, 0, 30, ForceMode.Force);
        else if (a == 3)
						GetComponent<Rigidbody>().MovePosition (GetComponent<Rigidbody>().position + new Vector3 (0, 0, -0.1f));
			//rigidbody.MoveRotation(rigidbody.rotation * deltaRotationRight);
            //rigidbody.AddForce(0, 0, -30, ForceMode.Force);
		else if (a == 4)
						GetComponent<Rigidbody>().MoveRotation (GetComponent<Rigidbody>().rotation * deltaRotationRight);
		// rigidbody.AddForce(0, 0, 30, ForceMode.Force);
		else if (a == 5)
						GetComponent<Rigidbody>().MoveRotation (GetComponent<Rigidbody>().rotation * deltaRotationLeft);
		//rigidbody.AddForce(0, 0, -30, ForceMode.Force);
		else if (a == 6)
			GetComponent<Rigidbody>().MoveRotation (GetComponent<Rigidbody>().rotation * deltaRotationUp);
			//rigidbody.MoveRotation(rigidbody.rotation * deltaRotationUp);
			// rigidbody.AddForce(0, 0, 30, ForceMode.Force);
						
		else if (a == 7)
			GetComponent<Rigidbody>().MoveRotation (GetComponent<Rigidbody>().rotation * deltaRotationDown);
			//rigidbody.MoveRotation(rigidbody.rotation * deltaRotationDown);
		//rigidbody.AddForce(0, 0, -30, ForceMode.Force);


    }
	private void respawn()

	{
	//	Debug.Log("SPADLEM" + gameObject.rigidbody.position.y + "transform y:" + transform.position.y);

		if (gameObject.GetComponent<Rigidbody>().position.y < -5) {
	//		Debug.Log("Niee" + gameObject.rigidbody.position.y + "transform y:" + transform.position.y);
			GetComponent<Rigidbody>().position = new Vector3(0,1,0);
		}

	}
	public void OnGUI()
	{
		Quaternion deltaRotationRight = Quaternion.Euler(eulerAngleVelocityRight * Time.deltaTime);
		Quaternion deltaRotationLeft = Quaternion.Euler(eulerAngleVelocityLeft * Time.deltaTime);
		Quaternion deltaRotationUp = Quaternion.Euler(eulerAngleVelocityUp * Time.deltaTime);
		Quaternion deltaRotationDown = Quaternion.Euler(eulerAngleVelocityDown * Time.deltaTime);
		if (GetComponent<NetworkView>().isMine) // check if other is mine
		{
			if (Time.time >= timeStamp )
			{	
				if (GUI.RepeatButton(new Rect(Screen.width/2-150,Screen.height-120, 140f, 50f), "Shot"))
				{
					//Debug.Log("Y:"+rigidbody.position.y);
					timeStamp = Time.time + coolDownPeriodInSeconds;

                    if (Network.isClient)
                        GetComponent<NetworkView>().RPC("shoot", RPCMode.Server, transform.FindChild("Cannon").position);
                    else if (Network.isServer)
                        shoot(transform.FindChild("Cannon").position);
				} 

				
			}
			if(Time.time>=timeStamp2)
			{
				//Debug.Log(rigidbody.position.y);
				if (GUI.RepeatButton(new Rect(Screen.width/2-150, Screen.height-70, 140f, 45f), "Jump"))
				{
				
				//Vector3 suf = new Vector3(0,2,0);
					if(GetComponent<Rigidbody>().position.y<=2)
						GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + new Vector3(0,0.2f,0));
					else timeStamp2=Time.time+coolDownPeriodInSeconds;
				}

			}
			else{

			}
			if (GUI.RepeatButton(new Rect(Screen.width-200, Screen.height-70, 50f, 50f), "a"))
			{
				GetComponent<Rigidbody>().MovePosition (GetComponent<Rigidbody>().position + new Vector3 (0, 0, 0.1f));
			}  
			if (GUI.RepeatButton(new Rect(Screen.width-100, Screen.height-70, 50f, 50f), "d"))
			{
				GetComponent<Rigidbody>().MovePosition (GetComponent<Rigidbody>().position + new Vector3 (0, 0, -0.1f));

			}  
			if (GUI.RepeatButton(new Rect(Screen.width-150,Screen.height-120, 50f, 50f), "w"))
			{
				GetComponent<Rigidbody>().MovePosition (GetComponent<Rigidbody>().position + new Vector3 (0.1f, 0, 0));

			}  
			if (GUI.RepeatButton(new Rect(Screen.width-150,Screen.height-70, 50f, 50f), "s"))
			{
				GetComponent<Rigidbody>().MovePosition (GetComponent<Rigidbody>().position + new Vector3 (-0.1f, 0, 0));

			}  
			if (GUI.RepeatButton(new Rect(Screen.width/6-50, Screen.height-70, 50f, 50f), "<"))
			{
				GetComponent<Rigidbody>().MoveRotation (GetComponent<Rigidbody>().rotation * deltaRotationRight);
			}  
			if (GUI.RepeatButton(new Rect(Screen.width/6, Screen.height-70, 50f, 50f), ">"))
			{
				GetComponent<Rigidbody>().MoveRotation (GetComponent<Rigidbody>().rotation * deltaRotationLeft);
				
			}  
			//if (GUI.RepeatButton(new Rect(230f, 260f, 50f, 50f), "w"))
			//{
			//	rigidbody.MoveRotation (rigidbody.rotation * deltaRotationUp);
				
			//}  
			//if (GUI.RepeatButton(new Rect(230f, 360f, 50f, 50f), "s"))
			//{
			//	rigidbody.MoveRotation (rigidbody.rotation * deltaRotationDown);
				
			//}  

		}
	}


}
