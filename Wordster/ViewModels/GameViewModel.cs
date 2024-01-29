
using CommunityToolkit.Maui.Views;
using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Wordster.Models;
using Wordster.Views.PopUps;

namespace Wordster.ViewModels
{
    class GameViewModel : BaseViewModel
    {
        private const int characterCountMax = 5;
        private const int slotCount = 6;
        private int _currentLine = 0;
        private const string _qwerty = "qwertyuiopasdfghjklzxcvbnm";



        public App CurrentApp { get; }

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

        private ObservableCollection<Letter> _keys;
        public ObservableCollection<Letter> Keys
        {
            get { return _keys; }
            set 
            {
                _keys = value;
                OnPropertyChanged(nameof(Keys));
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
        private Command _checkWordCommand;

        public Command CheckWordCommand
        {
            get 
            { 
                return _checkWordCommand; 
            }
        }
        private Color _emptyColour ;
        private Color _filledColour ;
        private Color _almostValidColour;
        private Color _validColour;
        private string validWord = "fault";

        public GameViewModel()
        {
            CurrentApp = (App)App.Current;
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

            GenerateKeys();

            _addLetterCommand = new Command<string>((character) =>
            {
                AddLetter(character);
            });

            _removeLetterCommand = new Command<string>((pos) =>
            {
                RemoveLetter(Convert.ToInt16(pos));
            });

            _checkWordCommand = new Command(() =>
            {
                CheckAttempt();
                // Go to next line
                _currentLine++;
            });
        }

        /// <summary>
        /// Check the word of the current line
        /// </summary>
        private void CheckAttempt()
        {
            // Look at all the slots of the currentline
            ObservableCollection<Letter> letters = Slots[_currentLine].Letters;

            for (int i = 0; i < letters.Count; i++)
            {
                // For each slots
                //
                // Get the value of the slot
                string value = letters[i].Value;
                // Get the value on the keyboard
                Letter keyBoardValue = Keys.First(key => key.Value.ToUpper() == value);
                //
                // Check if the character exist in the word
                if (validWord.Contains(value.ToLower()))
                {
                    // Indicate the valid result on the line
                    letters[i].BackgroundColour = _almostValidColour;

                    // Indicate the valid result on the keyboard
                    keyBoardValue.BackgroundColour = _almostValidColour;
                }
                else
                {
                    letters[i].BackgroundColour = _emptyColour;
                    keyBoardValue.BackgroundColour = _emptyColour;
                }
                //
                // Check if the character is at the right place
                if (letters[i].Value.ToLower() == validWord[i].ToString())
                    // Indicate the exactly valid result on the line
                    letters[i].BackgroundColour = _validColour;
            }
        }

        /// <summary>
        /// Generate the values for each keys
        /// </summary>
        private void GenerateKeys()
        {
            // Instanciate the collection of keys
            Keys = new ObservableCollection<Letter>();

            // Generate keys
            for (int i = 0; i < _qwerty.Length; i++)
                Keys.Add(new Letter
                {
                    Value = _qwerty[i].ToString(),
                    BackgroundColour = _filledColour,
                });
        }
        /// <summary>
        /// Generate the colour resources
        /// </summary>
        private void GetResourceColours()
        {
            App.Current.Resources.TryGetValue("DarkButtonBrush", out var emptycolorvalue);
            App.Current.Resources.TryGetValue("DefaultButtonBrush", out var defaultcolorvalue);
            App.Current.Resources.TryGetValue("AlmostRightButtonBrush", out var almostRightcolorvalue);
            App.Current.Resources.TryGetValue("NailedButtonBrush", out var nailedcolorvalue);
            _emptyColour = emptycolorvalue as Color;
            _filledColour = defaultcolorvalue as Color;
            _almostValidColour = almostRightcolorvalue as Color;
            _validColour = nailedcolorvalue as Color;
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
        public void DisplaySuccessPopup()
        {
            var popup = new ResultPopUp();

            (CurrentApp.MainPage as AppShell).CurrentPage.ShowPopup(popup);
        }
    }
}
