using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {
	public global globalObj;
	public GUIText text;
	// Use this for initialization
	void Start () {
		GameObject obj = GameObject.Find ("globalObj");
		globalObj = obj.GetComponent<global> ();
		text = gameObject.GetComponent<GUIText> ();
	}
	
	// Update is called once per frame
	void Update () {
		string lives, asteroids, score, heavyWeapon,time;
		lives = "Lives:"+globalObj.lives.ToString();
		score = "Score:"+globalObj.score.ToString();
		asteroids = "Asteroids:" + globalObj.asteroidNum.ToString ();
		time = "Time:" + globalObj.time.ToString ();
		if(globalObj.heavyWeapon<0)
			heavyWeapon = "Laser: 0";
		else
			heavyWeapon = "Laser:" + globalObj.heavyWeapon.ToString ();
		text.text = lives + "  " + score + "  " + asteroids+" "+heavyWeapon+" "+time;
	}
}
