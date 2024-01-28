
using MvvmHelpers;
using System.Collections.ObjectModel;
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
        private Color _emptyColour ;
        private Color _filledColour ;

        public GameViewModel()
        {
            _slots = new ObservableCollection<Slot>();
            GetResourceColours();
            for (int i = 0; i < slotCount; i++)
            {
                Slot slot = new()
                {
                    Letters = new ObservableCollection<Letter>()
                };
                for (int si = 0; si < characterCountMax; si++)
                    slot.Letters.Add(new Letter
                    {
                        BackgroundColour = _emptyColour,
                        Elevation = 0,
                        Value = ""
                    });

                // Add another line
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

        private void GetResourceColours()
        {
            App.Current.Resources.TryGetValue("DarkButtonBrush", out var emptycolorvalue);
            App.Current.Resources.TryGetValue("DefaultButtonBrush", out var filledcolorvalue);
            _emptyColour = emptycolorvalue as Color;
            _filledColour = filledcolorvalue as Color;
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
            Slots[_currentLine].Letters[index].BackgroundColour = _filledColour;
            Slots[_currentLine].Letters[index].Elevation = .25;
        }

        /// <summary>
        /// Remove a letter from the current line
        /// </summary>
        /// <param name="pos">position of the letter to delete. (-1 to remove the last letter)</param>
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
                        BackgroundColour = _emptyColour,
                        Elevation = 0,
                        Value = ""
                    } :
                    new Letter
                    {
                        Index = i,
                        BackgroundColour = letters[nextIndex].BackgroundColour,
                        Elevation = letters[nextIndex].Elevation,
                        Value = letters[nextIndex].Value
                    };

                    // Update the letter
                    Slots[_currentLine].Letters[i] = letter;
                }
            }
            else
            {
                // Add empty slot
                Slots[_currentLine].Letters[pos] = new Letter
                {
                    Index = pos,
                    BackgroundColour = _emptyColour,
                    Elevation = 0,
                    Value = ""
                };
            }
        }
    }
}
