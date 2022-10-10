using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
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
        private Usuario usuarioLogueado;
        


        public Banco()
        {
            this.cajas = new List<CajaDeAhorro>();
            this.usuarios = new List<Usuario>();
            this.pfs = new List<PlazoFijo>();
            this.tarjetas = new List<TarjetaDeCredito>();
            this.pagos = new List<Pago>();
            this.movimientos = new List<Movimiento>();
            
        }

        public string GetUsuarioLogueado() {
            try {
                return usuarioLogueado._nombre;
            }
            catch (Exception) { return "Indeterminado"; }
        }
        
        public bool IniciarSesion(int dni , string contrasena)
        {
            foreach (Usuario usuario in usuarios)
            {
                if(usuario._dni==dni && usuario._password==contrasena && usuario._intentosFallidos < 3)
                {
                    usuarioLogueado = usuario;
                    return true;
                }else if (usuarioLogueado._intentosFallidos>=3)
                {
                    usuarioLogueado._bloqueado = true;
                    return false;
                }
            }
            usuarioLogueado._intentosFallidos=usuarioLogueado._intentosFallidos+1;
            return false;
        }

        public bool CerrarSesion()
        {
            usuarioLogueado=null;
            return true;
        }

        public bool CrearCajaDeAhorro()
        {
            return false;
        }

        public bool Depositar(CajaDeAhorro caja,float monto)
        {
            caja._saldo=caja._saldo+monto;
            return true;
        }

        public bool Retirar(CajaDeAhorro caja, float monto)
        {
            caja._saldo=caja._saldo-monto;
            return true;
        }

        public bool Transferir(CajaDeAhorro emisor,CajaDeAhorro destino, float monto)
        {
            emisor._saldo=emisor._saldo-monto;
            destino._saldo=destino._saldo+monto;
            return true;
        }

        public List<Movimiento> BuscarMovimiento(CajaDeAhorro caja, string detalle, DateTime fecha,float monto)
        {
           List<Movimiento> move= new List<Movimiento>();
            
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
                    else if(fecha != null)
                    {
                         foreach (Movimiento movimiento in movimientos)
                        {
                             if (movimiento._cajaDeAhorro == caja && movimiento._fecha == fecha)
                            {
                                move.Add(movimiento);    
                                
                            }
                            return move;
                            
                        }
                    }else if(monto != 0) {
                        
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
              

        public bool PagarTarjeta(TarjetaDeCredito tarjeta, CajaDeAhorro caja){
            float monto = tarjeta._consumos; 

               if(caja._saldo>tarjeta._consumos){
                    caja._saldo = caja._saldo-tarjeta._consumos;
               
        AltaMovimiento(caja,"Pago Tarjeta",monto);
        return true;
    }
    else
    {
        return false;
    }


    }



    public bool AltaUsuario(int dni, string nombre, string apellido, string mail, string password)
        {
            
            try { 
            usuarios.Add(new Usuario(dni, nombre, apellido, mail, password));
                return true;
            }
            catch (Exception ex) { return false; }


     }

      public bool ModificarUsuario(int id,int dni, string nombre, string apellido,string password,string mail)
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
            catch(Exception ex) { return false; }
        } 

        public bool EliminarUsuario(int id)
        {
            try { 
            foreach (Usuario usuario in usuarios)
            {
                if (usuario._id==id) {
                    usuarios.Remove(usuario);
                }
                    
                }
                return true;
            }
            catch(Exception ex) { return false; }
        }
    
        public void AltaCajaDeAhorro()
        {

        }

        public void ModificarCajaDeAhorro()
        {
            

        }

        public void BajaCajaDeAhorro(int id)
        {
            

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
            catch(Exception ex) { return false; }
        }

        public bool ModificarPago(int id,int id_user)

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
            catch(Exception ex) { return false; }
            
        }

        public bool BajaPago(int id,int id_user)
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
            catch (Exception ex){ return false; }
        }

        public bool AltaPlazoFijo(Usuario titular, float monto, DateTime fechaFin, float tasa)
        {
            try
            {
                pfs.Add(new PlazoFijo(titular, monto, fechaFin, tasa));
                return true;
            }
            catch(Exception ex) { return false; }
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
            catch(Exception ex) { return false; }
           


        }

        public bool AltaTarjetaDeCredito(Usuario titular, int numero, int codigoV, float limite)
        {
            try
            {
                tarjetas.Add(new TarjetaDeCredito(titular, numero, codigoV, limite));
                return true;
            }
            catch(Exception ex) { return false; }
        }

        public bool ModificarTarjetaDeCredito(int id,  float limite)
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
            }catch(Exception ex) { return false; }
            

        }

        public bool BajaTarjetaDeCredito(int id,int id_user)
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
            catch(Exception ex) { return false; }
            

        }


        public List<CajaDeAhorro> MostrarCajasDeAhorro()
        {
       
            return cajas.ToList();
        }


        public List<Movimiento> MostrarMovimientos()
        {
            
            return movimientos.ToList();
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
