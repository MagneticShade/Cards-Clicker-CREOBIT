using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AddressableAssets;
public enum Scene{
    Cards,
    Clicker,
    MainMenu
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
                   await  Addressables.LoadSceneAsync("Assets/Scenes/Cards.unity");
                break;

                case Scene.Clicker:
                    await Addressables.LoadSceneAsync("Assets/Scenes/Clicker.unity");
                break;

                case Scene.MainMenu:
                    await Addressables.LoadSceneAsync("Assets/Scenes/MainMenu.unity");
                break;
            }
        
            await OverlayManager.instance.HideOverlay();
            SaveDataManager.instance.OnSceneLoad();
        }
}
