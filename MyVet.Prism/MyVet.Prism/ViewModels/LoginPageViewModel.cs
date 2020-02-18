using MyVet.Common.Models;
using MyVet.Common.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private bool _IsRuuning ;
        private bool _IsEnable;
        private DelegateCommand _LoginComand;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        public LoginPageViewModel(
            INavigationService navigationService,
            IApiService apiService): base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Login";
            _IsEnable = true;

            //TODO:Delete this lineas
            Email="valenm@gmail.com";
            Password = "123456";
        }


        public string Email { get; set; }

        public string Password { 
            get=>_Password;
            set=>SetProperty(ref _Password,value) ; 
        }

        public bool IsRunning {
            get=>_IsRuuning; 
            set=>SetProperty(ref _IsRuuning, value); 
        }


        public bool IsEnable
        {
            get => _IsEnable;
            set => SetProperty(ref _IsEnable, value);
        }

        public DelegateCommand LoginComand =>
            _LoginComand ?? (_LoginComand = new DelegateCommand(Login));


        private async void Login() {

            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error","You must enter an email","Accept");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an password", "Accept");
                return;
            }
            IsEnable = false;
            IsRunning = true;

            var request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
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

            var token = (TokenResponse)response.Result;
            var response2 = await _apiService.GetOwnerByEmailAsync(url, "api", "/Owners/GetOwnerByEmail", "bearer", token.Token, Email);

            if (!response.IsSuccess)
            {
                IsEnable = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "This user have a big problem,call support", "Accept");
                Password = string.Empty;
                return;
            }

            var owner = (OwnerResponse)response2.Result;
            var parameters = new NavigationParameters
            {
                { "owner", owner }
            };

            await _navigationService.NavigateAsync("PetsPage", parameters);
            IsEnable = true;
            IsRunning = false;
            Password = string.Empty;

        }


















    }
}
