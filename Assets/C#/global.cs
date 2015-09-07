using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class global : MonoBehaviour {
	public int asteroidNum,level,score,lives,time,deltaTime,heavyWeapon,UFONum,asteroidDestroyed,genAsteroidNum,highScore;
	public float width, height,depth, levelCountDown, gameOverCountDown,timeCount;
	private string[][] fileInfo;
	private string inputName;
	private bool levelPassed;
	private bool gameOver;
	private bool beatHighScore;
	public GameObject asteroid1,asteroid2,asteroid3,asteroid4,spaceship,ufo;
	public Vector3 originInScreenCoords;
	// Use this for initialization
	void Start () {
		score = 0;
		level = 1;
		heavyWeapon = -1;
		lives = 3;
		gameOver = false;
		beatHighScore = false;
		inputName = "name";
		fileInfo = new string[9][];
		for(int i=0;i<fileInfo.Length;i++)
			fileInfo[i]=new string[2];
		this.levelCountDown = 0;
		this.gameOverCountDown = 0;

		levelPassed = true;
		originInScreenCoords = Camera.main.WorldToScreenPoint (new Vector3(0, 0, 0));
		width = Camera.main.pixelWidth;
		height = Camera.main.pixelHeight;
		Vector3 v = Camera.main.ScreenToWorldPoint (new Vector3(width, height, originInScreenCoords.z));
		//width = v.x;
		//height = v.z;
		width = 5;
		height = 4;
		depth = 0.8f;

		LoadFile ("txt/score.txt");

		resetPlayer ();
		//GameObject obj = GameObject.Find ("spaceship(Clone)");
		//Destroy (obj);
		//resetPlayer ();
	}

	public void resetPlayer(){
		generatePlayer ();
		score -= 100;
		if(score<0) score=0;
	}

	public void showGameOver(){
		GameObject obj=GameObject.Find("LevelText");
		levelGUI text=obj.GetComponent<levelGUI>();
		text.showGameOver ();
		gameOver = true;
	}

	void generateLevel(){
		genAsteroidNum = 3 + 2*level;
		asteroidNum = genAsteroidNum;
		UFONum = 0;
		time = 15 + 5 * level;
		deltaTime = time;
		timeCount = 0;
		heavyWeapon += 2;
		asteroidDestroyed = 0;
		generate ();
		GameObject obj = GameObject.Find ("spaceship(Clone)");
		spaceship s = obj.GetComponent<spaceship> ();
		s.level = level;
		s.transform.position = new Vector3 (0, 0, 0);
	}

	void generatePlayer(){
		GameObject obj = Instantiate (spaceship, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		spaceship s = obj.GetComponent<spaceship> ();
		s.level = level;
		s.resetPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		timeCount += Time.deltaTime;
		if (asteroidDestroyed >=10) {
			asteroidDestroyed=0;
			GameObject obj=Instantiate(ufo,new Vector3(5,-0.038f,-1),Quaternion.identity) as GameObject;
			UFO u=obj.GetComponent<UFO>();
			u.locatePlayer();
			u.setForce();
			u.maxFindPlayerTime=5;
			UFONum++;
		}
		if (levelPassed) {
			levelCountDown+=Time.deltaTime;
			if(levelCountDown>3){
				levelCountDown=0;
				generateLevel();
				levelPassed=false;

				GameObject obj=GameObject.Find("LevelText");
				levelGUI text=obj.GetComponent<levelGUI>();
				text.removeText();
			}
		}
		if (asteroidNum == 0&&!levelPassed&&UFONum==0) {
			level++;
			GameObject obj=GameObject.Find("LevelText");
			levelGUI text=obj.GetComponent<levelGUI>();
			text.resetLevel(level);
			levelPassed=true;
			score+=deltaTime;
			if(score<0) score=0;
		}
		if (gameOver) {
			gameOverCountDown+=Time.deltaTime;
			if(gameOverCountDown>4){
				if(score>highScore){
					gameOver=false;
					beatHighScore=true;
					GameObject obj=GameObject.Find("LevelText");
					levelGUI text=obj.GetComponent<levelGUI>();
					text.removeText();
				}
				else
					Application.LoadLevel("startScene");
			}
		}
		if (timeCount > 1) {
			timeCount-=1;
			if(time>0) time-=1;
			deltaTime-=1;
		}
	}

	private void generate(){
		float xBound, zBound,x,y,z,n,xf,zf;
		GameObject obj;
		xBound = Camera.main.pixelWidth;
		zBound = Camera.main.pixelHeight;
		for (int i=0; i<genAsteroidNum; i++) {
			x=Random.Range (0.0f,xBound);
			while(Mathf.Abs(1.0f*x/xBound-0.5f)<0.2f) x=Random.Range (0.0f,xBound);
			z=Random.Range (0.0f,zBound);
			while(Mathf.Abs(1.0f*z/zBound-0.5f)<0.2f) z=Random.Range (0.0f,zBound);
			n=Random.Range(0,4);
			if(n<1) obj=Instantiate(asteroid1,Camera.main.ScreenToWorldPoint(new Vector3(x,z,originInScreenCoords.z)),Quaternion.identity) as GameObject;
			else if(n<2)obj=Instantiate(asteroid2,Camera.main.ScreenToWorldPoint(new Vector3(x,z,originInScreenCoords.z)),Quaternion.identity) as GameObject;
			else if(n<3)obj=Instantiate(asteroid3,Camera.main.ScreenToWorldPoint(new Vector3(x,z,originInScreenCoords.z)),Quaternion.identity) as GameObject;
			else obj=Instantiate(asteroid4,Camera.main.ScreenToWorldPoint(new Vector3(x,z,originInScreenCoords.z)),Quaternion.identity) as GameObject;
			xf=Random.Range(0f,50f+10*level)-25-5*level;
			zf=Random.Range(0f,50f+10*level)-25-5*level;
			asteroid a=obj.GetComponent<asteroid>();
			a.mass=100;
			a.rigidbody.AddRelativeForce(xf,0,zf);
		}
	}

	bool LoadFile(string filename){
		try{
			string fileLine;
			int k=0;
			StreamReader reader=new StreamReader(filename,Encoding.Default);
			fileLine=reader.ReadLine();
			while(fileLine!=null){
				string[] entries=fileLine.Split(',');
				int.TryParse(entries[1],out highScore);
				fileInfo[k][0]=entries[0];
				fileInfo[k][1]=entries[1];
				k++;
				fileLine=reader.ReadLine();
			}
			reader.Close();
		}
		catch(IOException e){
			return false;
		}
		return true;
	}

	bool WriteFile(string filename,string playerName){
		try{
			StreamWriter writer=new StreamWriter(filename);
			for(int i=0;i<fileInfo.Length;i++){
				int scoreTmp=0;
				int.TryParse(fileInfo[i][1],out scoreTmp);
				if(score>scoreTmp){
					for(int j=8;j>i;j--){
						fileInfo[j][0]=fileInfo[j-1][0];
						fileInfo[j][1]=fileInfo[j-1][1];
					}
					fileInfo[i][0]=playerName;
					fileInfo[i][1]=score.ToString();
					break;
				}
			}
			for(int i=0;i<fileInfo.Length;i++){
				string tmp=fileInfo[i][0]+","+fileInfo[i][1];
				writer.WriteLine(tmp);
			}
			writer.Close();
		}
		catch(IOException e){
			return false;
		}
		return true;
	}

	void OnGUI(){
		if(beatHighScore){
			GUILayout.BeginArea(new Rect(50,Screen.height/2,Screen.width-100,200));
			GUILayout.Label ("Congratulations! Please input your name");
			inputName=GUILayout.TextField (inputName);
			bool b=GUILayout.Button ("OK");
			GUILayout.EndArea ();
			if(b){
				WriteFile("txt/score.txt",inputName);
				Application.LoadLevel("highScoreScene");
			}
		}
	}
}
