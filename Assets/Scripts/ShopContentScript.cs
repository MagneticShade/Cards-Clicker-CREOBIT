using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopContentScript : MonoBehaviour,IDataPersistance
{
    private Dictionary<string,ShopButtonScript> buttons;

    [SerializeField] private GameObject prefab;
    [SerializeField] private ScoreScript score;
    [SerializeField] private ShopScript shop;
    [SerializeField] private ButtonSprites sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void CheckButtonAvailability(){
      List <string> buttonsToDelete=new List<string>();
        foreach (var(key,value) in buttons){
            value.CheckGold(score.GetCoins());
            if (value.GetLevel()==value.GetMaxLevel()){
                Destroy(value.gameObject);
                buttonsToDelete.Add(key);
            }
        }
        foreach (string elem in buttonsToDelete){
            buttons.Remove(elem);
        }
    }
    public void BuyWithCoins (int price){
        score.DecreaseCoins(price);
        CheckButtonAvailability();
    }


    public void LoadData(SaveData saveData)
    {
        buttons=new Dictionary<string,ShopButtonScript>();

        if(saveData.sharpnessCurrentLevel!=saveData.sharpnessMaxLevel){
            ShopButtonScript sharpness = Instantiate(prefab,gameObject.transform).GetComponent<ShopButtonScript>();
            sharpness.Initialise((price)=>{shop.SharpnesUpgrade(); CheckButtonAvailability(); BuyWithCoins(price);},15,"Sharpnes +1",saveData.sharpnessMaxLevel,saveData.sharpnessCurrentLevel,sprites.GetSharpnesSprite());
            buttons.Add("sharpness",sharpness);
        }
        if(saveData.staminaCurrentLevel!=saveData.staminaMaxLevel){
            ShopButtonScript stamina = Instantiate(prefab,gameObject.transform).GetComponent<ShopButtonScript>();
            stamina.Initialise((price)=>{shop.StaminaUpgrade(); CheckButtonAvailability(); BuyWithCoins(price);},25,"Stamina",saveData.staminaMaxLevel,saveData.staminaCurrentLevel,sprites.GetStaminaSprite());
            buttons.Add("stamina",stamina);
        }
    }

    
    public void SaveData(ref SaveData saveData)
    {
        ShopButtonScript button;
        if(buttons.TryGetValue("sharpness",out button)){
            saveData.sharpnessCurrentLevel = button.GetLevel();
            saveData.sharpnessMaxLevel = button.GetMaxLevel();
        }

        if(buttons.TryGetValue("stamina",out button)){
            saveData.staminaCurrentLevel = button.GetLevel();
            saveData.staminaMaxLevel = button.GetMaxLevel();
        }
    }

    
}
