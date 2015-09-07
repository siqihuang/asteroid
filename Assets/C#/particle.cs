using UnityEngine;
using System.Collections;

public class particle : MonoBehaviour {
	private float deadTime,deltaDeadTime;
	public Quaternion heading;
	// Use this for initialization
	void Start () {
		gameObject.rigidbody.MoveRotation (heading);
		deltaDeadTime = 0;
		deadTime = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		deltaDeadTime += Time.deltaTime;
		if(deltaDeadTime>deadTime)
			Destroy(gameObject);
	}
}
