using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MultiBuffer.WpfApp.Models.Controllers;
using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.WpfApp.Models.Handlers;
using MultiBuffer.WpfApp.Utils;
using MultiBuffer.WpfApp.ViewModels;
using MultiBuffer.WpfApp.ViewModels.Implements;
using MultiBuffer.WpfApp.Views;
using WindowsInput;

namespace MultiBuffer.WpfApp
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        IWindsorContainer container;

        public App()
        {
            container = new WindsorContainer();
            RegisterComponents();
            RegisterViewModels();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            App.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            var window = new MainWindow();
            var mainNavManager = new NavigationManager(Dispatcher, window.FrameContent);

            container.Register(Component
                .For<INavigationManager>()
                .Instance(mainNavManager));

            var helpViewModel = container.Resolve<HelpViewModel>();
            var buffersViewModel = container.Resolve<BuffersViewModel>();
            var windowViewModel = container.Resolve<MainWindowViewModel>();

            window.DataContext = windowViewModel;

            mainNavManager.Register<HelpViewModel, HelpView>(
                helpViewModel, NavigationKeys.HelpView);

            mainNavManager.Register<BuffersViewModel, BuffersView>(
                buffersViewModel, NavigationKeys.BuffersView);

            mainNavManager.Navigate(NavigationKeys.HelpView);
            window.Show();
        }

        private void RegisterComponents()
        {
            container.RegisterSingleton<IList<IBufferItem>, ObservableCollection<IBufferItem>>();

            container.RegisterService<ICommandFactory, CommandFactory>();

            container.RegisterService<IClipboardController, ClipboardController>();

            container.RegisterService<IBufferItemFactory, BufferItemFactory>();

            container.RegisterSingleton<IInputHandler, InputHandler>();

            container.RegisterSingleton<ICopyPasteController<IList<IBufferItem>>, CopyPasteController<IList<IBufferItem>>>();

            container.RegisterService<IInputSimulator, InputSimulator>();
        }

        private void RegisterViewModels()
        {
            container.RegisterService<HelpViewModel, HelpViewModel>();

            container.RegisterService<BuffersViewModel, BuffersViewModel>();

            container.RegisterService<MainWindowViewModel, MainWindowViewModel>();
        }
    }
}
