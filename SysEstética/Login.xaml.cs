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
using System.Windows.Shapes;

namespace SysVeterinaria
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
       
        }

        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            VerificarDatos();
        }

        private void VerificarDatos()
        {
            Domain.Models.UsuariosModel usuario = new Domain.Models.UsuariosModel();
            if (usuario.GetAll().Count == 0)
            {
                Domain.Models.UsuariosModel model = new Domain.Models.UsuariosModel
                {
                    Nombres = "Administrador",
                    Usuario = "admin",
                    Contraseña = "admin",
                    Email = "Administrador@gmail.com",
                    Id = 0,
                    Sucursal = 0,
                    Privilegios = 0,
                    State = Domain.ValueObjects.EntityState.Added,

                };
            }
            else
            {
                usuario = usuario.Login(txtUser.Text.Trim(), txtPassword.Password.Trim());
            }

            if (usuario != null)
            {
                Principal p = new Principal();
                p.Usuario = usuario;
                p.Show();

                Close();
                return;
            }

            MessageBox.Show("Usuario o Contraseña incorectos \nIntente de nuevo");
            txtUser.Focus();
            txtUser.SelectAll();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtUser.Focus();
        }

        private void txtUser_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter )
            {
                if(string.IsNullOrEmpty(txtPassword.Password))
                {
                    txtPassword.Focus();
                }
                else { VerificarDatos(); }
            }
        }

        private void txtPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(txtUser.Text))
                {
                    txtUser.Focus();
                }
                else
                { VerificarDatos(); }
            }
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
