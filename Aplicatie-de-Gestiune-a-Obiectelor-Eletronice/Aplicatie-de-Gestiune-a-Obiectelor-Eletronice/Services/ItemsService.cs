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
        void ProposeCasare();
        void Casare();
    }

    class ItemsService : Core.ViewModel, IItemsService
    {
        ElectronicObjectRepository ObjectRepository { get; set; }
        public ObservableCollection<ElectronicObjectDataGridCheckBox> Items { get; set; }
        
        private ElectronicObject _electronicObject;
        public ElectronicObject ElectronicObject
        {
            get => _electronicObject;
            set
            {
                _electronicObject = value;
                _electronicObject.Order = new string(_electronicObject.Order.Where(c => char.IsDigit(c)).ToArray());
                OnPropertyChanged();
                CurrentObjectType = _electronicObject.ActiveObjectType;
                CurrentDestinationType = _electronicObject.Destination;
            }
        }

        private ElectronicObject? _selectedObjectToEdit;
        public ElectronicObject? SelectedObjectToEdit
        {
            get => _selectedObjectToEdit;
            set
            {
                _selectedObjectToEdit = value;
                if (value != null)
                { 
                    ElectronicObject = value; 
                }
                OnPropertyChanged();
            }
        }

        private string _codeName;
        public string CodeName
        {
            get => _codeName;
            set
            {
                _codeName = value;
                OnPropertyChanged();
            }
        }

        private string _orderName;
        public string OrderName
        {
            get => _orderName;
            set
            {
                _orderName = value;
                OnPropertyChanged();
            }
        }

        public bool _objectType;
        public bool ObjectType
        {
            get => _objectType;
            set
            {
                _objectType = value;
                if (value)
                {
                    CodeName = "Cod NO";
                    OrderName = "Numar inventar";
                }
                else
                {
                    CodeName = "Cod INV";
                    OrderName = "Numar mijloc fix";
                }
                OnPropertyChanged();

            }
        }

        public string CurrentObjectType
        {
            get => ElectronicObject.ActiveObjectType;
            set
            {
                ElectronicObject.ActiveObjectType = value;
                if (ElectronicObject.ActiveObjectType == "Obiecte de inventar")
                    ObjectType = true;
                else
                    ObjectType = false;

                OnPropertyChanged();
            }
        }

        private string _currentDestinationNameTBlock;
        public string CurrentDestinationNameTBlock
        {
            get => _currentDestinationNameTBlock;
            set
            {
                _currentDestinationNameTBlock = value;
                OnPropertyChanged();
            }
        }


        public bool SuggestRooms { get; set; }
        public string CurrentDestinationType
        {
            get => ElectronicObject.Destination;
            set
            {
                ElectronicObject.Destination = value;
                SuggestRooms = false;
                switch (ElectronicObject.Destination)
                {
                    case "Student":
                        CurrentDestinationNameTBlock = "Nume student"; break;
                    case "Doctorand":
                        CurrentDestinationNameTBlock = "Nume doctorand"; break;
                    case "Cadru didactic":
                        CurrentDestinationNameTBlock = "Nume cadru didactic"; break;
                    case "Sala":
                        CurrentDestinationNameTBlock = "Denumire sala"; SuggestRooms = true; break;
                }

                OnPropertyChanged();
                OnPropertyChanged(nameof(SuggestRooms));
            }
        }

        private Visibility _editingObject;
        private Visibility _addingObject;

        public Visibility EditingObject
        {
            get => _editingObject;
            set
            {
                _editingObject = value;
                if(value == Visibility.Visible)
                {
                    _addingObject = Visibility.Hidden;
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(AddingObject));
            }
        }

        public Visibility AddingObject
        {
            get => _addingObject;
            set
            {
                _addingObject = value;
                if (value == Visibility.Visible)
                {
                    _editingObject = Visibility.Hidden;
                }
                OnPropertyChanged();
                OnPropertyChanged(nameof(EditingObject));
            }
        }


        private Visibility _visibilityCasareProposal;
        public Visibility VisibilityCasareProposal
        {
            get => _visibilityCasareProposal;
            set
            {
                _visibilityCasareProposal = value;
                OnPropertyChanged();
            }
        }

        private Visibility _visibilityCasare;
        public Visibility VisibilityCasare
        {
            get => _visibilityCasare;
            set
            {
                _visibilityCasare = value;
                OnPropertyChanged();
            }
        }

        private string _currentObjectTypeSearchFilter;
        public string CurrentObjectTypeSearchFilter
        {
            get => _currentObjectTypeSearchFilter;
            set
            {
                _currentObjectTypeSearchFilter = value;

                VisibilityCasareProposal = Visibility.Collapsed;
                VisibilityCasare = Visibility.Collapsed;
                if (value == "Fara filtru")
                {
                    ShowMarkForRemoval = Visibility.Hidden;
                }
                else
                {
                    ShowMarkForRemoval = Visibility.Visible;
                    if(value == "Activ")
                    {
                        VisibilityCasareProposal = Visibility.Visible;
                        VisibilityCasare = Visibility.Collapsed;
                    }
                    else if (value == "Propus pentru casare")
                    {
                        VisibilityCasareProposal = Visibility.Collapsed;
                        VisibilityCasare = Visibility.Visible;
                    }
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

        public bool CheckInput()
        {
            if (ElectronicObject.ActiveObjectType == "")
            {
                MessageBox.Show("Tipul obiectului nu poate fi necompletat");
                return false;
            }
            if (ElectronicObject.Code == "")
            {
                MessageBox.Show("Codul nu poate fi necompletat");
                return false;
            }
            if (ElectronicObject.Order == "")
            {
                MessageBox.Show("Numarul de inventar/mijloc fix nu poate fi necompletat");
                return false;
            }
            if (ElectronicObject.ReceiptNumber == "")
            {
                MessageBox.Show("Numarul de bon miscare nu poate fi necompletat");
                return false;
            }
            if (ElectronicObject.Date == "" || !DateTime.TryParse(ElectronicObject.Date, out _))
            {
                MessageBox.Show("Data este invalida");
                return false;
            }
            if (ElectronicObject.Name == "")
            {
                MessageBox.Show("Denumirea obiectului nu poate fi necompletata");
                return false;
            }
            if (ElectronicObject.Serial == "")
            {
                MessageBox.Show("Seria unica a obiectului nu poate fi necompletata");
                return false;
            }
            if (ElectronicObject.Destination == "")
            {
                MessageBox.Show("Destinatia obiectului nu poate fi necompletata");
                return false;
            }
            if (ElectronicObject.ReceiverName == "")
            {
                MessageBox.Show("Numele destinatarului nu poate fi necompletat");
                return false;
            }

            return true;
        }

        public bool AddItems(ElectronicObject obj)
        {
            if (!CheckInput())
                return false;

            ElectronicObject.Date = DateTime.Parse(ElectronicObject.Date).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            ObjectRepository.Add(ElectronicObject);

            MessageBox.Show("Obiect salvat cu succes");
            RefreshItems();
            ElectronicObject = new ElectronicObject();
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

            string destination;
            random = rand.Next(0, 100);
            if (random % 4 == 0)
                destination = "Student";
            else if (random % 4 == 1)
                destination = "Doctorand";
            else if (random % 4 == 2)
                destination = "Cadru didactic";
            else
                destination = "Sala";

            random = rand.Next(0, 100);
            string activeobjtype;
            if (random % 2 == 0)
                activeobjtype = "Obiecte de inventar";
            else
                activeobjtype = "Mijloace fixe";

            ObjectRepository.Add(new ElectronicObject()
            {
                Type = type,
                ActiveObjectType = activeobjtype,
                Code = rand.Next(1000,10000).ToString(),
                Order = rand.Next(1000, 10000).ToString(),
                ReceiptNumber = rand.Next(1000, 10000000).ToString(),
                Date = "25/04/2024",
                Name = "obiect",
                Serial = rand.Next(1000, 10000).ToString(),
                Destination = destination,
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

        public void ProposeCasare()
        {
            foreach(var obj in CurrentObjectRecommendation)
            {
                if(obj.MarkObjectForRemoval)
                {
                    obj.MarkObjectForRemoval = false;
                    obj.Type = "Propus pentru casare";
                    ObjectRepository.UpdateById(obj as ElectronicObject);
                }
            }

            RefreshItems();
        }

        public void Casare()
        {
            foreach (var obj in CurrentObjectRecommendation)
            {
                if (obj.MarkObjectForRemoval)
                {
                    obj.MarkObjectForRemoval = false;
                    obj.Type = "Casat";
                    ObjectRepository.UpdateById(obj as ElectronicObject);
                }
            }

            RefreshItems();
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
            ElectronicObject = new ElectronicObject();
            SelectedObjectToEdit = null;
            VisibilityCasareProposal = Visibility.Collapsed;
            VisibilityCasare = Visibility.Collapsed;
        }
    }
}
