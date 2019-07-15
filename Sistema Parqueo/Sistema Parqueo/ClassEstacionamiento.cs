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
        private DateTime horaSalida;
        private string placa;
        private string tipoVehiculo;
        private decimal costo;
        private int tiempoTotal;
        private decimal total;

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
        public DateTime HoraSalida
        {
            get { return horaSalida; }
            set { horaSalida = value; }
        }
        public decimal Costo
        {
            get { return costo; }
            set { costo = value; }
        }
        public int TiempoTotal
        {
            get { return tiempoTotal; }
            set { tiempoTotal = value; }
        }
        public decimal Total
        {
            get { return total; }
            set { total = value; }
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
                MessageBox.Show("Ha ocurrido un error con salidavehiculo");
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
                     MessageBox.Show("Placa no existe");

                 }              
             }        
        }

        


        // Creacion de la lista mostrar
        public List<ClassEstacionamiento> MostrarEntrada()
        {
            cn.Open();
            String query = @"SELECT Placa,TipoVehiculo,HoraEntrada FROM Estacionamiento.Vehiculo  INNER JOIN Estacionamiento.Detalle he
                                ON Placa = he.PlacaVehiculo WHERE Placa = Placa";
            SqlCommand comando = new SqlCommand(query, cn);
            List<ClassEstacionamiento> Lista = new List<ClassEstacionamiento>();
            SqlDataReader reder = comando.ExecuteReader();

            while (reder.Read())
            {
                ClassEstacionamiento dato = new ClassEstacionamiento();
                dato.Placa = reder.GetString(0);
                dato.TipoVehiculo = reder.GetString(1);
                dato.HoraEntrada = reder.GetDateTime(2);
                //lbVehiculosDentroEstacionamiento.SelectedValuePath = "Placa";
                Lista.Add(dato);
            }
            reder.Close();
            cn.Close();
            return Lista;
        }
        
        public List<ClassEstacionamiento> BuscarEntrada()
        {
            
            cn.Open();
            String query = @"SELECT Placa,TipoVehiculo,HoraEntrada FROM Estacionamiento.Vehiculo  INNER JOIN Estacionamiento.Detalle he
                                ON Placa = he.PlacaVehiculo WHERE Placa = @Placa";
            SqlCommand comando = new SqlCommand(query, cn);

            comando.Parameters.AddWithValue("@Placa", Placa);
            List<ClassEstacionamiento> ListaB = new List<ClassEstacionamiento>();
            SqlDataReader reder = comando.ExecuteReader();

            while (reder.Read())
            {
                ClassEstacionamiento datob = new ClassEstacionamiento();
                datob.Placa = reder.GetString(0);
                datob.TipoVehiculo = reder.GetString(1);
                datob.HoraEntrada = reder.GetDateTime(2);
               //lbVehiculosEstacionamiento.SelectedValuePath = "Placa";
                ListaB.Add(datob);
            }
            reder.Close();
            cn.Close();
            return ListaB;                        
        }
        
        // Creacion de la lista mostrarReporte
        public List<ClassEstacionamiento> MostrarReporte()
        {
            cn.Open();
            string query = "SELECT * FROM Estacionamiento.Reporte ";

            SqlCommand comando = new SqlCommand(query, cn);
            List<ClassEstacionamiento> reporte = new List<ClassEstacionamiento>();
            SqlDataReader reder = comando.ExecuteReader();

            while (reder.Read())
            {
                ClassEstacionamiento datoR = new ClassEstacionamiento();
                datoR.Placa = reder.GetString(1);
                datoR.TipoVehiculo = reder.GetString(2);
                datoR.HoraEntrada = reder.GetDateTime(3);
                datoR.HoraSalida = reder.GetDateTime(4);
                datoR.TiempoTotal = reder.GetInt32(5);
                datoR.Costo = reder.GetDecimal(6);

                // lbVehiculosDentroEstacionamiento.SelectedValuePath = "Placa";
                reporte.Add(datoR);
            }
            reder.Close();
            cn.Close();
            return reporte;
        }

        // Creacion de la lista Reporte de valor total
        public List<ClassEstacionamiento> Mostrartotal()
        {
            cn.Open();
            string query = "SELECT SUM (Costo) FROM Estacionamiento.Reporte ";

            SqlCommand comando = new SqlCommand(query, cn);
            List<ClassEstacionamiento> reporte = new List<ClassEstacionamiento>();
            SqlDataReader reder = comando.ExecuteReader();

            while (reder.Read())
            {
               
                ClassEstacionamiento datoR = new ClassEstacionamiento();
                datoR.Total = reder.GetDecimal(0);
                reporte.Add(datoR);
            }
            reder.Close();
            cn.Close();
            return reporte;
        }

        // Creacion de la lista mostrarMensaje
        public List<ClassEstacionamiento> MostrarPago()
        {
            cn.Open();
            string query = " SELECT TOP 1 * FROM Estacionamiento.Reporte ORDER BY id DESC";
            SqlCommand comando = new SqlCommand(query, cn);
            comando.Parameters.AddWithValue("@placa", Placa);
            List<ClassEstacionamiento> reporte = new List<ClassEstacionamiento>();
            SqlDataReader reder = comando.ExecuteReader();

            while (reder.Read())
            {
                ClassEstacionamiento datoR = new ClassEstacionamiento();
                datoR.Placa = reder.GetString(1);
                datoR.TipoVehiculo = reder.GetString(2);
                datoR.TiempoTotal = reder.GetInt32(5);
                datoR.Costo = reder.GetDecimal(6);
                
                reporte.Add(datoR);
            }
            reder.Close();
            cn.Close();
            return reporte;
        }

    }
}

