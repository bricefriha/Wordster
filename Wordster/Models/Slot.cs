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
        private ObservableCollection<Letter> _letters;

        public ObservableCollection<Letter> Letters 
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
