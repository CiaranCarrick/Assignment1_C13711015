using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadonClick : MonoBehaviour {

	public void LoadScene(int Level_){
		Level_ = 1;
		Application.LoadLevel(Level_);
	}
}
