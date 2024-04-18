using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Core;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services
{
    class WindowMapper
    {
        private readonly Dictionary<Type, Type> _mappings = new();

        public WindowMapper() 
        {
            RegisterMapping<MainViewModel, MainWindow>();
        }

        public void RegisterMapping<TViewModel, TWindow>() where TViewModel : ViewModel where TWindow : Window
        {
            _mappings[typeof(TViewModel)] = typeof(TWindow);
        }

        public Type GetWindowTypeForViewModel(Type viewModelType)
        {
            _mappings.TryGetValue(viewModelType, out var type);
            return type;
        }
    }
}
