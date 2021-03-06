using Microsoft.Toolkit.Uwp.UI.Animations;
using MovieNight.Services;
using MovieNight.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MovieNight.Views
{
    public sealed partial class Popular_MoviesDetailPage : Page
    {
        private MoviesDetailViewModel ViewModel
        {
            get { return ViewModelLocator.Current.Popular_MoviesDetailViewModel; }
        }

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public Popular_MoviesDetailPage()
        {
            InitializeComponent();
            ViewModel.LoadCompleted += ViewModel_LoadCompleted;
        }

        private void ViewModel_LoadCompleted()
        {
            if (ViewModel.Item.isAdult.Equals("Visible"))
            {
                adult.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            if(ViewModel.RecommendationsSource.Count < 1)
            {
                pivot.Items.Remove(recommendationsPivot);
                pivot.Items.Remove(sep1);
            }
            if (ViewModel.ReviewSource.Count < 1)
            {
                pivot.Items.Remove(reviewsPivot);
                pivot.Items.Remove(sep2);
            }
            if (ViewModel.CollectionSource.Count < 1)
            {
                pivot.Items.Remove(collectionPivot);
                pivot.Items.Remove(sep3);
            }
            else
            {
                collectionPivot.Header = ViewModel.Item.belongs_to_collection.name;
            }
            if (ViewModel.CastSource.Count < 1)
            {
                pivot.Items.Remove(castPivot);
                pivot.Items.Remove(sep4);
            }
            if (ViewModel.CrewSource.Count < 1)
            {
                pivot.Items.Remove(crewPivot);
                pivot.Items.Remove(sep5);
            }
            if (ViewModel.PosterSource.Count < 1)
            {
                pivot.Items.Remove(postersPivot);
                pivot.Items.Remove(sep6);
            }
            if (ViewModel.BackdropSource.Count < 1)
            {
                pivot.Items.Remove(imagesPivot);
                pivot.Items.Remove(sep7);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is int id)
            {
                ViewModel.Initialize(id);
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }
}
