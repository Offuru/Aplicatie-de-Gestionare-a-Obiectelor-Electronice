using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Services
{
    interface IWindowManager
    {
        void ShowWindow(ViewModel viewModel);
        void CloseWindow();
    }

    internal class WindowManager : IWindowManager
    {
        private readonly WindowMapper _windowMapper;

        public WindowManager(WindowMapper windowMapper)
        {
            _windowMapper = windowMapper;
        }

        public void CloseWindow()
        {
            MessageBox.Show("closewindow");
        }

        public void ShowWindow(ViewModel viewModel)
        {
            var windowType = _windowMapper.GetWindowTypeForViewModel(viewModel.GetType());  
            if (windowType != null)
            {
                var window = Activator.CreateInstance(windowType) as Window;
                window.DataContext = viewModel;
                window.Show();
                window.Closed += (sender, args) => CloseWindow();
            }
        }
    }
}
