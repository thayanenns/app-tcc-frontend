using AppTccFrontend.Models;
using AppTccFrontend.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTccFrontend.NovaPasta1
{
    public class Medicopicker : INotifyPropertyChanged
    {
        private ObservableCollection<MedicaoPorDiaDto> _medicoesPorDia;

        public ObservableCollection<MedicaoPorDiaDto> MedicoesPorDia
        {
            get { return _medicoesPorDia; }
            set
            {
                if (_medicoesPorDia != value)
                {
                    _medicoesPorDia = value;
                    OnPropertyChanged(nameof(MedicoesPorDia));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
