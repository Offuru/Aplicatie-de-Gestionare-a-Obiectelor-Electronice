using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Core;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ElectronicObject ElectronicObject { get => _electronicObject;
            set
            {
                _electronicObject = value;
                _electronicObject.Order = new string(_electronicObject.Order.Where(c => char.IsDigit(c)).ToArray());
                OnPropertyChanged(nameof(_electronicObject.Order));
            }
        }

        public ElectronicOverviewViewModel() 
        {
            CurrentObjectType = "Obiecte de inventar";
            ObjectTypesList = new List<string>{ "Obiecte de inventar", "Mijloace fixe"};
            ElectronicObject = new ElectronicObject();
        }
    }
}
