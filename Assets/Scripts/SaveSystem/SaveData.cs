using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int ore = 0;
    public int gold = 0;
    public int updrade = 0;
    public int orePerHit = 1;

    public float animationDuration = 2f;
    public float particleDuration = 0.25f;

    public float particleLifeTime = 1.7f;

    public float staminaGaugeScale = 1;

    public int sharpnessMaxLevel = 5;
    public int sharpnessCurrentLevel = 0;
    public int staminaMaxLevel = 4;

    public int staminaCurrentLevel = 0;
    public int cardsInDeck;

    public List<CardDataSave> peakCards;
    public List<CardDataSave> Deck;

    public bool SetCards = false;

    public int turns = 0;

    public int currentPowerWaste;
    public int lastCardOrder;

    public float time = 0;
}

[System.Serializable]
public class CardDataSave
{
    public int id;
    public float positionX;
    public float positionY;
    public float positionZ;
    public bool hasCollider;
    public bool faceUp;
    public int layerOrder;

    public CardDataSave(int cardId, float cardPostionX, float cardPostionY, float cardPostionZ, bool collider, bool dragable, int cardLayerOrder)
    {
        id = cardId;
        positionX = cardPostionX;
        positionY = cardPostionY;
        positionZ = cardPostionZ;
        hasCollider = collider;
        faceUp = dragable;
        layerOrder = cardLayerOrder;
    }
}
