using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameCounter_Preference", menuName = "Gameplay/ New GameCounter")]
public class Script_GameCounter : ScriptableObject
{
    [Header("���-�� ����� � ����������� ���-�� ������ ��� ������")]
    public int Shovel;
    public int Golds;
}
