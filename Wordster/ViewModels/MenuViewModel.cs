using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordster.Views;

namespace Wordster.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {

        public MenuViewModel()
        {
        }

        // Command used to navigate to a page
        public Command<string> NavCommand
		{
			get { return new Command<string>(async (pageRoute) =>
			{
				switch (pageRoute)
				{
					case "Game":

                        await Shell.Current.GoToAsync("gamePage");
                        break;
					default:
						break;
				}
			}) ; }
		}

	}
}
