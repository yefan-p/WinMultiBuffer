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
using MultiBuffer.WpfApp.Utils;
using MultiBuffer.WpfApp.ViewModels;
using MultiBuffer.WpfApp.ViewModels.Services;
using MultiBuffer.WpfApp.Views;

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
            var window = new MainWindow();
            var mainNavManager = new NavigationManager(Dispatcher, window.FrameContent);

            container.Register(Component
                .For<INavigationManager>()
                .Instance(mainNavManager));

            var helpViewModel = container.Resolve<HelpViewModel>();
            var buffersViewModel = container.Resolve<BuffersViewModel>();

            mainNavManager.Register<HelpViewModel, HelpView>(
                helpViewModel, NavigationKeys.HelpView);

            mainNavManager.Register<BuffersViewModel, BuffersView>(
                buffersViewModel, NavigationKeys.BuffersView);

            mainNavManager.Navigate(NavigationKeys.HelpView);
            window.Show();
        }

        private void RegisterComponents()
        {
            container.RegisterService<ICommandFactory, CommandFactory>();

            container.RegisterService<IClipboardController, ClipboardController>();

            container.RegisterService<IClipboardControllerFactory, ClipboardControllerFactory>();

            container.RegisterService<IList<IBufferItem>, ObservableCollection<IBufferItem>>();

            container.RegisterService<IBufferItemFactory, BufferItemFactory>();

            container.RegisterService<IInputSimulatorFactory, InputSimulatorFactory>();

            container.RegisterService<IInputController, InputController>();

            container.RegisterService<ICopyPasteController<IList<IBufferItem>>, CopyPasteController<IList<IBufferItem>>>();
        }

        private void RegisterViewModels()
        {
            container.RegisterService<HelpViewModel, HelpViewModel>();

            container.RegisterService<BuffersViewModel, BuffersViewModel>();

        }
    }
}
