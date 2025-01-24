using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using DG.Tweening;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;


public class CardGameScript : MonoBehaviour, IDataPersistance
{
    [SerializeField] private List<GameObject> peaks;

    private GameObject cardBlueprint;
    private CardHolder holder;

    [SerializeField] private Transform deckTransform;
    [SerializeField] private DeckScript deckScript;
    [SerializeField] private WasteScript wasteScript;
    private List<CardScript> cardsTotal;

    private List<CardScript> history;

    [SerializeField] TextMeshProUGUI turnsText;

    private int turns;

    void Start()
    {

    }

    public void AddTurn()
    {
        turns += 1;
        turnsText.text = turns.ToString();
    }

    public void Score()
    {
        AddTurn();
        foreach (CardScript card in cardsTotal)
        {
            card.CheckStartColliders();
        }
    }

    public void AddCardToHistory(CardScript card)
    {
        history.Add(card);
    }

    public void Undo()
    {
        if (history.Count > 0)
        {
            AddTurn();
            CardScript lastCard = history.Last();
            lastCard.SnapBack().OnComplete(() =>
            {

                if (lastCard.GetOrigin())
                {
                    lastCard.FlipBack(0);
                    deckScript.DecrementCardCounter();
                }
                else
                {
                    lastCard.AddCollider();
                    foreach (CardScript card in cardsTotal)
                    {
                        card.CheckStartColliders();
                    }
                }
            });



            history.Remove(lastCard);
            if (history.Count > 0)
            {
                wasteScript.SetCurrentPower(history.Last().GetPower());
            }
            else
            {
                wasteScript.SetCurrentPower(0);
            }
        }
    }

    public async UniTask LoadData(SaveData saveData)
    {
        UniTask<CardHolder> holderLoader = Addressables.LoadAssetAsync<CardHolder>("Assets/Scripts/ScriptableObjects/Instances/CardHolder.asset").Task.AsUniTask();
        UniTask<GameObject> cardLoader = Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/Cards/CardDeck.prefab").Task.AsUniTask();
        cardBlueprint = await cardLoader;
        holder = await holderLoader;

        history = new List<CardScript>();
        List<RowScript> firstRows = new List<RowScript>();
        cardsTotal = new List<CardScript>();

        if (!saveData.SetCards)
        {

            turns = 0;

            foreach (GameObject peak in peaks)
            {

                CardScript[] cards = peak.GetComponentsInChildren<CardScript>();
                cardsTotal.AddRange(cards);
                foreach (CardScript card in cards)
                {
                    int rng = Random.Range(0, 41);
                    Card cardData = holder.GetCardByIndex(rng);
                    card.SetCardFace(cardData.sprite);
                    card.SetPower(cardData.power);
                    card.SetId(rng);
                }

                firstRows.Add(peak.GetComponentInChildren<RowScript>());

            }
            float delay = 0;
            foreach (RowScript row in firstRows)
            {
                row.FlipFirstRow(delay);
                delay += 0.15f;

            }

            for (int i = 0; i < 25; i++)
            {
                CardScript card = Instantiate(cardBlueprint, deckTransform).GetComponent<CardScript>();
                int rng = Random.Range(0, 41);
                Card cardData = holder.GetCardByIndex(rng);
                card.SetCardFace(cardData.sprite);
                card.SetPower(cardData.power);
                card.SetOrigin(true);
                card.SetId(rng);
            }
        }
        else
        {
            turns = saveData.turns;
            turnsText.text = turns.ToString();
            foreach (GameObject peak in peaks)
            {

                CardScript[] cards = peak.GetComponentsInChildren<CardScript>();
                cardsTotal.AddRange(cards);
            }
            for (int i = 0; i < saveData.peakCards.Count(); i++)
            {

                Card cardData = holder.GetCardByIndex(saveData.peakCards[i].id);
                cardsTotal[i].SetCardFace(cardData.sprite);
                cardsTotal[i].SetPower(cardData.power);
                cardsTotal[i].SetId(saveData.peakCards[i].id);
                cardsTotal[i].SetPosition(new Vector3(saveData.peakCards[i].positionX, saveData.peakCards[i].positionY, saveData.peakCards[i].positionZ));
                cardsTotal[i].SetSortingOrder(saveData.peakCards[i].layerOrder);
                if (saveData.peakCards[i].faceUp)
                {
                    cardsTotal[i].Flip(0f);
                    cardsTotal[i].SetDragable(true);
                }
                if (!saveData.peakCards[i].hasCollider)
                {
                    cardsTotal[i].RemoveCollider();
                }
            }

            foreach (GameObject peak in peaks)
            {
                firstRows.Add(peak.GetComponentInChildren<RowScript>());
            }

            foreach (RowScript row in firstRows)
            {
                CardScript[] cards = row.GetComponentsInChildren<CardScript>();
                foreach (CardScript card in cards)
                {
                    card.SetFirsRow(true);
                }

            }



            for (int i = 0; i < 25; i++)
            {
                CardScript card = Instantiate(cardBlueprint, deckTransform).GetComponent<CardScript>();

                Card cardData = holder.GetCardByIndex(saveData.Deck[i].id);
                card.SetCardFace(cardData.sprite);
                card.SetPower(cardData.power);
                card.SetOrigin(true);
                card.SetId(saveData.Deck[i].id);
                card.SetPosition(new Vector3(saveData.Deck[i].positionX, saveData.Deck[i].positionY, saveData.Deck[i].positionZ));
                card.SetSortingOrder(saveData.Deck[i].layerOrder);
                if (saveData.Deck[i].faceUp)
                {
                    card.Flip(0f);
                }
            }
        }
        deckScript.Init();
    }


    public void SaveData(ref SaveData saveData)
    {
        saveData.SetCards = true;
        saveData.turns = turns;
        saveData.peakCards = new List<CardDataSave>();

        foreach (CardScript card in cardsTotal)
        {
            Vector3 position = card.GetPosition();
            saveData.peakCards.Add(new CardDataSave(card.GetId(), position.x, position.y, position.z, card.GetComponent<Collider2D>() != null, card.GetDragable(), card.GetSortingOrder()));
        }

    }
}



