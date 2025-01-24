using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "CardHolder", menuName = "Scriptable Objects/CardHolder")]
public class CardHolder : ScriptableObject
{
    public List<Card> cards;

    public Card GetCardByIndex(int id)
    {
        return cards[id];
    }
}
