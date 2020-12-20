﻿using Xamarin.Essentials;
using Xamarin.Forms;

namespace StatusChecker.Views
{
    public partial class AppInfoPage : ContentPage
    {
        public AppInfoPage()
        {
            InitializeComponent();

            VersionTracking.Track();

            _lblVersionInfo.Text = $"StatusChecker v{ VersionTracking.CurrentVersion }";
        }
    }
}
