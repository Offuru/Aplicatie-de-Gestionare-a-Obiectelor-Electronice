using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Database;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Database.Repositories;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ElectronicObject = Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Models.ElectronicObject;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Core;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.MVVM.Models;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services
{
    public interface IItemsService
    {
        bool AddItems(ElectronicObject obj);
        void AddItems();
        void CreateForm();
        void MarkItems();
        void UnMarkItems();
    }

    internal class ItemsService : Core.ViewModel, IItemsService
    {
        ElectronicObjectRepository ObjectRepository { get; set; }
        public ObservableCollection<ElectronicObjectDataGridCheckBox> Items { get; set; }

        private string _currentObjectTypeSearchFilter;
        public string CurrentObjectTypeSearchFilter
        {
            get => _currentObjectTypeSearchFilter;
            set
            {
                _currentObjectTypeSearchFilter = value;
                if(value == "Fara filtru")
                {
                    ShowMarkForRemoval = Visibility.Hidden;
                }
                else
                {
                    ShowMarkForRemoval = Visibility.Visible;
                }
                UnMarkItems();
                OnPropertyChanged(nameof(CurrentObjectRecommendation));
            }
        }
        public List<string> ObjectTypeSearchFilters { get; set; }

        private ObservableCollection<ElectronicObjectDataGridCheckBox> _currentObjectRecommendation;
        public ObservableCollection<ElectronicObjectDataGridCheckBox> CurrentObjectRecommendation
        {
            get
            {
                _currentObjectRecommendation.Clear();

                foreach (var obj in Items)
                {
                    if (CurrentObjectTypeSearchFilter == "Fara filtru" || obj.Type == CurrentObjectTypeSearchFilter)
                    {
                        if (obj.ReceiverName.StartsWith(CurrentSearchInput, StringComparison.OrdinalIgnoreCase)
                            || obj.Name.StartsWith(CurrentSearchInput, StringComparison.OrdinalIgnoreCase)
                            || obj.Code.StartsWith(CurrentSearchInput, StringComparison.OrdinalIgnoreCase))
                            _currentObjectRecommendation.Add(obj);
                    }
                }
                return _currentObjectRecommendation;
            }
            set
            {
                _currentObjectRecommendation = new ObservableCollection<ElectronicObjectDataGridCheckBox>();
                foreach (var obj in value)
                {
                    _currentObjectRecommendation.Add(obj);
                }
            }
        }

        private Visibility _showMarkForRemoval;
        public Visibility ShowMarkForRemoval
        {
            get => _showMarkForRemoval;
            set
            {
                _showMarkForRemoval = value;
                foreach(var obj in Items)
                {
                    obj.MarkObjectForRemoval = false;
                }
                OnPropertyChanged(nameof(Items));
                OnPropertyChanged();
            }
        }

        private string _currentSearchInput;
        public string CurrentSearchInput
        {
            get => _currentSearchInput ?? string.Empty;
            set
            {
                _currentSearchInput = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentObjectRecommendation));
            }
        }

        public bool AddItems(ElectronicObject obj)
        {
            if(obj.ActiveObjectType == "")
            {
                MessageBox.Show("Tipul obiectului nu poate fi necompletat");
                return false;
            }
            if(obj.Code == "")
            {
                MessageBox.Show("Codul nu poate fi necompletat");
                return false;
            }
            if (obj.Order == "")
            {
                MessageBox.Show("Numarul de inventar/mijloc fix nu poate fi necompletat");
                return false;
            }
            if (obj.ReceiptNumber == "")
            {
                MessageBox.Show("Numarul de bon miscare nu poate fi necompletat");
                return false;
            }
            if (obj.Date == "" || !DateTime.TryParse(obj.Date, out _))
            {
                MessageBox.Show("Data este invalida");
                return false;
            }
            if (obj.Name == "")
            {
                MessageBox.Show("Denumirea obiectului nu poate fi necompletata");
                return false;
            }
            if(obj.Serial == "")
            {
                MessageBox.Show("Seria unica a obiectului nu poate fi necompletata");
                return false;
            }
            if (obj.Destination == "")
            {
                MessageBox.Show("Destinatia obiectului nu poate fi necompletata");
                return false;
            }
            if (obj.ReceiverName == "")
            {
                MessageBox.Show("Numele destinatarului nu poate fi necompletat");
                return false;
            }

            obj.Date = DateTime.Parse(obj.Date).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ObjectRepository.Add(obj);

            MessageBox.Show("Obiect salvat cu succes");
            RefreshItems();
            return true;
        }

        public void AddItems()
        {
            var rand = new Random();
            int random = rand.Next(0, 100);

            string type;

            if (random % 3 == 0)
                type = "Activ";
            else if (random % 3 == 1)
                type = "Propus pentru casare";
            else
                type = "Casat";

            ObjectRepository.Add(new ElectronicObject()
            {
                Type = type,
                ActiveObjectType = ElectronicObject.ActiveType.Inventory.ToString(),
                Code = "cod",
                Order = "1131",
                ReceiptNumber = "receipt",
                Date = "25.04.2024",
                Name = "obiect",
                Serial = "serial",
                Destination = ElectronicObject.Receiver.Room.ToString(),
                ReceiverName = ElectronicObject.Classrooms[0]
            });

            RefreshItems();
        }

        public void RefreshItems()
        {
            Items.Clear();
            foreach(var obj in ObjectRepository.GetAll())
            {
                Items.Add(new ElectronicObjectDataGridCheckBox(obj));
            }

            OnPropertyChanged(nameof(CurrentObjectRecommendation));
        }

        public void CreateForm()
        {
            var objects = ObjectRepository.GetAll();
            ObjectListToWord.CreateWordFile(objects);
        }

        public void MarkItems()
        {
            if(CurrentObjectRecommendation != null)
                foreach (var obj in CurrentObjectRecommendation)
                    obj.MarkObjectForRemoval = true;
            OnPropertyChanged(nameof(CurrentObjectRecommendation));
        }

        public void UnMarkItems()
        {
            if(CurrentObjectRecommendation != null)
                foreach (var obj in CurrentObjectRecommendation)
                    obj.MarkObjectForRemoval = false;
            OnPropertyChanged(nameof(CurrentObjectRecommendation));
        }

        public ItemsService(ElectronicObjectRepository electronicObjectRepository)
        {
            ObjectRepository = electronicObjectRepository;
            Items = new ObservableCollection<ElectronicObjectDataGridCheckBox>();
            CurrentObjectRecommendation = new ObservableCollection<ElectronicObjectDataGridCheckBox>();
            RefreshItems();
            ObjectTypeSearchFilters = new List<string> { "Fara filtru", "Activ", "Propus pentru casare", "Casat" };
            CurrentObjectTypeSearchFilter = "Fara filtru";
            CurrentObjectRecommendation = Items;
            ShowMarkForRemoval = Visibility.Hidden;
        }
    }
}
