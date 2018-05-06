using UnityEngine;
using System.Collections;
using System.IO; // this is required for input and output of data
using System;
using System.Runtime.Serialization.Formatters.Binary;//this is required to convert data into binary

public class GameManager : MonoBehaviour
{

    //we make static so in games only one script is name as this
    public static GameManager singleton;

    //variable of gamedata
    private GameData data;
    public DataInserter api = new DataInserter();
    //data not to store on device
    public float timeForQuestion;
    public int currentScore;
    public bool isGameOver;

    public int currentMode;


    //data to store on device
    public int hiScore;
    public bool isMusicOn;
    public bool isGameStartedFirstTime;

    private AudioSource bgMusic;

    //it is call only once in a scene
    void Awake()
    {
        MakeSingleton();
        InitializeVariables();
    }

    void MakeSingleton()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        bgMusic = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Application.loadedLevelName == "GamePlay")
        {
            bgMusic.Stop();
        }
    }

    void InitializeVariables()
    {
        Load();

        if (data != null)
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
        }
        else
        {
            isGameStartedFirstTime = true;
        }

        if (isGameStartedFirstTime)
        {

            isGameStartedFirstTime = false;
            hiScore = 0;
            isMusicOn = true;

            data = new GameData();


            data.setHiScore(hiScore);
            data.setIsMusicOn(isMusicOn);
            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            Save();

            Load();

        }
        else
        {

            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            isMusicOn = data.getIsMusicOn();
            string[] gameData = api.getGamePeckDetails(2, 1);
            Debug.Log(api.GetDataValue(gameData[0], "answer:"));
            hiScore = api.getHighScore(2,1,1);

        }


    }



    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/GameData.dat");
            if (data != null)
            {

                api.insertScore(hiScore, 1, 1);
                data.setHiScore(hiScore);
                data.setIsMusicOn(isMusicOn);
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);

                bf.Serialize(file, data);

            }
        }
        catch (Exception e)
        {
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }



    public void Load()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);
        }
        catch (Exception e)
        { }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }


}

[Serializable]
class GameData
{
    private int hiScore;
    private bool isMusicOn;
    private bool isGameStartedFirstTime;



    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool getIsGameStartedFirstTime()
    {

        return isGameStartedFirstTime;
    }


    //HiScore
    public void setHiScore(int hiScore)
    {


        this.hiScore = hiScore;
    }

    public int getHiScore()
    {

        return hiScore;
    }

    //Music
    public void setIsMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }

    public bool getIsMusicOn()
    {
        return isMusicOn;
    }


}