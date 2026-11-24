
public class LogSystemInitializer
{
    private readonly ITimeProvider _timeProvider;

    // VContainer сам найдет, кто у нас в проекте отвечает за время, и передаст сюда
    public LogSystemInitializer(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public void Initialize()
    {
        // Инициализируем статический логгер
        GameLog.Initialize(_timeProvider);
    }
}