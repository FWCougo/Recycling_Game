using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text points_txt;
    public int points;

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangePoint(int _points)
    {
        points += _points;
        points_txt.text = $"Pontos {points}";
    }

}
