using UnityEngine;
using System.Collections;

public class spaceship : MonoBehaviour {
	public float thrustForce;
	public float rotationSpeed;
	public float rotatedAngle;
	public GameObject bullet,leftSpark,rightSpark,fcam,bcam,heavy;
	public AudioClip explosion;
	public float maxFireTime;
	public int level;
	private float fireTimer;
	public float width,height,depth;
	public bool newShip,enabled;
	public Quaternion heading;
	private float protectTime,blinkTime,deltaBlinkTime,deltaProtectTime,sparkTime,deltaSparkTime;
	// Use this for initialization
	void Start () {
		thrustForce = 1f;
		rotationSpeed = 2f;
		rotatedAngle = 0f;
		maxFireTime = 0.5f;
		protectTime = 3f;
		blinkTime = 0.3f;
		sparkTime = 0.2f;
		enabled = true;
		newShip = true;
		fcam = findChild ("frontCamera");
		bcam = findChild ("backCamera");
		//fcam=GameObject.Find("frontCamera");
		//bcam = GameObject.Find ("backCamera");
		GameObject obj = GameObject.Find ("globalObj");
		global g = obj.GetComponent<global> ();
		width = g.width;
		height = g.height;
		depth = g.depth;
	}

	private GameObject findChild(string name){
		Transform t = gameObject.transform;
		foreach (Transform tc in t) {
			if(tc.gameObject.name==name) return tc.gameObject;
		}
		return null;
	}

	void blinkShip(bool enabled){
		Renderer[] render = gameObject.GetComponentsInChildren<Renderer>();
		for(int i=0;i<render.Length;i++)
			render[i].enabled=enabled;
	}

	public void resetPlayer(){
		gameObject.layer = LayerMask.NameToLayer ("newShip");
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos;
		fireTimer += Time.deltaTime;
		deltaSparkTime += Time.deltaTime;
		GameObject obj=GameObject.Find("globalObj");
		global g = obj.GetComponent<global> ();
		if (Input.GetButtonDown ("Fire1")&&fireTimer>maxFireTime) {
			fireTimer=0;
			pos=gameObject.transform.position;
			//pos.x+=0.2f*Mathf.Cos((rotatedAngle-90)/180*Mathf.PI);
			//pos.z-=0.2f*Mathf.Sin((rotatedAngle-90)/180*Mathf.PI);
			pos.x=0;pos.y=0;pos.z=0.0f;
			obj=Instantiate(bullet,pos,Quaternion.identity) as GameObject;
			bullet b=obj.GetComponent<bullet>();
			b.transform.parent=gameObject.transform;
			obj.transform.position=transform.TransformPoint(-20,0,300);
			obj.transform.parent=null;
			//b.transform.position=gameObject.transform.position;
			//b.heading=Quaternion.Euler(0,rotatedAngle,0);
			b.heading=gameObject.transform.rotation;
			b.host=b.PLAYER;
			b.level=level;
		}
		else if(Input.GetButtonDown("Fire2")&&g.heavyWeapon>0){
			g.heavyWeapon-=1;
			pos=transform.TransformPoint(-20,0,300);
			//pos=gameObject.transform.position;
			heading=gameObject.transform.rotation;
			obj=Instantiate(heavy,pos,Quaternion.identity) as GameObject;
			heavyWeapon h=obj.GetComponent<heavyWeapon>();
		}
		pos = gameObject.transform.position;
		//if(pos.y!=0) pos.y=0;
		if(pos.x<-width) pos.x=width;
		else if(pos.x>width) pos.x=-width;
		if(pos.y<-depth) pos.y=-depth;
		else if(pos.y>depth) pos.y=depth;
		if(pos.z<-height) pos.z=height;
		else if(pos.z>height) pos.z=-height;
		gameObject.transform.position = pos; 

		if (newShip) {
			deltaBlinkTime+=Time.deltaTime;
			deltaProtectTime+=Time.deltaTime;
			if(deltaBlinkTime>blinkTime){
				deltaBlinkTime=0;
				enabled=!enabled;
				blinkShip(enabled);
			}
			if(deltaProtectTime>protectTime){
				deltaProtectTime=0;
				enabled=true;
				blinkShip(enabled);
				gameObject.layer = LayerMask.NameToLayer ("spaceship");
				newShip=false;
			}
		}

		//refreshCamera ();
	}

	void refreshCamera(){
		Vector3 pos = gameObject.transform.position;
		pos.x -= 0.5f * Mathf.Cos ((rotatedAngle - 90) / 180 * Mathf.PI);
		pos.z += 0.5f * Mathf.Sin ((rotatedAngle - 90) / 180 * Mathf.PI);
		pos.y = 0.2f;
		fcam.transform.position = pos;

		pos = gameObject.transform.position;
		pos.x -= -0.1f * Mathf.Cos ((rotatedAngle - 90) / 180 * Mathf.PI);
		pos.z += -0.1f * Mathf.Sin ((rotatedAngle - 90) / 180 * Mathf.PI);
		pos.y = 0.3f;
		bcam.transform.position = pos;
	}

	void FixedUpdate(){
		if (Input.GetKey(KeyCode.W)) {
			gameObject.rigidbody.AddRelativeForce(0,0,thrustForce);
			if(deltaSparkTime>sparkTime){
				Vector3 pos=gameObject.transform.position;
				deltaSparkTime=0;
				/*pos.x+=0.05f*Mathf.Cos((rotatedAngle+60)/180*Mathf.PI);
				pos.z-=0.05f*Mathf.Sin((rotatedAngle+60)/180*Mathf.PI);
				GameObject obj=Instantiate(leftSpark,pos,Quaternion.identity) as GameObject;
				obj.transform.Rotate(0,rotatedAngle+180,0);

				pos=gameObject.transform.position;
				pos.x+=0.05f*Mathf.Cos((rotatedAngle+120)/180*Mathf.PI);
				pos.z-=0.05f*Mathf.Sin((rotatedAngle+120)/180*Mathf.PI);
				obj=Instantiate(rightSpark,pos,Quaternion.identity) as GameObject;
				obj.transform.Rotate(0,rotatedAngle+180,0);*/
				GameObject obj=Instantiate(leftSpark,pos,Quaternion.identity) as GameObject;
				particle p=obj.GetComponent<particle>();
				obj.transform.parent=gameObject.transform;
				obj.transform.position=transform.TransformPoint(-70,0,-300);
				obj.transform.parent=null;
				Vector3 v=gameObject.transform.rotation.eulerAngles;
				v.y+=180;
				Quaternion q=Quaternion.Euler(v);
				p.heading=q;

				obj=Instantiate(rightSpark,pos,Quaternion.identity) as GameObject;
				p=obj.GetComponent<particle>();
				obj.transform.parent=gameObject.transform;
				obj.transform.position=transform.TransformPoint(50,0,-300);
				obj.transform.parent=null;
				v=gameObject.transform.rotation.eulerAngles;
				v.y+=180;
				q=Quaternion.Euler(v);
				p.heading=q;
			}
		}
		/*if (Input.GetAxisRaw ("Horizontal") > 0) {
			rotatedAngle+=rotationSpeed;
			//Quaternion q=Quaternion.Euler(0,rotatedAngle,0);
			Quaternion q=Quaternion.Euler(rotatedAngle,0,0);
			gameObject.rigidbody.MoveRotation(q);
			//fcam.transform.Rotate (0,0,rotationSpeed);
			//bcam.transform.Rotate (0,0,rotationSpeed);
		}
		else if (Input.GetAxisRaw ("Horizontal") < 0) {
			rotatedAngle-=rotationSpeed;
			//Quaternion q=Quaternion.Euler(0,rotatedAngle,0);
			Quaternion q=Quaternion.Euler(0,0,rotatedAngle);
			gameObject.rigidbody.MoveRotation(q);
			//fcam.transform.Rotate (0,-rotationSpeed,0);
			//bcam.transform.Rotate (0,-rotationSpeed,0);
		}*/
	}

	void OnCollisionEnter(Collision collision){
		//Collider collider = collision.collider;
		/*if (collider.CompareTag ("bullet")) {
			GameObject obj=collider.rigidbody.gameObject;
			bullet b=obj.GetComponent<bullet>();
			if(b.host==b.PLAYER)
				Physics.IgnoreCollision(this.collider,collider);
			else{
				Die ();
				AudioSource.PlayClipAtPoint(explosion,gameObject.transform.position);
			}
		}*/
		/*else{
			Die ();
			AudioSource.PlayClipAtPoint(explosion,gameObject.transform.position);
		}*/
		if(!collision.collider.CompareTag("wall")){
			Die ();
			AudioSource.PlayClipAtPoint(explosion,gameObject.transform.position);
		}
	}

	public void Die(){
		GameObject obj=GameObject.Find("globalObj");
		global g=obj.GetComponent<global>();
		g.lives-=1;
		if (g.lives <= 0){
			g.showGameOver();
		}
		else{
			fcam.transform.Rotate(0,-rotatedAngle,0);
			bcam.transform.Rotate(0,-rotatedAngle,0);
			g.resetPlayer();
		}
		Destroy (gameObject);
	}
}
