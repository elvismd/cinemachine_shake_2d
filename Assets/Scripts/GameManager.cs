using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public struct PlayerData
    {
        public int life;
        public int highscore;
    }

    int life = 5;

    int score = 0;
    int highScore;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    [SerializeField] private TextMeshProUGUI lifeLabel;
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI highScoreLabel;

    PlayerData playerData;

    void Start()
    {
        playerData = new PlayerData();
        playerData.life = 5;

        if (File.Exists(Application.dataPath + "/savedata.txt"))
            playerData = JsonUtility.FromJson<PlayerData>(File.ReadAllText(Application.dataPath + "/savedata.txt"));

        highScore = playerData.highscore;
        life = playerData.life;

        lifeLabel.text = life.ToString();

        score = 0;
        scoreLabel.text = score.ToString();
        highScoreLabel.text = highScore.ToString();

        //for (int i = 0; i < 20; i++)
        //{
        //    AddLife();
        //}
    }

    public void AddScore()
    {
        score++;
        scoreLabel.text = score.ToString();
        if (score > highScore)
        {
            highScore = score;
            highScoreLabel.text = highScore.ToString();

            //PlayerPrefs.SetInt("Highscore", highScore);

            playerData.highscore = highScore;

            SaveData();
        }
    }

    public void AddLife()
    {
        life++;
        lifeLabel.text = life.ToString();
        playerData.life = life;
        SaveData();
    }

    public void RemoveLife()
    {
        life--;
        lifeLabel.text = life.ToString();

        playerData.life = life;
        SaveData();

        impulseSource.GenerateImpulseWithForce(1f);
        playerController.DoJump();
    }

    void SaveData()
    {
        var jsonData = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.dataPath + "/savedata.txt", jsonData);
    }
}
