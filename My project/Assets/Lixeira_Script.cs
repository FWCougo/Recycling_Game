using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Lixeira_Script : MonoBehaviour
{
    [SerializeField] TipoDeLixo lixeiraTipo;
    [SerializeField] BoxCollider2D boxCollider;

    public BoxCollider2D Collider()
    {
        return boxCollider;
    }

    public TipoDeLixo GetLixeiraType()
    {
        return lixeiraTipo;
    }    
}




