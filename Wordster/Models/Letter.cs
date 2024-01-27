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
        private double _elevation;

        public double Elevation
        {
            get { return _elevation; }
            set 
            { 
                _elevation = value; 
                OnPropertyChanged(nameof(Elevation));
            }
        }
        private Color _backgroundColour ;

        public Color BackgroundColour
        {
            get { return _backgroundColour; }
            set 
            {
                _backgroundColour = value; 
                OnPropertyChanged(nameof(BackgroundColour));
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
