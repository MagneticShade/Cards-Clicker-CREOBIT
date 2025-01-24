using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseScript : MonoBehaviour
{

    [SerializeField] Scene targetScene;
    [SerializeField] SceneController sceneController;
    public void Pause(){
        gameObject.SetActive(true);
        Time.timeScale=0;
    }

    public async  void ChangeScene(){
        await sceneController.ChangeScene(targetScene);
        Time.timeScale=1;
    }

    public void UnPause(){
        gameObject.SetActive(false);
        Time.timeScale=1;
    }



}
