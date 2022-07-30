using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "Field",menuName ="Gameplay/ New Field")]
public class Field : MonoBehaviour
{
    public static Field Instance;

    [SerializeField]
    private Script_Field fieldData;

    //[Header("Характеристики Поля")]
    //public float CellSize;
    //public float CellSpacing;
    //public int FieldSize;
    //public int InitCellsCount;

    [Space(10)]
    [SerializeField]
    private Cell cellPref;
    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private Image image;

    private Cell[,] field;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    

    public void CreateField()
    {
        //if (field == null)
        //{
        //    field = new Cell[this.fieldData.FieldSize, this.fieldData.FieldSize];
        //}
        field = new Cell[this.fieldData.FieldSize, this.fieldData.FieldSize];
        float fieldWidth = this.fieldData.FieldSize * (this.fieldData.CellSize + this.fieldData.CellSpacing) + this.fieldData.CellSpacing;
        rectTransform.sizeDelta = new Vector2(fieldWidth, fieldWidth);

        float startX = -(fieldWidth / 2) + (this.fieldData.CellSize / 2) + this.fieldData.CellSpacing;
        float startY = (fieldWidth / 2) - (this.fieldData.CellSize / 2) - this.fieldData.CellSpacing;

        for (int x = 0; x < this.fieldData.FieldSize; x++)
        {
            for (int y = 0; y < this.fieldData.FieldSize; y++)
            {
                var cell = Instantiate(cellPref, transform, false);
                
                var position = new Vector2(startX + (x * (this.fieldData.CellSize + this.fieldData.CellSpacing)), startY - (y * (this.fieldData.CellSize + this.fieldData.CellSpacing)));
                cell.transform.localPosition = position;

                field[x, y] = cell;
                cell.SetValue(x, y, 0);
            }
        }       
    }

    public void GenerateField()
    {
        
        if (field == null)
            CreateField();
        
        for (int x = 0; x < this.fieldData.FieldSize; x++)
        {
            for (int y = 0; y < this.fieldData.FieldSize; y++)
            {
               field[x, y].SetValue(x, y, 0);
               field[x, y].SetValueGold(x, y, 10);
            }
        }
        for (int i = 0; i < this.fieldData.InitCellsCount; i++)
        {
            GenerateRandomCellGold();
        }
    }
    
    private void GenerateRandomCellGold()
    {
        //var emptyCells = new List<Cell>();

        //for (int x = 0; x < this.fieldData.FieldSize; x++)
        //{
        //    for (int y = 0; y < this.fieldData.FieldSize; y++)
        //    {

        //        if (field[x, y].IsEmpty)
        //        {
        //            emptyCells.Add(field[x,y]);
        //        }
        //    }
        //}

        //if (emptyCells.Count == 0)
        //    throw new System.Exception("Здесь нет пустых cell!");

        int value = Random.Range(1,4);
        var cell = field[Random.Range(0, 10), Random.Range(0, 10)];
        cell.SetValueGold(cell.X, cell.Y, value);
    }
}
