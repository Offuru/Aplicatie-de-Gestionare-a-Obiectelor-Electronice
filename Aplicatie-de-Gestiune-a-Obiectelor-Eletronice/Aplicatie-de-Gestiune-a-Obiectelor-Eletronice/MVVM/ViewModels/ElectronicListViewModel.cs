using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Core;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Database.Repositories;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Models;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ElectronicObjectRepository ObjectRepository { get; set; }
        public RelayCommand NavigateToMenuCommand { get; set; }
        public RelayCommand AddItemCommand { get; set; }
        public RelayCommand MarkObjects {  get; set; }
        public RelayCommand UnMarkObjects {  get; set; }

        public ElectronicListViewModel(INavigationService navService, IItemsService itemsService
            , ElectronicObjectRepository electronicObjectRepository)
        {
            Navigation = navService;
            NavigateToMenuCommand = new RelayCommand(o => { Navigation.NavigateTo<MenuViewModel>(); }, o => true);
            ItemsService = itemsService;
            AddItemCommand = new RelayCommand(o => { ItemsService.AddItems(); }, o => true);
            ObjectRepository = electronicObjectRepository;
            MarkObjects = new RelayCommand(o => { ItemsService.MarkItems(); }, o => true);
            UnMarkObjects = new RelayCommand(o => { ItemsService.UnMarkItems(); }, o => true);

        }
    }
}
