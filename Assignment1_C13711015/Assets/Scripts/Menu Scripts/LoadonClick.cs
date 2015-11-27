using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadonClick : MonoBehaviour {

	void Awake(){
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}
	public void LoadScene(int Level_){
		Level_ = 1;
		Application.LoadLevel(Level_);
		
	}
}
