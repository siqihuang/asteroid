using UnityEngine;
using System.Collections;

public class heavyWeapon : MonoBehaviour {
	public float force;
	public Quaternion heading;
	public int host;
	public int level;
	public AudioClip explosion;
	public float width,height,depth,deltaTimer,deathTime;
	// Use this for initialization
	void Start () {
		deathTime = 4f;
		GameObject obj = GameObject.Find ("spaceship(Clone)");
		spaceship s = obj.GetComponent<spaceship> ();
		heading = s.heading;
		gameObject.rigidbody.MoveRotation (heading);

		gameObject.rigidbody.drag = 0;
		obj = GameObject.Find ("globalObj");
		global g = obj.GetComponent<global> ();

		width = g.width;
		height = g.height;
		depth = g.depth;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.rigidbody.MoveRotation (heading);
		deltaTimer += Time.deltaTime;
		if(deltaTimer>deathTime)
			Die ();
	}
	
	void OnCollisionEnter(Collision collision){
		Collider collider = collision.collider;
		if (collider.CompareTag ("asteroid")) {
			GameObject obj=collider.rigidbody.gameObject;
			asteroid a=obj.GetComponent<asteroid>();
			AudioSource.PlayClipAtPoint(explosion,gameObject.transform.position);
			if(a.mass<85-5*level) a.Die();
			else a.Split();
			this.Die();
			obj=GameObject.Find("globalObj");
			global g=obj.GetComponent<global>();
			g.score+=10;
		}
		else if (collider.CompareTag ("UFO")) {
			GameObject obj=collider.rigidbody.gameObject;
			UFO ufo=obj.GetComponent<UFO>();
			AudioSource.PlayClipAtPoint(explosion,gameObject.transform.position);
			ufo.Die();
			this.Die();
		}
	}
	
	public void Die(){
		Destroy (gameObject);
	}
}
