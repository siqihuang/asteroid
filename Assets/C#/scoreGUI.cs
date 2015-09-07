using UnityEngine;
using System.Collections;

public class scoreGUI : MonoBehaviour {
	public global globalObj;
	public GUIText text;
	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.Find ("globalObj");
		globalObj = obj.GetComponent<global> ();
		text = gameObject.GetComponent<GUIText> ();
		text.text = "Level1";
		text.anchor = TextAnchor.MiddleCenter;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUILayout.BeginArea(new Rect(10,Screen.height/2,Screen.width-20,200));
		GUILayout.Label ("Congratulations! Please input your name");
		string text=GUILayout.TextField ("Your name");
		GUILayout.Button ("OK");
		GUILayout.EndArea ();
	}
}
