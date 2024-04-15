using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Core;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.ViewModels
{
    class MainViewModel : Core.ViewModel
    {
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
            Navigation.NavigateTo<MenuViewModel>();
        }
    }
}
