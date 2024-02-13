using CommunityToolkit.Maui.Views;
using Mopups.Pages;
using Mopups.Services;
using Wordster.ViewModels.Popups;

namespace Wordster.Views.PopUps;

public partial class ResultPopUp : PopupPage
{
    private Action _retryCallback;

    public ResultPopUp(string word, bool guessed, Action retryCallback)
	{
		InitializeComponent();
		BindingContext = new ResultViewModel(word, guessed);


        _retryCallback = retryCallback;
    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
        await MopupService.Instance.PopAllAsync();
    }

    private async void RetryButton_Clicked(object sender, EventArgs e)
    {
        // Launch the callback
        _retryCallback();

        // Remove the popup
        await MopupService.Instance.PopAllAsync();
    }
}