using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HisFeldTry1.ViewModel;
using HisFeldLibrary.Model;
using HisFeldTry1.Resources;

namespace HisFeldTry1
{
    public partial class OverviewPage : PhoneApplicationPage
    {
        OverviewVM viewModel;
        private bool navigationComplete;

        public OverviewPage()
        {
            InitializeComponent();
            viewModel = DataContext as OverviewVM;

            BuildLocalizedApplicationBar();
        }


        private void ListBoxTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!navigationComplete || ListBoxTasks == null || ListBoxTasks.SelectedItem == null)
            {
                return;
            }

            viewModel.MainTaskBook.SelectedTask = ListBoxTasks.SelectedItem as Task;
            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml", UriKind.Relative));

            // Reset selected index to -1 (no selection)
            ListBoxTasks.SelectedItem = null;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TaskNameFieldTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            //// Create a new button and set the text value to the localized string from AppResources.
            //ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
            //appBarButton.Text = AppResources.AppBarButtonText;
            //ApplicationBar.Buttons.Add(appBarButton);

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemTextAbout);
            ApplicationBar.Mode = ApplicationBarMode.Minimized;
            
            ApplicationBar.MenuItems.Add(appBarMenuItem);
        }

        private void OverviewPhonePage_Loaded(object sender, RoutedEventArgs e)
        {
            navigationComplete = true;
        }
    }
}