using System;
using System.Windows.Input;
using Storm.Mvvm;
using Xamarin.Forms;

namespace CroustiPizz.Mobile.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        public const string SELECTED_COLOR = "Black";
        public const string UNSELECTED_COLOR = "#BAC5DB";
        
        private string _loginTextColor;
        public string LoginTextColor
        {
            get => _loginTextColor;
            set => SetProperty(ref _loginTextColor, value);
        }
        
        private string _signupTextColor;
        public string SignUpTextColor
        {
            get => _signupTextColor;
            set => SetProperty(ref _signupTextColor, value);
        }
        
        private bool _showingLogin;
        public bool ShowingLogin
        {
            get => _showingLogin;
            set => SetProperty(ref _showingLogin, value);
        }

        private bool _showingSignup;
        public bool ShowingSignup
        {
            get => _showingSignup;
            set => SetProperty(ref _showingSignup, value);
        }
        public ICommand SignUpCommand { get; }
        public ICommand LoginCommand { get; }
        
        public ICommand ShowSignUpCommand { get; }
        public ICommand ShowLoginCommand { get; }
        
        public AuthViewModel()
        {
            ShowSignUpCommand = new Command(ShowSignUpAction);
            ShowLoginCommand = new Command(ShowLoginAction);

            SignUpCommand = new Command(SignUpAction);
            LoginCommand = new Command(LoginAction);

            ShowingLogin = true;
            ShowingSignup = !ShowingLogin;
            
            LoginTextColor = SELECTED_COLOR;
            SignUpTextColor = UNSELECTED_COLOR;
        }

        private void SignUpAction()
        {
            throw new NotImplementedException();
        }
        
        private void LoginAction()
        {
            throw new NotImplementedException();
        }

        private void ShowSignUpAction()
        {
            ShowingLogin = false;
            ShowingSignup = !ShowingLogin;

            LoginTextColor = UNSELECTED_COLOR;
            SignUpTextColor = SELECTED_COLOR;
        }
        
        private void ShowLoginAction()
        {
            ShowingLogin = true;
            ShowingSignup = !ShowingLogin;
            
            LoginTextColor = SELECTED_COLOR;
            SignUpTextColor = UNSELECTED_COLOR;
        }
    }
}