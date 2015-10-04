using UnityEngine;
using System.Collections;

public class Strobe : MonoBehaviour {

	public int SizeX, SizeY;
	public float twinkle;
	public int rateofchange;
	public bool Countup;


	void strobe(){
		transform.localScale = new Vector3 (SizeX+twinkle, SizeY+twinkle, 0.1f);
		if(rateofchange < 5){
			rateofchange++;
		}else{
			if(Countup){
				if(twinkle <= 0.35f){//max scale
					twinkle+=0.05f;
				}else{
					Countup = false;// no longer go up
				}
			}else{
				if(twinkle >= 0.25f){//min scale
					twinkle-=0.05f;
				}else{
					Countup = true;
				}
			}
			rateofchange = 0;
		}
	}

	// Use this for initialization
	void Start () {
		twinkle = 0.2f;
	
	}
	
	// Update is called once per frame
	void Update () {
		strobe ();
	
	}
}
