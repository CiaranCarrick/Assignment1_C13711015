using UnityEngine;
using System.Collections;

public class Particles : Main {
	Vector3 Direction; //Direction of the particles

	public void SetupParticle(float _x, float _y, float _xScale, float _yScale, float _speed, Vector3 _dir, Color _col, float _lifespan) {
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		speed = _speed;
		Direction = _dir;
		color= _col;

		Vector3 newPos = new Vector3(xPos, yPos,0);
		transform.position = newPos;
		Vector3 newScale = new Vector3(xScale, yScale, 0.1f);
		transform.localScale = newScale;
		GetComponent<Renderer>().material.color = _col;
		Invoke ("DestroyParticles", _lifespan);
	}


	void DestroyParticles(){// Remove particles
		Destroy(gameObject);
	}


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Direction * speed);
	}
}
