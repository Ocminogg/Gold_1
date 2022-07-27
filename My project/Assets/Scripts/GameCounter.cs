using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCounter : MonoBehaviour
{
    public static GameCounter Instance;

    //public static int Points { get; private set; } /////////!!!!!
    //public static int FindGolds { get; private set; } /////////!!!!!
    int Points;
    int FindGolds;

    public static int ShovelsCount { get; private set; }
    public static bool GameStarted { get; private set; }

    [SerializeField]
    private Script_GameCounter GameCounterData;

    //[Header("Кол-во лопат и необходимое кол-во золота для победы")]
    //public int Shovel;
    //public int Golds;

    [SerializeField]
    private TextMeshProUGUI gameResult;
    [SerializeField]
    public TextMeshProUGUI pointsText;
    [SerializeField]
    public TextMeshProUGUI shovelsText;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Points = 0;
        FindGolds = 0;
        ShovelsCount = this.GameCounterData.Shovel;
        gameResult.text = "";
        pointsText.text = "0";
        shovelsText.text = Convert.ToString(this.GameCounterData.Shovel);
        GameStarted = true;        
        //Field.Instance.ClearGold();
        Field.Instance.GenerateField();
    }
    ///////////////////////////////////////////////////////
    void Win()
    {
        GameStarted = false;
        gameResult.text = "U Win!";
    }
    void Lose()
    {
        GameStarted = false;
        gameResult.text = "U Lose!";
    }
    //////////////////////////////////////////////////////
    public void AddPoints()
    {
        SetPoints(Points + 1);
    }
    public void AddPoints(int points)
    {
        FindGolds += points;
    }
    void SetPoints(int points) ////////////!!!!!
    {
        Points = points;
        pointsText.text = Points.ToString();
        if (Points == this.GameCounterData.Golds)
            Win();
    }
    ///////////////////////////////////////////////////
    public void SubShovel() 
    {
        SetShovel(ShovelsCount - 1);
    }
    void SetShovel(int points) //////////!!!!!!
    {
        ShovelsCount = points;
        shovelsText.text = ShovelsCount.ToString();
        if ((ShovelsCount == 0) && (FindGolds < this.GameCounterData.Golds ))
        {
            Lose();
        }
    }
}
