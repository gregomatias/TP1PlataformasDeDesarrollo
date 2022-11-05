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
        //  private  int cbuAutonumerado = 0;
        private int idPagoAutonumerado = 0;
        /*TP2*/
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
                /*
                 DB.agregarMovimiento(CajaDeAhorro.id,"Pago de servicio",monto,DateTime.Now());
                 */
                caja._saldo = caja._saldo - monto;
                Movimiento muvi = new Movimiento(caja, "Pago de servicio", monto); ;
                movimientos.Add(muvi);
                caja._movimientos.Add(muvi);
                return true;

            }
            else { return false; }



        }

        public bool Pagar(int cbu, float monto)
        {

            try
            {
                foreach (CajaDeAhorro caja in usuarioLogueado.cajas)
                {
                    if (caja._cbu.Equals(cbu))
                    {
                        return Pagar(caja, monto);

                    }
                }
            }
            catch (Exception) { return false; }
            return false;
        }
        //////////////
        public bool PagarTester(int id_cajaDeAhorro, double monto, int id_pago)
        {
            try
            {
                DB.agregarMovimiento(id_cajaDeAhorro, "Pago de servicio", monto, DateTime.Now);
                DB.cambioPago(id_pago);
                return true;
            }
            catch (Exception) { return false; }

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
                    Movimiento movimiento = new Movimiento(caja, "Deposito", monto);
                    movimientos.Add(movimiento);
                    caja._movimientos.Add(movimiento);
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
                    double saldoActualizado = caja._saldo - monto;

                    DB.actualizaSaldoCajaAhorro(cbu, saldoActualizado);
                    caja._saldo = saldoActualizado;
                    Movimiento movimiento = new Movimiento(caja, "Retiro", monto);
                    movimientos.Add(movimiento);
                    caja._movimientos.Add(movimiento);
                    return true;
                }
            }
            return false;
        }



        public bool Transferir(string cbuEmisor, string cbuDestino, float monto)
        {


            CajaDeAhorro? cajaEmisor = cajas.Where(caja => caja._cbu == cbuEmisor && caja._saldo >= monto).FirstOrDefault();
            CajaDeAhorro? cajaDestino = cajas.Where(caja => caja._cbu == cbuDestino && caja._saldo >= monto).FirstOrDefault();


            if (cajaEmisor != null && cajaDestino != null)
            {

                try
                {
                    double saldoEmisor = cajaEmisor._saldo - monto;
                    double saldoDestino = cajaDestino._saldo + monto;
                    cajaEmisor._saldo = saldoEmisor;
                    cajaDestino._saldo = saldoDestino;

                    Movimiento movimientoEmisor = new Movimiento(cajaEmisor, "Transferencia emitida", monto);
                    Movimiento movimientoDestino = new Movimiento(cajaDestino, "Transferencia emitida", monto);
                    movimientos.Add(movimientoEmisor);
                    movimientos.Add(movimientoDestino);
                    DB.actualizaSaldoCajaAhorro(cbuEmisor, saldoEmisor);
                    DB.actualizaSaldoCajaAhorro(cbuDestino, saldoDestino);
                    return true;

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            }



            return false;


        }


        public List<Movimiento> BuscarMovimiento(CajaDeAhorro caja, string detalle = "default", DateTime? fecha = null, float monto = 0)
        {
            List<Movimiento> move = new List<Movimiento>();

            foreach (Movimiento movimiento in movimientos)
            {
                if (movimiento._cajaDeAhorro == caja)
                {
                    if (movimiento._detalle == detalle || movimiento._fecha.Date == fecha.Value.Date || movimiento._monto == monto)
                    {
                        move.Add(movimiento);
                    }
                }
            }



            /*
            if (detalle != "default")
            {
                foreach (Movimiento movimiento in movimientos)
                {
                    if (movimiento._cajaDeAhorro == caja && movimiento._detalle == detalle)
                    {
                        move.Add(movimiento);

                    }
                }
                return move;

            }
            else if (fecha != null)
            {
                foreach (Movimiento movimiento in movimientos)
                {
                    if (movimiento._cajaDeAhorro == caja && movimiento._fecha == fecha)
                    {
                        move.Add(movimiento);

                    }
                    return move;

                }
            }
            else if (monto != 0)
            {

                foreach (Movimiento movimiento in movimientos)
                {
                    if (movimiento._cajaDeAhorro == caja && movimiento._monto == monto)
                    {
                        move.Add(movimiento);

                    }
                    return move;

                }
            }
            */
            return move;

        }


        public List<Movimiento> BuscarMovimiento(int cbu, string detalle = "default", DateTime? fecha = null, float monto = 0)
        {
            List<Movimiento> listaMovimientos = new List<Movimiento>();
            foreach (CajaDeAhorro caja in usuarioLogueado.cajas)
            {
                if (caja._cbu.Equals(cbu))
                {

                    listaMovimientos = BuscarMovimiento(caja, detalle, fecha, monto);


                }
            }
            return listaMovimientos.ToList();
        }


        public bool PagarTarjeta(TarjetaDeCredito tarjeta, CajaDeAhorro caja)
        {
            double monto = tarjeta._consumos;

            if (caja._saldo > tarjeta._consumos)
            {
                caja._saldo = caja._saldo - tarjeta._consumos;

                AltaMovimiento(caja, "Pago Tarjeta", monto);
                return true;
            }
            else
            {
                return false;
            }


        }



        public bool AltaUsuario(int dni, string nombre, string apellido, string mail, string password)
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
                idNuevoUsuario = DB.agregarUsuario(dni, nombre, apellido, mail, password, false, false, 0);

                if (idNuevoUsuario != -1)
                {
                    //Agrega a la lista con el ID obtenido de la DB
                    Usuario nuevoUsuario = new Usuario(idNuevoUsuario, dni, nombre, apellido, mail, password, false, false, 0);
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

        public bool EliminarUsuario(int id)
        {
            try
            {
                foreach (Usuario usuario in usuarios)
                {
                    if (usuario._id == id)
                    {
                        usuarios.Remove(usuario);
                    }

                }
                return true;
            }
            catch (Exception ex) { return false; }
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
                                    return true;
                                }


                            }
                        }
                    }
                }
                else if (accion == 1)
                {
                    if (DB.altaTitularCajaDeAhorro(Titular, cajaBuscada._cbu))
                    {
                        cajaBuscada._titulares.Add(Titular);
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
                Movimiento movi = new Movimiento(caja, detalle, monto);
                caja._movimientos.Add(movi);
                movimientos.Add(movi);
                /*DB.agregarMovimiento(caja._id,detalle,monto,DateTime.Now)*/
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

        public bool AltaPago(float monto, string metodo, string detalle, int id_metodo)
        {
            try
            {
                Pago pago = new Pago(idPagoAutonumerado, usuarioLogueado, monto, metodo, detalle, id_metodo);
                idPagoAutonumerado = idPagoAutonumerado + 1;
                this.pagos.Add(pago);
                this.usuarioLogueado.pagos.Add(pago);
                //DB.agregarPago(usuarioLogeado._id,monto,metodo,detalle,id_metodo);
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
                        foreach (CajaDeAhorro caja in cajas)
                        {
                            if (pago._id_metodo == caja._id)
                            {
                                if (pago._pagado == false)
                                {
                                    if (this.Pagar(caja, pago._monto))
                                    {
                                        pago._pagado = true;
                                        //DB.cambioPago(id);
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
                        //DB.bajaPago(id);
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

        public bool AltaPlazoFijo(Usuario titular, double monto, DateTime fechaFin, double tasa)
        {
            try
            {
                pfs.Add(new PlazoFijo(titular, monto, fechaFin, tasa));
                //DB.agregarPlazoFijo(UsuarioLogeado._id,monto,fechaFin,tasa);
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public void ModificarPlazoFijo()
        {

        }

        public bool BajaPlazoFijo(int id, int id_user)
        {
            try
            {
                foreach (PlazoFijo pf in pfs)
                {
                    if (pf._id == id)
                    {
                        if (pf._pagado = true && (System.DateTime.Now - pf._fechaFin).TotalDays > 30)
                        {
                            pfs.Remove(pf);
                            //DB.bajaPlazoFijo(id);
                        }
                    }
                }

                foreach (Usuario usuario in usuarios)
                {
                    if (usuario._id == id_user)
                    {
                        foreach (PlazoFijo pf in usuario.pfs)
                        {
                            if (pf._id == id)
                            {
                                if (pf._pagado = true && (System.DateTime.Now - pf._fechaFin).TotalDays > 30)
                                {
                                    pfs.Remove(pf);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }



        }

        public bool AltaTarjetaDeCredito(Usuario titular, int numero, int codigoV, float limite)
        {
            try
            {
                tarjetas.Add(new TarjetaDeCredito(titular, numero, codigoV, limite));
                //DB.agragarTarjetaDeCredito((usuarioLogueado._id,numero,codigov, limite);
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public bool ModificarTarjetaDeCredito(int id, float limite)
        {
            try
            {
                foreach (TarjetaDeCredito tarjeta in tarjetas)
                {
                    if (tarjeta._id == id)
                    {

                        tarjeta._limite = limite;
                        //DB.cambioLimiteTarjeta(id,limite);
                    }
                }

                return true;
            }
            catch (Exception ex) { return false; }


        }

        public bool BajaTarjetaDeCredito(int id, int id_user)
        {

            try
            {
                foreach (TarjetaDeCredito tarjeta in tarjetas)
                {
                    if (tarjeta._id == id)
                    {
                        if (tarjeta._consumos == 0)
                        {
                            tarjetas.Remove(tarjeta);
                            //DB.bajaTarjetaDeCredito(tarjeta._id);
                        }
                    }
                }

                foreach (Usuario usuario in usuarios)
                {
                    if (usuario._id == id_user)
                    {
                        foreach (TarjetaDeCredito tarjeta in tarjetas)
                        {
                            if (tarjeta._id == id)
                            {
                                if (tarjeta._consumos == 0)
                                {
                                    tarjetas.Remove(tarjeta);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }


        }


        public List<CajaDeAhorro> MostrarCajasDeAhorro()
        {

            return DB.buscaCajasAhorroDeUsuario(usuarioLogueado._id).ToList();
        }




        public List<Movimiento> MostrarMovimientos(int id_caja)
        {
            List<Movimiento> movimientosCaja = new List<Movimiento>();
            foreach (Movimiento movimiento in movimientos)
            {
                if (movimiento._cajaDeAhorro._id == id_caja)
                {
                    movimientosCaja.Add(movimiento);
                }
            }
            return movimientosCaja;

            /*
             movimientosCaja = DB.buscarMovimientos(id_caja);
             return movimientosCaja.ToList();
             */

        }

        public List<Pago> MostrarPago(bool pagado)
        {
            List<Pago> pagosAux = new List<Pago>();
            foreach (Pago pago in usuarioLogueado.pagos)
            {
                if (pago._pagado == pagado)
                {
                    pagosAux.Add(pago);

                }

            }
            //pagosAux = DB.buscarPago(usuarioLogeado._id);
            return pagosAux.ToList();
        }



        public List<PlazoFijo> MostrarPf()
        {

            /*
            
            if(usuarioLogeado.esUsuarioAdmin==true){
                return pfs.ToList();
            }else{
                List<PlazoFijo> plazoAux = new List <PlazoFijo>();
                plazoAux= DB.buscarPlazoFijo(usuarioLogueado._id);
            }
             */
            return pfs.ToList();
        }



        public List<TarjetaDeCredito> MostrarTarjetasDeCredito()
        {

            return tarjetas.ToList();
            /*
            
            if(usuarioLogeado.esUsuarioAdmin==true){
                return tarjetas.ToList();
            }else{
                List<TarjetaDeCredito> tarjetaAux = new List <PlazoFijo>();
                tarjetaAux= DB.buscarTarjetasDeCredito(usuarioLogueado._id);
            }
             */
        }

        public static bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }





    }

}
