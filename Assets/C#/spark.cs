using UnityEngine;
using System.Collections;

public class spark : MonoBehaviour {
	public Quaternion heading;
	// Use this for initialization
	void Start () {
		gameObject.rigidbody.MoveRotation (heading);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
