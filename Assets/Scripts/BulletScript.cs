﻿using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Destroy(gameObject, 5); // Destroy bullet after 5 sec
	}
}
