using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StaminaScript : MonoBehaviour,IDataPersistance
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Slider slider;
    private bool ready = true;
    private RectTransform objectTransform;

    void Awake(){
        slider = gameObject.GetComponent<Slider>();
    }
    void Start()
    {
        objectTransform = gameObject.GetComponent< RectTransform >( );
    }

    public bool GetReady(){
        return ready;
    }

    private void IncrementProgress(float recoveryTime){
        DOTween.To(()=>slider.value,(x)=>slider.value=x,1,recoveryTime).SetEase(Ease.Linear).OnComplete(()=>{
            ready = true;
            slider.value = 0;    
        });
    }

    public void ChangeGaugeWidth(float multiplier){
        objectTransform.localScale= new Vector2(objectTransform.localScale.x*multiplier,objectTransform.localScale.y);
    }

    public void StartRecovery(float recoveryTime){
        ready=false;
        IncrementProgress(recoveryTime);
    }

    public float getValue(){
        return slider.value;
    }

    public void LoadData(SaveData saveData)
    {
        ChangeGaugeWidth(saveData.staminaGaugeScale);
    }

    
    public void SaveData(ref SaveData saveData)
    {
        saveData.staminaGaugeScale = objectTransform.localScale.x;
    }
}
