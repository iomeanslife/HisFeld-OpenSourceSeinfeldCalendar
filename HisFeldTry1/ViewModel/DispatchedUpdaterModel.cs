using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HisFeldTry1.ViewModel
{
    public abstract class DispatchedUpdaterModel : ViewModelBase
    {
        public DispatchedUpdaterModel()
        {
            App.DUpdater.PropertyChanged += DUpdater_PropertyChanged;
        }
        void DUpdater_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

       
    }
}
