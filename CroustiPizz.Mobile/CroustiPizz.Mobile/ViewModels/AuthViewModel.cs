using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CroustiPizz.Mobile.Dtos;
using CroustiPizz.Mobile.Dtos.Accounts;
using CroustiPizz.Mobile.Dtos.Authentications;
using CroustiPizz.Mobile.Dtos.Authentications.Credentials;
using CroustiPizz.Mobile.Services;
using Storm.Mvvm;
using Xamarin.Essentials;
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

        private string _email;

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _mdp;

        public string Mdp
        {
            get => _mdp;
            set => SetProperty(ref _mdp, value);
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
        
        private async void LoginAction()
        {
            IUserApiService service = DependencyService.Get<IUserApiService>();

            LoginWithCredentialsRequest utilisateur = new LoginWithCredentialsRequest()
            {
                Login = Email,
                Password = Mdp,
                ClientId = "MOBILE",
                ClientSecret = "UNIV"
            };
            
            Response<LoginResponse> response = await service.LoginUser(utilisateur);
            
            Console.WriteLine($"Appel HTTP : {response.IsSuccess}");
            
            if (response.IsSuccess)
            {
                Console.WriteLine($"Appel HTTP : {response.Data.AccessToken}");
            }
            else
            {
                Console.Write("    NE MARCHE PAS    ");
            }
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