﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfAppMultiBuffer.Models.Controllers;
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

            var copyPasteController = new CopyPasteController<ObservableCollection<BufferItem>, BufferItem>(
                                            inputController,
                                            new ObservableCollection<BufferItem>(),
                                            new BufferItemFactory(),
                                            new InputSimulatorFactory());

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
    }
}
