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

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services
{
    public interface IItemsService
    {
        public ObservableCollection<string> Items { get; set; }
        bool AddItems(ElectronicObject obj);
        void AddItems();
    }

    internal class ItemsService : IItemsService
    {
        ElectronicObjectRepository ObjectRepository { get; set; }
        public ObservableCollection<string> Items { get; set; } = new();

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
            return true;
        }

        public void AddItems()
        {
            var objects = ObjectRepository.GetAll();
            ObjectListToWord.CreateWordFile(objects);
            Items.Add("Item");
        }

        public ItemsService(ElectronicObjectRepository electronicObjectRepository)
        {
            ObjectRepository = electronicObjectRepository;
        }
    }
}
