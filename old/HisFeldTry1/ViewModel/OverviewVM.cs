using GalaSoft.MvvmLight.Command;
using HisFeldLibrary.Model;
using HisFeldTry1.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HisFeldTry1.ViewModel
{
    public class OverviewVM : DispatchedUpdaterModel
    {

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
                        MainTaskBook.TaskCollection.Remove(t);
                    },
                    (t) =>
                    {
                        return (MainTaskBook != null && t != null);
                    }
                    ));
            }
        }

        
    }
        #endregion
    //Commands
}
