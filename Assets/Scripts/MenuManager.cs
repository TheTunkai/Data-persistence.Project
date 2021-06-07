using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public Text inputText;
    public Text highScoreText;
    public string playerName;
    public int highScore;
    public string playerWithHighScore;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

        LoadHighScore();
        highScoreText.text = "High Score: " + playerWithHighScore + ": " + highScore;
    }

    public void LoadMainScene()
    {
        playerName = inputText.text;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();

#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string player;
    }

    public void SaveHighScore(int score, string player)
    {
        SaveData data = new SaveData();
        data.highScore = score;
        data.player = player;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            playerWithHighScore = data.player;
            
        }
    }
}
