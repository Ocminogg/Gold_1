using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

//[CreateAssetMenu(fileName = "Cell", menuName = "Gameplay/ New Cell")]
public class Cell : MonoBehaviour, IPointerClickHandler
{
    public static Cell Instance;
    public int X { get; private set; }
    public int Y { get; private set; } 

    public int Depth { get; private set; }// Уровень земли
    public int Gold { get; private set; }// Глубина где спрятано золото
    
    public int GoldFind { get; private set; }// Наличие золота в клетке
    public bool IsEmpty => Depth < 1;

    public const int MaxValue = 3;//Максимальная глубина
    
    
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
    //Обычный Cell для создания поля с травой
    public void SetValue(int x,int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;

        UpdateCell();
    }
    // Cell для закапывания золота
    public void SetValueGold(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Gold = depth;//Глубина нахождения золота
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        Clicked?.Invoke(this);
        //image.color == CollorManager.Instance.CellColor[4]
        if (Depth == Gold)//Проверяем лежит ли в клетке золото
        {
            GameCounter.Instance.AddPoints();//Добавляем в счетчик взятое золото
            image.color = CollorManager.Instance.CellColor[3];//Так как мы забрали золото, то на месте ничего не остается
            Depth = 6;
            GoldFind = 0;
        }
        if ((GameCounter.GameStarted == true) && (Depth != Gold) && (GameCounter.ShovelsCount!=0))
            Dig();
        
    }

    public void Dig()//Dig-копать
    {
        Depth++;
        if (GoldFind == 0 & (Depth <= MaxValue) )//Проверка нет ли в клетке уже золота и глубину яму
            GameCounter.Instance.SubShovel();//Тратим лопату
        if ((Depth <= MaxValue) && (Depth == Gold)) 
        {
            GoldFind = 1;
            GameCounter.Instance.AddPoints(1);//Мы нашли золота, но осталось его поднять повторным кликом
            UpdateCellGold();
            
        }
        

        if ((Depth <= MaxValue) && (GoldFind == 0))
            UpdateCell();
    }
    /// <summary>
    /// Меняем цвета клеток в зависимости от уровня земли
    /// </summary>
    public void UpdateCell()
    {
        
        //image.color = this.colorData.CellColor[Depth];
        image.color = CollorManager.Instance.CellColor[Depth];
    }
    /// <summary>
    /// Меняем цвет клетки на золото
    /// </summary>
    public void UpdateCellGold()
    {
        //image.color = this.colorData.CellColor[4];
        image.color = CollorManager.Instance.CellColor[4];
    }
}
