using CommunityToolkit.Maui.Views;
using Mopups.Pages;
using Mopups.Services;
using Wordster.ViewModels.Popups;

namespace Wordster.Views.PopUps;

public partial class GiveUpPopUp : PopupPage
{
    private Action _retryCallback;
    public GiveUpPopUp(string word, Action retryCallback)
	{
		InitializeComponent();
		BindingContext = new ResultViewModel(word, false);

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