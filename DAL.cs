using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.X86;
using System.Configuration.Provider;
using System.Threading.Tasks;

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
            List<CajaDeAhorro> cajasReturn = new List<CajaDeAhorro>();

            //Creo la query que carga la lista TP2
            string queryString = "select * from dbo.CAJA_AHORRO";

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
                    CajaDeAhorro aux;
                    while (reader.Read())
                    {
                        //0:ID,1:DNI,2:NOMBRE;3:APELLIDO,4:MAIL,5:PASSWORD,6:BLOQUEADO,7:ADMINISTRADOR,8:INTENTOS_LOGUEO
                        aux = new CajaDeAhorro(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetDouble(3));
                        cajasReturn.Add(aux);
                    }
                    //Luego de recorrer la query se libera la memoria:
                    reader.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message);

                    MessageBox.Show(ex.Message);
                }
            }



            return cajasReturn;
        }


        public List<CajaDeAhorro> buscaCajasAhorroDeUsuario(int id_usuario)
        {
            List<CajaDeAhorro> cajasReturn = new List<CajaDeAhorro>();

            //Creo la query que carga la lista TP2
            MessageBox.Show("Titular: " + id_usuario);
            string queryString = "select * from dbo.[CAJA_AHORRO] WHERE [TITULAR]="+ id_usuario+";";
            //WHERE [TITULAR]="+id_usuario+"

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
                    CajaDeAhorro aux;
                    while (reader.Read())
                    {
                        //0:ID,1:DNI,2:NOMBRE;3:APELLIDO,4:MAIL,5:PASSWORD,6:BLOQUEADO,7:ADMINISTRADOR,8:INTENTOS_LOGUEO
                        aux = new CajaDeAhorro(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetDouble(3));
                        cajasReturn.Add(aux);
                    }
                    //Luego de recorrer la query se libera la memoria:
                    reader.Close();
                }
                catch (Exception ex) { 
                   
                    MessageBox.Show(ex.Message);

                }
            }


           
            return cajasReturn;
        }


        public int agregarCajaAhorro(string cbu, int titular)
        {

            int resultadoQuery;
            int idNuevaCaja = -1;
            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "INSERT INTO [dbo].[CAJA_AHORRO] ([CBU],[TITULAR],[SALDO]) VALUES (@cbu,@titular,0);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@cbu", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@titular", SqlDbType.Int));

                command.Parameters["@cbu"].Value = cbu;
                command.Parameters["@titular"].Value = titular;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();

                    //*******************************************
                    //Ahora hago esta query para obtener el ID con un  where DNI por concurrencia
                    string ConsultaID = "SELECT MAX([ID]) FROM [dbo].[CAJA_AHORRO] WHERE [CBU]=@cbu AND [TITULAR]=@titular";
                    command = new SqlCommand(ConsultaID, connection);
                    command.Parameters.Add(new SqlParameter("@cbu", SqlDbType.NVarChar));
                    command.Parameters.Add(new SqlParameter("@titular", SqlDbType.Int));

                    command.Parameters["@cbu"].Value = cbu;
                    command.Parameters["@titular"].Value = titular;
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevaCaja = reader.GetInt32(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevaCaja;
            }
        }

        public bool bajaCajaDeAhorro(int id)
        {

            int resultadoQuery;

            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "DELETE FROM [dbo].[CAJA_AHORRO] WHERE [ID]=@id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@cbu", SqlDbType.NVarChar));
                command.Parameters["@cbu"].Value = id;

                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }

       


        public bool bajaTitularCajaDeAhorro(int id_titular)
        {

            int resultadoQuery;

            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "DELETE FROM [dbo].[CAJA_AHORRO] WHERE [TITULAR]=@id_titular;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_titular", SqlDbType.Int));
                command.Parameters["@id_titular"].Value = id_titular;

                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }

        public bool altaTitularCajaDeAhorro(int id_titular,string cbu)
        {

            int resultadoQuery;

            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "UPDATE [dbo].[CAJA_AHORRO] SET [TITULAR]=@id_titular WHERE [CBU]=@cbu;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_titular", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@cbu", SqlDbType.NVarChar));
                command.Parameters["@id_titular"].Value = id_titular;
                command.Parameters["@cbu"].Value = cbu;

                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }

        public bool cambioParametroCaja(int id_caja, String parametro,double valor)
        {

            int resultadoQuery;

            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "UPDATE [dbo].[CAJA_AHORRO] SET ["+parametro+"]=@valor WHERE [ID]=@caja;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_caja", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@valor", SqlDbType.Float));
                command.Parameters["@id_caja"].Value = id_caja;
                command.Parameters["@valor"].Value = valor;

                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;

                }

            }
        }


        public List<TarjetaDeCredito> inicializarTarjetaDeCredito()
        {

            List<TarjetaDeCredito> tarjetasReturn = new List<TarjetaDeCredito>();

            string queryString = "select * from dbo.TARJETA_CREDITO";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {

                    connection.Open();
                    //Ejecuta la query :
                    SqlDataReader reader = command.ExecuteReader();

                    TarjetaDeCredito aux;
                    while (reader.Read())
                    {
                        aux = new TarjetaDeCredito(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetDouble(4), reader.GetDouble(5));
                        tarjetasReturn.Add(aux);
                    }
                    reader.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }



            return tarjetasReturn;
        }

        public int agregarTarjetaDeCredito(int id_usuario, int numero, int codigov, double limite)
        {
            int resultadoQuery;
            int idNuevaTarjeta = -1;
            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "INSERT INTO [dbo].[TARJETA_CREDITO] ([ID_USUARIO],[NUMERO],[CODIGOV],[LIMITE],[CONSUMOS]) VALUES (@id_usuario,@numero,@codigov,@limite,@consumos);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_usuario", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@numero", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@codigov", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@limite", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@consumos", SqlDbType.Float));

                command.Parameters["@id_usuario"].Value = id_usuario;
                command.Parameters["@numero"].Value = numero;
                command.Parameters["@codigov"].Value = codigov;
                command.Parameters["@limite"].Value = limite;
                command.Parameters["@consumos"].Value = 0;
                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();

                    string ConsultaID = "SELECT MAX([ID]) FROM [dbo].[TARJETA_CREDITO] WHERE [NUMERO]=@numero";
                    command = new SqlCommand(ConsultaID, connection);
                    command.Parameters.Add(new SqlParameter("@numero", SqlDbType.Int));
                    command.Parameters["@numero"].Value = numero;
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevaTarjeta = reader.GetInt32(0);
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevaTarjeta;
            }
        }


        public bool bajaTarjetaDeCredito(int id)
        {

            int resultadoQuery;

            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "DELETE FROM [dbo].[TARJETA_CREDITO] WHERE [ID]=@id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;

                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();
                    connection.Close();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }


        public bool cambioLimiteTarjeta(int id_tarjeta, float nuevoLimite)
        {

            int resultadoQuery;

            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "UPDATE [dbo].[TARJETA_CREDITO] SET [LIMITE]=@nuevoLimite WHERE [ID]=@id_tarjeta;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_tarjeta", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nuevoLimite", SqlDbType.Float));
                command.Parameters["@id_tarjeta"].Value = id_tarjeta;
                command.Parameters["@nuevoLimite"].Value = nuevoLimite;

                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;

                }

            }
        }


        public List<TarjetaDeCredito> buscaTarjetasDeCreditoUsuario(int id_usuario)
        {
            List<TarjetaDeCredito> tarjetasReturn = new List<TarjetaDeCredito>();

            MessageBox.Show("Titular: " + id_usuario);
            string queryString = "select * from dbo.[TARJETA_CREDITO] WHERE [ID_USUARIO]= @id_usuario;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_usuario", SqlDbType.Int));
                command.Parameters["@id_usuario"].Value=id_usuario;



                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    TarjetaDeCredito aux;
                    while (reader.Read())
                    {
                        aux = new TarjetaDeCredito(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetDouble(4), reader.GetDouble(5));
                        tarjetasReturn.Add(aux);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);

                }
            }



            return tarjetasReturn;
        }



        public List<PlazoFijo> inicializarPlazoFijo()
        {

            List<PlazoFijo> plazoFijoReturn = new List<PlazoFijo>();

            string queryString = "select * from dbo.PLAZO_FIJO";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {

                    connection.Open();
                    //Ejecuta la query :
                    SqlDataReader reader = command.ExecuteReader();

                    PlazoFijo aux;
                    while (reader.Read())
                    {
                        aux = new PlazoFijo(reader.GetInt32(0),reader.GetInt32(1),reader.GetDouble(2),reader.GetDateTime(3),reader.GetDouble(4));
                        plazoFijoReturn.Add(aux);
                    }
                    reader.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }



            return plazoFijoReturn;
        }

        public int agregarPlazoFijo(int id_usuario, double monto,DateTime fechaFin,double tasa)
        {
            int resultadoQuery;
            int idNuevoPlazoFijo = -1;
            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "INSERT INTO [dbo].[PLAZO_FIJO] ([ID_USUARIO],[MONTO],[FECHA_FIN],[TASA] VALUES (@id_usuario,@monto,@fecha,@tasa);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_usuario", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@tasa", SqlDbType.Float));

                command.Parameters["@id_usuario"].Value = id_usuario;
                command.Parameters["@monto"].Value = monto;
                command.Parameters["@fecha"].Value = fechaFin;
                command.Parameters["@tasa"].Value = tasa;
                
                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();

                    string ConsultaID = "SELECT MAX([ID]) FROM [dbo].[PLAZO_FIJO] WHERE [MONTO]=@monto";
                    command = new SqlCommand(ConsultaID, connection);
                    command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Float));
                    command.Parameters["@monto"].Value = monto;
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoPlazoFijo = reader.GetInt32(0);
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevoPlazoFijo;
            }
        }

        public bool bajaPlazoFijo(int id)
        {

            int resultadoQuery;

            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "DELETE FROM [dbo].[PLAZO_FIJO] WHERE [ID]=@id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;

                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();
                    connection.Close();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }


        public List<PlazoFijo> buscaPlazoFijo(int id_usuario)
        {
            List<PlazoFijo> PlazoFijoReturn = new List<PlazoFijo>();

            MessageBox.Show("Titular: " + id_usuario);
            string queryString = "select * from dbo.[PLAZO_FIJO] WHERE [ID_USUARIO]= @id_usuario;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_usuario", SqlDbType.Int));
                command.Parameters["@id_usuario"].Value = id_usuario;



                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    PlazoFijo aux;
                    while (reader.Read())
                    {
                        aux = new PlazoFijo(reader.GetInt32(0),reader.GetInt32(1),reader.GetDouble(2), reader.GetDateTime(3),reader.GetFloat(4));
                        PlazoFijoReturn.Add(aux);
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);

                }
            }



            return PlazoFijoReturn;
        }


        public List<Movimiento> inicializarMovimiento()
        {

            List<Movimiento> movimientoReturn = new List<Movimiento>();

            string queryString = "select * from dbo.Movimiento";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    Movimiento aux;
                    while (reader.Read())
                    {
                        aux = new Movimiento(reader.GetInt32(0),reader.GetInt32(1),reader.GetString(2),reader.GetDouble(3),reader.GetDateTime(4));
                        movimientoReturn.Add(aux);
                    }
                    reader.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }



            return movimientoReturn;
        }


        public int agregarMovimiento(int id_cajaDeAhorro, String detalle, double monto, DateTime fecha)
        {
            int resultadoQuery;
            int idNuevoMovimiento = -1;
            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "INSERT INTO [dbo].[MOVIMIENTO] ([id_caja_ahorro],[descripcion],[monto],[fecha] VALUES (@id_cajaDeAhorro,@detalle,@monto,@fecha);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_cajaDeAhorro", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@detalle", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Float));

                command.Parameters["@id_cajaDeAhorro"].Value = id_cajaDeAhorro;
                command.Parameters["@monto"].Value = monto;
                command.Parameters["@fecha"].Value = fecha;
                command.Parameters["@detalle"].Value = detalle;

                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();

                    string ConsultaID = "SELECT MAX([id]) FROM [dbo].[MOVIMIENTO] WHERE [monto]=@monto";
                    command = new SqlCommand(ConsultaID, connection);
                    command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Float));
                    command.Parameters["@monto"].Value = monto;
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoMovimiento = reader.GetInt32(0);
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevoMovimiento;
            }
        }

        public bool bajaMovimiento(int id)
        {

            int resultadoQuery;

            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "DELETE FROM [dbo].[MOVIMIENTO] WHERE [id]=@id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;

                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();
                    connection.Close();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }


        public List<Movimiento> buscarMovimiento(int id_usuario)
        {
            List<Movimiento> movimientoReturn = new List<Movimiento>();

            MessageBox.Show("Titular: " + id_usuario);
            string queryString = "select * from dbo.[Movimiento] WHERE [ID_USUARIO]= @id_usuario;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_usuario", SqlDbType.Int));
                command.Parameters["@id_usuario"].Value = id_usuario;



                try
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    Movimiento aux;
                    while (reader.Read())
                    {
                        aux = new Movimiento(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDouble(3), reader.GetDateTime(4));
                        movimientoReturn.Add(aux);
                    }
                    reader.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }



            return movimientoReturn;
        }


        public List<Pago> inicializarPago()
        {

            List<Pago> pagoReturn = new List<Pago>();

            string queryString = "select * from dbo.Pago";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    Pago aux;
                    while (reader.Read())
                    {
                        aux = new Pago(reader.GetInt32(0),reader.GetInt32(1),reader.GetDouble(2) ,reader.GetInt32(3), reader.GetString(4), reader.GetString(5),reader.GetInt32(6));
                        pagoReturn.Add(aux);
                    }
                    reader.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }



            return pagoReturn;
        }


        public int agregarPago(int id_usuario, double monto,String metodo, String detalle, int id_metodo)
        {
            int resultadoQuery;
            int idNuevoPago = -1;
            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "INSERT INTO [dbo].[PAGO] ([ID_USUARIO],[MONTO],[PAGADO],[METODO],[DETALLE],[ID_METODO] VALUES (@id_usuario,@monto,@pagado,@metodo,@detalle,@id_metodo);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_usuario", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Float));
                command.Parameters.Add(new SqlParameter("@pagado", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@metodo", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@detalle", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@id_metodo", SqlDbType.Int));

                command.Parameters["@id_usuario"].Value = id_usuario;
                command.Parameters["@monto"].Value = monto;
                command.Parameters["@pagado"].Value = 0;
                command.Parameters["@metodo"].Value = metodo;
                command.Parameters["@detalle"].Value = detalle;
                command.Parameters["@id_metodo"].Value = id_metodo;


                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();

                    string ConsultaID = "SELECT MAX([id]) FROM [dbo].[Pago] WHERE [monto]=@monto";
                    command = new SqlCommand(ConsultaID, connection);
                    command.Parameters.Add(new SqlParameter("@monto", SqlDbType.Float));
                    command.Parameters["@monto"].Value = monto;
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoPago = reader.GetInt32(0);
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
                return idNuevoPago;
            }
        }

        public bool bajaPago(int id)
        {

            int resultadoQuery;

            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "DELETE FROM [dbo].[PAGO] WHERE [id]=@id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = id;

                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();
                    connection.Close();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }


        public List<Pago> buscarPago(int id_usuario)
        {
            List<Pago> pagoReturn = new List<Pago>();

            MessageBox.Show("Titular: " + id_usuario);
            string queryString = "select * from dbo.[PAGO] WHERE [ID_USUARIO]= @id_usuario;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_usuario", SqlDbType.Int));
                command.Parameters["@id_usuario"].Value = id_usuario;

                try
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    Pago aux;
                    while (reader.Read())
                    {
                        aux = new Pago(reader.GetInt32(0), reader.GetInt32(1), reader.GetDouble(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6));
                        pagoReturn.Add(aux);
                    }
                    reader.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }



            return pagoReturn;
        }


        public bool cambioPago(int id_pago)
        {

            int resultadoQuery;

            string connectionString = Properties.Resources.stringDeConexion;
            string queryString = "UPDATE [dbo].[PAGO] SET [PAGADO]=@pagado WHERE [ID]=@id_pago;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id_pago", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@pagado", SqlDbType.Int));
                command.Parameters["@id_pago"].Value = id_pago;
                command.Parameters["@pagado"].Value = 1;

                try
                {
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;

                }

            }
        }


    }
}
