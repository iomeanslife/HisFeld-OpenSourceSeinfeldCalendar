using GalaSoft.MvvmLight.Command;
using HisFeldLibrary.Model;
using HisFeldTry1.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;


namespace HisFeldTry1.ViewModel
{
    public class DetailsVM : DispatchedUpdaterModel
    {

        public DetailsVM()
        {
            if (SelectedTask != null)
            {
                SelectedTask.PropertyChanged += SelectedTask_PropertyChanged;
            }
        }

        void SelectedTask_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }


        public Task SelectedTask
        {
            get { return App.DUpdater.MainTaskBook.SelectedTask; }
        }

        MonthPage activeMonthPage;

        public MonthPage ActiveMonthPage
        {
            get { return activeMonthPage; }
            set
            {
                activeMonthPage = value;
                RaisePropertyChanged("ActiveMonthPage");
            }
        }

        public string CurrentMonth
        {
            get
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Today.Month).ToUpper();
            }
        }

        RelayCommand addChainCommand;

        public ICommand AddChainCommand
        {
            get
            {
                return addChainCommand ?? (addChainCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedTask.CurrentChain == null)
                        {
                            SelectedTask.ChainCollection.Add(new Chain() { Start = DateTime.Today, End = DateTime.Today });
                        }
                        else
                        {
                            SelectedTask.CurrentChain.End = DateTime.Today;
                        }
                        addChainCommand.RaiseCanExecuteChanged();
                    },
                    () =>
                    {
                        return (App.DUpdater.MainTaskBook != null);
                    }
                    ));
            }
        }

        //TODO: make private again
        public MonthPage calculateDaysToShow(DateTime dateToCalculate)
        {
            try
            {
                MonthPage placeHolder = new MonthPage();
                int length = 7 * 6;
                int extraDays = 0;

                DateTime firstDayDate = new DateTime(dateToCalculate.Year, dateToCalculate.Month, 1);

                switch (firstDayDate.DayOfWeek)
                {
                    case DayOfWeek.Friday:
                        extraDays += 5;
                        break;
                    case DayOfWeek.Monday:
                        extraDays += 1;
                        break;
                    case DayOfWeek.Saturday:
                        extraDays += 6;
                        break;
                    case DayOfWeek.Sunday:
                        extraDays += 7;
                        break;
                    case DayOfWeek.Thursday:
                        extraDays += 4;
                        break;
                    case DayOfWeek.Tuesday:
                        extraDays += 2;
                        break;
                    case DayOfWeek.Wednesday:
                        extraDays += 3;
                        break;
                    default:
                        break;
                }
                placeHolder.DaysDates = new DateTime[length];
                placeHolder.Hits = new bool[length];

                //placeHolder.PreDays = new int[extraDays];
                for (int i = 0; i < extraDays; i++)
                {
                    //placeHolder.PreDays[i] = firstDayDate.AddDays(-(extraDays - i)).Day;
                    placeHolder.DaysDates[i] = firstDayDate.AddDays(-(extraDays - i));
                }

                //placeHolder.Days = new int[DateTime.DaysInMonth(firstDayDate.Year, firstDayDate.Month)];

                int oi;
                for (oi = 1; oi <= DateTime.DaysInMonth(firstDayDate.Year, firstDayDate.Month); oi++)
                {
                    //placeHolder.Days[oi - 1] = oi;
                    placeHolder.DaysDates[oi + extraDays - 1] = new DateTime(firstDayDate.Year, firstDayDate.Month, oi);
                }

                //placeHolder.PostDays = new int[length - (oi + extraDays - 1)];

                int postcount = length - (oi + extraDays - 1);
                oi += 1;

                for (int i = 0; i < postcount; i++)
                {
                    //placeHolder.PostDays[i] = firstDayDate.AddDays(oi - 2).Day;

                    //placeHolder.DaysDates[placeHolder.PreDays.Count() + placeHolder.Days.Count() + i] = firstDayDate.AddDays(oi - 2);
                    placeHolder.DaysDates[extraDays + DateTime.DaysInMonth(firstDayDate.Year, firstDayDate.Month) + i] = firstDayDate.AddDays(oi - 2);
                    
                    oi++;
                }
                return placeHolder;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
