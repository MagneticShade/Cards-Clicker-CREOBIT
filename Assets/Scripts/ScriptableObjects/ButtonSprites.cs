using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "ButtonSprites", menuName = "Scriptable Objects/ButtonSprites")]
public class ButtonSprites : ScriptableObject
{
    [SerializeField] private List<Sprite> sprites;


    public Sprite GetSharpnesSprite()
    {
        return sprites[0];
    }

    public Sprite GetStaminaSprite()
    {
        return sprites[1];
    }

}
