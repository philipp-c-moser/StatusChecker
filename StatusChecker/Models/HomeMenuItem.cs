﻿namespace StatusChecker.Models
{
    public enum MenuItemType
    {
        Browse
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
