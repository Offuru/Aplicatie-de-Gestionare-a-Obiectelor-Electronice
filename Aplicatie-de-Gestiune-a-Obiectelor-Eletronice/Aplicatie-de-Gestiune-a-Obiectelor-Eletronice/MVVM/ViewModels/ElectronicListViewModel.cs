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
        public RelayCommand RefreshTableCommand { get; set; }

        public ObservableCollection<ElectronicObject> ElectronicObjects{ get; set; }

        void RefreshTable()
        {
            ElectronicObjects.Clear();

            foreach(var obj in ObjectRepository.GetAll())
            {
                ElectronicObjects.Add(obj);
            }
        }

        public ElectronicListViewModel(INavigationService navService, IItemsService itemsService
            , ElectronicObjectRepository electronicObjectRepository)
        {
            Navigation = navService;
            NavigateToMenuCommand = new RelayCommand(o => { Navigation.NavigateTo<MenuViewModel>(); }, o => true);
            ItemsService = itemsService;
            AddItemCommand = new RelayCommand(o => { ItemsService.AddItems(); }, o => true);
            RefreshTableCommand = new RelayCommand(o => { RefreshTable(); }, o => true);
            ObjectRepository = electronicObjectRepository;

            ElectronicObjects = new ObservableCollection<ElectronicObject>();
            RefreshTable();
        }
    }
}
