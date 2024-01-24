using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordster.Models
{
    public class Slot : NotifiableModel
    {
        private ObservableCollection<string> _letters;

        public ObservableCollection<string> Letters 
        {
            get
            {
                return _letters;
            }
            set
            {
                _letters = value;
                OnPropertyChanged(nameof(Letters));

            } 
        }
    }
}
