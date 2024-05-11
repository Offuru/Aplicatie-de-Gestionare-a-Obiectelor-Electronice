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
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.ViewModels
{
    class ElectronicOverviewViewModel : Core.ViewModel
    {
        private INavigationService _navigation;
        public ItemsService ItemsService { get; set; }

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public List<String> ObjectTypesList { get; set; }
        public List<String> DestinationTypesList { get; set; }
        public List<String> RoomsTypeList { get; set; }

        public string DestinationName
        {
            get
            {
                return ItemsService.ElectronicObject.ReceiverName;
            }
            set
            {
                ItemsService.ElectronicObject.ReceiverName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentRoomRecomandation));

            }
        }

        public string AutofillRoom
        {
            get => DestinationName;
            set
            {
                if (Int16.TryParse(value, out _))
                    DestinationName = _currentRoomRecomandation[Int16.Parse(value)];
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentRoomRecomandation));
            }
        }

        private ObservableCollection<string> _currentRoomRecomandation = new ObservableCollection<string>();
        public ObservableCollection<string> CurrentRoomRecomandation
        {
            get
            {
                _currentRoomRecomandation.Clear();

                foreach (string room in RoomsTypeList)
                {
                    if (room.Contains(DestinationName, StringComparison.OrdinalIgnoreCase))
                        _currentRoomRecomandation.Add(room);
                }
                return _currentRoomRecomandation;
            }
            set { _currentRoomRecomandation = value; }
        }

        public string ObjectCode
        {
            get => ItemsService.ElectronicObject.Code;
            set
            {
                ItemsService.ElectronicObject.Code = value;
                OnPropertyChanged();
            }
        }

        public string ObjectOrder
        {
            get => ItemsService.ElectronicObject.Order;
            set
            {
                ItemsService.ElectronicObject.Order = value;
                OnPropertyChanged();
            }
        }

        public string ObjectReceiptNumber
        {
            get => ItemsService.ElectronicObject.ReceiptNumber;
            set
            {
                ItemsService.ElectronicObject.ReceiptNumber = value;
                OnPropertyChanged();
            }
        }

        public string ObjectDate
        {
            get => ItemsService.ElectronicObject.Date;
            set
            {
                ItemsService.ElectronicObject.Date = value;
                OnPropertyChanged();
            }
        }

        public string ObjectName
        {
            get => ItemsService.ElectronicObject.Name;
            set
            {
                ItemsService.ElectronicObject.Name = value;
                OnPropertyChanged();
            }
        }

        public string ObjectSerial
        {
            get => ItemsService.ElectronicObject.Serial;
            set
            {
                ItemsService.ElectronicObject.Serial = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand NavigateToMenuCommand { get; set; }
        public RelayCommand NavigateToListCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        private void ResetFields()
        {
            ItemsService.CurrentObjectType = "Obiecte de inventar";
            ItemsService.CurrentDestinationType = "Student";
            AutofillRoom = "";
            DestinationName = "";
            ObjectCode = "";
            ObjectDate = "";
            ObjectName = "";
            ObjectSerial = "";
            ObjectOrder = "";
            ObjectReceiptNumber = "";
        }

        public ElectronicOverviewViewModel(INavigationService navService, IItemsService itemsService
            , ElectronicObjectRepository electronicObjectRepository)
        {
            Navigation = navService;
            ItemsService = itemsService as ItemsService;

            ItemsService.CurrentObjectType = "Obiecte de inventar";
            ItemsService.CurrentDestinationNameTBlock = "Nume student";
            ObjectTypesList = new List<string> { "Obiecte de inventar", "Mijloace fixe" };
            DestinationTypesList = new List<string> { "Student", "Doctorand", "Cadru didactic", "Sala" };

            RoomsTypeList = ElectronicObject.Classrooms;
            AutofillRoom = "";
            CurrentRoomRecomandation = new ObservableCollection<string>();


            NavigateToMenuCommand = new RelayCommand(o => { Navigation.NavigateTo<MenuViewModel>(); }, o => true);
            SaveCommand = new RelayCommand(o =>
            {
                if (ItemsService.AddItems(ItemsService.ElectronicObject))
                    ResetFields();
            }, o => true);

            NavigateToListCommand = new RelayCommand(o => { ItemsService.SelectedObjectToEdit = null; Navigation.NavigateTo<ElectronicListViewModel>(); }, o => true);
            UpdateCommand = new RelayCommand(o =>
            {
                if(ItemsService.CheckInput())
                {
                    if(electronicObjectRepository.UpdateById(ItemsService.ElectronicObject))
                        MessageBox.Show("Obiect actualizat cu succes");
                    else
                        MessageBox.Show("Obiectul nu a putut fi actualizat");
                    ItemsService.RefreshItems();
                    Navigation.NavigateTo<ElectronicListViewModel>();
                }
            }, o => true);
            DeleteCommand = new RelayCommand(o =>
            {
                if(electronicObjectRepository.DeleteById(ItemsService.ElectronicObject.Id))
                    MessageBox.Show("Obiect sters cu succes");
                else
                    MessageBox.Show("Obiectul nu a putut fi sters");
                ItemsService.RefreshItems();
                Navigation.NavigateTo<ElectronicListViewModel>();
            }, o => true);
        }
    }
}
