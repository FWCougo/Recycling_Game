using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Lixo : MonoBehaviour
{
    public List<Lixo_script> pooledObjects;
    public Lixo_script objectToPool;
    public int amountToPool;

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

    public float intervalo;

    public bool spawning;

    IEnumerator SpawnLixo()
    {
        while (spawning)
        {
            yield return new WaitForSeconds(intervalo);

            print("Spawned");

            Lixo_script _Lixo = GetPooledObject();
            if(_Lixo != null )
            {
                _Lixo.gameObject.SetActive(true);
                _Lixo.Respawn();
            }

            yield return null;
        }

        yield return null;
    }
}
