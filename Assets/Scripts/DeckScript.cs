using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;

public class DeckScript : MonoBehaviour, IPointerDownHandler, IDataPersistance
{
    [SerializeField] Transform wasteTrasform;
    [SerializeField] WasteScript wasteScript;
    [SerializeField] CardGameScript cardGameScript;
    private CardScript[] cardScripts;
    private int cardCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Init()
    {
        cardScripts = gameObject.GetComponentsInChildren<CardScript>();
    }


    public int GetCardCounter()
    {
        return cardCounter;
    }

    public void DecrementCardCounter()
    {
        cardCounter -= 1;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (cardCounter < 25)
        {
            cardScripts[cardCounter].Snap(wasteTrasform.position, 0.1f);
            cardScripts[cardCounter].Flip(0);
            cardScripts[cardCounter].SetSortingOrder(wasteScript.GetLastCardOrder());
            wasteScript.SetCurrentPower(cardScripts[cardCounter].GetPower());
            wasteScript.IncrementLastCardOrder();
            cardGameScript.AddTurn();
            cardGameScript.AddCardToHistory(cardScripts[cardCounter]);
            cardCounter += 1;


        }
    }

    public async UniTask LoadData(SaveData saveData)
    {
        cardCounter = saveData.cardsInDeck;
    }


    public void SaveData(ref SaveData saveData)
    {
        saveData.cardsInDeck = cardCounter;
        saveData.Deck = new List<CardDataSave>();

        foreach (CardScript card in cardScripts)
        {
            Vector3 position = card.GetPosition();
            saveData.Deck.Add(new CardDataSave(card.GetId(), position.x, position.y, position.z, card.GetComponent<Collider2D>() != null, position != gameObject.transform.position, card.GetSortingOrder()));
        }

    }
}

