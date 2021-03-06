using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Uwp.UI.Animations;
using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.Services;

namespace MovieNight.ViewModels
{
    public class ImageHolder
    {
        public ObservableCollection<Poster> Posters = new ObservableCollection<Poster>();
        public ObservableCollection<Backdrop> Backdrops = new ObservableCollection<Backdrop>();
        public ObservableCollection<Profile> Profiles = new ObservableCollection<Profile>();
        public ObservableCollection<TaggedImagesResult> TaggedImages = new ObservableCollection<TaggedImagesResult>();
        public int selectedIndex = 0;
    }

    public class MoviesDetailViewModel : ViewModelBase
    {
        private Film film = new Film();

        public Film Film
        {
            get { return film; }
            set { Set(ref film, value); }
        }

        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommandCollection;

        public ICommand ItemClickCommandCollection => _itemClickCommandCollection ?? (_itemClickCommandCollection = new RelayCommand<Part>(OnItemClick));

        private ICommand _itemClickCommandCast;

        public ICommand ItemClickCommandCast => _itemClickCommandCast ?? (_itemClickCommandCast = new RelayCommand<Cast>(OnItemClick));

        private ICommand _itemClickCommandCrew;

        public ICommand ItemClickCommandCrew => _itemClickCommandCrew ?? (_itemClickCommandCrew = new RelayCommand<Crew>(OnItemClick));

        private ICommand _itemClickCommandRecommendations;

        public ICommand ItemClickCommandRecommendations => _itemClickCommandRecommendations ?? (_itemClickCommandRecommendations = new RelayCommand<Result1>(OnItemClick));

        private ICommand _itemClickCommandPoster;

        public ICommand ItemClickCommandPoster => _itemClickCommandPoster ?? (_itemClickCommandPoster = new RelayCommand<Poster>(OnItemClick));

        private ICommand _itemClickCommandImage;

        public ICommand ItemClickCommandImage => _itemClickCommandImage ?? (_itemClickCommandImage = new RelayCommand<Backdrop>(OnItemClick));

        public ObservableCollection<Poster> PosterSource { get; set; }
        public ObservableCollection<Backdrop> BackdropSource { get; set; }
        public ObservableCollection<ReviewResult> ReviewSource { get; set; }
        public ObservableCollection<Part> CollectionSource { get; set; }

        public ObservableCollection<Cast> CastSource { get; set; }

        public ObservableCollection<Crew> CrewSource { get; set; }

        public ObservableCollection<Result1> RecommendationsSource { get; set; }

        private Film _item;

        public Film Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public MoviesDetailViewModel()
        {
            PosterSource = new ObservableCollection<Poster>();
            BackdropSource = new ObservableCollection<Backdrop>();
            ReviewSource = new ObservableCollection<ReviewResult>();
            CollectionSource = new ObservableCollection<Part>();
            CastSource = new ObservableCollection<Cast>();
            CrewSource = new ObservableCollection<Crew>();
            RecommendationsSource = new ObservableCollection<Result1>();
        }

        async Task LoadMovie(int id)
        {
            Item = await TMDbService.GetDetailedFilmAsync(id);
            Item.poster_path = "";
            Item.backdrop_path = "";

            PosterSource.Clear();
            foreach (var posterItem in Item.images.posters)
                PosterSource.Add(posterItem);

            BackdropSource.Clear();
            foreach (var backdropItem in Item.images.backdrops)
                BackdropSource.Add(backdropItem);

            ReviewSource.Clear();
            foreach (var reviewItem in Item.reviews.results)
                ReviewSource.Add(reviewItem);

            CollectionSource.Clear();
            if (Item.belongs_to_collection != null && Item.collection_films != null)
            {
                foreach (var collectionItem in Item.collection_films)
                    CollectionSource.Add(collectionItem);
            }

            RecommendationsSource.Clear();
            foreach (var recommendationItem in Item.recommendations.results)
                RecommendationsSource.Add(recommendationItem);

            CastSource.Clear();
            foreach (var castItem in Item.credits.cast)
                CastSource.Add(castItem);

            CrewSource.Clear();
            foreach (var crewItem in Item.credits.crew)
                CrewSource.Add(crewItem);

            LoadCompleted();
        }

        public void Initialize(int id)
        {
            _ = LoadMovie(id);
        }

        private void OnItemClick(Part clickedItem)
        {
            if (clickedItem != null /*&& clickedItem.id != Item.id*/)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(MoviesDetailViewModel).FullName, clickedItem.id);
            }
        }

        private void OnItemClick(Cast clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(PeopleDetailViewModel).FullName, clickedItem.id);
            }
        }

        private void OnItemClick(Crew clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(PeopleDetailViewModel).FullName, clickedItem.id);
            }
        }

        private void OnItemClick(Result1 clickedItem)
        {
            if (clickedItem != null)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(MoviesDetailViewModel).FullName, clickedItem.id);
            }
        }

        private void OnItemClick(Poster clickedItem)
        {
            if (clickedItem != null)
            {
                int idx = 0;
                foreach(Poster p in PosterSource)
                {
                    if(clickedItem.file_path == p.file_path)
                    {
                        break;
                    }
                    idx++;
                }
                ImageHolder ih = new ImageHolder();
                ih.Posters = PosterSource;
                ih.selectedIndex = idx;

                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(PosterFlipViewModel).FullName, ih);
            }
        }

        private void OnItemClick(Backdrop clickedItem)
        {
            if (clickedItem != null)
            {
                int idx = 0;
                foreach (Backdrop b in BackdropSource)
                {
                    if (clickedItem.file_path == b.file_path)
                    {
                        break;
                    }
                    idx++;
                }
                ImageHolder ih = new ImageHolder();
                ih.Backdrops = BackdropSource;
                ih.selectedIndex = idx;

                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(BackdropFlipViewModel).FullName, ih);
            }
        }
    }
}
