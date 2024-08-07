using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text points_txt;
    public int points;
    public int max_Points;

    public static GameManager Instance;


    [SerializeField] Spawn_Lixo spawn_Lixo;

    [SerializeField] int level;
    
    public float LixoFallSpeed()
    {
        if (points > 50 && points < 100)
        {
            return 1.5f;
        }

        if (points > 100 && points < 150)
        {
            return 2f;
        }

        if (points > 150 && points < 200)
        {
            return 2.5f;
        }

        if (points >= 200)
        {
            return 3;
        }

        return 1;
    }

    public void RestarGame()
    {
        SceneManager.LoadScene(0);        
    }

    private void Awake()
    {
        Instance = this;

        max_Points = PlayerPrefs.GetInt("max_poins");
    }

    public void ChangePoint(int _points)
    {
        points += _points;
        points_txt.text = $"Pontos {points}";

        ChangeSpawnIntervalo();
    }

    public void ChangeSpawnIntervalo()
    {
        if (points < 50)
        {
            if (level != 1)
            {
                spawn_Lixo.intervalo = 3;
                foreach(Lixo_script _lixo in spawn_Lixo.pooledObjects)
                {
                    _lixo.fallSpeed = LixoFallSpeed();
                }
                level = 1;
            }

            return;

        }

        if (points > 50 && points < 100)
        {
            if (level != 2)
            {
                spawn_Lixo.intervalo = 2;
                foreach (Lixo_script _lixo in spawn_Lixo.pooledObjects)
                {
                    _lixo.fallSpeed = LixoFallSpeed();
                }
                level = 2;

            }

            return;
        }

        if (points > 100 && points < 175)
        {
            if (level != 3)
            {
                spawn_Lixo.intervalo = 1.5f;
                foreach (Lixo_script _lixo in spawn_Lixo.pooledObjects)
                {
                    _lixo.fallSpeed = LixoFallSpeed();
                }
                level = 3;

            }

            return;
        }

        if (points > 175)
        {
            if (level != 4)
            {
                spawn_Lixo.intervalo = 1f;
                foreach (Lixo_script _lixo in spawn_Lixo.pooledObjects)
                {
                    _lixo.fallSpeed = LixoFallSpeed();
                }
                level = 4;

            }

            return;
        }

    }

    [SerializeField] GameObject gameover_menu;

    public void GameOver()
    {
        gameover_menu.SetActive(true);

        if (points > max_Points)
        {
            max_Points = points;
            PlayerPrefs.SetInt("max_poins", max_Points);
        }

        spawn_Lixo.spawning = false;


    }

}
