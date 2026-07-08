using Unity.Jobs;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private const string MENU_SCENE = "Menu";
    private const string GAMEPLAY_SCENE = "GamePlay";
    private string _currentMapName;

    public void LoadScene(string mapName)
    {
        Scene menuScene = SceneManager.GetSceneByName(MENU_SCENE);
        Scene gameplayScene = SceneManager.GetSceneByName(GAMEPLAY_SCENE);

        if (menuScene.isLoaded)
            SceneManager.UnloadSceneAsync(MENU_SCENE);

        if (!gameplayScene.isLoaded)
            SceneManager.LoadSceneAsync(GAMEPLAY_SCENE, LoadSceneMode.Additive);

        if (_currentMapName != null)
        {
            Scene mapScene = SceneManager.GetSceneByName(_currentMapName);

            if (mapScene.isLoaded)
                SceneManager.UnloadSceneAsync(_currentMapName);
        }

            SceneManager.LoadSceneAsync(mapName, LoadSceneMode.Additive);
            _currentMapName = mapName;
    }

    public void BackToMenu()
    {
        Scene menuScene = SceneManager.GetSceneByName(MENU_SCENE);
        Scene gameplayScene = SceneManager.GetSceneByName(GAMEPLAY_SCENE);
        Scene mapScene = SceneManager.GetSceneByName(_currentMapName);
        
        if(_currentMapName != null)
            if(mapScene.isLoaded)
                SceneManager.UnloadSceneAsync(_currentMapName);

        if (gameplayScene.isLoaded)
            SceneManager.UnloadSceneAsync(GAMEPLAY_SCENE);

        if (!menuScene.isLoaded)
            SceneManager.LoadSceneAsync(MENU_SCENE, LoadSceneMode.Additive);
    }

    public void StartGame()
    {
        Scene menuScene = SceneManager.GetSceneByName(MENU_SCENE);

        if(!menuScene.isLoaded)
            SceneManager.LoadSceneAsync(MENU_SCENE, LoadSceneMode.Additive);
    }
}
