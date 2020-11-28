namespace Common.Domain
{
    /// <summary>
    /// This interface contains the definition of the logger service.
    /// </summary>
    public interface ILog
    {
        void Info(string message);
        void Info(object obj);
        void Info(string message, object obj);
        void Warning(string message);
        void Warning(object obj);
        void Warning(string message, object obj);
        void Error(string message);
        void Error(object obj);
        void Error(string message, object obj);
        void InitForDebug();
    }
}