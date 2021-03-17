using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CroustiPizz.Mobile.ViewModels;
using Storm.Mvvm.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CroustiPizz.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PizzaDetailsPage : BaseContentPage
    {
        public PizzaDetailsPage()
        {
            BindingContext = new PizzaDetailsViewModel();
            InitializeComponent();
        }
    }
}