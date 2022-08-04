using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[CreateAssetMenu(fileName = "Field_Preference", menuName = "Gameplay/ New Field", order = 51)]
public class Script_Field : ScriptableObject
{
    [Header("�������������� ����")]
    public float CellSize;
    public float CellSpacing;
    public int FieldSize;
    public int InitCellsCount;    
}
