using UnityEngine;
using System.Collections;

public class Rotatearound : MonoBehaviour {
	public Transform Target;
	public float speed=100f;
	public float size, currentLerpTime;
	float lerpTime = 1f;
	public Vector3 pos;	// Use this for initialization
	void Start () {
		currentLerpTime = 0f;
		pos = transform.position;
	}
	void rotate(Transform _T, Vector3 _dir){
		//increment timer once per frame
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}
		float perc = currentLerpTime / lerpTime;
		transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(size, size, size), perc);
		//transform.localScale = new Vector3 (size, size, 3f);
		if (size <= 2.2f) {//max scale
			size+=0.05f;
		}
		transform.Rotate(10 * Time.deltaTime,20 * Time.deltaTime, 0, Space.Self);
		//transform.RotateAround (_T.gameObject.transform.position, _dir, speed*Time.deltaTime);
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
