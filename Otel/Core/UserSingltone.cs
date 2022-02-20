using Otel.Model;

namespace Otel.Core
{
    /// <summary>
    /// Класс, который запоминает пользователя 
    /// </summary>
    public static class UserSingltone
    {
        public static User User { get; set; }
    }
}
