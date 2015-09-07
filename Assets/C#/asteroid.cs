using UnityEngine;
using System.Collections;

public class asteroid : MonoBehaviour {
	public GameObject asteroid1;
	public GameObject asteroid2;
	public GameObject asteroid3;
	public GameObject asteroid4;
	public GameObject explosionParcitle;
	public float speed;
	public Vector3 rotationAngle;
	public Vector3 rotationSpeed;
	public float mass;
	public float width,height,depth;
	public int level;
	public bool destroyed;
	public AudioClip explosion;
	// Use this for initialization
	void Start () {
		destroyed = false;
		GameObject obj = GameObject.Find ("globalObj");
		global g = obj.GetComponent<global> ();
		width = g.width;
		height = g.height;
		depth = g.depth;
		level = g.level;
		gameObject.rigidbody.AddRelativeTorque (Random.Range (0, 1f), Random.Range (0, 1f), Random.Range (0, 1f));
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = gameObject.transform.position;
		//if(pos.y!=0) pos.y=0;
		if(pos.x<-width){
			pos.x=width;
		}
		else if(pos.x>width) pos.x=-width;
		if(pos.y<-depth){
			pos.y=-depth;
			//gameObject.rigidbody.AddRelativeForce(0,1,0);
		}
		else if(pos.y>depth){
			pos.y=depth;
			//gameObject.rigidbody.AddRelativeForce(0,-1,0);
		}
		if(pos.z<-height) pos.z=height;
		else if(pos.z>height) pos.z=-height;
		gameObject.transform.position = pos; 
	}

	void FixedUpdate(){
		//Vector3 force = gameObject.rigidbody.GetRelativePointVelocity(new Vector3(0,0,0));
		//if(force.y!=0) force.y=0;
		//gameObject.rigidbody.AddRelativeForce (force);

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

	public void Split(){
		GameObject obj;
		int piece = Random.Range (2, 4+level/3);
		float sum = 0,xAllMon=0,zAllMon=0;
		float[] weight=new float[piece];
		float[] subMass = new float[piece];
		Vector3[] pos = new Vector3[piece];
		float deltaAngle = 360f / piece;
		float startAngle = Random.Range (0, 360);
		Vector3 position = gameObject.transform.position;
		for(int i=0;i<piece;i++){
			weight[i]=Random.Range(30f,100f);
			sum+=weight[i];
			pos[i].x=0.2f*Mathf.Cos ((startAngle+deltaAngle*i)/180*Mathf.PI)+position.x;
			pos[i].y=position.y;
			pos[i].z=0.2f*Mathf.Sin ((startAngle+deltaAngle*i)/180*Mathf.PI)+position.z;
		}
		for(int i=0;i<piece;i++){
			weight[i]/=sum;
		}
		for (int i=0; i<piece; i++) {
			float xMon,zMon;
			int n;
			subMass[i]=mass*weight[i];
			if(i!=piece-1){
				xMon=Random.Range(0f,(1+level*0.1f)*subMass[i])-(0.5f+0.05f*level)*subMass[i];
				xAllMon+=xMon;
				zMon=Random.Range(0f,(1+level*0.1f)*subMass[i])-(0.5f+0.05f*level)*subMass[i];
				zAllMon+=zMon;
			}
			else{
				xMon=-xAllMon;
				zMon=-zAllMon;
			}
			n=Random.Range(1,5);
			if(n==1) obj=Instantiate(asteroid1,pos[i],Quaternion.identity) as GameObject;
			else if(n==2) obj=Instantiate(asteroid2,pos[i],Quaternion.identity) as GameObject;
			else if(n==3) obj=Instantiate(asteroid3,pos[i],Quaternion.identity) as GameObject;
			else obj=Instantiate(asteroid4,pos[i],Quaternion.identity) as GameObject;
			asteroid a=obj.GetComponent<asteroid>();
			float scale=Mathf.Pow(weight[i],0.8f);
			if(scale<0.5) scale=0.5f;
			a.transform.localScale*=scale;
			a.rigidbody.AddRelativeForce(xMon,0,zMon);
			a.mass=subMass[i];
		}
		obj=GameObject.Find("globalObj");
		global g=obj.GetComponent<global>();
		g.asteroidNum+=piece;
		Die ();
	}

	public void Die(){
		Destroy (gameObject);
		GameObject obj=GameObject.Find("globalObj");
		global g=obj.GetComponent<global>();
		g.asteroidNum-=1;
		g.asteroidDestroyed += 1;
		obj = Instantiate (explosionParcitle, gameObject.transform.position, Quaternion.identity) as GameObject;
	}
}
