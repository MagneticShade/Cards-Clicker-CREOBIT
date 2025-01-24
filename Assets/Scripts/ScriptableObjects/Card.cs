using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    public Sprite sprite;
    public int power;

    public Sprite GetCardSprite(){
        return sprite;
    }

    public int GetCardPower(){
        return power;
    }
    
}
