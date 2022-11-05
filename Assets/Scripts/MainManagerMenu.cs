using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class MainManagerMenu : MonoBehaviour
{
    public static MainManagerMenu Instance;
    public string textInputEntered;
    [SerializeField] TextMeshProUGUI bestScoreText;

    public void Awake()
    {
        // as only one instance exist of this class/gameobject, we first check if its null or not. This pattern is called SINGLETON.
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this; // assigning the current instance of this class.
        DontDestroyOnLoad(gameObject); // by this line, our MainManager gameobject to which this script is assigned wont be destroyed when the scene changes.

       
    }

    [System.Serializable]
    public class SaveData // making this class serializable, we are making it available to JSON format to store out best score.
    {
        public int bestScore; // a variable to store the best Score..
        public string playerName;
    }

    public void SaveBestScoreAndName(int bestScore)
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;
        data.playerName = textInputEntered;
        // Serialization - storing our data in JSON format & writing it onto a file.
        string json = JsonUtility.ToJson(data); // storing our bestscore in data object & data object into JSON format.
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // write string to a file at the provided path & also mention the file name.
    }

    public int LoadBestScore()
    {
        SaveData data = new SaveData();
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            // Deserialization - fetching our data from file to JSON & then converting it into our data object.
            string json = File.ReadAllText(path); // reading all data from file 
            data = JsonUtility.FromJson<SaveData>(json); // from JSON to our data object format.
        }
        return data.bestScore; // directly returning the bestScore which was stored in the previous session.
    }

    public string LoadPlayerName()
    {
        SaveData data = new SaveData();
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            // Deserialization - fetching our data from file to JSON & then converting it into our data object.
            string json = File.ReadAllText(path); // reading all data from file 
            data = JsonUtility.FromJson<SaveData>(json); // from JSON to our data object format.
        }
        return data.playerName;
    }

}
