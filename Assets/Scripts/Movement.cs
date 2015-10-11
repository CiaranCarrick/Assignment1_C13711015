﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	Main main; //Referance Main
	// Use this for initialization
	void Start () {
		main = GetComponent<Main> ();//Add Main to script
	}
	
	// Update is called once per frame
	void Update () {
		//Movement
		if (Input.GetAxis ("Horizontal") != 0) //This takes input for the ships X movement
		{
			transform.Translate(Vector3.right * Input.GetAxis("Horizontal")* main.speed);
			Vector3 pos = transform.position;
			pos.x=Mathf.Clamp(pos.x, (Main.ScreenWidthLeft-main.xScale/2)+main.xScale, Main.ScreenWidthRight-main.xScale/2); // Clamps its current pos (pos) from the min to max value set in this case -ScreenWidth to ScreenWidth
			transform.position=pos; //Save transform change for effect
		}
	}
}