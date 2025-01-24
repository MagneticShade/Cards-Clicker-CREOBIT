using UnityEngine;
using UnityEngine.EventSystems;

public class BackdropScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] ShopScript shopScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnPointerClick(PointerEventData eventData)
    {
        shopScript.CloseShop();
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
}
