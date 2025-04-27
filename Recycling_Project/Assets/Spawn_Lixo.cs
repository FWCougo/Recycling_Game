using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Lixo : MonoBehaviour
{
    public List<Lixo_script> pooledObjects;
    public Lixo_script objectToPool;
    public int amountToPool;
    public float intervalo;

    public bool spawning;

    void Start()
    {
        pooledObjects = new List<Lixo_script>();
        Lixo_script tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.gameObject.SetActive(false);
            pooledObjects.Add(tmp);
        }

        
    }

    public void Iniciar()
    {
        spawning = true;
        StartCoroutine(SpawnLixo());
    }

    public Lixo_script GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }


    IEnumerator SpawnLixo()
    {
        while (spawning)
        {
            Lixo_script _Lixo = GetPooledObject();
            if(_Lixo != null )
            {
                _Lixo.gameObject.SetActive(true);
                _Lixo.Respawn();
            }

            yield return new WaitForSeconds(intervalo);

            yield return null;
        }

        yield return null;
    }
}
