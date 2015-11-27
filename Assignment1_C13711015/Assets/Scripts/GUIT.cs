using UnityEngine;
using System.Collections;

public class GUIT : MonoBehaviour{
	TextAlignment Textalign = TextAlignment.Center;
	TextAnchor Textanchor = TextAnchor.MiddleCenter;
	public Vector3 WorldOffset=Vector3.up * 1.0f;
	Vector3 ScreenOffset = Vector3.zero;
	float duration=2f;
	GameObject GUIPopup;
	Color Alpha;
	GUIText guitext;

	public void SetText(string _text, Vector3 _trans){
		GUIPopup = new GameObject ();
		GUIPopup.name = "GUIPopup";
		GUIPopup.transform.parent = gameObject.transform;
		GUIPopup.AddComponent<GUIText> ();
	    guitext = GUIPopup.GetComponent<GUIText> ();
		guitext.text=_text;
		transform.position = _trans;
		Alpha = new Color (1, 1, 1, 1);
		guitext.alignment = Textalign;
		guitext.anchor = Textanchor;
	}
	// Use this for initialization
	void Start () {
	}
	

	// Update is called once per frame
	public void LateUpdate() {
		if (GUIPopup) {
			if (Alpha.a > 0) {
				GUIPopup.transform.position = Camera.main.WorldToViewportPoint (transform.position + WorldOffset) + ScreenOffset;//Change GUI's viewpoint coordinates to world. add Worldoffset aka where GUI spawns
				WorldOffset += Vector3.up * (Time.deltaTime*duration);
				Alpha.a -= Time.deltaTime / duration;
				guitext.material.SetColor ("_Color", Alpha);
			}
		} else
			return;
	}
}
