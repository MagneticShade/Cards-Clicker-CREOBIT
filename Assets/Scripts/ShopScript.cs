using UnityEngine;

public class ShopScript : MonoBehaviour
{

    [SerializeField] private MainScript main;
    [SerializeField] private ShopContentScript content;
    [SerializeField] private BackdropScript backdrop;
    

    public void OpenShop(){
        gameObject.SetActive(true);
        content.CheckButtonAvailability();
        backdrop.Open();
    }

    public void CloseShop(){
        gameObject.SetActive(false);

    }


    public void SharpnesUpgrade(){
        main.AddOreValue(1);
    }

    public void StaminaUpgrade(){
        main.ChangeCycleLength(0.7f);
    }


}
