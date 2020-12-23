using Xamarin.Forms;

using StatusChecker.Models.Database;
using StatusChecker.ViewModels.Gadgets;
using StatusChecker.DataStore.Interfaces;

namespace StatusChecker.Views.GadgetPages
{
    public partial class GadgetDetailPage : ContentPage
    {
        private IDataStore<Gadget> _dataStore => DependencyService.Get<IDataStore<Gadget>>();

        private readonly GadgetDetailViewModel viewModel;

        public GadgetDetailPage(GadgetDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public GadgetDetailPage()
        {
            InitializeComponent();

            var gadget = new GadgetViewModel
            {
                Name = "",
                IpAddress = ""
            };

            viewModel = new GadgetDetailViewModel(gadget);
            BindingContext = viewModel;
        }

        private async void RemoveGadget_Clicked(object sender, System.EventArgs e)
        {
            await _dataStore.DeleteAsync(viewModel.Gadget.Id);
            await Navigation.PushAsync(new GadgetsPage());
        }

        private async void EditGadget_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new EditGadgetPage(viewModel));
        }
    }
}