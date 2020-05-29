using MyVet.Common.Helpers;
using MyVet.Common.Models;
using MyVet.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;

namespace MyVet.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        /***** Orden
         Atributos privados
         Constructor
         Propiedaddes
         MetodosPublicos
         Metodos Privados
         *****/

        private string _Password;
        private bool _IsRuuning;
        private bool _IsEnabled;
        private DelegateCommand _loginComand;
        private DelegateCommand _registerComand;
        private DelegateCommand _forgotPasswordCommand;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        public LoginPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Login";
            IsEnable = true;
            IsRemember = true;

        }

        public string Email { get; set; }

        public string Password
        {
            get => _Password;
            set => SetProperty(ref _Password, value);
        }

        public bool IsRunning
        {
            get => _IsRuuning;
            set => SetProperty(ref _IsRuuning, value);
        }


        public bool IsEnable
        {
            get => _IsEnabled;
            set => SetProperty(ref _IsEnabled, value);
        }

        public DelegateCommand LoginComand =>
            _loginComand ?? (_loginComand = new DelegateCommand(LoginAsync));

        public DelegateCommand RegisterComand =>
       _registerComand ?? (_registerComand = new DelegateCommand(RegisterAsync));
        public DelegateCommand ForgotPasswordCommand =>
        _forgotPasswordCommand ?? (_forgotPasswordCommand = new DelegateCommand(ForgotPasswordAsync));

       
        public bool IsRemember { get; set; }

        private async void LoginAsync()
        {

            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an email", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an password", "Accept");
                return;
            }

            IsEnable = false;
            IsRunning = true;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnection(url);

            if (!connection)
            {
                IsEnable = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Check the internet connection.", "Accept");
                return;
            }

            var request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            var response = await _apiService.GetTokenAsync
                (url, "Account", "/CreateToken", request);


            if (!response.IsSuccess)
            {
                IsEnable = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Email or password incorrect.", "Accept");
                Password = string.Empty;
                return;
            }

            var token = response.Result;
            var response2 = await _apiService.GetOwnerByEmailAsync
                (url, "api", "/Owners/GetOwnerByEmail", "bearer", token.Token, Email);

            if (!response2.IsSuccess)
            {
                IsEnable = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", $"{response2.Message}" + ". This user have a big problem,call support", "Accept");
                Password = string.Empty;
                return;
            }

            var owner = response2.Result;

            Settings.Owner        = JsonConvert.SerializeObject(owner);
            Settings.Token        = JsonConvert.SerializeObject(token);
            Settings.IsRemembered = IsRemember;


            //var parameters = new NavigationParameters
            //{
            //    { "owner", owner }
            //};

            await _navigationService.NavigateAsync("/VeterinaryMasterDetailPage/NavigationPage/PetsPage");

            Password = string.Empty;
            IsEnable = true;
            IsRunning = false;


        }


        private async void RegisterAsync()
        {
            await _navigationService.NavigateAsync("RegisterPage");
        }

        private async void ForgotPasswordAsync()
        {
            await _navigationService.NavigateAsync("RememberPasswordPage");
        }




    }
}
