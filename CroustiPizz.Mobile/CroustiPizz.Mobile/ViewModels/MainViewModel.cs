using System;
using System.Threading.Tasks;
using Storm.Mvvm;

namespace CroustiPizz.Mobile.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private int _selectedViewModelIndex;

        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set => SetProperty(ref _selectedViewModelIndex, value);
        }

        public MainViewModel()
        {
        }
        
    }
}