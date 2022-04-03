using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class MainMenu : MonoBehaviour
{
  public static MainMenu Instance;

  public TextMeshProUGUI inputField;
  public GameObject mainCanvas;
  public static string playername = "Player";
  public static string highScoreName = "Player2";
  public static int high_Points = 0;

  [System.Serializable]
  class SaveData
  {
      public int high_Points;
      public string highScoreName;
  }
  public void SaveScore()
  {
    SaveData data = new SaveData();
    data.high_Points = high_Points;
    data.highScoreName = highScoreName;

    string json = JsonUtility.ToJson(data);

    File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
  }

  public void LoadScore()
  {
    string path = Application.persistentDataPath + "/savefile.json";
    if (File.Exists(path))
    {
        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        high_Points = data.high_Points;
        highScoreName = data.highScoreName;
    }
  }

  private void Awake()
  {

    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    LoadScore();
  }

  public void StartNew()
  {
  playername = inputField.text;
  SceneManager.LoadScene(1);
  mainCanvas.SetActive(false);
  }
}
