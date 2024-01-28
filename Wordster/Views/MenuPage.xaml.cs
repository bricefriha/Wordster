using Wordster.ViewModels;

namespace Wordster.Views;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
		BindingContext = new MenuViewModel();

    }
}