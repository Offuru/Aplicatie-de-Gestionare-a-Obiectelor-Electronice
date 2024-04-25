using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Database;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Database.Repositories;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicObject = Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Models.ElectronicObject;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services
{
    public interface IItemsService
    {
        public ObservableCollection<string> Items { get; set; }
        void AddItems();
    }

    internal class ItemsService : IItemsService
    {
        ElectronicObjectRepository ObjectRepository { get; set; }
        public ObservableCollection<string> Items { get; set; } = new();

        public void AddItems()
        {
            Items.Add("Item");
            ObjectRepository.Add(new ElectronicObject()
            {
                Id = 1,
                Type = ElectronicObject.ObjectType.Active.ToString(),
                ActiveObjectType = ElectronicObject.ActiveType.Inventory.ToString(),
                Code = "cod",
                Order = 1131,
                ReceiptNumber = "receipt",
                Date = "25.04.2024",
                Name = "obiect",
                Serial = "serial",
                Destination = ElectronicObject.Receiver.Room.ToString(),
                ReceiverName = ElectronicObject.Classrooms[0]
            });
        }

        public ItemsService(ElectronicObjectRepository electronicObjectRepository)
        {
            ObjectRepository = electronicObjectRepository;
        }
    }
}
