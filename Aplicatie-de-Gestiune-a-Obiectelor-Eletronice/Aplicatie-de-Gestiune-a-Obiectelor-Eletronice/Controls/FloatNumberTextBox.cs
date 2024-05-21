using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Controls
{
    public class FloatNumberTextBox : TextBox
    {
        private static readonly Regex regex = new Regex(@"^[-+]?\d*([\.]?)(\d*)([eE][-+]?\d*)?$", RegexOptions.ECMAScript);
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!regex.IsMatch(e.Text))
                e.Handled = true;
            base.OnPreviewTextInput(e);
        }
    }
}
