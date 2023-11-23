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
            if (HighlightInvalidTextBoxes(name_TBox) && HighlightInvalidTextBoxes(lastname_TBox) && IsValidEmail(email_TBox))
            {
                // Llama al método AddUser del UserViewModel con los valores obtenidos
                _userViewModel.AddUser(name_TBox.Text, lastname_TBox.Text, email_TBox.Text);

                // Limpiar estilos de los TextBox después de una validación exitosa
                ClearTextBoxesStyles(new TextBox[] { name_TBox, lastname_TBox, email_TBox});
            }
        }

        private bool HighlightInvalidTextBoxes(TextBox textBox)
        {
            if(string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BorderBrush = Brushes.Red;
                return false;
            }
            else
            {
                textBox.ClearValue(BorderBrushProperty);
                return true;
            }
        }

        private void ClearTextBoxesStyles(TextBox[] textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.ClearValue(BorderBrushProperty);
            }
        }

        private bool IsValidEmail(TextBox emailTBox)
        {
            if (!emailTBox.Text.Contains("@"))
            {
                emailTBox.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
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
                MessageBox.Show("Selecciona al menos un usuario antes.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (users_dataGrid.SelectedItem != null)
            {
                User selectedUser = users_dataGrid.SelectedItem as User;
                if (HighlightInvalidTextBoxes(name_TBox) && HighlightInvalidTextBoxes(lastname_TBox) && IsValidEmail(email_TBox))
                {
                    selectedUser.Name = name_TBox.Text;
                    selectedUser.LastName = lastname_TBox.Text;
                    selectedUser.Email = email_TBox.Text;
                    ClearTextBoxesStyles(new TextBox[] { name_TBox, lastname_TBox, email_TBox });
                }
            }
            else
            {
                MessageBox.Show("Selecciona al menos un usuario antes.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
