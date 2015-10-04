using UnityEngine;
using System.Collections;

public class Stars : Main {
	Vector3 pos;
	public void SetStars(float _x, float _y, float _xScale, float _yScale, float _speed){
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		speed = _speed;


		xPos = Random.Range (-ScreenWidth, ScreenWidth);
		yPos = Random.Range(0, ScreenHeight);
		pos= new Vector3(xPos, yPos,Random.Range(-1, 1)); //Randomly spawns stars under and above ships for cool FX
		transform.position = pos;
		Vector3 scale = new Vector3(xScale, Random.Range(0.2f, 1.0f), 0.1f);
		transform.localScale = scale;
	}

	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(0, speed, 0));// Goes Down
		if (transform.position.y <= -5f) {// Resets position once it reachs -1
			pos.x = Random.Range(-ScreenWidth, ScreenWidth);//Sets it to random x position from -x to x
			pos.y = ScreenHeight;
			transform.position=pos; //Save changes
		}

	}
}
