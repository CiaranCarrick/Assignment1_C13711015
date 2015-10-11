using UnityEngine;
using System.Collections;

public class Ship : Main {
	GameObject Rtruster;// Create trusters outside of constructor to allow scope for TrusterParticles Method
	GameObject Ltruster;//

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
		Ltruster = GameObject.CreatePrimitive (PrimitiveType.Cube);
		Ltruster.name = "Ltruster";
		Vector3 Thruster_scale = new Vector3 (xScale * 0.3f, yScale * 0.5f, 0.1f);//Scale for both thruster Left & Right
		Vector3 TL_pos = new Vector3 (transform.position.x-xScale/2, transform.position.y-yScale/2, 0.1f);
		Ltruster.transform.position = TL_pos;
		Ltruster.transform.localScale = Thruster_scale;
		Ltruster.transform.parent = transform; //Makes LTruster a child to Ship

		//Create Right Thruster of Ship
		Rtruster = GameObject.CreatePrimitive (PrimitiveType.Cube);
		Rtruster.name = "Rtruster";
		Vector3 TR_pos = new Vector3 (transform.position.x+xScale/2, transform.position.y-yScale/2, 0.1f);
		Rtruster.transform.position = TR_pos;
		Rtruster.transform.localScale =Thruster_scale;
		Rtruster.transform.parent = transform; //Makes RTruster a child to Ship
	}


	void TrusterParticles(){
		//Right Thruster Particles
		if (Rtruster != null || Ltruster != null) {
			GameObject RT_Parts = GameObject.CreatePrimitive (PrimitiveType.Quad);
			GameObject LT_Parts = GameObject.CreatePrimitive (PrimitiveType.Quad);
			RT_Parts.name = "SParts";
			LT_Parts.name = "SParts";
			Vector3 Direction = new Vector3 (Random.Range (xPos - xScale / 2, xPos + xScale / 2), -1f, 0); //Spawns Parts from left(xPos - xScale / 2) to right (xPos + xScale / 2) of ship going downwards
			RT_Parts.AddComponent<Particles> ().SetupParticle (Rtruster.transform.position.x, Rtruster.transform.position.y, 0.06f, 0.06f, 0.04f, Direction, Color.white, 2f);
			RT_Parts.transform.position=new Vector3(Rtruster.transform.position.x, Rtruster.transform.position.y, 0.1f);
			//Left Thruster Particles
			//Direction = new Vector3 (Random.Range (xPos - xScale / 2, xPos + xScale / 2), -1f, 0);
			LT_Parts.AddComponent<Particles> ().SetupParticle (Ltruster.transform.position.x, Rtruster.transform.position.y, 0.06f, 0.06f, 0.04f, Direction, Color.white, 2f);
			LT_Parts.transform.position=new Vector3(Ltruster.transform.position.x, Ltruster.transform.position.y, 0.1f);
			if(ParticleManager){
				LT_Parts.transform.parent = ParticleManager.transform;
				RT_Parts.transform.parent = ParticleManager.transform;
			}
		}
	}
	void Start ()
	{
		InvokeRepeating ("TrusterParticles", 0, 0.08f);//Create new particle
	}
	
	// Update is called once per frame
	void Update () 
	{
		//MOVEMENT, Keep Movement script anyway
		if (Input.GetAxis ("Horizontal") != 0) //This takes input for the ships X movement
		{
			transform.Translate(Vector3.right * Input.GetAxis("Horizontal")* speed);
			Vector3 pos = transform.position;
			pos.x=Mathf.Clamp(pos.x, (ScreenWidthLeft-xScale/2)+xScale, ScreenWidthRight-xScale/2); // Clamps its current pos (pos) from the min to max value set in this case -ScreenWidth to ScreenWidth
			transform.position=pos; //Save transform change for effect
		}
		//
	}//end Update
}// end class
