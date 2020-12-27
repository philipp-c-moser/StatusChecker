﻿using System.Collections.Generic;

using Xamarin.Forms;

using StatusChecker.Models;

namespace StatusChecker.Views
{
    public partial class MenuPage : ContentPage
    {
        private MainPage RootPage { get => Application.Current.MainPage as MainPage; }

        private readonly List<HomeMenuItem> menuItems;

        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem { Id = MenuItemType.GadgetOverview, Title = "Geräte" },
                new HomeMenuItem { Id = MenuItemType.Setting, Title = "Einstellungen" },
                new HomeMenuItem { Id = MenuItemType.AppInfo, Title = "Info" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null) return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;

                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}