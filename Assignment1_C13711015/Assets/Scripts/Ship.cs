using UnityEngine;
using System.Collections;

public class Ship : Main {
	GameObject Thruster, Clone;// Create trusters outside of constructor to allow scope for TrusterParticles Method
	Vector3 T_pos;
	public void SetShip(float _x, float _y, float _xScale, float _yScale, float _speed, Color _color) { //Constructor which inherits values from Main ship method
		name="Ship";
		xPos = _x;
		yPos = _y;
		xScale = _xScale;
		yScale = _yScale;
		speed = _speed;
		color = _color;

		Vector3 pos = new Vector3 (xPos, yPos, 0); //
		transform.position = pos;
		Vector3 Scale = new Vector3 (xScale, yScale, 0.1f);
		transform.localScale = Scale;
		GetComponent<Renderer>().material.color = color;//Sets colour to Main SetShip method value

		//Create Gun Barrel of Ship
		GameObject Gun = GameObject.CreatePrimitive (PrimitiveType.Quad);
		Gun.name = "Barrel";
		Vector3 G_scale = new Vector3 (xScale/5, 0.3f, 0.1f);//Scale for both thruster Left & Right
		Vector3 G_pos = new Vector3 (transform.position.x, transform.position.y+yScale/2, 0.1f);// transform.position spawns ships at the position of the ship
		Gun.transform.position = G_pos;
		Gun.transform.localScale = G_scale;
		Gun.transform.parent = transform; //Makes LTruster a child to Ship

		//Create Left Thruster of Ship
		Thruster = GameObject.CreatePrimitive (PrimitiveType.Cube);
		Thruster.name = "Thruster";
		Vector3 Thruster_scale = new Vector3 (xScale * 0.3f, yScale * 0.5f, 0.1f);//Scale for both thruster Left & Right
		T_pos = new Vector3 (transform.position.x-xScale/2, transform.position.y-yScale/2, 0.1f);
		Thruster.transform.position = T_pos;
		Thruster.transform.localScale = Thruster_scale;
		Thruster.transform.parent = transform; //Makes LTruster a child to Ship


		Clone = Instantiate(Thruster, T_pos,  transform.rotation) as GameObject;
		Vector3 TR_pos = new Vector3 (transform.position.x+xScale/2, transform.position.y-yScale/2, 0.1f);
		TR_pos = new Vector3 (transform.position.x+xScale/2, transform.position.y-yScale/2, 1f);
		Clone.transform.position = TR_pos;
		Clone.transform.parent = transform; //Makes LTruster a child to Ship


	}



	 IEnumerator ThrusterParticles(float time, GameObject _T){
		//Right Thruster Particles 
		while(gameObject) {
		if (Thruster != null) {
			GameObject T_Parts = GameObject.CreatePrimitive (PrimitiveType.Quad);
			T_Parts.name = "SParts";
			Vector3 Direction = new Vector3 (Random.Range (xPos - xScale / 2, xPos + xScale / 2), -1f, 0); //Spawns Parts from left(xPos - xScale / 2) to right (xPos + xScale / 2) of ship going downwards
			//Direction = new Vector3 (Random.Range (xPos - xScale / 2, xPos + xScale / 2), -1f, 0);
			T_Parts.AddComponent<Particles> ().SetupParticle (_T.transform.position.x, _T.transform.position.y, 0.06f, 0.06f, 0.08f, Direction, Color.white, 1f);
			T_Parts.transform.position=new Vector3(_T.transform.position.x, _T.transform.position.y, 0.1f);
			if(ParticleManager){
				T_Parts.transform.parent = ParticleManager.transform;
				}
			}
			yield return new WaitForSeconds(time);
		}
	}



	void Start ()
	{
		StartCoroutine(ThrusterParticles(0.03f, Thruster));
		StartCoroutine(ThrusterParticles(0.03f, Clone));
	}

	float lerpTime = 5f;//
	float currentLerpTime;
	// Update is called once per frame
	void Update () 
	{
		//lerp!
		float perc = currentLerpTime / lerpTime;//This will return the value to it's orignal avoiding the nuance of lerp leaving numbers off by 0.000001 
		if (ship != null) {
			//MOVEMENT, Keep Movement script anyway
			if (Input.GetAxis ("Horizontal") != 0) { //This takes input for the ships X movement
					transform.Translate (Vector3.right * Input.GetAxis ("Horizontal") * speed);
					Vector3 pos = transform.position;
					pos.x = Mathf.Clamp (pos.x, (ScreenWidthLeft - xScale / 2) + xScale, ScreenWidthRight - xScale / 2); // Clamps its current pos (pos) from the min to max value set in this case -ScreenWidth to ScreenWidth
					transform.position = pos; //Save transform change for effect
			}
			if(Input.GetKey(KeyCode.Space))
			{
				speed=0.35f;
				currentLerpTime = 0f;//Reset to 0
			}
			else
				currentLerpTime += Time.deltaTime;//Start incrementing value over time
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
			speed = Mathf.Lerp (speed, 0.5f, perc);//from the current speed up to original, by perc aka currentLerpTime / lerpTime
		}
		if (ship == null) {
			gameObject.transform.Translate (transform.up * speed/2);//called on GameOver

		}

		//
	}//end Update
}// end class
