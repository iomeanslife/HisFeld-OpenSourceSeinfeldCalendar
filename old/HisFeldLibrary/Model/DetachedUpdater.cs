using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HisFeldLibrary.Model
{
    public class DetachedUpdater : ModelBase
    {
        public DetachedUpdater(TaskBook taskBook)
        {
            MainTaskBook = taskBook;
            if (MainTaskBook.FinishedFromSerialization == false)
            {
                MainTaskBook.ReHookEvents();
                MainTaskBook.FinishedFromSerialization = true;     
            }
           

            MainTaskBook.PropertyChanged += mainTaskBook_PropertyChanged;
        }

        void mainTaskBook_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("MainTaskBook");
        }
        private TaskBook mainTaskBook;
        public TaskBook MainTaskBook
        {
            get
            {
                return mainTaskBook;
            }
            set
            {
                mainTaskBook = value;
                RaisePropertyChanged("MainTaskBook");
            }
        }
    }
}
