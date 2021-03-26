using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Rg.Plugins.Popup.Services;
using Storm.Mvvm;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.ViewModels
{
    public class PizzaDetailsViewModel : ViewModelBase
    {
        private string _pizzaName;

        public string PizzaName
        {
            get => _pizzaName;
            set => SetProperty(ref _pizzaName, value);
        }

        private string _pizzaDescription;

        public string PizzaDescription
        {
            get => _pizzaDescription;
            set => SetProperty(ref _pizzaDescription, value);
        }

        private double _pizzaPrice;

        public double PizzaPrice
        {
            get => _pizzaPrice;
            set => SetProperty(ref _pizzaPrice, value);
        }

        private string _pizzaPhoto;

        public string PizzaPhoto
        {
            get => _pizzaPhoto;
            set => SetProperty(ref _pizzaPhoto, value);
        }

        public ICommand BackCommand { get; }

        public PizzaDetailsViewModel(Dictionary<string, object> dico)
        {
            BackCommand = new Command(BackAction);
            PizzaName = dico["PizzaName"] as string;
            PizzaDescription = dico["PizzaDescription"] as string;
            PizzaPhoto = dico["PizzaPhoto"] as string;
            PizzaPrice = dico["PizzaPrice"] is double ? (double) dico["PizzaPrice"] : 0;
        }

        public override async Task OnResume()
        {
            await base.OnResume();
        }

        private void BackAction()
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}