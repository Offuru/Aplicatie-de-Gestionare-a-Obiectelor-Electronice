using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services
{
    public interface IItemsService
    {
        public ObservableCollection<string> Items { get; set; }
        void AddItems();
    }

    internal class ItemsService : IItemsService
    {
        public ObservableCollection<string> Items { get; set; } = new();

        public void AddItems()
        {
            Items.Add("Item");
        }
    }
}
