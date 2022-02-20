using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Otel.Core
{
    /// <summary>
    /// Класс, который позволяет правильно управлять навигацией приложения
    /// </summary>
    public static class FrameManager
    {
        public static Frame MainFrame { get; set; }

        /// <summary>
        /// Метод, управляет навигацией приложения
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        public static void SetSource<T>(T target) where T : Page
        {
            var contentType = MainFrame.Content?.GetType();

            if (contentType != typeof(T))
            {
                MainFrame.Navigate(target);

                MainFrame.NavigationService.RemoveBackEntry();

                GC.Collect();
            }
        }
    }
}
