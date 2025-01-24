using System;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class CardScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{


    [SerializeField] private GameObject cardFace;
    private bool dragable = false;
    private bool dragging = false;
    private Vector3 offset;
    private Vector3 startPosition;
    private SortingGroup sortingGroup;
    private int startOrder;

    private List<Collider2D> colliders;
    private List<Collider2D> startColiders;
    private int power;

    private bool originDeck = false;

    private bool firstRow = false;

    private int id;

    void Awake()
    {
        sortingGroup = GetComponent<SortingGroup>();
        startPosition = transform.position;
        if (gameObject.GetComponent<Collider2D>() != null)
        {
            startColiders = new List<Collider2D>();
            Physics2D.OverlapCollider(gameObject.GetComponent<Collider2D>(), startColiders);
        }
    }

    void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }


    }

    public bool GetOrigin()
    {
        return originDeck;
    }

    public void SetOrigin(bool origin)
    {
        originDeck = origin;
    }
    public void SetId(int cardId)
    {
        id = cardId;
    }

    public int GetId()
    {
        return id;
    }

    public void SetPosition(Vector3 position)
    {
        gameObject.transform.position = position;
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public void SetCardFace(Sprite sprite)
    {
        SpriteRenderer render = cardFace.GetComponent<SpriteRenderer>();
        render.sprite = sprite;
    }

    public void Flip(float delay)
    {
        gameObject.transform.DORotate(new Vector3(0, 0, 0), 0.25f).SetDelay(delay);
    }

    public void FlipBack(float delay)
    {
        gameObject.transform.DORotate(new Vector3(0, 180, 0), 0.25f).SetDelay(delay);
    }

    public void SetDragable(bool value)
    {
        dragable = value;
    }

    public bool GetDragable()
    {
        return dragable;
    }
    public Tween Snap(Vector3 position, float time)
    {
        return transform.DOMove(position, time);

    }

    public int GetPower()
    {
        return power;
    }

    public void SetPower(int num)
    {
        power = num;
    }

    public Tween SnapBack()
    {
        SetSortingOrder(startOrder);
        Vector3 delta = Camera.main.ScreenToWorldPoint(Input.mousePosition) - startPosition;
        float displacement = (float)Math.Sqrt(Math.Pow(Math.Abs(delta.x), 2) + Math.Pow(Math.Abs(delta.y), 2));
        return Snap(startPosition, 0.04f * displacement);
    }

    public void AddCollider()
    {
        gameObject.AddComponent<BoxCollider2D>();
        BoxCollider2D collider2D = gameObject.GetComponent<BoxCollider2D>();
        collider2D.size = new Vector2(1.295668f, 1.850046f);
    }

    public void RemoveCollider()
    {
        Destroy(gameObject.GetComponent<Collider2D>());
    }

    private void InteractWithWaste(Collider2D collider, WasteScript wasteScript)
    {
        Snap(collider.transform.position, 0.15f);
        SetSortingOrder(wasteScript.GetLastCardOrder());
        wasteScript.IncrementLastCardOrder();
        wasteScript.SetCurrentPower(GetPower());
        wasteScript.Score(this);
    }

    public void SetSortingOrder(int order)
    {
        sortingGroup.sortingOrder = order;
    }

    public int GetSortingOrder()
    {
        return sortingGroup.sortingOrder;
    }

    public int GetStartSortingOrder()
    {
        return startOrder;
    }

    public void CheckStartColliders()
    {
        if (gameObject.GetComponent<Collider2D>() != null)
        {
            colliders = new List<Collider2D>();
            Physics2D.OverlapCollider(gameObject.GetComponent<Collider2D>(), colliders);
            if (!firstRow)
            {
                if (Math.Abs(startColiders.Count - colliders.Count) >= 2)
                {
                    Flip(0f);
                    SetDragable(true);
                }
                else
                {
                    FlipBack(0f);
                    SetDragable(false);
                }

            }
        }
    }

    public void SetFirsRow(bool value)
    {
        firstRow = value;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (dragable)
        {

            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragging = true;
            startOrder = sortingGroup.sortingOrder;
            SetSortingOrder(99);
            DOTween.Kill(gameObject.transform);

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (dragging)
        {
            dragging = false;

            colliders = new List<Collider2D>();
            Physics2D.OverlapCollider(gameObject.GetComponent<Collider2D>(), colliders);

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag == "Waste")
                {
                    WasteScript wasteScript = collider.GetComponent<WasteScript>();
                    if (wasteScript.ValidatePower(GetPower()))
                    {
                        RemoveCollider();
                        InteractWithWaste(collider, wasteScript);
                        return;
                    }

                }
            }
            SnapBack();
        }
    }


}






