using Wordster.ViewModels;

namespace Wordster.Views;

public partial class GamePage : ContentPage
{
	public GamePage()
	{
		InitializeComponent();
		BindingContext = new GameViewModel();

    }
}