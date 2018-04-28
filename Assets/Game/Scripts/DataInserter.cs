using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DataInserter  {
     
	public string inputUserName;
	public string inputPassword;
	public string inputEmail;

	string apiUrl = "http://arek.advances.pl/unity/api.php";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) insertScore(33, 11, 11);
	}

	public void insertScore(int score, int userId, int gameId){
		WWWForm form = new WWWForm();
		form.AddField("score", score);
		form.AddField("method", "insert_score" );

        form.AddField("user_id", userId);
		form.AddField("game_id", gameId);

		WWW www = new WWW(apiUrl, form); 
 
	}
    public int getHighScore()
    {

        WWWForm form = new WWWForm();
        form.AddField("method", "get_high_score");
        WWW www = new WWW(apiUrl, form);

        WaitForSeconds w;
        while (!www.isDone)
            w = new WaitForSeconds(0.1f);

 
          return Convert.ToInt32(( www.text ) );
    
       
    }
} 
