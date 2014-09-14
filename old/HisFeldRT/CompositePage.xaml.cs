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
    public sealed partial class CompositePage : Page
    {
        DetailsVM detailsViewModel;
        OverviewVM overviewViewModel;

        public CompositePage()
        {
            this.InitializeComponent();
        }
        private void ListBoxTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxTasks == null || ListBoxTasks.SelectedItem == null)
            {
                return;
            }

            //overviewViewModel.MainTaskBook.SelectedTask = ListBoxTasks.SelectedItem as Task;

            // Navigate to the new page

            // Reset selected index to -1 (no selection)


            //this.Frame.Navigate(typeof(DetailsPage));
        }

        private void TaskNameFieldTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TaskNameFieldTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void UpdateCalendar()
        {
            if (detailsViewModel.SelectedTask == null)
            {
                return;
            }

            detailsViewModel.ActiveMonthPage.Hits = new bool[detailsViewModel.ActiveMonthPage.Hits.Length];
            foreach (Chain inChain in detailsViewModel.SelectedTask.ChainCollection)
            {
                if (
                    inChain.End < detailsViewModel.ActiveMonthPage.DaysDates[0].Date ||
                    inChain.Start > detailsViewModel.ActiveMonthPage.DaysDates[detailsViewModel.ActiveMonthPage.DaysDates.Count() - 1].Date
                    )
                {
                    continue;
                }

                for (int i = 0; i < inChain.Length; i++)
                {
                    if (
                        inChain.Start.AddDays(i) < detailsViewModel.ActiveMonthPage.DaysDates[0].Date ||
                        inChain.Start.AddDays(i) > detailsViewModel.ActiveMonthPage.DaysDates[detailsViewModel.ActiveMonthPage.DaysDates.Count() - 1].Date
                        )
                    {
                        continue;
                    }

                    for (int iMP = 0; iMP < detailsViewModel.ActiveMonthPage.DaysDates.Count(); iMP++)
                    {
                        if (detailsViewModel.ActiveMonthPage.DaysDates[iMP].Date == inChain.Start.AddDays(i).Date)
                        {
                            detailsViewModel.ActiveMonthPage.Hits[iMP] = true;
                        }
                    }
                }
            }
            detailsViewModel.ActiveMonthPage.UpdateBindings("Hits");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            overviewViewModel = Resources["overviewVm"] as OverviewVM;
            overviewViewModel.PropertyChanged += overviewViewModel_PropertyChanged;

            detailsViewModel = Resources["detailsVm"] as DetailsVM;   
            //overviewViewModel = Resources["detailsVm"] as OverviewVM;
            detailsViewModel.PropertyChanged += viewModel_PropertyChanged;
            detailsViewModel.ActiveMonthPage = detailsViewModel.calculateDaysToShow(DateTime.Today);

            UpdateCalendar();
        }

        void overviewViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        private void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateCalendar();
        }
    }
}
