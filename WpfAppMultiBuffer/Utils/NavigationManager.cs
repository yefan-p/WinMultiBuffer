using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfAppMultiBuffer.Utils
{
    public class NavigationManager : INavigationManager
    {
        #region Fields

        private readonly Dispatcher dispatcher;
        private readonly ContentControl frameControl;
        private readonly IDictionary<string, object> viewModelsByNavigationKey = new Dictionary<string, object>();
        private readonly IDictionary<Type, Type> viewTypesByViewModelType = new Dictionary<Type, Type>();

        #endregion

        #region Constructors

        public NavigationManager(Dispatcher dispatcher, ContentControl frameControl)
        {
            this.dispatcher   = dispatcher   ?? throw new ArgumentNullException("dispatcher");
            this.frameControl = frameControl ?? throw new ArgumentNullException("frameControl");
        }

        #endregion

        public void Register<TViewModel, TView>(TViewModel viewModel, string navigationKey)
            where TViewModel : class
            where TView : FrameworkElement
        {
            if (viewModel == null)
                throw new ArgumentNullException("viewModel");
            if (navigationKey == null)
                throw new ArgumentNullException("navigationKey");

            viewModelsByNavigationKey[navigationKey] = viewModel;
            viewTypesByViewModelType[typeof(TViewModel)] = typeof(TView);
        }

        public void Navigate(string navigationKey, object arg = null)
        {
            if (navigationKey == null)
                throw new ArgumentNullException("navigationKey");

            InvokeInMainThread(() =>
            {
                InvokeNavigatingFrom();
                var viewModel = GetNewViewModel(navigationKey);
                InvokeNavigatingTo(viewModel, arg);

                var view = CreateNewView(viewModel);
                frameControl.Content = view;
            });
        }

        private void InvokeInMainThread(Action action)
        {
            dispatcher.Invoke(action);
        }

        private FrameworkElement CreateNewView(object viewModel)
        {
            var viewType = viewTypesByViewModelType[viewModel.GetType()];
            var view = (FrameworkElement)Activator.CreateInstance(viewType);
            view.DataContext = viewModel;
            return view;
        }

        private object GetNewViewModel(string navigationKey)
        {
            return viewModelsByNavigationKey[navigationKey];
        }

        private void InvokeNavigatingFrom()
        {
            var oldView = frameControl.Content as FrameworkElement;
            if (oldView == null)
                return;

            var navigationAware = oldView.DataContext as INavigationAware;
            if (navigationAware == null)
                return;

            navigationAware.OnNavigatingFrom();
        }

        private static void InvokeNavigatingTo(object viewModel, object arg)
        {
            var navigationAware = viewModel as INavigationAware;
            if (navigationAware == null)
                return;

            navigationAware.OnNavigatingTo(arg);
        }
    }
}