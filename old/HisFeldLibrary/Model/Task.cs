using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HisFeldLibrary.Model
{
    [DataContract]
    public class Task : ModelBase
    {
        public Task()
        {
            chainCollection = new ObservableCollection<Chain>();
            chainCollection.CollectionChanged += chainCollection_CollectionChanged;
        }

        public override void ReHookEvents()
        {
            chainCollection.CollectionChanged += chainCollection_CollectionChanged;
            foreach (Chain inChain in chainCollection)
            {
                inChain.PropertyChanged += inChain_PropertyChanged;
                inChain.ReHookEvents();
            }
        }

        void chainCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (Chain inChain in e.NewItems)
            {
                inChain.PropertyChanged += inChain_PropertyChanged;
            }

            RaisePropertyChanged("ChainCollection");
            RaisePropertyChanged("CurrentChain");
            RaisePropertyChanged("TodayIsSet");

            if (e.OldItems == null)
            {
                return;
            }

            foreach (Chain inChain in e.OldItems)
            {
                inChain.PropertyChanged -= inChain_PropertyChanged;
            }

        
        }

        void inChain_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("ChainCollection");
            RaisePropertyChanged("CurrentChain");
            RaisePropertyChanged("TodayIsSet");
        }

        public bool TodayIsSet
        {
            get
            {
                return (CurrentChain != null && CurrentChain.End.Date == DateTime.Today);
            }
        }

        [DataMember]
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged("Title"); }
        }

        [DataMember]
        private ObservableCollection<Chain> chainCollection;

        public ObservableCollection<Chain> ChainCollection
        {
            get { return chainCollection; }
            set { chainCollection = value; RaisePropertyChanged("ChainCollection"); }
        }

        public Chain CurrentChain
        {
            get
            {
                return ChainCollection.FirstOrDefault(
                    (c) => c.End >= DateTime.Today.AddDays(-1)
                    );
            }
        }


    }
}
