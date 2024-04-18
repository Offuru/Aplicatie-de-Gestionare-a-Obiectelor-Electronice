using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Core;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.ViewModels
{
    class MenuViewModel : Core.ViewModel
    {
        private INavigationService _navigation;
        public IItemsService ItemsService { get; set; }

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToElectronicOverviewCommand { get; set; }
        public RelayCommand NavigateToElectronicListCommand { get; set; }
        public RelayCommand NavigateToFormCommand { get; set; }
        public RelayCommand AddItemCommand {  get; set; }

        public string Name {  get; set; }

        public MenuViewModel(INavigationService navService, IItemsService itemsService)
        {
            Navigation = navService;
            ItemsService = itemsService;
            AddItemCommand = new RelayCommand(o => { ItemsService.AddItems(); }, o => true);
            NavigateToElectronicOverviewCommand = new RelayCommand(o => { Navigation.NavigateTo<ElectronicOverviewViewModel>(); }, o => true);
            NavigateToElectronicListCommand = new RelayCommand(o => { Navigation.NavigateTo<ElectronicListViewModel>(); }, o => true);
            NavigateToFormCommand = new RelayCommand(o => { Navigation.NavigateTo<FormViewModel>(); }, obj => true);
        }
    }
}
