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
           
        }

        private void MostrarRTotal()
        {
            ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
            lbtotal.ItemsSource = estacionamiento.Mostrartotal();
        }

        private void Salir(object sender, RoutedEventArgs e)
        {
            MainWindow main =new MainWindow();
            this.Hide();
            main.Show();
        }

        private void Atras_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Hide();
            main.Show();
        }

        private void total(object sender, RoutedEventArgs e)
        {
            this.lbtotal.ItemsSource = estacionamiento.Mostrartotal();
        }
    }
}
