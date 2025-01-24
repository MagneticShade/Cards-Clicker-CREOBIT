using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class ScoreScript : MonoBehaviour,IDataPersistance
{
    [SerializeField] private TextMeshProUGUI oreText;
    [SerializeField] private TextMeshProUGUI goldText;

    private int ore;
    private int gold;
    private void UpdateGold(){
        goldText.text = Convert.ToString(gold);
    }

    private void UpdateOre(){
        oreText.text = Convert.ToString(ore);
    }

    public int GetCoins(){
        return gold;
    }

    public void AddOre(int ammount){
        ore+=ammount;
        UpdateOre();
        
    }

    public void ExchangeOre(){
        if (ore!=0){
            gold = ore*2 +gold;
            ore = 0;
            UpdateGold();
            UpdateOre();
        }
    }

    public void DecreaseCoins(int value){
        gold -=value;
        UpdateGold();
    }

    public void LoadData(SaveData saveData)
    {
        ore = saveData.ore;
        gold = saveData.gold;
        UpdateOre();
        UpdateGold();
    }

    
    public void SaveData(ref SaveData saveData)
    {
        saveData.ore = ore;
        saveData.gold=gold; 
    }
}
