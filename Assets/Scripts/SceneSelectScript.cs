using UnityEngine;

public class SceneSelectScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Scene cards;
    [SerializeField] Scene clicker;
    [SerializeField] SceneController sceneController;

    public void UnPause()
    {
        gameObject.SetActive(false);
    }

    public async void LoadCards()
    {
        await sceneController.ChangeScene(cards);
    }

    public async void LoadClicker()
    {
        await sceneController.ChangeScene(clicker);
    }
}
