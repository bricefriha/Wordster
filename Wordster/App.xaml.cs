using Wordster.Services;

namespace Wordster
{
    public partial class App : Application
    {
        public Fetcher DataFetcher { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            DataFetcher = new Fetcher();
        }
    }
}