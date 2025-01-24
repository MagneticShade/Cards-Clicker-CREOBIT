using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class TimerScript : MonoBehaviour,IDataPersistance
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TextMeshProUGUI text;
    private float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime+= Time.deltaTime;
        
        int minutes = (int)Math.Floor(elapsedTime/60);
        int seconds = (int) Math.Floor(elapsedTime%60);
        text.text = string.Format("{1:00}:{0:00}",seconds,minutes);
    }

    public void LoadData(SaveData saveData)
    {
        elapsedTime = saveData.time;
    }

    
    public void SaveData(ref SaveData saveData)
    {
        saveData.time = elapsedTime;
    }

}
