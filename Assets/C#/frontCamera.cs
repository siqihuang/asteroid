using UnityEngine;
using System.Collections;

public class frontCamera : MonoBehaviour {
	public GameObject obj;
	public spaceship ship;
	// Use this for initialization
	void Start () {
		obj = GameObject.Find ("spaceship(Clone)");
		ship = obj.GetComponent<spaceship> ();
		Debug.Log ("!!!");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = ship.transform.position;
		gameObject.transform.position = pos;
		Debug.Log ("!!!");
	}
}
