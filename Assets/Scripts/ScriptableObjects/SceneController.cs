using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Scene{
    Cards,
    Clicker
}

[CreateAssetMenu(fileName = "SceneManager", menuName = "Scriptable Objects/SceneManager")]
public class SceneController : ScriptableObject
{
    
        public async UniTask ChangeScene(Scene newScene){
            DOTween.KillAll();

            await OverlayManager.instance.DisplayOverlay();
            SaveDataManager.instance.SaveGame();

            
            switch (newScene){
                case Scene.Cards:
                   await SceneManager.LoadSceneAsync("Cards");
                break;

                case Scene.Clicker:
                    await SceneManager.LoadSceneAsync("Clicker");
                break;
            }
        
            await OverlayManager.instance.HideOverlay();
            SaveDataManager.instance.OnSceneLoad();
        }
}
