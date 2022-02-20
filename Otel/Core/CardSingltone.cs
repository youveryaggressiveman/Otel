using Otel.Model;

namespace Otel.Core
{
    /// <summary>
    /// Класс, который запоминает карту пользователя
    /// </summary>
    public static class CardSingltone
    {
        public static Card Card { get; set; }
    }
}
