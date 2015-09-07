using UnityEngine;
using System.Collections;

public class UFO : MonoBehaviour {
	public float rotationSpeed;
	public float maxFindPlayerTime;
	private float findPlayerTimer;
	private Vector3 playerLocation;
	private Vector3 forceVector;
	public float width,height,depth;
	public bool destroyed;
	public AudioClip explosion;
	public GameObject bullet;
	// Use this for initialization
	void Start () {
		destroyed = false;
		rotationSpeed = 1f;
		gameObject.rigidbody.AddRelativeTorque (0, rotationSpeed, 0);
		findPlayerTimer = 0;
		GameObject obj = GameObject.Find ("globalObj");
		global g = obj.GetComponent<global> ();
		width = g.width;
		height = g.height;
		depth = g.depth;
	}


	void OnParticleCollision(GameObject obj){
		if(!destroyed){
			destroyed = true;
			obj=GameObject.Find("globalObj");
			global g=obj.GetComponent<global>();
			g.score += 10;
			AudioSource.PlayClipAtPoint(explosion,gameObject.transform.position);
			Die ();
		}
	}
	// Update is called once per frame
	void Update () {
		findPlayerTimer += Time.deltaTime;
		if (findPlayerTimer > maxFindPlayerTime) {
			findPlayerTimer=0;
			locatePlayer();
			setForce();
		}
		Vector3 pos = gameObject.transform.position;
		pos.y=-0.038f;
		if(pos.x<-width) pos.x=width;
		else if(pos.x>width) pos.x=-width;
		if(pos.y<-depth) pos.y=-depth;
		else if(pos.y>depth) pos.y=depth;
		if(pos.z<-height) pos.z=height;
		else if(pos.z>height) pos.z=-height;
		gameObject.transform.position = pos; 
	}

	public void locatePlayer(){
		GameObject obj = GameObject.Find ("spaceship(Clone)");
		if(obj!=null){
			spaceship s = obj.GetComponent<spaceship> ();
			if(s!=null) playerLocation = s.transform.position;
			else playerLocation=new Vector3(0,0,0);
		}
		else playerLocation=new Vector3(0,0,0);
	}

	public void setForce(){
		Vector3 speed = gameObject.rigidbody.GetPointVelocity (gameObject.transform.position);
		Debug.Log (speed);
		Vector3 dis = playerLocation - gameObject.transform.position;
		forceVector = 5*(dis - speed);
		gameObject.rigidbody.AddRelativeForce (forceVector);
		GameObject obj = Instantiate (bullet, gameObject.transform.position, Quaternion.identity)as GameObject;
		bullet b = obj.GetComponent<bullet> ();
		dis = Vector3.Normalize (dis);
		b.heading = Quaternion.Euler (dis);
	}

	void OnCollisionEnter(Collision collision){
		Collider collider = collision.collider;
		if (collider.CompareTag ("asteroid")) {
			Physics.IgnoreCollision(this.collider,collider);
		}
	}

	public void Die(){
		Destroy (gameObject);
		GameObject obj = GameObject.Find ("globalObj");
		global globalObj = obj.GetComponent<global> ();
		globalObj.UFONum -= 1;
	}
}
