using UnityEngine;
using System.Collections;

public class Stars : Main {
	Vector3 pos, scale;
	bool randomyscale;
	public void SetStars(float _x, float _y, float _xScale, float _yScale, float _speed, float _minS, float _maxS, bool _randomyscale){
		name = "Star";
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		speed = _speed;
		minspeed = _minS;
		maxspeed = _maxS;
		randomyscale = _randomyscale;

		xPos = Random.Range (ScreenWidthLeft, ScreenWidthRight);//Returns random int
		yPos = Random.Range (0, ScreenHeight);
		pos = new Vector3 ((xPos) + xScale / 2, yPos, Random.Range (0, 2)); // Z spawns stars above and below ships for visual effect
		transform.position = pos;
		scale = new Vector3 (xScale, yScale, 0.1f);

		transform.localScale = scale;
	}

	
	// Use this for initialization
	void Start () {             
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.up * speed * Time.deltaTime);
		if (transform.position.y <= -10f) {// Resets position once it reachs -1
			pos.x = Random.Range(ScreenWidthLeft-xScale, ScreenWidthRight-xScale);//Adding Scale returns random float value as well as added size
			if (randomyscale == false){
				scale = new Vector3 (xScale, yScale, 0.1f);
			}
			else
				scale = new Vector3 (xScale, Random.Range(0.3f, 2f), 0.1f);
			speed=Random.Range(minspeed ,maxspeed);
			pos.y = ScreenHeight;
			transform.position=pos; //Save changes
			transform.localScale = scale;
		}

	}
}
