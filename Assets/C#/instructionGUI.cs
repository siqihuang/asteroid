using UnityEngine;
using System.Collections;

public class instructionGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUILayout.BeginArea (new Rect (10, 10, Screen.width - 20, Screen.height - 20));
		GUIStyle style=new GUIStyle();
		style.fontSize = 10;
		GUILayout.Label ("Asteroids is an arcade space shooter released in November 1979[1] by Atari, Inc. and designed by Lyle Rains and Ed Logg. The player controls a spaceship in an asteroid field which is periodically traversed by flying saucers. The object of the game is to shoot and destroy asteroids and saucers while not colliding with either, or being hit by the saucers' counter-fire. The game becomes harder as the number of asteroids increases." +
						"Asteroids was conceived during a meeting between Logg and Rains and used hardware developed by Howard Delman previously used for Lunar Lander. Based on an unfinished game titled Cosmos and inspired by Spacewar! and Computer Space, both early shoot 'em up video games, Asteroids '​ physics model and control scheme were derived by Logg from these earlier games and refined through trial and error. The game is rendered on a vector display in a two-dimensional view that wraps around in both screen axes." +
						"Acclaimed by players and video game critics for its vector graphics, controls, and addictive gameplay, Asteroids was one of the first major hits of the golden age of arcade games. The game sold over 70,000 arcade cabinets and proved both popular with players and influential with developers. It has since been ported to multiple platforms. Asteroids was widely imitated and directly influenced two popular and often cloned arcade games, Defender and Gravitar, as well as many other video games.");
		GUILayout.EndArea ();
		bool b;
		GUILayout.BeginArea(new Rect(200,300,Screen.width-400,100));
		GUILayout.FlexibleSpace ();
		GUILayout.BeginHorizontal ();
		GUILayout.FlexibleSpace ();
		b=GUILayout.Button ("OK");
		GUILayout.FlexibleSpace ();
		GUILayout.EndHorizontal ();
		GUILayout.FlexibleSpace ();
		GUILayout.EndArea ();
		if(b) Application.LoadLevel("startScene");
	}
}
