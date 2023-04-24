using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveKeyInfo : MonoBehaviour {

	string path;
	bool isStart = false;
	float keyTime = 0;

	string json = "";

	KeyCode k = KeyCode.None;
	// Use this for initialization
	void Start () {
		path = Application.streamingAssetsPath + "/KeyInfo.txt";	
	}
	
	// Update is called once per frame
	void Update () {
		if(isStart)
        {
			keyTime += Time.deltaTime;

        }

         
		if (Input.GetKeyDown(KeyCode.W))
        {
			k = KeyCode.W;
			isStart = true;
			keyTime = 0;
        }

		if (Input.GetKeyUp(KeyCode.W))
		{
			isStart = false;
			json += "\n" + k.ToString() + ": " + keyTime.ToString();
			SaveItemInfo(json);
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			k = KeyCode.A;
			isStart = true;
			keyTime = 0;
		}

		if (Input.GetKeyUp(KeyCode.A))
		{
			isStart = false;
			json += "\n" + k.ToString() + ": " + keyTime.ToString();
			SaveItemInfo(json);
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			k = KeyCode.S;
			isStart = true;
			keyTime = 0;
		}

		if (Input.GetKeyUp(KeyCode.S))
		{
			isStart = false;
			json += "\n" + k.ToString() + ": " + keyTime.ToString();
			SaveItemInfo(json);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			k = KeyCode.D;
			isStart = true;
			keyTime = 0;
		}

		if (Input.GetKeyUp(KeyCode.D))
		{
			isStart = false;
			json += "\n" + k.ToString() + ": " + keyTime.ToString();
			SaveItemInfo(json);
		}



		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			k = KeyCode.UpArrow;
			isStart = true;
			keyTime = 0;
		}

		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			isStart = false;
			json += "\n" + k.ToString() + ": " + keyTime.ToString();
			SaveItemInfo(json);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			k = KeyCode.DownArrow;
			isStart = true;
			keyTime = 0;
		}

		if (Input.GetKeyUp(KeyCode.DownArrow))
		{
			isStart = false;
			json += "\n" + k.ToString() + ": " + keyTime.ToString();
			SaveItemInfo(json);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			k = KeyCode.LeftArrow;
			isStart = true;
			keyTime = 0;
		}

		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			isStart = false;
			json += "\n" + k.ToString() + ": " + keyTime.ToString();
			SaveItemInfo(json);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			k = KeyCode.RightArrow;
			isStart = true;
			keyTime = 0;
		}

		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			isStart = false;
			json += "\n" + k.ToString() + ": " + keyTime.ToString();
			SaveItemInfo(json);
		}

	}


	public void SaveItemInfo(string json)
	{
		//string path = player.name + "_" + System.DateTime.Now.GetHashCode() + ".json";

		using (FileStream fs = new FileStream(path, FileMode.Create))
		{
			using (StreamWriter writer = new StreamWriter(fs))
			{
				writer.Write(json);
			}
		}
	}
}
