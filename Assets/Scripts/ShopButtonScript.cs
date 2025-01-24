
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButtonScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI upgradeName;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button button;

    [SerializeField] private Image logo;


    private int price;
    private Action<int> action;
    private int level;
    private int maxLevel;

    public void Initialise(Action<int> function,int coins,string name,int limitLevel, int startlevel,Sprite image){
        action = function;
        upgradeName.text = name;
        maxLevel = limitLevel;
        logo.sprite = image;
        levelText.text = Convert.ToString(startlevel);
        level = startlevel;
        price=coins*(int)Math.Pow(4,level);
        priceText.text = Convert.ToString(price);
        
    }

    public void CheckGold(int gold){
        if (gold >=price){
        
            button.interactable = true;
        }
        else{
            button.interactable = false;
            
        }
    }

    public void Use(){
        action(price);
        price=price*4;
        priceText.text = Convert.ToString(price);
        level+=1;
        levelText.text = Convert.ToString(level);
    }

    public int GetLevel(){
        return level;
    }

    public int GetMaxLevel(){
        return maxLevel;
    }

    public int GetPrice(){
        return price;
    }


}
