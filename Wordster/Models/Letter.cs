using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordster.Models
{
    public class Letter : NotifiableModel
    {
        // Static variable to keep track of the index
        private static int nextIndex = 0;

        private string _value;

        public string Value
        {
            get { return _value; }
            set 
            {
                _value = value; 
                OnPropertyChanged(nameof(Value));
            }
        }


        public Letter()
        {
            Index = nextIndex;
            nextIndex++;
        }

        // Auto-increment integer index property but overidable
        public int Index { get; set; }
    }
}
