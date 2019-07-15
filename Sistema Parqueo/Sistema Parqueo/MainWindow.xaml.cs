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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Sistema_Parqueo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
        SqlConnection cn = new SqlConnection("Data Source = ABELCONSUEGRA; Initial Catalog = SistemaDeEstacionamiento; Integrated Security = True");

        public MainWindow()
        {
            InitializeComponent();
            this.lbVehiculosDentroEstacionamiento.ItemsSource = estacionamiento.MostrarEntrada();            
            txtPlaca.Focus();


            //MostrarVehiculosDentro();
        }

        private void Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Aceptar(object sender, RoutedEventArgs e)
        {
            ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
            if (txtPlaca.Text.Equals("") == false && cmbTipoVehiculo.SelectedIndex != -1)
            {

                estacionamiento.Placa = txtPlaca.Text;
                estacionamiento.TipoVehiculo = cmbTipoVehiculo.Text;
                estacionamiento.InsertarVehiculo();
                txtPlaca.Clear();
                cmbTipoVehiculo.SelectedIndex = -1;
                txtPlaca.Focus();
                this.lbVehiculosDentroEstacionamiento.ItemsSource = estacionamiento.MostrarEntrada();                
            }
            else
            {
                MessageBox.Show("Ingrese todos los datos");
                txtPlaca.Clear();
                cmbTipoVehiculo.SelectedIndex = -1;
                txtPlaca.Focus();
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            txtPlaca.Clear();
            cmbTipoVehiculo.SelectedIndex = -1;
            txtPlaca.Focus();
        }


        

        //Mostar los datos en el listbox
        private void MostrarVehiculosDentro()
        {
            ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
            lbVehiculosDentroEstacionamiento.ItemsSource = estacionamiento.MostrarEntrada();            
        }



        //Boton de actualizar
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow listar = new MainWindow();
            listar.Show();
        }

        private void BtnReporte_Click(object sender, RoutedEventArgs e)
        {
            Reporte reporte = new Reporte();
            this.Hide();
            reporte.Show();
        }

        // Boton Pagar
        private void BtnPagar_Click(object sender, RoutedEventArgs e)
        {
            if ( lbVehiculosEstacionamiento.SelectedItem==null)
                MessageBox.Show("Debes seleccionar un Vehiculo");
            else
            {
                mensajePago ms = new mensajePago();
                ms.ShowDialog();
                ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
                estacionamiento.Placa = txtBuscarPlaca.Text;
                estacionamiento.SalidaVehiculo();
                
            }
            
            this.lbVehiculosDentroEstacionamiento.ItemsSource = estacionamiento.MostrarEntrada();           
            txtBuscarPlaca.Text = String.Empty;
            lbVehiculosEstacionamiento.ItemsSource = "";
            txtBuscarPlaca.Focus();
        }        

       
        //boton cancelar
        private void BtnCancelarBuscar_Click(object sender, RoutedEventArgs e)
        {
            txtBuscarPlaca.Text = String.Empty;
            lbVehiculosEstacionamiento.ItemsSource = "";
            txtBuscarPlaca.Focus();
        }

        private void Buscar(object sender, RoutedEventArgs e)
        {
            if (txtBuscarPlaca.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar una placa en la caja de texto.");
                txtBuscarPlaca.Focus();
            }
            ClassEstacionamiento estacionamiento = new ClassEstacionamiento();

            estacionamiento.Placa = txtBuscarPlaca.Text;
            this.lbVehiculosEstacionamiento.ItemsSource = estacionamiento.BuscarEntrada();
            this.lbVehiculosEstacionamiento.SelectedValuePath = estacionamiento.Placa;            
        }
        
    }
}


