using DialogSemiconductor.ViewModels;
using DialogSemiconductor.Views;
using System;
using System.Windows;

namespace DialogSemiconductor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Метод старта программы
        /// </summary>
        /// <param name="args">Аргументы</param>
        protected override void OnStartup(StartupEventArgs args)
        {
            try
            {
                base.OnStartup(args);

                MainView mainShellView = new MainView();
                MainViewModel mainShellViewModel = new MainViewModel();
                mainShellView.DataContext = mainShellViewModel;
                mainShellView.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
        }
    }
}
