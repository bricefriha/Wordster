using CommunityToolkit.Maui.Views;
using Mopups.Pages;
using Mopups.Services;
using Wordster.ViewModels.Popups;

namespace Wordster.Views.PopUps;

public partial class GiveUpPopUp : PopupPage
{
	public GiveUpPopUp(string word)
	{
		InitializeComponent();
		BindingContext = new ResultViewModel(word, false);

    }

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
        await MopupService.Instance.PopAllAsync();
    }
}