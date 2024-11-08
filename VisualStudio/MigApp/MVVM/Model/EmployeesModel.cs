using MigApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigApp.MVVM.Model
{
    public class EmployeesModel : ObservableObject
    {
        private int _employeeId;
        public int ID
        {
            get => _employeeId;
            set
            {
                _employeeId = value;
                OnPropertyChanged();
            }
        }
        private string _fio;
        public string ФИО
        {
            get => _fio;
            set
            {
                _fio = value;
                OnPropertyChanged();
            }
        }
        private string _group;
        public string Отдел
        {
            get => _group;
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }
        private string _room;
        public string Кабинет
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged();
            }
        }
    }
}
