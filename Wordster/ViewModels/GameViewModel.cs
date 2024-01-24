using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordster.Models;

namespace Wordster.ViewModels
{
    class GameViewModel : BaseViewModel
    {
        private const int characterCountMax = 5;
        private ObservableCollection<Slot> _slots;

        public ObservableCollection<Slot> Slots
        {
            get { return _slots; }
            set 
            {
                _slots = value;
                OnPropertyChanged(nameof(Slots));
            }
        }

        public GameViewModel()
        {
            _slots = new ObservableCollection<Slot>();

            for (int i = 0; i < 5; i++)
            {
                Slot slot = new Slot();
                slot.Letters = new ObservableCollection<string>();
                for (int si = 0; si < characterCountMax; si++)
                    slot.Letters.Add("");

                Slots.Add(slot);
            }
        }
    }
}
