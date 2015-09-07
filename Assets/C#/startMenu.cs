using UnityEngine;
using System.Collections;

public class startMenu : MonoBehaviour {
	public GUIStyle buttonStyle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUILayout.BeginArea(new Rect(10,Screen.height/2+100,Screen.width-20,200));
		if (GUILayout.Button ("New Game")) {
			Application.LoadLevel("MainScene");
		}
		if(GUILayout.Button ("Instruction")){
			Application.LoadLevel("instructionScene");
		}
		if (GUILayout.Button ("High Score")) {
			Application.LoadLevel("highScoreScene");
		}
		if(GUILayout.Button("Exit")){
			Application.Quit();
		}
		GUILayout.EndArea ();
	}
}
