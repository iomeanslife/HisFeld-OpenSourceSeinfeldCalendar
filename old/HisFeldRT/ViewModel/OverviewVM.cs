using GalaSoft.MvvmLight.Command;
using HisFeldLibrary.Model;
using HisFeldRT;
using HisFeldRT.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HisFeldRT.ViewModel
{
    public class OverviewVM : DispatchedUpdaterModel
    {
        public OverviewVM()
        {
            App.DUpdater.PropertyChanged += DUpdater_PropertyChanged;
        }

        void DUpdater_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
            //TODO: Ugly
            RaisePropertyChanged("SelectedTask");
            deleteTaskCommand.RaiseCanExecuteChanged();
        }

       #region Properties
        private string taskNameField;
        #endregion

        public string TaskNameField
        {
            get { return taskNameField; }
            set
            {
                taskNameField = value;
                RaisePropertyChanged("TaskNameField");
                addTaskCommand.RaiseCanExecuteChanged();
                
            }
        }
        public TaskBook MainTaskBook
        {
            get { return App.DUpdater.MainTaskBook; }
        }


        #region Commands

        RelayCommand addTaskCommand;

        public ICommand AddTaskCommand
        {
            get
            {
                return addTaskCommand ?? (addTaskCommand = new RelayCommand(
                    () =>
                    {
                        MainTaskBook.TaskCollection.Add(new Task() { Title = TaskNameField });
                        TaskNameField = String.Empty;
                    },
                    () =>
                    {
                        return (MainTaskBook != null &&  !String.IsNullOrEmpty(TaskNameField));
                    }
                    ));
            }
        }


        RelayCommand<Task> deleteTaskCommand;

        public ICommand DeleteTaskCommand
        {
            get
            {
                return deleteTaskCommand ?? (deleteTaskCommand = new RelayCommand<Task>(
                    (t) =>
                    {
                        MainTaskBook.TaskCollection.Remove(MainTaskBook.SelectedTask);
                    },
                    (t) =>
                    {
                        return (MainTaskBook != null && MainTaskBook.SelectedTask != null);
                    }
                    ));
            }
        }
    }
        #endregion
    //Commands
}
