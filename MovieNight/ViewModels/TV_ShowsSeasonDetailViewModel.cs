using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MovieNight.Core.Models;
using MovieNight.Core.Services;
using MovieNight.Services;

namespace MovieNight.ViewModels
{
    public class TV_ShowsSeasonDetailViewModel : ViewModelBase
    {
        private TVShowSeason tvshowseason = new TVShowSeason();

        public TVShowSeason TVShowSeason
        {
            get { return tvshowseason; }
            set { Set(ref tvshowseason, value); }
        }

        public delegate void loadCompleted();
        public event loadCompleted LoadCompleted;

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        private ICommand _itemClickCommandCast;

        public ICommand ItemClickCommandCast => _itemClickCommandCast ?? (_itemClickCommandCast = new RelayCommand<Cast>(OnItemClick));

        private ICommand _itemClickCommandCrew;

        public ICommand ItemClickCommandCrew => _itemClickCommandCrew ?? (_itemClickCommandCrew = new RelayCommand<Crew>(OnItemClick));

        private ICommand _itemClickCommandPoster;

        public ICommand ItemClickCommandPoster => _itemClickCommandPoster ?? (_itemClickCommandPoster = new RelayCommand<Poster>(OnItemClick));

        public ObservableCollection<Poster> PosterSource { get; set; }

        public ObservableCollection<Cast> CastSource { get; set; }

        public ObservableCollection<Crew> CrewSource { get; set; }

        public ObservableCollection<Episode> EpisodeSource { get; set; }

        private TVShowSeason _item;

        public TVShowSeason Item
        {
            get { return _item; }
            set { Set(ref _item, value); }
        }

        public TV_ShowsSeasonDetailViewModel()
        {
            PosterSource = new ObservableCollection<Poster>();
            EpisodeSource = new ObservableCollection<Episode>();
            CastSource = new ObservableCollection<Cast>();
            CrewSource = new ObservableCollection<Crew>();
        }

        async Task LoadTVShowSeason(int tv_id, int season_number, string showName)
        {
            Item = await Task.Run(() => TMDbService.GetDetailedTVShowSeasonAsync(tv_id, season_number, showName));
            Item.poster_path = "";

            PosterSource.Clear();
            foreach (var posterItem in Item.images.posters)
                PosterSource.Add(posterItem);

            CastSource.Clear();
            foreach (var castItem in Item.credits.cast)
                CastSource.Add(castItem);

            CrewSource.Clear();
            foreach (var crewItem in Item.credits.crew)
                CrewSource.Add(crewItem);

            EpisodeSource.Clear();
            foreach (var episodeItem in Item.episodes)
                EpisodeSource.Add(episodeItem);

            LoadCompleted();
        }

        public void Initialize(int tv_id, int season_number, string showName)
        {
            _ = LoadTVShowSeason(tv_id, season_number, showName);
        }

        private void OnItemClick(Cast clickedItem)
        {
            if (clickedItem != null)
            {
                //NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(PeopleDetailViewModel).FullName, clickedItem.id);
            }
        }

        private void OnItemClick(Crew clickedItem)
        {
            if (clickedItem != null)
            {
                //NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(PeopleDetailViewModel).FullName, clickedItem.id);
            }
        }

        private void OnItemClick(Poster clickedItem)
        {
            if (clickedItem != null)
            {
                int idx = 0;
                foreach (Poster p in PosterSource)
                {
                    if (clickedItem.file_path == p.file_path)
                    {
                        break;
                    }
                    idx++;
                }
                ImageHolder ih = new ImageHolder
                {
                    Posters = PosterSource,
                    selectedIndex = idx
                };

                //NavigationService.Frame.SetListDataItemForNextConnectedAnimation(clickedItem);
                NavigationService.Navigate(typeof(PosterFlipViewModel).FullName, ih);
            }
        }
    }
}
