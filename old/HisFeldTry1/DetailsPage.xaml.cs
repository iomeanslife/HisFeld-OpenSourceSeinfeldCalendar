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
using System.Windows.Data;
using HisFeldTry1.Resources;

namespace HisFeldTry1
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        DetailsVM viewModel;
        public DetailsPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
        }

        private void UpdateCalendar()
        {
            foreach (Chain inChain in viewModel.SelectedTask.ChainCollection)
            {
                if (
                    inChain.End < viewModel.ActiveMonthPage.DaysDates[0].Date ||
                    inChain.Start > viewModel.ActiveMonthPage.DaysDates[viewModel.ActiveMonthPage.DaysDates.Count() - 1].Date
                    )
                {
                    continue;
                }

                for (int i = 0; i < inChain.Length; i++)
                {
                    if (
                        inChain.Start.AddDays(i) < viewModel.ActiveMonthPage.DaysDates[0].Date ||
                        inChain.Start.AddDays(i) > viewModel.ActiveMonthPage.DaysDates[viewModel.ActiveMonthPage.DaysDates.Count() - 1].Date
                        )
                    {
                        continue;
                    }

                    for (int iMP = 0; iMP < viewModel.ActiveMonthPage.DaysDates.Count(); iMP++)
                    {
                        if (viewModel.ActiveMonthPage.DaysDates[iMP].Date == inChain.Start.AddDays(i).Date)
                        {
                            viewModel.ActiveMonthPage.Hits[iMP] = true;
                        }
                    }
                }
            }
            //GetBindingExpression(TextBlock.VisibilityProperty).UpdateSource();
            viewModel.ActiveMonthPage.UpdateBindings("Hits");
        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = DataContext as DetailsVM;
            viewModel.PropertyChanged += viewModel_PropertyChanged;
            //TODO: Ugly. better use commands etc.
            viewModel.ActiveMonthPage = viewModel.calculateDaysToShow(DateTime.Today);

            //foreach (DateTime inMonthPage in viewModel.ActiveMonthPage.DaysDates)
            //{
            //    viewModel.SelectedTask.ChainCollection.Where(
            //        (c)=> {
            //            c.End.Date == inMonthPage.Date
            //        });
            //}

            UpdateCalendar();
        }

        void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            UpdateCalendar();
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
    }
}