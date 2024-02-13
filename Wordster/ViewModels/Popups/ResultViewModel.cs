using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordster.ViewModels.Popups
{
    public class ResultViewModel : BaseViewModel
    {
        private readonly string _wordResult;

        public string WordResult
        {
            get { return _wordResult; }
            //set 
            //{
            //    _wordResult = value; 
            //    OnPropertyChanged(WordResult);
            //}
        }
        private bool _guessed;

        public bool Guessed
        {
            get { return _guessed; }
            set 
            {
                _guessed = value; 
                OnPropertyChanged(nameof(Guessed));
            }
        }


        public ResultViewModel(string word, bool guessed)
        {
            _wordResult = word.ToUpper();
            _guessed = guessed;
        }
    }
}
