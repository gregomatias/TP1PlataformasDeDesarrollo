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
        private static int cbuAutonumerado = 0;


        public Banco()
        {
            this.cajas = new List<CajaDeAhorro>();
            this.usuarios = new List<Usuario>();
            this.pfs = new List<PlazoFijo>();
            this.tarjetas = new List<TarjetaDeCredito>();
            this.pagos = new List<Pago>();
            this.movimientos = new List<Movimiento>();

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
                    if (usuarioLogueado._intentosFallidos >= 3)
                    {
                        usuarioLogueado._bloqueado = true;
                        return false;
                    }
                    if (usuarioLogueado._password == contrasena)
                    {
                        return true;
                    }
                    else
                    {
                        usuarioLogueado._intentosFallidos++;
                        return false;
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
            CajaDeAhorro caja = new CajaDeAhorro(cbuAutonumerado, usuarioLogueado);
            cbuAutonumerado = cbuAutonumerado + 1;
            AltaCajaDeAhorro(usuarioLogueado, caja);
            cajas.Add(caja);
            return true;
        }

        public void AltaCajaDeAhorro(Usuario usuario, CajaDeAhorro caja)
        {
            usuario._Cajas.Add(caja);

        }

        public bool Depositar(CajaDeAhorro caja, float monto)
        {
            caja._saldo = caja._saldo + monto;
            movimientos.Add(new Movimiento(caja,"Deposito",monto));
            caja._movimientos.Add(new Movimiento(caja,"Deposito",monto));
            return true;
        }

        public bool Depositar(int cbu, float monto)
        {
            try
            {
                foreach (CajaDeAhorro caja in usuarioLogueado.cajas)
                {
                    if (caja._cbu == cbu)
                    {
                        Depositar(caja, monto);

                    }
                }
            }
            catch (Exception) { return false; }
            return true;

        }



        public bool Retirar(CajaDeAhorro caja, float monto)
        {
            if (caja._saldo >= monto) { 
            caja._saldo = caja._saldo - monto;
            movimientos.Add(new Movimiento(caja,"Retiro",monto));
            caja._movimientos.Add(new Movimiento(caja,"Retiro",monto));
            return true;

            }
            else { return false; }
        }

        public bool Retirar(int cbu, float monto)
        {

            try
            {
                foreach (CajaDeAhorro caja in usuarioLogueado.cajas)
                {
                    if (caja._cbu == cbu)
                    {
                        Retirar(caja, monto);

                    }
                }
            }
            catch (Exception) { return false; }
            return true;
        }

        public bool Transferir(int emisor, int destino, float monto)
        {
            bool encontro = false;
            foreach(CajaDeAhorro caja in cajas){
                if(caja._cbu==emisor && caja._saldo>=monto){
                    caja._saldo=caja._saldo-monto;
                    encontro = true;
                    movimientos.Add(new Movimiento(caja,"Transferencia emitida",monto));
                    caja._movimientos.Add(new Movimiento(caja,"Transferencia emitida",monto));
                }
            }

            if (encontro) { 
                foreach (CajaDeAhorro cajita in cajas)
                {
                    if (cajita._cbu == destino)
                    {
                        cajita._saldo = cajita._saldo + monto;
                        movimientos.Add(new Movimiento(cajita,"Transferencia recibida",monto));
                        cajita._movimientos.Add(new Movimiento(cajita,"Transferencia recibida",monto));
                        return true;
                    }
                }
            }

            return false;
        }


        public List<Movimiento> BuscarMovimiento(CajaDeAhorro caja, string detalle, DateTime fecha, float monto)
        {
            List<Movimiento> move = new List<Movimiento>();

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
            return move;

        }


        public bool PagarTarjeta(TarjetaDeCredito tarjeta, CajaDeAhorro caja)
        {
            float monto = tarjeta._consumos;

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

            try
            {
                usuarios.Add(new Usuario(dni, nombre, apellido, mail, password));
                return true;
            }
            catch (Exception ex) { return false; }


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



        public void ModificarCajaDeAhorro(int id)
        {

            foreach (CajaDeAhorro caja in cajas)
            {
                if (caja._id == id)
                {
                    foreach (Usuario user in caja._titulares)
                    {
                        if (usuarioLogueado == user)
                        {
                            caja._titulares.Remove(user);

                        }


                    }
                    caja._titulares.Add(usuarioLogueado);
                }
            }
        }

        public bool BajaCajaDeAhorro(int id)
        {
            foreach (CajaDeAhorro caja in cajas)
            {
                if (caja._id == id && caja._saldo == 0)
                {
                    cajas.Remove(caja);
                    return true;
                }
            }
            return false;

        }

        public bool AltaMovimiento(CajaDeAhorro cajaDeAhorro, string detalle, float monto)
        {
            try
            {
                movimientos.Add(new Movimiento(cajaDeAhorro, detalle, monto));
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

        public bool AltaPago(Usuario usuario, float monto, bool pagado, string metodo)
        {
            try
            {
                pagos.Add(new Pago(usuario, monto, pagado, metodo));
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public bool ModificarPago(int id, int id_user)

        {
            try
            {
                foreach (Pago pago in pagos)
                {
                    if (pago._id == id)
                    {
                        pago._pagado = true;
                    }
                }

                foreach (Usuario usuario in usuarios)
                {
                    if (usuario._id == id_user)
                    {
                        foreach (Pago pago in usuario.pagos)
                        {
                            if (pago._id == id)
                            {
                                pago._pagado = true;
                            }
                        }
                    }
                }
                return true;
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

        public bool AltaPlazoFijo(Usuario titular, float monto, DateTime fechaFin, float tasa)
        {
            try
            {
                pfs.Add(new PlazoFijo(titular, monto, fechaFin, tasa));
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


            return usuarioLogueado.cajas.ToList();
        }


        public List<Movimiento> MostrarMovimientos(int id_caja)
        {
            List<Movimiento> movimientosCaja = new List<Movimiento>();
            foreach (Movimiento movimiento in movimientos)
            {
                if (movimiento._cajaDeAhorro._cbu == id_caja)
                {
                    movimientosCaja.Add(movimiento);
                }
            }
            return movimientosCaja;

        }

        public List<Pago> MostrarPago()
        {
            return pagos.ToList();
        }



        public List<PlazoFijo> MostrarPf()
        {
            return pfs.ToList();
        }



        public List<TarjetaDeCredito> MostrarTarjetasDeCredito()
        {

            return tarjetas.ToList();
        }





    }

}
