using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> _users;

        public UserViewModel()
        {
            _users = new ObservableCollection<User>();
            // Agrega algunos usuarios de ejemplo (puedes cargarlos desde tu lógica de datos aquí)
            _users.Add(new User { Name = "John", LastName = "Doe", Email = "john@example.com" });
            _users.Add(new User { Name = "Jane", LastName = "Doe", Email = "jane@example.com" });
        }
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
