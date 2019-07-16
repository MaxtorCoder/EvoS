namespace EvoS.Framework.Logging
{
    public interface ILog
    {
        void Print(LogType _type, object _obj, bool showTime, bool showLogLevel);
        void Print(LogType _type, object _obj);
    }
}
