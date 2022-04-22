using R2API.Utils;

namespace CheatersGoBrr
{
    internal class Utils
    {
        public static void SendMessage(string message)
        {
            if (message != "")
            {
                ChatMessage.Send(message);
            }
        }
        public static void SendMessage(string message, string hex)
        {
            if (message != "")
            {
                ChatMessage.SendColored(message, hex);
            }
        }
    }
}
