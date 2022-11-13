using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TP1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TP1
{

    internal class Banco
    {
        private List<CajaDeAhorro> cajas;
        private List<Usuario> usuarios;
        private List<PlazoFijo> pfs;
        private List<TarjetaDeCredito> tarjetas;
        private List<Pago> pagos;
        private List<Movimiento> movimientos;
        private Usuario? usuarioLogueado;

        DAL DB;


        public Banco()
        {

            this.pfs = new List<PlazoFijo>();
            this.tarjetas = new List<TarjetaDeCredito>();
            this.pagos = new List<Pago>();
            this.movimientos = new List<Movimiento>();
            //TP2 Cargo Listas con la info de la DB
            DB = new DAL();
            this.usuarios = DB.inicializarUsuarios();
            this.cajas = DB.inicializarCajasAhorro();
            this.pfs = DB.inicializarPlazoFijo();
            this.tarjetas = DB.inicializarTarjetaDeCredito();
            this.pagos = DB.inicializarPago();
            this.movimientos = DB.inicializarMovimiento();

            /*Hardcodeo de Admin para poder probarlo:*/
            AltaUsuario(123, "Walter", "Gomez", "wg@gmail.com", "123", false, true, 0);


        }


        public string obtieneSecuencia(Usuario usuario)
        {
            //Genera secuencia unica de CBU o Tarjeta
            //El usuario se pasa porque el Admin podria crear TJ o CBU
            DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
            string fecha = now.ToString("yyyyMMddHHmmssfff");

            return usuario._id + fecha;


        }



        public bool Pagar(CajaDeAhorro caja, double monto)
        {
            if (caja._saldo >= monto)
            {
                try
                {

                    double saldoNuevo = caja._saldo - monto;
                    caja._saldo = saldoNuevo;
                    DB.actualizaSaldoCajaAhorro(caja._cbu, saldoNuevo);

                    AltaMovimiento(caja, "Pago Con Caja Ahorro", monto);

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            else { return false; }

        }



        public string GetNombreUsuarioLogueado()
        {
            try
            {
                return usuarioLogueado._nombre + " " + usuarioLogueado._apellido;
            }
            catch (Exception) { return "Usuario Indeterminado"; }
        }

        public bool IniciarSesion(int dni, string contrasena)
        {


            foreach (Usuario usuario in usuarios)
            {

                if (usuario._dni == dni)
                {

                    usuarioLogueado = usuario;
                    if (usuarioLogueado._bloqueado)
                    {
                        return false;

                    }
                    else if (usuarioLogueado._intentosFallidos >= 3)
                    {
                        //Por referencia tanto en el logueado como en la lista
                        usuarioLogueado._bloqueado = true;
                        DB.bloquearUsuario(dni);
                        return false;

                    }
                    else if (usuarioLogueado._password == contrasena)
                    {
                        usuarioLogueado._intentosFallidos = 0;
                        DB.actualizaIntentosDeLogueo(usuarioLogueado._dni, 0);
                        usuarioLogueado._Cajas = DB.buscaCajasAhorroDeUsuario(usuarioLogueado._id);
                        return true;
                    }
                    else
                    {
                        usuarioLogueado._intentosFallidos++;
                        DB.actualizaIntentosDeLogueo(usuarioLogueado._dni, usuarioLogueado._intentosFallidos);

                    }
                }
            }

            return false;
        }

        public bool CerrarSesion()
        {
            usuarioLogueado = null;
            return true;
        }

        public bool CrearCajaDeAhorro()
        {

            string cbuNuevo = obtieneSecuencia(usuarioLogueado);


            int idCajaNueva = DB.agregarCajaAhorro(cbuNuevo, usuarioLogueado._id);


            if (idCajaNueva != -1)
            {
                CajaDeAhorro caja = new CajaDeAhorro(idCajaNueva, cbuNuevo, usuarioLogueado._id, 0);
                AltaCajaDeAhorro(usuarioLogueado, caja);
                cajas.Add(caja);
                return true;

            }
            else { return false; }

        }


        public void AltaCajaDeAhorro(Usuario usuario, CajaDeAhorro caja)
        {
            usuario._Cajas.Add(caja);

        }

        public bool Depositar(string cbu, double monto)
        {
            foreach (CajaDeAhorro caja in cajas)
            {

                if (caja._cbu == cbu)
                {
                    double saldoActualizado = caja._saldo + monto;

                    DB.actualizaSaldoCajaAhorro(cbu, saldoActualizado);
                    caja._saldo = saldoActualizado;

                    AltaMovimiento(caja, "Deposito", monto);
                    return true;
                }
            }
            return false;



        }







        public bool Retirar(string cbu, double monto)
        {

            foreach (CajaDeAhorro caja in cajas)
            {

                if (caja._cbu == cbu)

                {
                    if (caja._saldo > monto)
                    {
                        double saldoActualizado = caja._saldo - monto;

                        DB.actualizaSaldoCajaAhorro(cbu, saldoActualizado);
                        caja._saldo = saldoActualizado;

                        AltaMovimiento(caja, "Retiro", monto);
                        return true;

                    }
                    else { return false; }

                }
            }
            return false;
        }



        public bool Transferir(string cbuEmisor, string cbuDestino, float monto)
        {


            CajaDeAhorro? cajaEmisor = cajas.Where(caja => caja._cbu == cbuEmisor && caja._saldo >= monto).FirstOrDefault();
            CajaDeAhorro? cajaDestino = cajas.Where(caja => caja._cbu == cbuDestino).FirstOrDefault();


            if (cajaEmisor != null && cajaDestino != null)
            {

                try
                {
                    double saldoEmisor = cajaEmisor._saldo - monto;
                    double saldoDestino = cajaDestino._saldo + monto;
                    cajaEmisor._saldo = saldoEmisor;
                    cajaDestino._saldo = saldoDestino;

                    AltaMovimiento(cajaEmisor, "Transferencia Emitida", monto);
                    AltaMovimiento(cajaDestino, "Transferencia Recibida", monto);

                    DB.actualizaSaldoCajaAhorro(cbuEmisor, saldoEmisor);
                    DB.actualizaSaldoCajaAhorro(cbuDestino, saldoDestino);





                    return true;

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }



            return false;


        }




        public string buscarCBU(int id)
        {
            string cbu = "";
            try
            {
                cbu = DB.buscarCbuById(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se produjo un error: " + ex.Message);
            }

            return cbu;
        }




        public List<Movimiento> BuscarMovimiento(string cbuCaja, string detalle = "default", DateTime? fecha = null, float monto = 0)
        {
            List<Movimiento> listaMovimientos = new List<Movimiento>();
            List<Movimiento> listaMovimientosFiltrados = new List<Movimiento>();
            usuarioLogueado.cajas = DB.buscaCajasAhorroDeUsuario(usuarioLogueado._id);

            //Busca Id de caja de ahoro
            CajaDeAhorro? caja = usuarioLogueado._Cajas.Where(caja => caja._cbu == cbuCaja).FirstOrDefault();


            //Busca movimientos de la caja buscada anteriormente
            listaMovimientos = DB.buscarMovimiento(caja._id);



            foreach (Movimiento movimiento in listaMovimientos)
            {

                if (movimiento._detalle == detalle || movimiento._fecha.Date == fecha.Value.Date || movimiento._monto == monto)


                {
                    listaMovimientosFiltrados.Add(movimiento);
                }

            }





            return listaMovimientosFiltrados.ToList();
        }





        public bool PagarTarjeta(string numeroTarjeta, string cbuCajaAhorro)
        {


            TarjetaDeCredito? tarjeta = tarjetas.Where(tarjeta => tarjeta._numero == numeroTarjeta && tarjeta._consumos > 0).FirstOrDefault();

            if (tarjeta != null)
            {

                CajaDeAhorro? caja = cajas.Where(caja => caja._cbu == cbuCajaAhorro && caja._saldo >= tarjeta._consumos).FirstOrDefault();

                if (caja != null)
                {


                    try
                    {

                        double saldoActualizado = caja._saldo - tarjeta._consumos;
                        DB.actualizaSaldoCajaAhorro(cbuCajaAhorro, saldoActualizado);
                        caja._saldo = saldoActualizado;
                        AltaMovimiento(caja, "Consumos de Tarjeta", tarjeta._consumos);
                        tarjeta._consumos = 0;
                        DB.actualizaConsumoTarjeta(numeroTarjeta, 0);



                        return true;

                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); return false; }

                }


            }

            return false;

        }



        public bool AltaUsuario(int dni, string nombre, string apellido, string mail, string password, bool bloqueado, bool esAdmin, int intentosLogueo)
        {

            //valida usuario Duplicado
            bool esValido = true;
            foreach (Usuario usuario in usuarios)
            {
                if (usuario._dni == dni)
                    esValido = false;
            }
            if (esValido)
            {
                int idNuevoUsuario;
                idNuevoUsuario = DB.agregarUsuario(dni, nombre, apellido, mail, password, bloqueado, esAdmin, intentosLogueo);

                if (idNuevoUsuario != -1)
                {
                    //Agrega a la lista con el ID obtenido de la DB
                    Usuario nuevoUsuario = new Usuario(idNuevoUsuario, dni, nombre, apellido, mail, password, bloqueado, esAdmin, intentosLogueo);
                    usuarios.Add(nuevoUsuario);

                    return true;

                }
                else
                {
                    //No se genero el ID nuevo, por ende hay un error
                    return false;
                }
            }
            else
                return false;


        }


        public bool ModificarUsuario(int id, int dni, string nombre, string apellido, string password, string mail)
        {
            try
            {
                foreach (Usuario usuario in usuarios)
                {
                    if (usuario._id == id)
                    {
                        usuario._dni = dni;
                        usuario._nombre = nombre;
                        usuario._apellido = apellido;
                        usuario._password = password;
                        usuario._mail = mail;
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public bool desbloquearUsuario(int id)
        {
            Usuario? Us = usuarios.Where(US => US._id == id).FirstOrDefault();

            if (Us._bloqueado == true)
            {
                DB.desbloquearUsuario(id);
                Us._bloqueado = false;
                return true;
            }
            else
            {
                return false;
            }


        }

        public bool EliminarUsuario(int id_usuario)
        {
            try
            {
                List<CajaDeAhorro> cajasDelUsuario = DB.buscaCajasAhorroDeUsuario(id_usuario);
                //Elimino relacion usuario con cajas de ahorro.Plazo Fijo y Tarjeta
                DB.eliminaRelacionUsuarioCajaAhorro(id_usuario);
                DB.eliminaRegistrosTarjetasDeCreditoDelUsuario(id_usuario);
                DB.eliminaRegistrosPlazosFijoDelUsuario(id_usuario);
                DB.eliminaRegistrosPagosDelUsuarioAEliminar(id_usuario);

                //List<CajaDeAhorro> cajasDelUsuario = DB.buscaCajasAhorroDeUsuario(id_usuario);
                List<CajaDeAhorro>? cajasConUnSoloTitular = new List<CajaDeAhorro>();
                List<CajaDeAhorro>? cajasAEliminar = new List<CajaDeAhorro>();

                //Cargo cajas con 1 solo titular para eliminarlas
                foreach (CajaDeAhorro caja in cajasDelUsuario)
                //cajasDelUsuario.ForEach(delegate (CajaDeAhorro caja)
                {
                    if (caja._titulares.Count() == 1)
                    {
                        cajasConUnSoloTitular.Add(caja);
                    }
                //});
                };


                //Elimino las anteriores de las listas de cajas y usuario.cajas
                foreach (CajaDeAhorro cajaUnica in cajasDelUsuario)
                //cajasDelUsuario.ForEach(delegate (CajaDeAhorro cajaUnica)
                {
                    foreach (CajaDeAhorro caja in cajas)
                    //cajas.ForEach(delegate (CajaDeAhorro caja)
                    {
                        if (caja._id == cajaUnica._id)
                        {
                            cajasAEliminar.Add(caja);

                        }
                        //});
                    }

                //});
                };

                //Elimino las cajas
           

                foreach (CajaDeAhorro caja in cajasAEliminar)
                {
                    cajas.Remove(caja);

                    DB.eliminamMovimientosPorIdDeCaja(caja._id);
                    DB.bajaCajaDeAhorro(caja._id);
                }


                //Elimino El usuario
                List<Usuario> listAuxUser = usuarios.ToList();
                foreach (Usuario usuario in listAuxUser)
                {
                    if (usuario._id == id_usuario)
                    {
                        usuarios.Remove(usuario);
                        DB.bajaUsuario(id_usuario);

                    }

                }

                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }



        public bool ModificarCajaDeAhorro(string cbu, int Titular, int accion)
        {
            CajaDeAhorro? cajaBuscada = cajas.Find(caja => caja._cbu.Equals(cbu));
            if (cajaBuscada != null)
            {
                if (accion == 0)
                {


                    if (cajaBuscada._titulares.Count() > 1)
                    {
                        foreach (int titular in cajaBuscada._titulares)
                        {
                            if (titular == Titular)
                            {

                                if (DB.bajaTitularCajaDeAhorro(titular))
                                {
                                    cajaBuscada._titulares.Remove(titular);
                                    usuarioLogueado._Cajas.Remove(cajaBuscada);
                                    return true;
                                }


                            }
                        }
                    }
                }
                else if (accion == 1)
                {
                    int idInternoTitular = 0;

                    idInternoTitular = cajaBuscada._titulares.Find(id => id == Titular);



                    //Si la caja no tiene el titular, lo inserta:
                    if (idInternoTitular == 0 && DB.altaTitularCajaDeAhorro(Titular, cajaBuscada._id))
                    {
                        cajaBuscada._titulares.Add(Titular);
                        usuarioLogueado._Cajas.Add(cajaBuscada);
                        return true;

                    }

                }
            }
            return false;
        }

        public bool BajaCajaDeAhorro(int id)
        {
            foreach (CajaDeAhorro caja in cajas)
            {
                if (caja._id == id && caja._saldo == 0)
                {

                    if (DB.bajaCajaDeAhorro(id))
                    {
                        cajas.Remove(caja);
                        return true;
                    }

                }
            }
            return false;

        }


        public bool AltaMovimiento(CajaDeAhorro caja, string detalle, double monto)
        {
            try
            {
                int idAux = DB.agregarMovimiento(caja._id, detalle, monto, DateTime.Now);

                Movimiento movimiento = new Movimiento(idAux, caja._id, detalle, monto, DateTime.Now);
                movimientos.Add(movimiento);
                caja._movimientos.Add(movimiento);




                return true;
            }
            catch (Exception ex) { return false; }
        }





        public void ModificarMovimiento()
        {

        }

        public void BajaMovimiento()
        {

        }

        public bool AltaPago(float monto, string metodo, string detalle, long id_metodo)
        {
            int idAux = 0;
            try
            {
                Pago pago;
                if (metodo.Equals("CA"))
                {
                    idAux = DB.agregarPago(usuarioLogueado._id, monto, metodo, detalle, DB.buscarCajaDeAhorroByCbu(id_metodo));
                    pago = new Pago(idAux, usuarioLogueado, monto, metodo, detalle, DB.buscarCajaDeAhorroByCbu(id_metodo));
                }
                else
                {
                    idAux = DB.agregarPago(usuarioLogueado._id, monto, metodo, detalle, DB.buscarTarjetaDeCreditoByNro(id_metodo));
                    pago = new Pago(idAux, usuarioLogueado, monto, metodo, detalle, DB.buscarTarjetaDeCreditoByNro(id_metodo));
                }
                this.pagos.Add(pago);
                this.usuarioLogueado.pagos.Add(pago);
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool ModificarPago(int id)
        {
            try
            {
                foreach (Pago pago in pagos)
                {
                    if (pago._id == id)
                    {

                        if (pago._metodo.Equals("CA"))
                        {

                            foreach (CajaDeAhorro caja in cajas)
                            {
                                if (pago._id_metodo == caja._id)
                                {
                                    if (pago._pagado == false)
                                    {
                                        if (this.Pagar(caja, pago._monto))
                                        {
                                            pago._pagado = true;
                                            DB.cambioPago(id);
                                            return true;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Debe seleccionar un ID de pago pendiente");
                                        return false;
                                    }
                                }

                            }
                        }
                        else
                        {
                            foreach (TarjetaDeCredito tc in tarjetas)
                            {
                                if (pago._id_metodo == tc._id)
                                {

                                    if (pago._pagado == false)
                                    {
                                        if (DB.actualizaConsumoTarjeta(tc._numero, pago._monto))
                                        {
                                            tc._consumos = tc._consumos + pago._monto;


                                            pago._pagado = true;
                                            return DB.cambioPago(id); ;
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("Debe seleccionar un ID de pago pendiente");
                                        return false;
                                    }
                                }

                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception ex) { return false; }

        }

        public bool EliminarPago(int id)
        {
            try
            {
                foreach (Pago pago in pagos)
                {
                    if (pago._id == id)
                    {
                        pagos.Remove(pago);
                        usuarioLogueado.pagos.Remove(pago);
                        DB.bajaPago(id);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex) { return false; }

        }


        public bool BajaPago(int id, int id_user)
        {
            try
            {
                foreach (Pago pago in pagos)
                {
                    if (pago._id == id)
                    {
                        pagos.Remove(pago);
                    }
                }

                foreach (Usuario usuario in usuarios)
                {
                    if (usuario._id == id_user)
                    {
                        foreach (Pago pago in usuario.pagos)
                        {
                            pagos.Remove(pago);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public bool AltaPlazoFijo(double monto, String cbu)
        {
            CajaDeAhorro? caja = cajas.Where(caja => caja._cbu == cbu).FirstOrDefault();
            int id;
            DateTime fechaFin = DateTime.Now;
            try
            {
                if (caja._saldo > monto)
                {
                    id = DB.agregarPlazoFijo(usuarioLogueado._id, monto, fechaFin.AddMonths(1));
                    if (id >= 0)
                    {
                        usuarioLogueado.pfs.Add(new PlazoFijo(usuarioLogueado, monto, fechaFin, 7));
                        pfs.Add(new PlazoFijo(id, usuarioLogueado._id, monto, fechaFin.AddMonths(1), 7, 0));
                        return true;
                    }


                }


                return false;


            }
            catch (Exception ex) { return false; }
        }

        public void ModificarPlazoFijo()
        {

        }

        public bool BajaPlazoFijo(int id)
        {
           
            PlazoFijo? plazo = pfs.Where(plazo => plazo._id == id).FirstOrDefault();




            try
            {


                if (plazo._pagado == 1)
                {
                    DB.bajaPlazoFijo(id);
                }
                else
                {

                    return false;
                }



                return true;
            }
            catch (Exception ex) { return false; }



        }

        public bool AltaTarjetaDeCredito()

        {
            try
            {
                string idNuevaTarjeta = this.obtieneSecuencia(usuarioLogueado);
                int idSeqTarjeta = DB.agregarTarjetaDeCredito(usuarioLogueado._id, idNuevaTarjeta, 1, 500000);
                if (idSeqTarjeta > 0)
                {
                    tarjetas.Add(new TarjetaDeCredito(idSeqTarjeta, usuarioLogueado._id, idNuevaTarjeta, 1, 500000, 0));

                    return true;
                }
            }
            catch (Exception ex) { return false; }

            return false;
        }

        public bool ModificarTarjetaDeCredito(string numeroTarjeta, float limite)
        {
            try
            {
                foreach (TarjetaDeCredito tarjeta in tarjetas)
                {
                    if (tarjeta._numero == numeroTarjeta)
                    {

                        tarjeta._limite = limite;
                        DB.cambioLimiteTarjeta(numeroTarjeta, limite);
                    }
                }

                return true;
            }
            catch (Exception ex) { return false; }


        }

        public bool BajaTarjetaDeCredito(string numeroTarjeta)
        {

            try
            {
                foreach (TarjetaDeCredito tarjeta in tarjetas)
                {
                    if (tarjeta._numero == numeroTarjeta)
                    {
                        if (tarjeta._consumos == 0)
                        {
                            tarjetas.Remove(tarjeta);
                            DB.bajaTarjetaDeCredito(numeroTarjeta);
                            return true;
                        }
                    }
                }

                return false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }


        }


        public List<CajaDeAhorro> MostrarCajasDeAhorro()
        {
            if (usuarioLogueado._esUsuarioAdmin == true)
            {
                return cajas;
            }
            else
            {
                return DB.buscaCajasAhorroDeUsuario(usuarioLogueado._id).ToList();
            }

        }




        public List<Movimiento> MostrarMovimientos(int id_caja)
        {
            List<Movimiento> movimientosCaja = new List<Movimiento>();
            foreach (Movimiento movimiento in movimientos)
            {
                if (movimiento._id_CajaDeAhorro == id_caja)
                {
                    movimientosCaja.Add(movimiento);
                }
            }
            return movimientosCaja;



        }

        public List<Pago> MostrarPago(bool pagado)
        {
            List<Pago> pagosAux = new List<Pago>();
            usuarioLogueado.pagos = DB.inicializarPago();
            foreach (Pago pago in usuarioLogueado.pagos)
            {
                if (pago._pagado == pagado && pago._id_usuario == usuarioLogueado._id)
                {
                    pagosAux.Add(pago);

                }

            }

            return pagosAux.ToList();
        }



        public List<PlazoFijo> MostrarPf()
        {


            return pfs.ToList();
        }

        public List<PlazoFijo> MostrarPfUsuario()
        {

            if (usuarioLogueado._esUsuarioAdmin == true)
            {
                return pfs.ToList();
            }
            else
            {
                return DB.buscaPlazoFijo(usuarioLogueado._id);
            }

        }

        public bool esAdmin()
        {
            if (usuarioLogueado._esUsuarioAdmin)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<TarjetaDeCredito> MostrarTarjetasDeCredito()
        {
            if (usuarioLogueado._esUsuarioAdmin == true)
            {
                return tarjetas.ToList();
            }
            else
            {
                return DB.buscaTarjetasDeCreditoUsuario(usuarioLogueado._id);
            }


        }

        public static bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }


        public List<Usuario> MostrarUsuarios()
        {
            if (usuarioLogueado._esUsuarioAdmin == true)
            {
                return usuarios.ToList();
            }
            else
            {
                List<Usuario> listadoUsuariosMostrar = new List<Usuario>();
                return listadoUsuariosMostrar;
            }


        }



    }

}
