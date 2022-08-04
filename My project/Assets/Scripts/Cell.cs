using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public static Cell Instance;
    public int X { get; private set; }
    public int Y { get; private set; }

    
    int Depth;
    
    int Gold;
    
    int GoldFind;
    
    const int MaxValue = 3;//Максимальная глубина
    
    
    public event Action<Cell> Clicked;

    [SerializeField]
    private Script_CollorManager colorData;
    
    [SerializeField]
    private Image image;

    private void Awake()
    {        
        GoldFind = 0;
        Depth = 0;
        UpdateCell();
    }
    
    public void SetValue(int x,int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;
        GoldFind = 0;
        UpdateCell();
    }
    
    public void SetValueGold(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Gold = depth;      
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        Clicked?.Invoke(this);                
        if (Depth == Gold)
        {
            GameCounter.Instance.AddPoints();
            image.color = CollorManager.Instance.CellColor[3];
            Depth = 4;
            
        }
        if ((GameCounter.GameStarted == true) & (Depth != Gold) & (GameCounter.ShovelsCount!=0) & (Depth < MaxValue))
            Dig();
        
    }

    public void Dig()
    {
        
        Depth++;
        Debug.Log("Глубина " + Depth + "   Золото " + Gold + "   GoldFind "+ GoldFind);
        if ((GoldFind == 0) & (Depth <= MaxValue) & (Depth != Gold))
        {
            
            GameCounter.Instance.SubShovel();
            UpdateCell();
        }
            
        if ((Depth <= MaxValue) && (Depth == Gold)) 
        {
            GameCounter.Instance.SubShovel();
            GoldFind = 1;
            GameCounter.Instance.AddPointsOnField();
            UpdateCellGold();
            
        }        
    }
    
    public void UpdateCell()
    {        
        //image.color = this.colorData.CellColor[Depth];
        image.color = CollorManager.Instance.CellColor[Depth];
    }
    
    public void UpdateCellGold()
    {
        //image.color = this.colorData.CellColor[4];
        image.color = CollorManager.Instance.CellColor[4];
    }
}
