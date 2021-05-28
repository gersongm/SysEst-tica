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
using Domain.Models;
using Domain.ValueObjects;

namespace SysVeterinaria.Pantallas
{
    /// <summary>
    /// Lógica de interacción para RegistroUsuario.xaml
    /// </summary>
    public partial class RegistroUsuario : UserControl
    {
        UsuariosModel usuariosModel = new UsuariosModel();
        PrivilegiosModel privilegiosModel = new PrivilegiosModel();
        Domain.ValueObjects.EntityState state = Domain.ValueObjects.EntityState.Added;
        public RegistroUsuario()
        {
            InitializeComponent();
        }
        void ValidarCampos()


        {
            

            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                MessageBox.Show(" El campo usuario no puede estar vacío");
                txtUsuario.SelectAll();
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtNombres.Text)) 
            {
                MessageBox.Show("El campo nombre no puede estar vacío");
                txtNombres.SelectAll();
                txtNombres.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtcontraseña.Password))
            {
                MessageBox.Show("El campo contraseña no puede estar vacío");
                txtcontraseña.SelectAll();
                txtcontraseña.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtConfirmar.Password) || txtConfirmar.Password!= txtcontraseña.Password )
            {
                MessageBox.Show("Los campos contraseñas no coinciden");
                txtConfirmar.SelectAll();
                txtConfirmar.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("El campo email no puede estar vacío");
                txtEmail.SelectAll();
                txtEmail.Focus();
                return;
            }
            if (!Helper.ValidarEmail(txtEmail.Text))
            {
                MessageBox.Show("Formato de correo incorrecto");
                txtEmail.SelectAll();
                txtEmail.Focus();
                return;
            }
            if (comboBox.SelectedIndex==-1)
            {
                MessageBox.Show("Seleccione el campo privilegio del usuario");
                comboBox.Focus();
                return;
            }
        }
        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {

            ValidarCampos();

            if (state == Domain.ValueObjects.EntityState.Added)
            {
              
                usuariosModel = new UsuariosModel
                {
                    Usuario = txtUsuario.Text,
                    Contraseña = txtcontraseña.Password,
                    Email = txtEmail.Text,
                    Nombres = txtNombres.Text,
                    Privilegios = Convert.ToInt32(comboBox.SelectedValue.ToString()),
                    Sucursal = 1,
                    State = Domain.ValueObjects.EntityState.Added
                };
            }
            else if(state== Domain.ValueObjects.EntityState.Modified)
            {
                int id = usuariosModel.Id;
                usuariosModel = new UsuariosModel
                {
                    Id = id,
                    Usuario = txtUsuario.Text,
                    Contraseña = txtcontraseña.Password,
                    Email = txtEmail.Text,
                    Nombres = txtNombres.Text,
                    Privilegios = Convert.ToInt32(comboBox.SelectedValue.ToString()),
                    Sucursal = 1,
                    State = Domain.ValueObjects.EntityState.Modified
                };
            }

            MessageBox.Show(usuariosModel.SaveChanges());
            CargarDatos();
            Limpiar();
        }
        void Limpiar()
        {
            txtNombres.Text = "";
            txtcontraseña.Password = "";
            txtConfirmar.Password = "";
            txtEmail.Text = "";
            txtUsuario.Text = "";
            comboBox.SelectedIndex = -1;
            btnRegistrar.Content = "Registrar";

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           
            CargarDatos();
            Limpiar();
        }

        private void CargarDatos (UsuariosModel usu)
        {
            Limpiar();

            if (usu == null) return;

            usuariosModel = usu;
            txtUsuario.Text = usu.Usuario;
            txtNombres.Text = usu.Nombres;
            txtEmail.Text = usu.Email;
            txtcontraseña.Password = usu.Contraseña;
            txtConfirmar.Password = usu.Contraseña;
            comboBox.SelectedValue = usu.Privilegios;

            state = Domain.ValueObjects.EntityState.Modified;

        }

        void CargarDatos()
        {
            dgListaUsuarios.ItemsSource = null;
            comboBox.ItemsSource = null;
            dgListaUsuarios.ItemsSource = usuariosModel.GetAll();
            comboBox.ItemsSource = privilegiosModel.GetAll();
         
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
           
            UsuariosModel obj = ((FrameworkElement)sender).DataContext as UsuariosModel;
            CargarDatos(obj);
            btnRegistrar.Content = "Modificar";
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (usuariosModel.Id == 0)
            {
                MessageBox.Show("Seleccione un registro");
                return;
            }
            usuariosModel.State = Domain.ValueObjects.EntityState.Deleted;
            MessageBox.Show( usuariosModel.SaveChanges());
            CargarDatos();
            Limpiar();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            state = Domain.ValueObjects.EntityState.Added;
            Limpiar();
        
        }
    }
}
