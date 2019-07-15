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
        // Instancia de la conexión
        ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
        SqlConnection cn = new SqlConnection("Data Source = ABELCONSUEGRA; Initial Catalog = SistemaDeEstacionamiento; Integrated Security = True");

        public MainWindow()
        {
            InitializeComponent();

            // para que al abrir el programa se cargue el listbox principal
            this.lbVehiculosDentroEstacionamiento.ItemsSource = estacionamiento.MostrarEntrada();  
            // para que automáticamente aparezca seleccionado el textbox de placa
            txtPlaca.Focus();
        }

        // botón de salir
        private void Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Botón aceptar (guarda los registros)
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

        // Botón Cancelar (limpia los componentes)
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

        // Botón de reporte
        private void BtnReporte_Click(object sender, RoutedEventArgs e)
        {
            Reporte reporte = new Reporte();
            // función Hide, para esconder la ventana que le especifiquemos
            this.Hide();
            reporte.Show();
        }


        // Boton Pagar
        private void BtnPagar_Click(object sender, RoutedEventArgs e)
        {
            // condición
            if ( lbVehiculosEstacionamiento.SelectedItem==null)
                MessageBox.Show("Debes seleccionar un Vehiculo");
            else
            {                
                ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
                estacionamiento.Placa = txtBuscarPlaca.Text;
                estacionamiento.SalidaVehiculo();
                // se muestra la ventana de pago al salir un vehiculo
                mensajePago ms = new mensajePago();
                ms.ShowDialog();
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

        // Boton buscar
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