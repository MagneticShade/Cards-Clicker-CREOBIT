using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainScript : MonoBehaviour,IDataPersistance
{
    [SerializeField] private Transform pickaxe;
    [SerializeField] private float cycle_length;
    [SerializeField] private StaminaScript stamina;
    [SerializeField] private ScoreScript score;

    [SerializeField] private PointScript point;


    private float particleLifeTime;
    private float particleDuration;


    private int oreValue;

    public void Swing(){

        if (stamina.GetReady()){
            stamina.StartRecovery(cycle_length);
            pickaxe.DORotate(new Vector3(0,0,0),cycle_length*0.2f).SetEase(Ease.InCubic).OnComplete(()=>{
                score.AddOre(oreValue);
                pickaxe.DORotate(new Vector3(0,0,-75),cycle_length-cycle_length*0.2f).SetEase(Ease.InCubic);
            });

        }
    }




    public void AddOreValue(int value){
        oreValue+=value;
    }

    public void ChangeCycleLength(float multiplier){
        cycle_length*=multiplier;
        stamina.ChangeGaugeWidth(multiplier);
        particleLifeTime *=multiplier;
        particleDuration *=multiplier;
        point.SetParticleDuration(particleLifeTime,particleDuration);
    }


    public void LoadData(SaveData saveData)
    {
        oreValue = saveData.orePerHit;
        particleLifeTime = saveData.particleLifeTime;
        particleDuration = saveData.particleDuration;
        cycle_length = saveData.animationDuration;
        point.SetParticleDuration(particleLifeTime,particleDuration);
    }

    
    public void SaveData(ref SaveData saveData)
    {
        saveData.orePerHit = oreValue ;
        saveData.particleLifeTime = particleLifeTime;
        saveData.particleDuration = particleDuration;
        saveData.animationDuration = cycle_length;
    }

}
