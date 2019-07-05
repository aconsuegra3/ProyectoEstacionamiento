using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Sistema_Parqueo
{
    class ClassEstacionamiento
    {
        private string placa;
        private string tipoVehiculo;
        SqlConnection cn = new SqlConnection("Data Source = LAPTOP-H5OOPDVV\\SQLEXPRESS; Initial Catalog = SistemaDeEstacionamiento; Integrated Security = True");
     public ClassEstacionamiento()
        {
            placa = "PorDefecto";
            tipoVehiculo = "PorDefecto";
        }
        
        public string Placa
        {
            get { return placa; }
            set { placa = value; }
        }
        public string TipoVehiculo
        {
            get {return tipoVehiculo;}
            set {tipoVehiculo= value;}
        }

        public void InsertarVehiculo()
        {
            try
            {
                cn.Open();
                string query = "INSERT INTO Estacionamiento.Vehiculo VALUES (@placa,@tipovehiculo)";
                SqlCommand comando = new SqlCommand(query, cn);
                comando.Parameters.AddWithValue("@placa", Placa);
                comando.Parameters.AddWithValue("@tipovehiculo", TipoVehiculo);
                comando.ExecuteNonQuery();
                MessageBox.Show("El vehiculo se ha agregado");
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.ToString());
            }
            finally
            {
                cn.Close();
            }



        }





    }
}
