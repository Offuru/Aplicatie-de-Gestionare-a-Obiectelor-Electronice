using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.MVVM.Models
{
    class ElectronicObjectDataGridCheckBox : ElectronicObject
    {
        public bool MarkObjectForRemoval { get; set; }

        public ElectronicObjectDataGridCheckBox() : base() { MarkObjectForRemoval = false; }
        public ElectronicObjectDataGridCheckBox(ElectronicObject obj) : base(obj) { MarkObjectForRemoval = false; }
    }
}
