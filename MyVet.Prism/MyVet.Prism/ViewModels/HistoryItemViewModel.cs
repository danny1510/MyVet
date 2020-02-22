using MyVet.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyVet.Prism.ViewModels
{
    public class HistoryItemViewModel: HistoryResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selecthistorycommand;

        public HistoryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectHistoryCommand=> _selecthistorycommand ??(_selecthistorycommand= new DelegateCommand(SelectHistory));

        private async void SelectHistory()
        {
            var parameters = new NavigationParameters
            {
                { "history", this }
            };
            await _navigationService.NavigateAsync("HistoryPage", parameters);
        }







    }
}
