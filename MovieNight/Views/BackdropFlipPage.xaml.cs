using MovieNight.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MovieNight.Views
{
    public sealed partial class BackdropFlipPage : Page
    {
        private BackdropFlipViewModel ViewModel
        {
            get { return ViewModelLocator.Current.BackdropFlipViewModel; }
        }

        public BackdropFlipPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is ImageHolder ih)
            {
                ViewModel.Initialize(ih);
            }
        }
    }
}
