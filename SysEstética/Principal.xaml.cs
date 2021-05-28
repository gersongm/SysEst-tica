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

namespace SysVeterinaria
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();
        }
        public Domain.Models.UsuariosModel Usuario { private get; set; }
        private Domain.Models.PrivilegiosModel privilegio { get; set; }

        Pantallas.RegistroUsuario frmUsuario ;
        Pantallas.ListaUsuario frmLista = new Pantallas.ListaUsuario();

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
           if( MessageBox.Show("Desea salir del sistema?","SysVeterinaria",MessageBoxButton.YesNo ,MessageBoxImage.Question)== MessageBoxResult.Yes)
            Application.Current.Shutdown();
        }


        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnRegistro_Click(object sender, RoutedEventArgs e)
        {
            if (frmUsuario == null) frmUsuario = new Pantallas.RegistroUsuario();

            frmUsuario.btnCerrar.Click += btnCerrarVentana_Click; 

            txtCaption.Text = "Registro de Usuario";
            GetVista(frmUsuario);

        }

        private void BtnListaUsuarios_Click(object sender, RoutedEventArgs e)
        {
            if (frmLista == null) frmLista = new Pantallas.ListaUsuario();

            txtCaption.Text = "Lista de Usuarios";
            GetVista(frmLista);
        }

        void GetVista(UIElement frm)
        {
            btnCerrar.Visibility = Visibility.Visible;
            Vista.Children.Clear();
            Vista.Children.Add(frm);
        }

    
        private void btnCerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            btnCerrar.Visibility = Visibility.Hidden;
            Vista.Children.Clear();
            txtCaption.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            security();
            Permisos();

           // MessageBox.Show(Usuario.Nombres + "|" + Usuario.Email);

            btnCerrarVentana_Click(this, new RoutedEventArgs());
            privilegio = new Domain.Models.PrivilegiosModel();

            txtNombre.Text = Usuario.Nombres;
            txtEmail.Text = Usuario.Email;
            txtPrivilegio.Text = Usuario.Posición;
        }

        private void security()
        {
            
            if (Usuario.securityLogin() == false || Usuario ==null)
            {
                MessageBox.Show("Error Fatal, se detectó que está intentando acceder al sistema sin credenciales, por favor inicie sesión e indentifiquese");
                Application.Current.Shutdown();
            }
        }
        private void Permisos()
        {

            if (Usuario.Posición.Trim() == "FACTURACION")
            {
                btnInventario.IsEnabled = false;
                btnPerfil.IsEnabled=false;
                btnReportes.IsEnabled=false;
                btnRegistrar.IsEnabled=false;
                btnCaja.IsEnabled=false;
            }
            else if (Usuario.Posición.Trim() == "CAJA")
            {
                btnInventario.IsEnabled=false;
                btnPerfil.IsEnabled=false;
                btnReportes.IsEnabled=false;
                btnRegistrar.IsEnabled=false;
                btnFacturacion.IsEnabled=false;
                btnClientes.IsEnabled = false;
            }
            else if (Usuario.Posición.Trim() == "INVENTARIO")
            {
                btnCaja.IsEnabled = false;
                btnPerfil.IsEnabled = false;
                btnReportes.IsEnabled = false;
                btnRegistrar.IsEnabled = false;
                btnFacturacion.IsEnabled = false;
                btnClientes.IsEnabled = false;
            }


        }
    }
}
