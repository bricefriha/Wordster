using CommunityToolkit.Maui.Views;
using Mopups.Pages;
using Mopups.Services;
using Wordster.ViewModels.Popups;

namespace Wordster.Views.PopUps;

public partial class ResultPopUp : PopupPage
{
	public ResultPopUp(string word, bool guessed)
	{
		InitializeComponent();
		BindingContext = new ResultViewModel(word, guessed);

    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
        await MopupService.Instance.PopAllAsync();
    }
}