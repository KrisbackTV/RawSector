using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class SceneBootstrapper
{
    // 1. УКАЖИ ТУТ ПУТЬ К СВОЕЙ CORE СЦЕНЕ
    private const string GlobalStartScenePath = "Assets/Scenes/GameScene/Core.unity";

    static SceneBootstrapper()
    {
        // Этот метод вызывается сразу при загрузке редактора
        ApplyStartScene();
    }

    // Добавляем кнопку в верхнее меню, чтобы можно было вкл/выкл
    [MenuItem("Tools/Scene Autoloader/Enable Auto-Boot")]
    public static void ApplyStartScene()
    {
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(GlobalStartScenePath);
        if (sceneAsset != null)
        {
            EditorSceneManager.playModeStartScene = sceneAsset;
            Debug.Log($"<color=green>✔ Bootstrapper:</color> Теперь игра всегда стартует с <b>{sceneAsset.name}</b>");
        }
        else
        {
            Debug.LogError($"<color=red>✘ Bootstrapper:</color> Не нашел сцену по пути {GlobalStartScenePath}");
        }
    }

    [MenuItem("Tools/Scene Autoloader/Disable Auto-Boot")]
    public static void DisableStartScene()
    {
        EditorSceneManager.playModeStartScene = null;
        Debug.Log("<color=yellow>⚠ Bootstrapper:</color> Автозапуск отключен. Будет запускаться текущая сцена.");
    }
}