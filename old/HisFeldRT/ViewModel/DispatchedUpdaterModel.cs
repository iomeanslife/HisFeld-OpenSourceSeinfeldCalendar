using HisFeldRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HisFeldRT.ViewModel
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
