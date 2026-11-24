using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class GameLog
{
    // Сюда VContainer подставит наш сервис времени, когда игра запустится
    private static ITimeProvider _timeProvider;

    public static void Initialize(ITimeProvider timeProvider)
    {
        _timeProvider = new WorldTimeTracker();
        LogInfo("GameLog", "Система логирования успешно связана с таймером игры.");
    }


    //Debug - все возможная инфа. Игрок двинулс на 0.1мм, выкинут предмет и т.п.
    [Conditional("DEVELOPMENT_BUILD"), Conditional("UNITY_EDITOR")]
    public static void LogDebug(string message, [CallerFilePath] string scriptPath = "")
    {
        string scriptName = GetScriptName(scriptPath);
        string timeTag = GetTimeTag();
        UnityEngine.Debug.Log($"<color=#808080>[{timeTag}]</color> <color=#ADD8E6><b>[DEBUG]</b></color> <b>[{scriptName}]</b>: {message}");
    }


    //Info - базовая информация. инициализация игры, игрока, спавн мобов и т.д.
    public static void LogInfo(string message, [CallerFilePath] string scriptPath = "")
    {
        string scriptName = GetScriptName(scriptPath);
        string timeTag = GetTimeTag();
        UnityEngine.Debug.Log($"<color=#32CD32>[{timeTag}]</color> <color=#32CD32><b>[INFO]</b></color> <b>[{scriptName}]</b>: {message}");
    }


    //Warning - ошибка но игра не упала. Отсутствует текстура или т.п.
    public static void LogWarning(string message, [CallerFilePath] string scriptPath = "")
    {
        string scriptName = GetScriptName(scriptPath);
        string timeTag = GetTimeTag();
        UnityEngine.Debug.LogWarning($"<color=#FFD700>[{timeTag}]</color> <b>[WARN]</b> <b>[{scriptName}]</b>: {message}");
    }


    //Error - игра упала. Ошибка при которой продолжение игры не возможно
    public static void LogError(string message, [CallerFilePath] string scriptPath = "")
    {
        string scriptName = GetScriptName(scriptPath);
        string timeTag = GetTimeTag();
        UnityEngine.Debug.LogError($"<color=#FF4500>[{timeTag}]</color> <b>[ERROR]</b> <b>[{scriptName}]</b>: {message}");
    }



    private static string GetTimeTag()
    {
        // Если VContainer еще не успел подсунуть провайдер (например, в самых первых кадрах Awake),
        // логгер не упадет, а просто выведет стандартный текст
        return _timeProvider != null ? _timeProvider.CurrentGameTime : "00:00:00";
    }

    private static string GetScriptName(string filePath)
    {
        if (string.IsNullOrEmpty(filePath)) return "UnknownScript";
        return Path.GetFileNameWithoutExtension(filePath);
    }
}