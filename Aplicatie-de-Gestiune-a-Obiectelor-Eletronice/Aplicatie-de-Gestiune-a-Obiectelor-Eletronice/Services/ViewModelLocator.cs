using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services
{
    class ViewModelLocator
    {
        private readonly IServiceProvider _provider;

        public ViewModelLocator(IServiceProvider provider) 
        {
            _provider = provider;
        }

        public MainViewModel MainViewModel => _provider.GetRequiredService<MainViewModel>();
        public ElectronicListViewModel ElectronicListViewModel => _provider.GetRequiredService<ElectronicListViewModel>();
        public FormViewModel FormViewModel => _provider.GetRequiredService<FormViewModel>();
        public ElectronicOverviewViewModel ElectronicOverviewViewModel => _provider.GetRequiredService<ElectronicOverviewViewModel>();
        public MenuViewModel MenuViewModel => _provider.GetRequiredService<MenuViewModel>();
    }
}
