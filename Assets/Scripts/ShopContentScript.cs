using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
public class ShopContentScript : MonoBehaviour, IDataPersistance
{
    private Dictionary<string, ShopButtonScript> buttons;

    private GameObject prefab;
    [SerializeField] private ScoreScript score;
    [SerializeField] private ShopScript shop;
    private ButtonSprites sprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    public void CheckButtonAvailability()
    {
        List<string> buttonsToDelete = new List<string>();
        foreach (var (key, value) in buttons)
        {
            value.CheckGold(score.GetCoins());
            if (value.GetLevel() == value.GetMaxLevel())
            {
                Destroy(value.gameObject);
                buttonsToDelete.Add(key);
            }
        }
        foreach (string elem in buttonsToDelete)
        {
            buttons.Remove(elem);
        }
    }
    public void BuyWithCoins(int price)
    {
        score.DecreaseCoins(price);
        CheckButtonAvailability();
    }


    public async UniTask LoadData(SaveData saveData)
    {
        UniTask<GameObject> itemInTheShopLoader = Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/ItemInTheShop.prefab").Task.AsUniTask();
        UniTask<ButtonSprites> buttonSpritesLoader = Addressables.LoadAssetAsync<ButtonSprites>("Assets/Scripts/ScriptableObjects/Instances/ButtonSprites.asset").Task.AsUniTask();
        prefab = await itemInTheShopLoader;


        sprites = await buttonSpritesLoader;

        buttons = new Dictionary<string, ShopButtonScript>();

        if (saveData.sharpnessCurrentLevel != saveData.sharpnessMaxLevel)
        {
            ShopButtonScript sharpness = Instantiate(prefab, gameObject.transform).GetComponent<ShopButtonScript>();
            sharpness.Initialise((price) => { shop.SharpnesUpgrade(); CheckButtonAvailability(); BuyWithCoins(price); }, 15, "Sharpnes +1", saveData.sharpnessMaxLevel, saveData.sharpnessCurrentLevel, sprites.GetSharpnesSprite());
            buttons.Add("sharpness", sharpness);
        }
        if (saveData.staminaCurrentLevel != saveData.staminaMaxLevel)
        {
            ShopButtonScript stamina = Instantiate(prefab, gameObject.transform).GetComponent<ShopButtonScript>();
            stamina.Initialise((price) => { shop.StaminaUpgrade(); CheckButtonAvailability(); BuyWithCoins(price); }, 25, "Stamina", saveData.staminaMaxLevel, saveData.staminaCurrentLevel, sprites.GetStaminaSprite());
            buttons.Add("stamina", stamina);
        }
    }


    public void SaveData(ref SaveData saveData)
    {
        ShopButtonScript button;
        if (buttons.TryGetValue("sharpness", out button))
        {
            saveData.sharpnessCurrentLevel = button.GetLevel();
            saveData.sharpnessMaxLevel = button.GetMaxLevel();
        }

        if (buttons.TryGetValue("stamina", out button))
        {
            saveData.staminaCurrentLevel = button.GetLevel();
            saveData.staminaMaxLevel = button.GetMaxLevel();
        }
    }


}
