using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour {

	public float ScrollSpeed;//Scroll Speed
	//public float tilesizeZ=30;
	//private Vector3 Startpos;
	public Renderer rend;
	// Use this for initialization
	void Start () {
		ScrollSpeed = 0.02f;//Intialize
		//Startpos = transform.position;
		rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		float offset = Time.time * ScrollSpeed;
		rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
		//float newPosition = Mathf.Repeat (Time.time * ScrollSpeed,tilesizeZ);//Works like Modulo
		//transform.position = Startpos + Vector3.down * newPosition;//Move background to new position
	}
	
}
