﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Toolkit.Uwp.UI.Animations;

using MovieNight.Services;
using MovieNight.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MovieNight.Views
{
    public sealed partial class TV_ShowsSeasonDetailPage : Page
    {
        private TV_ShowsSeasonDetailViewModel ViewModel
        {
            get { return ViewModelLocator.Current.TV_ShowsSeasonDetailViewModel; }
        }

        public NavigationServiceEx NavigationService => ViewModelLocator.Current.NavigationService;

        public TV_ShowsSeasonDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Holder h)
            {
                ViewModel.Initialize(h.tv_id, h.season_number, h.showName);
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