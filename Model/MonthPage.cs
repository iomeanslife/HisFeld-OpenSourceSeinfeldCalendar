using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HisFeldLibrary.Model
{
    [DataContract]
    public class MonthPage : ModelBase
    {
       
      
        [DataMember]
        private bool[] hits;

        public bool[] Hits
        {
            get { return hits; }
            set
            {
                hits = value;
                RaisePropertyChanged("Hits");
            }
        }

        [DataMember]
        private DateTime[] daysDates;

        public DateTime[] DaysDates
        {
            get { return daysDates; }
            set
            {
                daysDates = value;

            }
        }

        public void UpdateBindings(string propertyName)
        {
            RaisePropertyChanged(propertyName);
        }
    }
}
