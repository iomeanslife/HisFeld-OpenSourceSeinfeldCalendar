using HisFeldLibrary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HisFeldLibrary.Model
{
    [DataContract]
    public class TaskBook : ModelBase
    {
        public TaskBook()
        {
            taskCollection = new ObservableCollection<Task>();
            taskCollection.CollectionChanged += taskCollection_CollectionChanged;
            FinishedFromSerialization = true;
        }

        public override void ReHookEvents()
        {
            taskCollection.CollectionChanged += taskCollection_CollectionChanged;

            foreach (Task inTask in taskCollection)
            {
                inTask.ReHookEvents();
            }
        }

        void taskCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("TaskCollection");
        }

        public bool FinishedFromSerialization = false;

        [DataMember]
        private ObservableCollection<Task> taskCollection;

        public ObservableCollection<Task> TaskCollection
        {
            get { return taskCollection; }
            set
            {
                taskCollection = value;
                RaisePropertyChanged("TaskCollection");
            }
        }


        private Task selectedTask;

        public Task SelectedTask
        {
            get { return selectedTask; }
            set
            {
                if (selectedTask != null)
                {
                    selectedTask.PropertyChanged -= selectedTask_PropertyChanged;
                }
                selectedTask = value;

                if (selectedTask != null)
                {
                    selectedTask.PropertyChanged += selectedTask_PropertyChanged;
                }


                RaisePropertyChanged("SelectedTask");
            }
        }

        void selectedTask_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("SelectedTask");
        }

        
    }
}
