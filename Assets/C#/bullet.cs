using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {
	public float force;
	public Quaternion heading;
	public int host;
	public int level;
	public int PLAYER = 0, UFO = 1;
	public AudioClip explosion;
	public float width,height,depth;
	// Use this for initialization
	void Start () {
		force = 600f;
		gameObject.rigidbody.MoveRotation (heading);
		gameObject.rigidbody.AddRelativeForce (0, 0, force);
		gameObject.rigidbody.drag = 0;
		GameObject obj = GameObject.Find ("globalObj");
		global g = obj.GetComponent<global> ();
		width = g.width;
		height = g.height;
		depth = g.depth;
	}
	
	// Update is called once per frame
	void Update () {
		/*Vector3 pos = gameObject.transform.position;
		//if(pos.y!=0) pos.y=0;
		if (pos.x < -width-5 || pos.x >width+5) Die ();
		if(pos.y<-depth-5||pos.y>depth+5) Die ();
		if(pos.z<-height-5||pos.z+5>height) Die ();*/
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
		else if(collider.CompareTag("wall")){
			Die();
		}
	}

	public void Die(){
		Destroy (gameObject);
	}
}
