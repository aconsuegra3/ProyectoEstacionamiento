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
        private DateTime horaEntrada;
        private string placa;
        private string tipoVehiculo;
        private decimal costo;
        SqlConnection cn = new SqlConnection("Data Source = ABELCONSUEGRA; Initial Catalog = SistemaDeEstacionamiento; Integrated Security = True");
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
            get { return tipoVehiculo; }
            set { tipoVehiculo = value; }
        }
        public DateTime HoraEntrada
        {
            get { return horaEntrada; }
            set { horaEntrada = value; }
        }
        public decimal Costo
        {
            get { return costo; }
            set { costo = value; }
        }

        //Valida si la placa existe o se debe insertar
        public Boolean Validar()
        {
            cn.Open();
            string query = "SELECT COUNT(*) FROM Estacionamiento.Vehiculo WHERE Placa=@placa";
            SqlCommand comando = new SqlCommand(query, cn);
            comando.Parameters.AddWithValue("@placa", Placa);

            int cant = Convert.ToInt32(comando.ExecuteScalar());
            cn.Close();
            if (cant == 0)
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        //Valida si el vehiculo es igual a la placa 
        public Boolean ValidarVehiculo()
        {
            cn.Open();
            string query = "SELECT COUNT(*) FROM Estacionamiento.Vehiculo WHERE Placa=@placa AND TipoVehiculo=@tipo";
            SqlCommand comando = new SqlCommand(query, cn);
            comando.Parameters.AddWithValue("@placa", Placa);
            comando.Parameters.AddWithValue("@tipo", TipoVehiculo);
            int cant = Convert.ToInt32(comando.ExecuteScalar());
            cn.Close();
            if (cant == 0)
            {
                return false;

            }
            else
            {
                return true;
            }
        }
        

        //Insertar vehiculo a la base de datos
        public void InsertarVehiculo()
        {
            //Si la placa no existe se guarda en la base de datos
            if (Validar() == false)
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
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    cn.Close();
                }
            }

            //Luego se pasa a guardar el ingreso del vehiculo
            if (ValidarVehiculo() == true)
            {
                try
                {
                    cn.Open();
                    string query = "INSERT INTO Estacionamiento.Detalle VALUES (GETDATE(),GEtDATE(),@placa)";
                    SqlCommand comando = new SqlCommand(query, cn);
                    comando.Parameters.AddWithValue("@placa", Placa);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Bienvenido");
                    cn.Close();
                }
                catch (Exception )
                {
                    MessageBox.Show("El vehiculo aun esta en el estacionamiento");
                }
            }
            else
            {
                MessageBox.Show("El vehiculo no coincide con la placa registrada");
            }

        }

        public void SalidaVehiculo()
        {
            try
            {
                cn.Open();
                string query = "UPDATE Estacionamiento.Detalle SET horaSalida=GETDATE() WHERE placaVehiculo =@placa";
                SqlCommand comando = new SqlCommand(query, cn);
                comando.Parameters.AddWithValue("@placa", Placa);
                comando.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception )
            {
                MessageBox.Show("Ha ocurrido un error");
            }
            finally
             {
                 try
                 {
                     cn.Open();
                     string query = "DELETE FROM Estacionamiento.Detalle where PlacaVehiculo = @placa";
                     SqlCommand comando = new SqlCommand(query, cn);
                     comando.Parameters.AddWithValue("@placa", Placa);
                     comando.ExecuteNonQuery();
                     cn.Close();
                 }
                 catch (Exception )
                 {
                     MessageBox.Show("Ha ocurrido un error");

                 }              
             }        
        }

    }
}

