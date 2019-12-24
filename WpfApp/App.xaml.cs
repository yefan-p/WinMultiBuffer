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
using WpfAppMultiBuffer.Models.Controllers;
using WpfAppMultiBuffer.Models.Interfaces;
using WpfAppMultiBuffer.Utils;
using WpfAppMultiBuffer.ViewModels;
using WpfAppMultiBuffer.ViewModels.Services;
using WpfAppMultiBuffer.Views;

namespace WpfAppMultiBuffer
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
            container.Register(Component
                .For<ICommandFactory>()
                .ImplementedBy<CommandFactory>());

            container.Register(Component
                .For<IClipboardController>()
                .ImplementedBy<ClipboardController>());

            container.Register(Component
                .For<IClipboardControllerFactory>()
                .ImplementedBy<ClipboardControllerFactory>());

            container.Register(Component
                .For<IList<IBufferItem>>()
                .ImplementedBy<ObservableCollection<IBufferItem>>());

            container.Register(Component
                .For<IBufferItemFactory>()
                .ImplementedBy<BufferItemFactory>());

            container.Register(Component
                .For<IInputSimulatorFactory>()
                .ImplementedBy<InputSimulatorFactory>());

            container.Register(Component
                .For<IInputController>()
                .ImplementedBy<InputController>());

            container.Register(Component
               .For<ICopyPasteController<IList<IBufferItem>>>()
               .ImplementedBy<CopyPasteController<IList<IBufferItem>>>());
        }

        private void RegisterViewModels()
        {
            container.Register(Component
                  .For<HelpViewModel>()
                  .ImplementedBy<HelpViewModel>());

            container.Register(Component
                  .For<BuffersViewModel>()
                  .ImplementedBy<BuffersViewModel>());

        }
    }
}
