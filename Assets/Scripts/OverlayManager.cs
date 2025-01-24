using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class OverlayManager : MonoBehaviour
{
    private const string WrapperCls = "overlay";
    private const string WrapperActiveCls = "overlay--active";
    static public OverlayManager instance;
    [SerializeField] private UIDocument uiDoc;
    private VisualElement rootEl;
    private VisualElement wrapperEl;

    private void OnEnable(){
        rootEl = uiDoc.rootVisualElement;
        wrapperEl = rootEl.Q(className:WrapperCls);
    }

    private void Awake(){
        if (instance ==null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance !=this){
            Destroy(gameObject);
        }
    }

    public async UniTask DisplayOverlay(){
        wrapperEl.AddToClassList(WrapperActiveCls);
        await UniTask.Delay(500,true);
    }

    public async UniTask HideOverlay(){
        
        wrapperEl.RemoveFromClassList(WrapperActiveCls);
        await UniTask.Delay(500,true);
    }
}
