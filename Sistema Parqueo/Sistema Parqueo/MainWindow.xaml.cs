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
        SqlConnection cn = new SqlConnection("Data Source = LAPTOP-H5OOPDVV\\SQLEXPRESS; Initial Catalog = SistemaDeEstacionamiento; Integrated Security = True");

        public MainWindow()
        {
            InitializeComponent();
            this.dtgrid.ItemsSource = MostrarEntrada();


            //MostrarVehiculosDentro();
        }

        private void Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Aceptar(object sender, RoutedEventArgs e)
        {
            ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
            if (txtPlaca.Text.Equals("") == false )
            {

                estacionamiento.Placa = txtPlaca.Text;
                estacionamiento.TipoVehiculo = cmbTipoVehiculo.Text;
                estacionamiento.InsertarVehiculo();
                txtPlaca.Clear();
                cmbTipoVehiculo.SelectedIndex = -1;
                txtPlaca.Focus();
                this.dtgrid.ItemsSource = MostrarEntrada();
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

        
        // Creacion de la lista mostrar
        private List<ClassEstacionamiento> MostrarEntrada()
        {
            cn.Open();
            String query = @"SELECT Placa,TipoVehiculo,HoraEntrada FROM Estacionamiento.Vehiculo  INNER JOIN Estacionamiento.HoraEntrada he
                                ON Placa = he.PlacaVehiculo WHERE Placa = Placa";
            SqlCommand comando = new SqlCommand(query, cn);
            List<ClassEstacionamiento> Lista = new List<ClassEstacionamiento>();
            SqlDataReader reder = comando.ExecuteReader();
            
            while (reder.Read())
            {
                ClassEstacionamiento dato = new ClassEstacionamiento();
                dato.HoraEntrada = reder.GetDateTime(2);
                dato.Placa = reder.GetString(0);
                dato.TipoVehiculo = reder.GetString(1);
                Lista.Add(dato);
             }
            reder.Close();
            cn.Close();
            return Lista;

        }

        //Mostar los datos en el dataGrid
        private void Dtgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow listar = new MainWindow();
            listar.Show();
        }

  

        //Boton de actualizar
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow listar = new MainWindow();
            listar.Show();
        }



        //HoRA SALIDA

        private void BtnReporte_Click(object sender, RoutedEventArgs e)
        {
           

        }


        private void BtnPagar_Click(object sender, RoutedEventArgs e)
        {
            if (lbBuscarPlaca.SelectedValue == null)
                MessageBox.Show("Debes seleccionar un placa");
            else
            {

                try
                {
                    cn.Open();
                    string query = "INSERT INTO Estacionamiento.HoraSalida VALUES (GETDATE(),@placa)";
                    SqlCommand comando = new SqlCommand(query, cn);
                    comando.Parameters.AddWithValue("@placa", txtBuscarPlaca.Text);
                    comando.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    try
                    {
                        cn.Open();
                        string query = "DELETE HoraEntrada FROM Estacionamiento.HoraEntrada where PlacaVehiculo = @placa";
                        SqlCommand comando = new SqlCommand(query, cn);
                        comando.Parameters.AddWithValue("@placa", txtBuscarPlaca.Text);
                        comando.ExecuteNonQuery();
                        cn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ha ocurrido un error");
                        
                    }
                    MessageBox.Show("Gracias por su visita");
                    this.dtgrid.ItemsSource = MostrarEntrada();
                    txtBuscarPlaca.Text = String.Empty;
                    lbBuscarPlaca.ItemsSource = "";
                    txtBuscarPlaca.Focus();
                }
            }
        }

        private void BtnCancelarBuscar_Click(object sender, RoutedEventArgs e)
        {
            txtBuscarPlaca.Text = String.Empty;
            lbBuscarPlaca.ItemsSource = "";
            txtBuscarPlaca.Focus();
        }

        private void Buscar(object sender, RoutedEventArgs e)
        {
            if (txtBuscarPlaca.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar una placa en la caja de texto.");
                txtBuscarPlaca.Focus();
            }
            try
            {
                // Query para consultar
                string query = @"SELECT v.Placa, v.TipoVehiculo, he.HoraEntrada FROM Estacionamiento.Vehiculo v INNER JOIN Estacionamiento.HoraEntrada he
                                ON v.Placa = he.PlacaVehiculo WHERE v.Placa = @PlacaV";

                SqlCommand sqlCommand = new SqlCommand(query, cn);

                // SqlDataAdapter es una interfaz entre las tablas y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {
                    sqlCommand.Parameters.AddWithValue("@PlacaV", txtBuscarPlaca.Text);

                    //            Objecto en C# que refleja una tabla de una BD
                    DataTable tablaVehiculos = new DataTable();

                    //  Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaVehiculos);

                    lbBuscarPlaca.DisplayMemberPath = "HoraEntrada";

                    lbBuscarPlaca.SelectedValuePath = "Placa";

                    lbBuscarPlaca.ItemsSource = tablaVehiculos.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}


