using Xamarin.Forms;

using StatusChecker.Models.Database;
using StatusChecker.ViewModels.Gadgets;
using StatusChecker.DataStore.Interfaces;
using StatusChecker.I18N;

namespace StatusChecker.Views.GadgetPages
{
    public partial class GadgetDetailPage : ContentPage
    {
        #region Fields
        private IDataStore<Gadget> _dataStore => DependencyService.Get<IDataStore<Gadget>>();

        private readonly GadgetDetailViewModel viewModel;
        #endregion


        #region Construction
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
        #endregion


        #region View Events
        /// <summary>
        /// Triggered when clicked on Delete-Button
        /// Deletes the Item after approval
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RemoveGadget_Clicked(object sender, System.EventArgs e)
        {
            bool deleteRequestApproval = await DisplayAlert(
                                               string.Format(AppTranslations.Page_GadgetDetail_Delete_Alert_Title_Delete, viewModel.Gadget.Name),
                                               AppTranslations.Page_GadgetDetail_Delete_Alert_Approve_Message,
                                               AppTranslations.Page_GadgetDetail_Delete_Alert_Approve_Yes,
                                               AppTranslations.Page_GadgetDetail_Delete_Alert_Approve_No);

            if(deleteRequestApproval)
            {
                await _dataStore.DeleteAsync(viewModel.Gadget.Id);

                Application.Current.MainPage = new MainPage();
            }
        }

        /// <summary>
        /// Opens EditView for the current Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void EditGadget_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new EditGadgetPage(viewModel));
        }
        #endregion
    }
}