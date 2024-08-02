using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Lixo_script : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public TipoDeLixo tipoDeLixo;

    bool isFalling = true;

    public float fallSpeed;

    [SerializeField]
    Camera cam;

    private void Awake()
    {
        cam = FindAnyObjectByType<Camera>();
    }

    private void Update()
    {
        if (isFalling)
        {
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }
    }

    public void Fall()
    {
        isFalling = true;
    }

    public void StopFall()
    {
        isFalling = false;
    }

    public void Respawn()
    {

        float xDistance = transform.position.x - col_1.transform.position.x;

        float x2Distance = transform.position.x - col_2.transform.position.x;

        x2Distance = Mathf.Abs(x2Distance);
        xDistance = Mathf.Abs(xDistance);

        print($"xDistance {xDistance}");
        print($"x2Distance {x2Distance}");
       
        if(x2Distance > xDistance)
        {
            if(col_1.GetLixeiraType().ToString() == tipoDeLixo.ToString())
            {
                GameManager.Instance.ChangePoint(10);
            }

        }else if (x2Distance < xDistance)
        {
            if(col_2.GetLixeiraType().ToString() == tipoDeLixo.ToString())
            {
                GameManager.Instance.ChangePoint(10);
            }
        }
        else if(x2Distance==xDistance)
        {
            if (col_2.GetLixeiraType().ToString() == tipoDeLixo.ToString())
            {
                GameManager.Instance.ChangePoint(10);
            }
        }

        transform.position = new Vector2(Random.Range(-2.3f, 2.3f), 6);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 _movePos = cam.ScreenToWorldPoint(eventData.position);

        _movePos.z = 0;

        transform.position = _movePos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StopFall();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Fall();
    }


    [SerializeField] Lixeira_Script col_1;
    [SerializeField] Lixeira_Script col_2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lixeira"))
        {
            Lixeira_Script _lixeira;
            _lixeira = collision.GetComponentInParent<Lixeira_Script>();

            if (_lixeira != null)
            {
                if (col_1 == null)
                {
                    col_1 = _lixeira;
                    return;
                }
                else
                {
                    if (col_1 != _lixeira)
                    {
                        col_1 = _lixeira;
                        return;

                    }
                }

                if (col_2 == null)
                {
                    col_2 = _lixeira;
                    return;
                }
                else
                {
                    if (col_2 != _lixeira)
                    {
                        col_2 = _lixeira;

                    }

                }
            }
        }
        if (collision.CompareTag("EndLine"))
        {
            Respawn();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Lixeira"))
        {
            Lixeira_Script _lixeira;
            _lixeira = collision.GetComponentInParent<Lixeira_Script>();

            if (_lixeira != null)
            {
                if (col_1 == null)
                {
                    col_1 = _lixeira;
                    return;
                }
                else
                {
                    if (col_1 != _lixeira)
                    {
                        col_1 = _lixeira;
                        return;

                    }
                }

                if (col_2 == null)
                {                    
                    col_2 = _lixeira;
                    return;
                }
                else
                {
                    if (col_2 != _lixeira)
                    {
                        col_2 = _lixeira;
                        
                    }

                }
            }
        }
    }

    [ContextMenu("Turn Null")]
    public void TestEquality()
    {
        if (col_1.GetLixeiraType().ToString() == tipoDeLixo.ToString())
        {
            print("it is");
            GameManager.Instance.ChangePoint(10);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lixeira"))
        {
           
           
           if (col_1.name == collision.transform.parent.name)
           {
                col_1 = null;

            }
                
            if (col_2.name == collision.transform.parent.name)
                {
                    col_2 = null;
                }
            }
        }
    }


public enum TipoDeLixo
{
    Metal, Vidro, Papel, Plastico, Organico
}
