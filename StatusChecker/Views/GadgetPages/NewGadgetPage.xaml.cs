﻿using System;

using Xamarin.Forms;

using StatusChecker.Models.Database;

namespace StatusChecker.Views.GadgetPages
{
    public partial class NewGadgetPage : ContentPage
    {
        public Gadget Gadget { get; set; }

        public NewGadgetPage()
        {
            InitializeComponent();

            Gadget = new Gadget
            {
                Name = "",
                Location = "",
                IpAddress = "",
                Description = ""
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Gadget);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}