using UnityEngine;
using System.Collections;

public class levelGUI : MonoBehaviour {
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

	public void resetLevel(int level){
		Vector3 pos = new Vector3 (0.5f, 0.5f, 0);
		text.text = "Level" + level.ToString ();
		gameObject.transform.position = pos;
	}

	public void removeText(){
		Vector3 pos = new Vector3 (10, 10, 0);
		gameObject.transform.position = pos;
	}

	public void showGameOver(){
		Vector3 pos = new Vector3 (0.5f, 0.5f, 0);
		text.text = "GAME OVER";
		text.fontSize = 100;
		gameObject.transform.position = pos;
	}
}
