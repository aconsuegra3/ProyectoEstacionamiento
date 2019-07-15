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
    /// Lógica de interacción para mensajePago.xaml
    /// </summary>
    public partial class mensajePago : Window
    {
        // Instancia de la Clase estacionamiento
        ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
        public mensajePago()
        {
            InitializeComponent();
            // para que el listbox muestre al cargar la ventana
            this.lbpago.ItemsSource = estacionamiento.MostrarPago();
        }

        private void mostrarMensaje()
        {
            ClassEstacionamiento estacionamiento = new ClassEstacionamiento();
            lbpago.ItemsSource = estacionamiento.MostrarPago();
        }

        // botón aceptar     
        private void Aceptar(object sender, RoutedEventArgs e)
        {
            // se cierra (esconde) esta ventana al dar click al botón aceptar
            this.Hide();
        }
    }
}
