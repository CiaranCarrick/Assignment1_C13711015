using UnityEngine;
using System.Collections;

public class CleanUp : MonoBehaviour {
	// Use this for initialization
	void Start () {
		Invoke ("Destroyme", 3f);
	}
	
	void Destroyme(){
		Destroy (gameObject);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
