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
        private const int slotCount = 6;
        private ObservableCollection<Slot> _slots;
        private int _currentLine = 0;

        public ObservableCollection<Slot> Slots
        {
            get { return _slots; }
            set 
            {
                _slots = value;
                OnPropertyChanged(nameof(Slots));
            }
        }
        private Command<string> _addLetterCommand;

        public Command<string> AddLetterCommand
        {
            get 
            { 
                return _addLetterCommand; 
            }
        }
        private Command<string> _removeLetterCommand;

        public Command<string> RemoveLetterCommand
        {
            get 
            { 
                return _removeLetterCommand; 
            }
        }


        public GameViewModel()
        {
            _slots = new ObservableCollection<Slot>();
            for (int i = 0; i < slotCount; i++)
            {
                Slot slot = new()
                {
                    Letters = new ObservableCollection<string>()
                };
                for (int si = 0; si < characterCountMax; si++)
                    slot.Letters.Add("");

                Slots.Add(slot);
            }


            _addLetterCommand = new Command<string>((character) =>
            {
                ObservableCollection<string> letters = Slots[_currentLine].Letters;
                int index = letters.IndexOf(letters.FirstOrDefault(l => string.IsNullOrEmpty(l)));

                if (index == -1)
                    return;

                Slots[_currentLine].Letters[index] = character;
            });

            _removeLetterCommand = new Command<string>((pos) =>
            {
                ObservableCollection<string> letters = Slots[_currentLine].Letters;
                int index = letters.IndexOf(letters.LastOrDefault(l => !string.IsNullOrEmpty(l)));

                int v = Convert.ToInt16(pos);
                if (v == -1)
                    v = index;

                Slots[_currentLine].Letters[v] = string.Empty;
            });
        }
    }
}
