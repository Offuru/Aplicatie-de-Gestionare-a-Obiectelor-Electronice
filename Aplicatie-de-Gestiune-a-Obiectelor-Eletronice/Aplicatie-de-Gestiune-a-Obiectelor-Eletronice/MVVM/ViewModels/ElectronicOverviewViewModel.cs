using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Core;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.ViewModels
{
    class ElectronicOverviewViewModel : Core.ViewModel
    {

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
                if(value)
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

        private string _currentObjectType { get; set; }
        public string CurrentObjectType 
        {
            get => _currentObjectType;
            set
            {
                _currentObjectType = value;
                if(_currentObjectType == "Obiecte de inventar")
                    ObjectType = true;
                else
                    ObjectType = false;

                OnPropertyChanged();
            }
        }

        public List<String> ObjectTypesList { get; set; }

        private ElectronicObject _electronicObject;

        public ElectronicObject ElectronicObject 
        {   
            get => _electronicObject;
            set
            {
                _electronicObject = value;
                _electronicObject.Order = new string(_electronicObject.Order.Where(c => char.IsDigit(c)).ToArray());
                OnPropertyChanged(nameof(_electronicObject.Order));
            }
        }

        public List<String> DestinationTypesList { get; set; }
        public List<String> RoomsTypeList { get; set; }

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
       

        public bool SuggestRooms {  get; set; }
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

                OnPropertyChanged(nameof(SuggestRooms));
            }
        }


        public string DestinationName
        {
            get
            {
                return ElectronicObject.ReceiverName;
            }
            set
            {
                ElectronicObject.ReceiverName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentRoomRecomandation));

            }
        }

        public string AutofillRoom
        {
            get => DestinationName;
            set
            {
                if(Int16.TryParse(value, out _))
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
                    if(room.ToLower().Contains(DestinationName.ToLower()))
                        _currentRoomRecomandation.Add(room);
                }
                return _currentRoomRecomandation;
            }
            set { _currentRoomRecomandation = value; }
        }

        public ElectronicOverviewViewModel() 
        { 
            ElectronicObject = new ElectronicObject();
            CurrentObjectType = "Obiecte de inventar";
            ElectronicObject.Destination = "Student";
            ObjectTypesList = new List<string>{ "Obiecte de inventar", "Mijloace fixe"};
            DestinationTypesList = new List<string>{ "Student", "Doctorand", "Cadru didactic", "Sala"};
           
            RoomsTypeList = ElectronicObject.Classrooms;
            AutofillRoom = "";
            CurrentRoomRecomandation = new ObservableCollection<string>();
        }
    }
}
