using Microsoft.Maui.Controls;
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
                    Letters = new ObservableCollection<Letter>()
                };
                for (int si = 0; si < characterCountMax; si++)
                    slot.Letters.Add(new Letter
                    {
                        Value = ""
                    });

                Slots.Add(slot);
            }


            _addLetterCommand = new Command<string>((character) =>
            {
                AddLetter(character);
            });

            _removeLetterCommand = new Command<string>((pos) =>
            {
                RemoveLetter(Convert.ToInt16(pos));
            });
        }
        /// <summary>
        /// Add a letter at the end of the current row
        /// </summary>
        /// <param name="letter">Letter to add</param>
        private void AddLetter(string letter)
        {
            ObservableCollection<Letter> letters = Slots[_currentLine].Letters;
            int index = letters.IndexOf(letters.FirstOrDefault(l => string.IsNullOrEmpty(l.Value)));

            if (index == -1)
                return;

            Slots[_currentLine].Letters[index].Value = letter;
        }

        /// <summary>
        /// Remove a letter from the current line
        /// </summary>
        /// <param name="pos">position of the leter to delete. (-1 to remove the last letter)</param>
        private void RemoveLetter(int pos)
        {
            ObservableCollection<Letter> letters = Slots[_currentLine].Letters;
            int index = letters.IndexOf(letters.LastOrDefault(l => !string.IsNullOrEmpty(l.Value)));

            bool isRegular = pos == -1;
            if (isRegular)
                pos = index;

            if (!isRegular && !string.IsNullOrEmpty(letters[pos + 1].Value))
            {
                int nextSlotsCount = characterCountMax - pos;

                for (int i = pos; i < characterCountMax; i++)
                {
                    int nextIndex = i + 1;
                    Letter letter = nextIndex > letters.Count - 1 || nextSlotsCount < i ? new Letter
                    {
                        Index = i,
                        Value = ""
                    } :
                    new Letter
                    {
                        Index = i,
                        Value = letters[nextIndex].Value
                    };
                    Slots[_currentLine].Letters[i] = letter;
                }
            }
            else
            {


                Slots[_currentLine].Letters[pos] = new Letter
                {
                    Index = pos,
                    Value = ""
                };
            }
        }
    }
}
