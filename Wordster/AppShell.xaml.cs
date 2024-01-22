using Wordster.Views;

namespace Wordster
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("gamePage", typeof(GamePage));
        }
    }
}