using HandyControl.Tools;
using System.Windows;

namespace Otel
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ConfigHelper.Instance.SetLang("ru-ru");
        }
    }
}
