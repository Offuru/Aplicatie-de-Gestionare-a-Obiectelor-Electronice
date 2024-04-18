using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Core;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.ViewModels
{
    class ElectronicListViewModel : Core.ViewModel
    {
        private INavigationService _navigation;
        public IItemsService ItemsService { get; private set; }

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToMenuCommand { get; set; }
        public RelayCommand AddItemCommand { get; set; }

        public ElectronicListViewModel(INavigationService navService, IItemsService itemsService)
        {
            Navigation = navService;
            NavigateToMenuCommand = new RelayCommand(o => { Navigation.NavigateTo<MenuViewModel>(); }, o => true);
            ItemsService = itemsService;
            AddItemCommand = new RelayCommand(o => { ItemsService.AddItems(); }, o => true);
        }
    }
}
