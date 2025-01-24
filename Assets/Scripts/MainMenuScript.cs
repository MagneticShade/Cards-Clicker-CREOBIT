using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject selectSceneMenu;

    public void StartGame()
    {
        selectSceneMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
