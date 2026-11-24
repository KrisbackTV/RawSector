using UnityEngine;

public enum MenuWindow
{
    MainMenu,
    SaveLoadMenu
}

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _gMainMenu;
    [SerializeField] private GameObject _gSaveLoadMenu;
    private IWindow _mainMenu;
    private IWindow _saveLoadMenu;


    private void Start()
    {
        _mainMenu = _gMainMenu.GetComponent<IWindow>();
        _saveLoadMenu = _gSaveLoadMenu.GetComponent<IWindow>();

        if (_mainMenu == null || _saveLoadMenu == null)
        {

        }

        _mainMenu.Close();
        _saveLoadMenu.Close();

        OpenWindowForType(MenuWindow.MainMenu);
    }

    private void OpenWindowForType(MenuWindow menuWindow)
    {
        _mainMenu.Close();
        _saveLoadMenu.Close();

        switch (menuWindow)
        {
            case MenuWindow.MainMenu:
                _mainMenu.Open();
                break;
            case MenuWindow.SaveLoadMenu:
                _saveLoadMenu.Open();
                break;
        }
    }

    private void UnSubscribe()
    {
    }
    private void OnDestroy() => UnSubscribe();
}
