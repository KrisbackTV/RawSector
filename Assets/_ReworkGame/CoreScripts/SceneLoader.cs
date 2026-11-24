using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public event Action<string, GameObject> OnLocationLoaded;
    public event Action<string> OnLocationUnloaded;

    public string CurrentLocationName { get; private set; }
    public GameObject CurrentLocationRoot { get; private set; }

    private bool _isLoading = false;

    /// <summary>
    /// Загружает локацию аддитивно. Если уже что-то загружено — выгружает.
    /// </summary>
    /// <param name="sceneName">Имя сцены (должна быть в Build Settings)</param>
    public async Awaitable LoadLocation(string sceneName)
    {
        if (_isLoading)
        {
            GameLog.LogWarning($"Сцена уже загружается, подождите");
            return;
        }

        _isLoading = true;

        try
        {
            if (!string.IsNullOrEmpty(CurrentLocationName))
            {
                GameLog.LogInfo($"[SceneLoader] Выгружаем старую локацию: {CurrentLocationName}");

                Scene oldScene = SceneManager.GetSceneByName(CurrentLocationName);
                if (oldScene.isLoaded)
                {
                    await SceneManager.UnloadSceneAsync(oldScene);
                    OnLocationUnloaded?.Invoke(CurrentLocationName);
                }

                CurrentLocationName = null;
                CurrentLocationRoot = null;
            }

            GameLog.LogInfo($"[SceneLoader] Загружаем локацию: {sceneName}");
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            await Awaitable.NextFrameAsync();

            CurrentLocationName = sceneName;


            GameObject locationRoot = GameObject.Find("CoreLocation");
            CurrentLocationRoot = locationRoot;

            if (locationRoot != null)
            {
                GameLog.LogInfo($"Игровой объект {locationRoot.name} успешно найден.");
            }
            else
            {
                GameLog.LogWarning($"Игровой объект {locationRoot.name} НЕ найден.");
            }

            GameLog.LogInfo($"[SceneLoader] Локация {sceneName} успешно загружена.");
            OnLocationLoaded?.Invoke(sceneName, CurrentLocationRoot);
        }
        finally
        {
            _isLoading = false;
        }
    }


    /// <summary>
    /// Выгружает текущую локацию (если она есть) и возвращает в "пустое" состояние.
    /// </summary>
    public async Awaitable UnloadCurrentLocationAsync()
    {
        if (string.IsNullOrEmpty(CurrentLocationName)) return;

        Scene oldScene = SceneManager.GetSceneByName(CurrentLocationName);
        if (oldScene.isLoaded)
        {
            await SceneManager.UnloadSceneAsync(oldScene);
            OnLocationUnloaded?.Invoke(CurrentLocationName);
        }

        CurrentLocationName = null;
        CurrentLocationRoot = null;
    }
}