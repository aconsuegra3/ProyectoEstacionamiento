﻿using System;
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
        SqlConnection cn;
        public MainWindow()
        {
            InitializeComponent();
            cn = new SqlConnection("Data Source = ABELCONSUEGRA; Initial Catalog = SistemaDeEstacionamiento; Integrated Security = True");
            MostrarVehiculosDentro();
        }

        private void Salir(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Aceptar(object sender, RoutedEventArgs e)
        {
            ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
            estacionamiento.Placa = txtPlaca.Text;
            estacionamiento.TipoVehiculo = cmbTipoVehiculo.Text;
            estacionamiento.InsertarVehiculo();

        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            txtPlaca.Clear();
            cmbTipoVehiculo.SelectedIndex = -1;
        }

        // Método para mostrar los vehículos dentro del estacionamiento
        private void MostrarVehiculosDentro()
        {
            try
            {
                // Query para consultar los vehiculos con su hora de entrada
                string query = @"SELECT v.Placa, v.TipoVehiculo, he.HoraEntrada FROM Estacionamiento.Vehiculo v INNER JOIN Estacionamiento.HoraEntrada he
                                ON v.Placa = he.PlacaVehiculo";

                // Comando para el query
                SqlCommand sqlCommand = new SqlCommand(query, cn);

                // Adaptador para realizar el comando del query
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                using (sqlDataAdapter)
                {
                    // Creamos la DataTable para mostrar en pantalla
                    DataTable tablaVehiculosDentro = new DataTable();

                    // Llenar el objeto de tipo DataTable
                    sqlDataAdapter.Fill(tablaVehiculosDentro);

                    lbVehiculosDentro.DisplayMemberPath = "Placa";

                    lbVehiculosDentro.SelectedValuePath = "Placa";

                    lbVehiculosDentro.ItemsSource = tablaVehiculosDentro.DefaultView;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
