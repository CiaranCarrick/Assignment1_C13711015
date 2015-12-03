using UnityEngine;
using System.Collections;

public class offsetscroller : MonoBehaviour {

	public float scrollSpeed;

	void Start () {

	}

	void Update () {
		float y = Mathf .Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (0, y);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);	
	}
}
