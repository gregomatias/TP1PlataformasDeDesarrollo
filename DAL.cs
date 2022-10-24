using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.X86;
using System.Configuration.Provider;

namespace TP1
{
    internal class DAL
    {
        private string connectionString;

        public DAL()
        {
            //Ver>Explorador de Servidores, conectar DB, en las propiedades esta el string
            //Cambiar string de conexion en Proyecto>Propiedades>CrearAbrirRecurso..
            connectionString = Properties.Resources.stringDeConexion;
        }


        public List<Usuario> inicializarUsuarios()
        {

            List<Usuario> usuariosReturn = new List<Usuario>();

            //Creo la query que carga la lista TP2
            string queryString = "select * from dbo.USUARIO";

            //Clic derecho en proyecto e instalar poryecto Nuget System.Data.SqlClient
            //Creo una conexion con la DB
            //string providerName = "System.Data. SqlClient";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                //Union entre la conexion y el query a ejecutar
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    //Abro la conexion
                    connection.Open();
                    //Ejecuta la query :
                    SqlDataReader reader = command.ExecuteReader();

                    //reader.Read() se para en cada fila de la query y mira si tene datos
                    Usuario aux;
                    while (reader.Read())
                    {
                        //0:ID,1:DNI,2:NOMBRE;3:APELLIDO,4:MAIL,5:PASSWORD,6:BLOQUEADO,7:ADMINISTRADOR,8:INTENTOS_LOGUEO
                        aux = new Usuario(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetBoolean(6), reader.GetBoolean(7), reader.GetInt32(8));
                        usuariosReturn.Add(aux);
                    }
                    //Luego de recorrer la query se libera la memoria:
                    reader.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }



            return usuariosReturn;

        }
        public int agregarUsuario(int Dni, string Nombre, string Apellido, string Mail, string Password, bool Bloqueado, bool EsADM,int IntentosLogueo)
        {

            int resultadoQuery;
            int idNuevoUsuario = -1;
            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "INSERT INTO [dbo].[USUARIO] ([DNI],[NOMBRE],[APELLIDO],[MAIL],[PASSWORD],[BLOQUEADO],[ADMINISTRADOR],[INTENTOS_LOGUEO]) VALUES (@dni,@nombre,@apellido,@mail,@password,@bloqueado,@esadm,@intentos);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // MessageBox.Show(Dni + " " + Nombre + " " + Apellido + " " + Mail + " " + Password + " " + Bloqueado + " " + EsADM);
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@mail", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@bloqueado", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@esadm", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@intentos", SqlDbType.Int));

                command.Parameters["@dni"].Value = Dni;
                command.Parameters["@nombre"].Value = Nombre;
                command.Parameters["@apellido"].Value = Apellido;
                command.Parameters["@mail"].Value = Mail;
                command.Parameters["@password"].Value = Password;
                command.Parameters["@bloqueado"].Value = Bloqueado;
                command.Parameters["@esadm"].Value = EsADM;
                command.Parameters["@intentos"].Value = IntentosLogueo;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();

                    //*******************************************
                    //Ahora hago esta query para obtener el ID con un  where DNI por concurrencia
                    string ConsultaID = "SELECT MAX([ID]) FROM [dbo].[USUARIO] WHERE [DNI]=@dni";
                    command = new SqlCommand(ConsultaID, connection);
                    command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                    command.Parameters["@dni"].Value = Dni;
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoUsuario = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevoUsuario;
            }
        }

        public void bloquearUsuario(int Dni)
        {

            int resultadoQuery;

            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "UPDATE [dbo].[USUARIO] SET [BLOQUEADO]=1 WHERE [DNI]=@dni;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters["@dni"].Value = Dni;

                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }

            }
        }
        public void actualizaIntentosDeLogueo(int Dni,int numeroDeIntento)
        {

            int resultadoQuery;

            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "UPDATE [dbo].[USUARIO] SET [INTENTOS_LOGUEO]=@intentos WHERE [DNI]=@dni;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@intentos", SqlDbType.Int));
                command.Parameters["@dni"].Value = Dni;
                command.Parameters["@intentos"].Value = numeroDeIntento;

                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }

            }
        }


        public List<CajaDeAhorro> inicializarCajasAhorro()
        {
            return new List<CajaDeAhorro>();
        }


    }
}
