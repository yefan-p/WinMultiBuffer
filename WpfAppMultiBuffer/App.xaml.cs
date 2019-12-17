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
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var copyPasteController = container.Resolve<ICopyPasteController<IList<IBufferItem>>>();

            var helpSwitchingController = new HelpSwitchingController<HelpItem>(new HelpItem());

            var window = new MainWindow();
            var mainNavManager = new NavigationManager(Dispatcher, window.FrameContent);

            mainNavManager.Register<HelpViewModel, HelpView>(
                new HelpViewModel(mainNavManager, helpSwitchingController), NavigationKeys.HelpView);

            mainNavManager.Register<BuffersViewModel, BuffersView>(
                new BuffersViewModel(mainNavManager, copyPasteController), NavigationKeys.BuffersView);

            mainNavManager.Navigate(NavigationKeys.HelpView);
            window.Show();
        }

        private void RegisterComponents()
        {
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
    }
}
