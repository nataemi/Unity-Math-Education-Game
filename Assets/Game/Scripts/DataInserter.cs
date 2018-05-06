using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DataInserter
{

    public string inputUserName;
    public string inputPassword;
    public string inputEmail;

    string apiUrl = "http://arek.advances.pl/unity/api.php";

    // Use this for initialization
    void Start()
    {

    }


    // Funkcja do dodawania nowego wyniku  danego uzytkownika do bazy
    // Przyklad uzycia : 
    // api.insertScore(2,1,1,55);

    public void insertScore(int gameId, int peck_id, int userId, int score   )
    {
        WWWForm form = new WWWForm();
        form.AddField("score", score);
        form.AddField("method", "insert_score");

        form.AddField("user_id", userId);
        form.AddField("game_id", gameId); 

        WWW www = new WWW(apiUrl, form);

    }
    // Funkcja do pobierania High Scora danego uzytkownika
    // Przyklad uzycia : 
    //  hiScore = api.getHighScore(2,1,1);

    public int getHighScore(int game_id, int peck_id, int user_id)
    {
       
        WWWForm form = new WWWForm();
        form.AddField("method", "get_high_score");
        form.AddField("game_id", game_id);
        form.AddField("peck_id", peck_id);
        form.AddField("user_id", user_id);
        WWW www = new WWW(apiUrl, form);

        WaitForSeconds w;
        while (!www.isDone)
            w = new WaitForSeconds(0.1f);


        return Convert.ToInt32((www.text));


    }
    // Funkcja do pobierania ustawien gry pod dany peck
    // Aby jej uzywac, konieczne jest aby po pobraniu danych wyciagac z niej dane przez GetDataValue
    // Przyklad uzycia : 
    //  string[] gameData = api.getGamePeckDetails(2, 1);
    // Debug.Log(api.GetDataValue(gameData[0], "answer:"));
    public string[] getGamePeckDetails(int game_id, int peck_id)
    {
        Debug.Log("Pobieram dane");

        string[] game_data;
        WWWForm form = new WWWForm();
        form.AddField("method", "get_game_peck_details");
        form.AddField("game_id", game_id);
        form.AddField("peck_id", peck_id);
        WWW www = new WWW(apiUrl, form);

        WaitForSeconds w;
        while (!www.isDone)
            w = new WaitForSeconds(0.1f);

        string data = www.text;
       
        game_data = data.Split(';');
        return game_data;
    }

    // Funkcja do pobierania ustawien gry pod dany peck
    // Aby jej uzywac, konieczne jest aby po pobraniu danych wyciagac z niej dane przez GetDataValue
    // Przyklad uzycia : 
    //  string[] gameData = api.getGamePeckDetails(2, 1);
    // Debug.Log(api.GetDataValue(gameData[0], "answer:"));
    public string[] getGamesByPeck(int peck_id)
    {
      
        string[] game_data;
        WWWForm form = new WWWForm();
        form.AddField("method", "get_games_by_peck");
        form.AddField("peck_id", peck_id);
        WWW www = new WWW(apiUrl, form);

        WaitForSeconds w;
        while (!www.isDone)
            w = new WaitForSeconds(0.1f);

        string data = www.text;
      
        game_data = data.Split(';');
        return game_data;
    }

    // Funkcja do pobierania pecka
    // Aby jej uzywac, konieczne jest aby po pobraniu danych wyciagac z niej dane przez GetDataValue
    // Przyklad uzycia : 
    //  string[] peck_data = api.getPeckDetails(1);
    // Debug.Log(api.GetDataValue(gameData[0], "answer:"));
    public string[] getPeckDetails(int peck_id)
    {
     
        string[] peck_data;
        WWWForm form = new WWWForm();
        form.AddField("method", "get_peck_details");
        form.AddField("peck_id", peck_id);
        WWW www = new WWW(apiUrl, form);

        WaitForSeconds w;
        while (!www.isDone)
            w = new WaitForSeconds(0.1f);

        string data = www.text;
       
        peck_data = data.Split(';');
        return peck_data;
    }



    public string GetDataValue(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|")) value = value.Remove(value.IndexOf("|"));
        return value;
    }

}