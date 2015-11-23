using UnityEngine;
using System.Collections;

public class Rotatearound : MonoBehaviour {
	public Transform Target;
	public float speed=100f;
	public Vector3 pos;	// Use this for initialization
	void Start () {
		pos = transform.position;
	}
	void rotate(Transform _T, Vector3 _dir){
		transform.RotateAround (_T.gameObject.transform.position, _dir, speed*Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {

		if (Target == null) {
			Target = GameObject.FindGameObjectWithTag ("Player").transform;
			pos.x=Target.transform.position.x;
			pos.y=Target.transform.position.y;
			transform.position=pos;
			gameObject.transform.parent = Target.transform;

		}

		rotate (Target, Vector3.forward);
	}
}
