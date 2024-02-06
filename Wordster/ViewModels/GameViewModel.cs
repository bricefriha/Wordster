
using CommunityToolkit.Maui.Views;
using Mopups.Services;
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
        private readonly Command<string> _addLetterCommand;

        public Command<string> AddLetterCommand
        {
            get 
            { 
                return _addLetterCommand; 
            }
        }
        private readonly Command<string> _removeLetterCommand;

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
        private readonly Command _giveUpCommand;

        public Command GiveUpCommand
        {
            get 
            { 
                return _giveUpCommand; 
            }
        }

        private readonly Action _retryAction;
        private Color _emptyColour ;
        private Color _filledColour ;
        private Color _almostValidColour;
        private Color _validColour;
        private string validWord = "fault";

        public GameViewModel()
        {
            CurrentApp = (App)App.Current;

            // Initiate components etc
            Initialisation();

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
                CheckTheCurrentWord();

            });
            _giveUpCommand = new Command(() =>
            {
                DisplayGiveUpPopup();
            });
            _retryAction = () => GenerateNewGame();
        }

        /// <summary>
        /// Initiate data the gameplay need
        /// </summary>
        private void Initialisation()
        {
            // Set the colours
            GetResourceColours();

            // Initialise the slots
            InitialiseSlots();

            // Set data of the keyboard
            GenerateKeys();
        }

        /// <summary>
        /// Prepare or reset the board
        /// </summary>
        private void InitialiseSlots()
        {
            // Intanciation of slots
            if (_slots == null)
                _slots = new ObservableCollection<Slot>();

            // Reset line
            _currentLine = 0;

            // make sure the slots are empty
            Slots.Clear();

            // Create/recreate the slots
            for (int i = 0; i < slotCount; i++)
            {
                // Generate a slot
                Slot slot = new()
                {
                    Letters = new ObservableCollection<Letter>()
                };

                // Create a placement for each slots
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
        }
        /// <summary>
        /// Rest the keyboard
        /// </summary>
        private void ResetKeyboard()
        {
            // Go through all the letters
            for (int i = 0; i < _keys.Count(); i++)
                // Get the value on the keyboard
                Keys[i].BackgroundColour = _filledColour;
        }
        /// <summary>
        /// Initiate a new game
        /// </summary>
        private void GenerateNewGame()
        {
            // Genearate a new word
            //
            // Code for it
            // 

            // Reset the Keyboard
            ResetKeyboard();

            // Reset the slots
            InitialiseSlots();
        }
        /// <summary>
        /// TO check if the current word is correct
        /// </summary>
        private void CheckTheCurrentWord()
        {
            CheckAttempt();

            if (CheckIfWordFound())
                DisplaySuccessPopup();
            else
                // Go to next line
                if (++_currentLine == slotCount)
                DisplayFailPopup();
        }

        private bool CheckIfWordFound()
        {
            bool result = true;

            ObservableCollection<Letter> letters = Slots[_currentLine].Letters;
            for (int i = 0; i < letters.Count && result; i++)
                if (letters[i].BackgroundColour != _validColour)
                    result = false;

            return result;
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

        /// <summary>
        /// Show the success pop up
        /// </summary>
        public void DisplaySuccessPopup()
        {
            MopupService.Instance.PushAsync(new ResultPopUp(validWord, true, _retryAction));
        }
        /// <summary>
        /// Show the popup in case of a failure
        /// </summary>
        public void DisplayFailPopup()
        {
            MopupService.Instance.PushAsync(new ResultPopUp(validWord, false, _retryAction));
        }
        /// <summary>
        /// Show a pop up when giving up that display the word
        /// </summary>
        public void DisplayGiveUpPopup()
        {
            MopupService.Instance.PushAsync(new GiveUpPopUp(validWord, _retryAction));
        }
    }
}
