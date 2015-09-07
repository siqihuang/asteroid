using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class highScoreGUI : MonoBehaviour {
	private string[][] fileInfo;
	// Use this for initialization
	void Start () {
		fileInfo = new string[9][];
		for(int i=0;i<fileInfo.Length;i++)
			fileInfo[i]=new string[2];
		LoadFile ("txt/score.txt");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	bool LoadFile(string filename){
		try{
			string fileLine;
			int k=0;
			StreamReader reader=new StreamReader(filename,Encoding.Default);
			fileLine=reader.ReadLine();
			while(fileLine!=null){
				string[] entries=fileLine.Split(',');
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

	void OnGUI(){
		bool b;
		GUILayout.BeginArea(new Rect(100,40,Screen.width/2-100,20));
		GUILayout.Label ("name");
		GUILayout.EndArea();
		GUILayout.BeginArea(new Rect(Screen.width/2+100,40,Screen.width-100,20));
		GUILayout.Label ("score");
		GUILayout.FlexibleSpace ();
		GUILayout.EndArea();
		for (int i=0; i<fileInfo.Length; i++) {
			GUILayout.BeginArea(new Rect(100,80+20*i,Screen.width/2-100,20));
			GUILayout.Label(fileInfo[i][0]);
			GUILayout.EndArea();
			GUILayout.BeginArea(new Rect(Screen.width/2+100,80+20*i,Screen.width-100,20));
			GUILayout.Label(fileInfo[i][1]);
			GUILayout.EndArea();
		}
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
