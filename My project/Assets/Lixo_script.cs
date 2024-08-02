using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Lixo_script : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public TipoDeLixo tipoDeLixo;

    bool isFalling = true;

    public float fallSpeed;

    public bool canRespawn = true;

    [SerializeField]
    Camera cam;

    [SerializeField] Collider2D col;

    [SerializeField] SpriteRenderer mainSprite;

    public RetrieveImage retrieveImage;

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
        canRespawn = true;
    }
    public void StopFall()
    {
        isFalling = false;
        canRespawn = false; 
    }


    [ContextMenu("Randomize Type")]
    void ChangeType()
    {
        tipoDeLixo = (TipoDeLixo)Random.Range(0, 5);

        switch (tipoDeLixo)
        {
            case TipoDeLixo.Metal:
                mainSprite.sprite = retrieveImage.metal_img[Random.Range(0, retrieveImage.metal_img.Length)];
                break;

            case TipoDeLixo.Vidro:
                mainSprite.sprite = retrieveImage.vidro_img[Random.Range(0, retrieveImage.vidro_img.Length)];
                break;

            case TipoDeLixo.Papel:
                mainSprite.sprite = retrieveImage.papel_img[Random.Range(0, retrieveImage.papel_img.Length)];
                break;

            case TipoDeLixo.Plastico:
                mainSprite.sprite = retrieveImage.plastico_img[Random.Range(0, retrieveImage.plastico_img.Length)];
                break;

            case TipoDeLixo.Organico:
                mainSprite.sprite = retrieveImage.organico_img[Random.Range(0, retrieveImage.organico_img.Length)];
                break;

        }
    }

    public void Respawn()
    {

        if (!canRespawn) return;

        float xDistance = 10;
        float x2Distance = 10;

        if (lixeira_1 != null)
        {
            xDistance = transform.position.x - lixeira_1.transform.position.x;
            xDistance = Mathf.Abs(xDistance);
            print($"xDistance {xDistance}");

        }

        if (lixeira_2 != null)
        {
            x2Distance = transform.position.x - lixeira_2.transform.position.x;
            x2Distance = Mathf.Abs(x2Distance);
            print($"x2Distance {x2Distance}");

        }             

       
        if(x2Distance > xDistance)
        {
            if (lixeira_1 != null)
            {
                if (lixeira_1.GetLixeiraType().ToString() == tipoDeLixo.ToString())
                {
                    GameManager.Instance.ChangePoint(10);
                }
            }

        }else if (x2Distance < xDistance)
        {
            if (lixeira_2 != null)
            {
                if (lixeira_2.GetLixeiraType().ToString() == tipoDeLixo.ToString())
                {
                    GameManager.Instance.ChangePoint(10);
                }
            }

        }
        else if(x2Distance==xDistance)
        {

            if (lixeira_1 != null)
            {
                if (lixeira_1.GetLixeiraType().ToString() == tipoDeLixo.ToString())
                {
                    GameManager.Instance.ChangePoint(10);
                }
            }
            else if(lixeira_2 != null)
            {
                if (lixeira_2.GetLixeiraType().ToString() == tipoDeLixo.ToString())
                {
                    GameManager.Instance.ChangePoint(10);
                }
            }
            
        }


        ChangeType();

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
        mainSprite.sortingOrder = 20;

        StopFall();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mainSprite.sortingOrder = 0;

        Fall();
    }

    [SerializeField] Lixeira_Script lixeira_1;
    [SerializeField] Lixeira_Script lixeira_2;

    [ContextMenu("Turn Null")]
    public void TestEquality()
    {
        if (lixeira_1.GetLixeiraType().ToString() == tipoDeLixo.ToString())
        {
            print("it is");
            GameManager.Instance.ChangePoint(10);
        }
    }


    [SerializeField] List<Collider2D> _col2D = new();
    [SerializeField] ContactFilter2D contactFilter;

    void ColetarLixeira1()
    {
        foreach (Collider2D col in _col2D)
        {
            if (col.CompareTag("Lixeira"))
            {
                Lixeira_Script _lixeira;
                _lixeira = col.GetComponentInParent<Lixeira_Script>();

                if (_lixeira != null)
                {

                    if (lixeira_1 == _lixeira)
                    {
                        return;
                    }

                    if (lixeira_1 != _lixeira)
                    {
                        if (lixeira_2 != _lixeira)
                        {
                            lixeira_1 = _lixeira;
                            return;

                        }
                    }

                    /*
                    if (lixeira_1 == null)
                    {
                        lixeira_1 = _lixeira;
                        return;
                    }
                    else if (lixeira_1 != _lixeira)
                    {
                        if (lixeira_2 != null || lixeira_2 == _lixeira)
                        {
                            lixeira_1 = _lixeira;
                            return;
                        }

                    }
                    */
                }

            }
        }
    }

    void ColetarLixeira2()
    {

        foreach (Collider2D col in _col2D)
        {
            if (col.CompareTag("Lixeira"))
            {
                Lixeira_Script _lixeira;
                _lixeira = col.GetComponentInParent<Lixeira_Script>();

                if (_lixeira != null)
                {

                    if (lixeira_2 == _lixeira)
                    {
                        return;
                    }

                    if(lixeira_2 != _lixeira)
                    {
                        if(lixeira_1 != _lixeira)
                        {
                            lixeira_2 = _lixeira;
                        }
                    }


                    /*
                    if (lixeira_2 == null)
                    {
                        lixeira_2 = _lixeira;
                        return;
                    }
                    else if (lixeira_2 != _lixeira)
                    {
                        if (lixeira_1 != null || lixeira_1 == _lixeira)
                            lixeira_2 = _lixeira;

                    }
                    */
                }

            }

        }
    }

    private void FixedUpdate()
    {
        Physics2D.OverlapCollider(col, contactFilter, _col2D);

        ColetarLixeira1();
        ColetarLixeira2();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EndLine"))
        {
            Respawn();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("EndLine"))
        {
            Respawn();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lixeira"))
        {           
            if(lixeira_1 != null)
            {
                if (lixeira_1.name == collision.transform.parent.name)
                {
                    lixeira_1 = null;
                }
            }

            if (lixeira_2 != null)
            {
                if (lixeira_2.name == collision.transform.parent.name)
                {
                    lixeira_2 = null;
                }
            }
        }
        }
    }


public enum TipoDeLixo
{
    Metal, Vidro, Papel, Plastico, Organico
}

