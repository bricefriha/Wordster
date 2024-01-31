using CommunityToolkit.Maui.Views;
using Mopups.Pages;
using Wordster.ViewModels.Popups;

namespace Wordster.Views.PopUps;

public partial class ResultPopUp : PopupPage
{
	public ResultPopUp(string word, bool guessed)
	{
		InitializeComponent();
		BindingContext = new ResultViewModel(word, guessed);

    }
}