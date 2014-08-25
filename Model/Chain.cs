using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HisFeldLibrary.Model
{
    [DataContract]
    public class Chain : ModelBase
    {
        [DataMember]
        private DateTime start;


        public DateTime Start
        {
            get { return start; }
            set
            {
                start = value;
                RaisePropertyChanged("Start");
            }
        }

        [DataMember]
        private DateTime end;

        public DateTime End
        {
            get { return end; }
            set
            {
                end = value;
                RaisePropertyChanged("End");
            }
        }

        public int Length
        {
            get
            {
                return Convert.ToInt32((End - Start).TotalDays + 1);
            }
        }
        
      
    }
}
