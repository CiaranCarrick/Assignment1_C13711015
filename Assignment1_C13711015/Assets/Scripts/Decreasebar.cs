using UnityEngine;
using System.Collections;

public class Decreasebar : Main {
	public static float size,Scaler;
	public float width;
	Main main;
	public void SetUpBar(){

	}
	void Start(){
		GameObject M = GameObject.Find ("Main");
		main=M.GetComponent<Main>();
		size = 100;
		width = transform.localScale.x;
		Scaler = (Mathf.Round(size/main.Leveltime));//100/30==3.33333(rounded=)30f
	}
	 void Update(){
		if (size >= 0f && main.Gamestart == true && ship) {
			size -= Time.deltaTime * Scaler;
			Decrease (this.transform);
		} else
			main.Gamestart = false;
	}
	
	void Decrease(Transform _b){
			Vector3 pos = _b.transform.position;
			Vector3 scale = _b.transform.localScale;
			scale.x = (width / 100) * size;
			pos.x = _b.transform.position.x - ((_b.transform.localScale.x - scale.x) / 2);
			transform.position = pos;
			transform.localScale = scale;
	}
}
