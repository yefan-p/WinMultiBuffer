﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfAppMultiBuffer.Controllers;
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

            ObservableCollection<BufferItem> collection = new ObservableCollection<BufferItem>();
            CopyPasteController<ObservableCollection<BufferItem>, BufferItem> copyPasteController
                = new CopyPasteController<ObservableCollection<BufferItem>, BufferItem>(
                    inputController,
                    collection,
                    new BufferItemFactory());

            var window = new MainWindow();
            var mainNavManager = new NavigationManager(Dispatcher, window.FrameContent);

            mainNavManager.Register<HelpViewModel, HelpView>(
                new HelpViewModel(mainNavManager), NavigationKeys.HelpView);

            mainNavManager.Register<BuffersViewModel, BuffersView>(
                new BuffersViewModel(mainNavManager, copyPasteController), NavigationKeys.BuffersView);

            mainNavManager.Navigate(NavigationKeys.HelpView);
            window.Show();
        }
    }
}
