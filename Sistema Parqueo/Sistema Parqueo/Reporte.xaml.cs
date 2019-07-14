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

namespace Sistema_Parqueo
{
    /// <summary>
    /// Lógica de interacción para Reporte.xaml
    /// </summary>
    public partial class Reporte : Window
    {
        ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
        public Reporte()
        {
            InitializeComponent();
            this.lbReporte.ItemsSource = estacionamiento.MostrarReporte();
        }

        private void MostrarVehiculosDentro()
        {
            ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
            lbReporte.ItemsSource = estacionamiento.MostrarReporte();
        }

        private void Salir(object sender, RoutedEventArgs e)
        {

            this.Hide();
        }
    }
}
