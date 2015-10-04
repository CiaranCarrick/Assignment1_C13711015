using UnityEngine;
using System.Collections;

public class ShipShoot : Main {
	

	public void SetBullet(float _x, float _y, float _xScale, float _yScale, float _speed, AudioSource _explosionsound, int _mycooldown) { //Constructor which inherits values from Main ship method
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		speed = _speed;
		explosionsound = _explosionsound;
		mycooldown = _mycooldown;

		Vector3 pos = new Vector3 (xPos, yPos, 0.1f);
		transform.position = pos;
		Vector3 scale = new Vector3 (xScale, yScale, 0.1f);
		transform.localScale = scale;
	}
		
	// Use this for initialization
	void Start () {
	}

	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.up * speed);
		if ((transform.position.y) >= ScreenHeight) {
				Destroy (gameObject);
		}
		if (mycooldown <=5){
			transform.position += transform.right* Mathf.Sin (Time.time * 10) * 0.02f;
		}
		//COLLISIONS
	}//end update
}
