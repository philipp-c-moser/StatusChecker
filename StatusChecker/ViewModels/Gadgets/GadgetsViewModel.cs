﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using StatusChecker.Models;
using StatusChecker.Models.Database;
using StatusChecker.Views.GadgetPages;
using Microsoft.AppCenter.Crashes;
using System.Collections.Generic;

namespace StatusChecker.ViewModels.Gadgets
{
    public class GadgetsViewModel : BaseViewModel
    {
        public ObservableCollection<GadgetViewModel> Gadgets { get; set; }
        public Command LoadItemsCommand { get; set; }

        public GadgetsViewModel()
        {
            Title = "Übersicht";
            Gadgets = new ObservableCollection<GadgetViewModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewGadgetPage, Gadget>(this, "AddItem", async (obj, gadget) =>
            {
                var newGadget = gadget;

                Gadgets.Add(new GadgetViewModel {
                       Id = gadget.Id,
                       Name = gadget.Name,
                       Location = gadget.Location,
                       IpAddress = gadget.IpAddress,
                       Description = gadget.Description
                });

                

                await _gadgetDataStore.AddAsync(newGadget);
            });
        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Gadgets.Clear();
                var gadgets = await _gadgetDataStore.GetAllAsync(true);
                foreach (var gadget in gadgets)
                {
                    GadgetStatus gadgetStatus = await _webRequestService.GetStatusAsync(gadget.IpAddress);

                    var statusIndicatorColor = GetStatusIndicatorColor(gadgetStatus);

                    if(gadgetStatus == null)
                    {
                        gadgetStatus = new GadgetStatus
                        {
                            temperature = 0.00,
                            overtemperature = false,
                            temperature_status = "undefined",
                            mac = "",
                            voltage = 0.00
                        };                       
                    }

                    var viewModel = new GadgetViewModel
                    {
                        Id = gadget.Id,
                        DeviceId = gadgetStatus.mac,
                        Name = gadget.Name,
                        Location = gadget.Location,
                        IpAddress = gadget.IpAddress,
                        Description = gadget.Description,
                        IsStatusOk = gadgetStatus.temperature_status == "Normal",
                        StatusIndicatorColor = statusIndicatorColor.ToString(),
                        TemperatureStatus = gadgetStatus.temperature_status,
                        Temperature = gadgetStatus.temperature,
                        TemperatureC = $"{gadgetStatus.temperature} °C",
                        Voltage = $"{ gadgetStatus.voltage } V"
                    };



                    Gadgets.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                 var properties = new Dictionary<string, string> {
                    { "Method", "ExecuteLoadItemsCommand" },
                    { "Event", "Could not Add GadgetViewModel" }
                };

                if(App.PermissionTrackErrors)
                {
                    Crashes.TrackError(ex, properties);
                }
                

                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private StatusIndicatorColors GetStatusIndicatorColor(GadgetStatus gadgetStatus)
        {
            if (gadgetStatus == null) return StatusIndicatorColors.Red;

            if(gadgetStatus.overtemperature == false && gadgetStatus.temperature <= 90.00 && gadgetStatus.voltage <= 250.00)
            {
                return StatusIndicatorColors.Green;
            }

            return StatusIndicatorColors.Red;

        }
    }
}