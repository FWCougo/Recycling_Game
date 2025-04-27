using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using DG.Tweening;

public class Lixeira_Script : MonoBehaviour
{
    [SerializeField] TipoDeLixo lixeiraTipo;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] Transform lixo_go;
    [SerializeField] Vector2 scale_Change;

    [ContextMenu("Change Scale")]
    public void ChangeScale()
    {
        Vector3 tempScale = lixo_go.localScale;
        lixo_go.DOScale(scale_Change, .5f).OnComplete(()=> lixo_go.DOScale(tempScale, .5f));
    }

    public BoxCollider2D Collider()
    {
        return boxCollider;
    }

    public TipoDeLixo GetLixeiraType()
    {
        return lixeiraTipo;
    }    
}




