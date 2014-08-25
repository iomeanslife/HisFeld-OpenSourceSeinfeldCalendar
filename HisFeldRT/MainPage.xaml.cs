using HisFeldLibrary.Model;
using HisFeldRT.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HisFeldRT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        OverviewVM viewModel;
        private bool navigationComplete;
        public MainPage()
        {
            this.InitializeComponent();

            viewModel = DataContext as OverviewVM;
        }

        private void ListBoxTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!navigationComplete || ListBoxTasks == null || ListBoxTasks.SelectedItem == null)
            {
                return;
            }

            viewModel.MainTaskBook.SelectedTask = ListBoxTasks.SelectedItem as Task;
            // Navigate to the new page

            // Reset selected index to -1 (no selection)
            
            
            this.Frame.Navigate(typeof(DetailsPage));
        }

     
        private void TaskNameFieldTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TaskNameFieldTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            navigationComplete = true;
        }
    }
}
