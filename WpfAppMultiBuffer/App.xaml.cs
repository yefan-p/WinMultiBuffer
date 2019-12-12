using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfAppMultiBuffer.Controllers;
using WpfAppMultiBuffer.Models;
using WpfAppMultiBuffer.Utils;
using WpfAppMultiBuffer.ViewModels;
using WpfAppMultiBuffer.Views;

namespace WpfAppMultiBuffer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            InputController inputController = new InputController();
            CopyPasteController copyPasteController = new CopyPasteController(inputController);


            var window = new MainWindow();
            var mainNavManager = new NavigationManager(Dispatcher, window.FrameContent);

            mainNavManager.Register<BuffersViewModels, BuffersView>(
                new BuffersViewModels(mainNavManager, copyPasteController), NavigationKeys.BuffersView);

            mainNavManager.Navigate(NavigationKeys.BuffersView);
            window.Show();
        }
    }
}
