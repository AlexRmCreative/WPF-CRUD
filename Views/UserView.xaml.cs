using CRUD.Models;
using CRUD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRUD.Views
{
    /// <summary>
    /// Lógica de interacción para UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        private UserViewModel _userViewModel;
        public UserView()
        {
            InitializeComponent();

            _userViewModel = new UserViewModel();

            DataContext = _userViewModel;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            _userViewModel.AddUser();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUsers = users_dataGrid.SelectedItems.Cast<User>().ToList();

            // Verifica si hay usuarios seleccionados antes de intentar eliminar
            if (selectedUsers.Any())
            {
                foreach (var user in selectedUsers)
                {
                    _userViewModel.DeleteUser(user);
                }
            }
            else
            {
                MessageBox.Show("Selecciona al menos un usuario antes de eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
