using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WasteScript : MonoBehaviour,IDataPersistance
{
    private int currentPower;
    private int lastCardOrder=1;
    [SerializeField] CardGameScript cardGameScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetCurrentPower(int power){
        currentPower = power;
    }
    public int GetCurrentPower(){
        return currentPower;

    }

    public int GetLastCardOrder(){
        return lastCardOrder;
    }

    public void IncrementLastCardOrder(){
        lastCardOrder+=1;
    }



    public void CheckGameover(){
        //Remember
    }

    public bool ValidatePower(int cardPower){
        int calc = Math.Abs(cardPower-GetCurrentPower());
        if (GetCurrentPower()!=0){
        if (calc==1||calc==10){
            return true;
        }
        else{
            return false;
        }
    }
    return true;
    }

    public void Score(CardScript card){
            cardGameScript.Score();
            cardGameScript.AddCardToHistory(card);
    }

    public void LoadData(SaveData saveData)
    {
        currentPower = saveData.currentPowerWaste;
        lastCardOrder = saveData.lastCardOrder;
    }

    
    public void SaveData(ref SaveData saveData)
    {
        saveData.currentPowerWaste=currentPower;
        saveData.lastCardOrder = lastCardOrder;
    }
}
