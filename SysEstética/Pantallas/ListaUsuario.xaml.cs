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

namespace SysVeterinaria.Pantallas
{
    /// <summary>
    /// Lógica de interacción para ListaUsuario.xaml
    /// </summary>
    public partial class ListaUsuario : UserControl
    {
        UsuariosModel usuarios = new UsuariosModel();
        public ListaUsuario()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        void CargarDatos()
        {
            dgListaUsuariios.ItemsSource = usuarios.GetAll();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
  
        
       }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("cerrando ventana");
        }
    }
}
