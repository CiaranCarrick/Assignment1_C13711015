using UnityEngine;
using System.Collections;

public class Stars : Main {
	Vector3 pos;
	public void SetStars(float _x, float _y, float _xScale, float _yScale, float _speed){
		name = "Star";
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		speed = _speed;


		xPos = Random.Range (ScreenWidthLeft, ScreenWidthRight);//Returns random int
		yPos = Random.Range(0, ScreenHeight);
		pos= new Vector3((xPos)+xScale/2, yPos,Random.Range(0, 2)); // Z spawns stars above and below ships for visual effect
		transform.position = pos;
		Vector3 scale = new Vector3(xScale, Random.Range(0.2f, 1.0f), 0.1f);
		transform.localScale = scale;
	}

	
	// Use this for initialization
	void Start () {             
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.up * speed * Time.deltaTime);
		if (transform.position.y <= -5f) {// Resets position once it reachs -1
			pos.x = Random.Range(ScreenWidthLeft-xScale, ScreenWidthRight-xScale);//Adding Scale returns random float value as well as added size
			speed=Random.Range(-3f, -40f);
			pos.y = ScreenHeight;
			transform.position=pos; //Save changes
		}

	}
}
