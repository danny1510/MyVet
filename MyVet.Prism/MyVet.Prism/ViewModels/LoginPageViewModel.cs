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
        }


        public string Email { get; set; }

        public string Password { 
            get=>_Password;
            set=>SetProperty(ref _Password,value) ; 
        }

        public bool IsRuuning {
            get=>_IsRuuning; 
            set=>SetProperty(ref _IsRuuning,value); 
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
            IsRuuning = true;

            var request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            var url = App.Current.Resources["UrlAPI"].ToString();
            var response = await _apiService.GetTokenAsync
                (url, "Account", "/CreateToken", request);

            IsEnable = true;
            IsRuuning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Email or password incorrect.", "Accept");
                Password = string.Empty;
                return;
            }
             await _navigationService.NavigateAsync("PetsPage");

        }


















    }
}
