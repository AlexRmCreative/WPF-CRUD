using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.IO;
using System.Windows;

namespace CRUD.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public UserViewModel()
        {
            _users = LoadUsers();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<User> _users;

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

        string jsonFile = "users.json";

        public ObservableCollection<User> LoadUsers()
        {
            if (File.Exists(jsonFile))
            {
                string json = File.ReadAllText(jsonFile);
                if (!string.IsNullOrEmpty(json))
                {
                    var result = JsonSerializer.Deserialize<ObservableCollection<User>>(json);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return new ObservableCollection<User>();
        }
        private void SaveUsers(string filePath)
        {
            string json = JsonSerializer.Serialize(Users);
            File.WriteAllText(filePath, json);
        }

        public void AddUser(string name, string lastname, string email)
        {
            _users.Add(new User
            {
                Id = GetNewId(),
                Name = name,
                LastName = lastname,
                Email = email,
                Password = "Password", // Puedes establecer una contraseña predeterminada o manejarla de alguna otra manera
            });
            SaveUsers(jsonFile);
        }


        public void DeleteUser(User userToRemove)
        {
            _users.Remove(userToRemove);
            SaveUsers(jsonFile);
        }

        public uint GetNewId()
        {
            return _users.Count > 0 ? (uint)_users.Count : 0;
        }
    }
}
