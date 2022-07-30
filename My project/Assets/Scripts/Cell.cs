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

    public int Depth { get; private set; }// ������� �����
    public int Gold { get; private set; }// ������� ��� �������� ������
    
    public int GoldFind { get; private set; }// ������� ������ � ������
    public bool IsEmpty => Depth < 1;

    public const int MaxValue = 3;//������������ �������
    
    
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
    //������� Cell ��� �������� ���� � ������
    public void SetValue(int x,int y, int depth)
    {
        X = x;
        Y = y;
        Depth = depth;

        UpdateCell();
    }
    // Cell ��� ����������� ������
    public void SetValueGold(int x, int y, int depth)
    {
        X = x;
        Y = y;
        Gold = depth;//������� ���������� ������
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
        Clicked?.Invoke(this);
        //image.color == CollorManager.Instance.CellColor[4]
        if (Depth == Gold)//��������� ����� �� � ������ ������
        {
            GameCounter.Instance.AddPoints();//��������� � ������� ������ ������
            image.color = CollorManager.Instance.CellColor[3];//��� ��� �� ������� ������, �� �� ����� ������ �� ��������
            Depth = 6;
            GoldFind = 0;
        }
        if ((GameCounter.GameStarted == true) && (Depth != Gold) && (GameCounter.ShovelsCount!=0))
            Dig();
        
    }

    public void Dig()//Dig-������
    {
        Depth++;
        if (GoldFind == 0 & (Depth <= MaxValue) )//�������� ��� �� � ������ ��� ������ � ������� ���
            GameCounter.Instance.SubShovel();//������ ������
        if ((Depth <= MaxValue) && (Depth == Gold)) 
        {
            GoldFind = 1;
            GameCounter.Instance.AddPoints(1);//�� ����� ������, �� �������� ��� ������� ��������� ������
            UpdateCellGold();
            
        }
        

        if ((Depth <= MaxValue) && (GoldFind == 0))
            UpdateCell();
    }
    /// <summary>
    /// ������ ����� ������ � ����������� �� ������ �����
    /// </summary>
    public void UpdateCell()
    {
        
        //image.color = this.colorData.CellColor[Depth];
        image.color = CollorManager.Instance.CellColor[Depth];
    }
    /// <summary>
    /// ������ ���� ������ �� ������
    /// </summary>
    public void UpdateCellGold()
    {
        //image.color = this.colorData.CellColor[4];
        image.color = CollorManager.Instance.CellColor[4];
    }
}
