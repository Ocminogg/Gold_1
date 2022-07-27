using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameCounter_Preference", menuName = "Gameplay/ New GameCounter")]
public class Script_GameCounter : ScriptableObject
{
    [Header("Кол-во лопат и необходимое кол-во золота для победы")]
    public int Shovel;
    public int Golds;
}
