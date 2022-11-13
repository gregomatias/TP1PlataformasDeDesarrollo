using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace TP1
{
    internal partial class Form3 : Form
    {
        private object[] argumentos;
        private List<List<string>> datos;
        private Banco banco;
        private TransfDelegadoForm2 transEvento;
        private int celda;
        //alan
        public Form3(Banco banco, TransfDelegadoForm2 transEvento)
        {

            this.transEvento = transEvento;

            this.banco = banco;
            InitializeComponent();
            label2.Text = banco.GetNombreUsuarioLogueado();

            this.cargaCajasAhorro();
            this.cargaTarjetasDeCredito();
            this.cargaPlazoFijo();
            this.cargaUsuarios();
            //Valida Amin
            if (!banco.esAdmin()) { 
                this.tabControl.TabPages.Remove(tabUsuarios);
                this.tabControl.TabPages.Remove(tabTrasladoCajas);
           
            }

        }
        public Form3(object[] args)
        {
            InitializeComponent();
            argumentos = args;
            label2.Text = (string)args[0];

            datos = new List<List<string>>();
        }


        public delegate void TransfDelegadoForm2();


        private void button1_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        private void refreshData()
        {
            //borro los datos
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();


        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void btn_crearCajaAhorro_Click(object sender, EventArgs e)
        {


            if (banco.CrearCajaDeAhorro())
            {


                cargaCajasAhorro();

            }
            else { MessageBox.Show("No se pudo crear la caja de ahorro"); }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            banco.CerrarSesion();
            this.transEvento();

        }



        private void comboBox1_Click(object sender, EventArgs e)
        {
            //comboBox1.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Refresh();
            List<CajaDeAhorro> listaCajaAhorro = new List<CajaDeAhorro>();
            listaCajaAhorro = banco.MostrarCajasDeAhorro();

            foreach (CajaDeAhorro caja in listaCajaAhorro)
            {
                comboBox1.Items.Add(caja._cbu);
            }


        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            txtb_monto.Enabled = true;
            btn_extraer.Enabled = true;
            btn_depositar.Enabled = true;

            int selectedIndex = comboBox1.SelectedIndex;
            List<Movimiento> listaMovimientos = new List<Movimiento>();
            listaMovimientos = banco.MostrarMovimientos(selectedIndex);



        }

        private void btn_depositar_Click(object sender, EventArgs e)
        {
            if (txtb_monto.Text != "")
            {
                float monto = float.Parse(txtb_monto.Text);
                if (monto > 0)
                {



                    if (banco.Depositar(comboBox1.SelectedItem.ToString(), monto))
                    {
                        MessageBox.Show("Deposito efectuado");
                        txtb_monto.Text = "";
                        // refreshData();
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }

                }
                else { MessageBox.Show("Monto debe ser mayor a cero"); }


            }
            else { MessageBox.Show("El monto no puede estar vacio"); }

        }

        private void txtb_monto_TextChanged(object sender, EventArgs e)
        {
            //"[^0-9]"
            if (System.Text.RegularExpressions.Regex.IsMatch(txtb_monto.Text, "^[0-9]+\\.$"))


            {
                txtb_monto.Text = txtb_monto.Text.Remove(txtb_monto.Text.Length - 1);
            }

        }

        private void btn_extraer_Click(object sender, EventArgs e)
        {


            if (txtb_monto.Text != "")
            {
                float monto = float.Parse(txtb_monto.Text);
                if (monto > 0)
                {


                    if (banco.Retirar(comboBox1.SelectedItem.ToString(), monto))
                    {
                        MessageBox.Show("Retiro efectuado");
                        txtb_monto.Text = "";
                        // refreshData();
                    }
                    else
                    {
                        MessageBox.Show("El retiro no pudo efectuarse, verifique su saldo");
                    }

                }
                else { MessageBox.Show("Monto debe ser mayor a cero"); }


            }
            else { MessageBox.Show("El monto no puede estar vacio"); }

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_transferir_Click(object sender, EventArgs e)
        {



            if (txtb_monto_transferencia.Text != "" && txtb_cbu_destino.Text != "")
            {
                float monto = float.Parse(txtb_monto_transferencia.Text);
                string cbu_destino = txtb_cbu_destino.Text;
                if (monto > 0)
                {


                    if (banco.Transferir(comboBox2.SelectedItem.ToString(), cbu_destino, monto))
                    {
                        MessageBox.Show("transferencia efectiva");
                        txtb_monto_transferencia.Text = "";
                        // refreshData();
                    }
                    else
                    {
                        MessageBox.Show("La transferencia no pudo efectuarse, verifique su saldo");
                    }

                }
                else { MessageBox.Show("Monto debe ser mayor a cero"); }


            }
            else { MessageBox.Show("El monto no puede estar vacio"); }


        }

        private void comboBox3_movimientos_Click(object sender, EventArgs e)
        {

            comboBox3_movimientos.Items.Clear();
            comboBox3_movimientos.Refresh();
            List<CajaDeAhorro> listaCajaAhorro = new List<CajaDeAhorro>();
            listaCajaAhorro = banco.MostrarCajasDeAhorro();

            foreach (CajaDeAhorro caja in listaCajaAhorro)
            {
                comboBox3_movimientos.Items.Add(caja._cbu);
            }
        }

        private void btn_busca_movimiento_Click(object sender, EventArgs e)
        {
            if (comboBox3_movimientos.Text != "")
            {
                float montoFiltro;
                if (txtb_filtro_monto.Text != "")
                {
                    montoFiltro = float.Parse(txtb_filtro_monto.Text);

                }
                else { montoFiltro = 0; }

                List<Movimiento> listaMovimientos = new List<Movimiento>();

                listaMovimientos = banco.BuscarMovimiento(comboBox3_movimientos.Text,txtb_filtro_detalle.Text, dateTimePicker_filtro.Value, montoFiltro);

                dataGridView_movimiento.Rows.Clear();
                dataGridView_movimiento.Refresh();


                int fila;
                foreach (Movimiento intem in listaMovimientos)
                {
                    fila = dataGridView_movimiento.Rows.Add();
                    dataGridView_movimiento.Rows[fila].Cells[0].Value = banco.buscarCBU(intem._id_CajaDeAhorro);
                    dataGridView_movimiento.Rows[fila].Cells[1].Value = intem._detalle;
                    dataGridView_movimiento.Rows[fila].Cells[2].Value = intem._monto;
                    dataGridView_movimiento.Rows[fila].Cells[3].Value = intem._fecha;

                }




            }
            else { MessageBox.Show("La cuenta es obligatoria"); }
        }





        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch ((sender as TabControl).SelectedIndex)
            {
                case 0:
                    // Do nothing here (let's suppose that TabPage index 0 is the address information which is already loaded.

                    cargaCajasAhorro();

                    break;
                case 1:
                    // PLazo Fijo
                    break;
                case 2:
                    // Pagos
                    cargarPagos();
                    break;
                case 3:

                    cargaTarjetasDeCredito();

                    break;
                case 6:
                    // Let's suppose TabPage index 1 is the one for the transactions.
                    // Assuming you have put a DataGridView control so that the transactions can be listed.
                    // currentCustomer.Id can be obtained through the CurrencyManager of your BindingSource object used to data bind your data to your Windows form controls.

                    break;
            }
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {

            comboBox2.Items.Clear();
            comboBox2.Refresh();
            List<CajaDeAhorro> listaCajaAhorro = new List<CajaDeAhorro>();
            listaCajaAhorro = banco.MostrarCajasDeAhorro();

            foreach (CajaDeAhorro caja in listaCajaAhorro)
            {
                comboBox2.Items.Add(caja._cbu);
            }
        }
        private void btn_ingresar_pago_Click(object sender, EventArgs e)
        {
            float montoPago = 0;
            try { montoPago = float.Parse(txtb_monto_pago.Text); }
            catch (Exception ex) { MessageBox.Show("Debe ingresar el monto del pago"); }


            if (cBox_tarjeta.Text == "" && cBox_caja_ahorro.Text == "")
            {
                MessageBox.Show("Debe ingresar un método de pago");
            }
            else
            {
                if ((cBox_tarjeta.Text == "" && cBox_caja_ahorro.Text != "") || (cBox_tarjeta.Text != "" && cBox_caja_ahorro.Text == ""))
                {

                    if (cBox_tarjeta.Text != "")
                    {
                        banco.AltaPago(montoPago, "TJ", txtb_concepto_pago.Text, Int64.Parse(cBox_tarjeta.Text));

                        cargarPagos();
                    }
                    else if (cBox_caja_ahorro.Text != "")
                    {
                        banco.AltaPago(montoPago, "CA", txtb_concepto_pago.Text, Int64.Parse(cBox_caja_ahorro.Text));
                        MessageBox.Show("Pago ingresado");
                        cargarPagos();
                    }
                    else { MessageBox.Show("Tarjeta o Caja de Ahorro deben tener datos"); }
                }
                else
                {
                    MessageBox.Show("Debe ingresar solo un método de pago");
                    cBox_caja_ahorro.Text = "";
                    cBox_tarjeta.Text = "";
                }
            }

        }

        private void cBox_caja_ahorro_Click(object sender, EventArgs e)
        {
            cBox_caja_ahorro.Items.Clear();
            cBox_caja_ahorro.Refresh();
            List<CajaDeAhorro> listaCajaAhorro = new List<CajaDeAhorro>();
            listaCajaAhorro = banco.MostrarCajasDeAhorro();

            foreach (CajaDeAhorro caja in listaCajaAhorro)
            {
                cBox_caja_ahorro.Items.Add(caja._cbu);
            }

        }

        private void cBox_caja_Click(object sender, EventArgs e)
        {
            comboBoxPlazo.Items.Clear();
            comboBoxPlazo.Refresh();
            List<CajaDeAhorro> listaCajaAhorro = new List<CajaDeAhorro>();
            listaCajaAhorro = banco.MostrarCajasDeAhorro();

            foreach (CajaDeAhorro caja in listaCajaAhorro)
            {
                comboBoxPlazo.Items.Add(caja._cbu);
            }

        }

        public void cargarPagos()
        {
            this.cargaListaPagos(true);
            this.cargaListaPagos(false);

        }

        public void cargaListaPagos(bool pagado)
        {
            if (!pagado)
            {
                dataGridView3.Rows.Clear();
                dataGridView3.Refresh();
                List<Pago> listaPago = new List<Pago>();
                listaPago = banco.MostrarPago(pagado);
                int fila;
                foreach (Pago pago in listaPago)
                {

                    fila = dataGridView3.Rows.Add();
                    dataGridView3.Rows[fila].Cells[0].Value = pago._id;
                    dataGridView3.Rows[fila].Cells[1].Value = pago._metodo;
                    dataGridView3.Rows[fila].Cells[2].Value = pago._detalle;
                    dataGridView3.Rows[fila].Cells[3].Value = pago._monto;

                }
            }
            else
            {

                dataGridView4_pagos_pendientes.Rows.Clear();
                dataGridView4_pagos_pendientes.Refresh();
                List<Pago> listaPago = new List<Pago>();
                listaPago = banco.MostrarPago(pagado);
                int fila;
                foreach (Pago pago in listaPago)
                {

                    fila = dataGridView4_pagos_pendientes.Rows.Add();
                    dataGridView4_pagos_pendientes.Rows[fila].Cells[0].Value = pago._id;
                    dataGridView4_pagos_pendientes.Rows[fila].Cells[1].Value = pago._metodo;
                    dataGridView4_pagos_pendientes.Rows[fila].Cells[2].Value = pago._detalle;
                    dataGridView4_pagos_pendientes.Rows[fila].Cells[3].Value = pago._monto;


                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (banco.ModificarPago(this.celda))
            {
                MessageBox.Show("El pago se realizo de manera exitosa");
                this.cargaListaPagos(true);
                this.cargaListaPagos(false);
            }
            else
            {
                MessageBox.Show("El pago no pudo realizarse-Seleccione el ID de fila");
            }
        }


        private void dataGridView3_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (Banco.IsNumeric(dataGridView3.CurrentCell.Value.ToString()))
            {
                this.celda = int.Parse(dataGridView3.CurrentCell.Value.ToString());
            }
            else
            {
                MessageBox.Show("Debe seleccionar un ID de pago válido");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (banco.EliminarPago(this.celda))
            {
                MessageBox.Show("El pago se elimino de manera exitosa");
                this.cargaListaPagos(true);
                //   this.cargaListaPagos(false);
            }
            else
            {
                MessageBox.Show("El pago no pudo eliminarse-Seleccione el ID de fila");
            }

        }


        private void dataGridView4_pagos_pendientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Banco.IsNumeric(dataGridView4_pagos_pendientes.CurrentCell.Value.ToString()))
            {
                this.celda = int.Parse(dataGridView4_pagos_pendientes.CurrentCell.Value.ToString());
            }
            else
            {
                MessageBox.Show("Debe seleccionar un ID de pago válido");
            }

        }

        private void cargaCajasAhorro()
        {
            int fila;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            List<CajaDeAhorro> listaCajaAhorro = new List<CajaDeAhorro>();
            listaCajaAhorro = banco.MostrarCajasDeAhorro();
            foreach (CajaDeAhorro caja in listaCajaAhorro)
            {

                fila = dataGridView1.Rows.Add();
                dataGridView1.Rows[fila].Cells[0].Value = caja._cbu;
                dataGridView1.Rows[fila].Cells[1].Value = caja._saldo;

            }
        }

        private void cargaTarjetasDeCredito()
        {
            int fila;
            dataGView_Tarjetas.Rows.Clear();
            dataGView_Tarjetas.Refresh();
            List<TarjetaDeCredito> tarjetasCredito = new List<TarjetaDeCredito>();
            tarjetasCredito = banco.MostrarTarjetasDeCredito();

            foreach (TarjetaDeCredito tarjeta in tarjetasCredito)
            {

                fila = dataGView_Tarjetas.Rows.Add();
                dataGView_Tarjetas.Rows[fila].Cells[0].Value = tarjeta._numero;
                dataGView_Tarjetas.Rows[fila].Cells[1].Value = tarjeta._limite;
                dataGView_Tarjetas.Rows[fila].Cells[2].Value = tarjeta._consumos;

            }

        }



        private void btn_Crear_Tarjeta_Click(object sender, EventArgs e)
        {
            if (banco.AltaTarjetaDeCredito())
            {
                MessageBox.Show("Tarjeta Creada exitosamente");
                cargaTarjetasDeCredito();
            }
            else
            {
                MessageBox.Show("Hubo un error al crear la tarjeta");
            }
        }

        private void btn_PagarTarjeta_Click(object sender, EventArgs e)
        {
           if( banco.PagarTarjeta(dataGView_Tarjetas.CurrentCell.Value.ToString(), cbx_lista_CajasAhorro.SelectedItem.ToString()))
            {
                
                MessageBox.Show("Se ha cancelado el saldo de su tarjeta");
                this.cargaTarjetasDeCredito();
            }
            else
            {
                MessageBox.Show("No se realizo el pago, verifique su saldo");
            }
        }

        private void cbx_lista_CajasAhorro_Click(object sender, EventArgs e)
        {
            cbx_lista_CajasAhorro.Items.Clear();
            cbx_lista_CajasAhorro.Refresh();
            List<CajaDeAhorro> listaCajaAhorro = new List<CajaDeAhorro>();
            listaCajaAhorro = banco.MostrarCajasDeAhorro();

            foreach (CajaDeAhorro caja in listaCajaAhorro)
            {
                cbx_lista_CajasAhorro.Items.Add(caja._cbu);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                if (banco.BajaPlazoFijo(int.Parse(dataGridPlazo.CurrentCell.Value.ToString())))
                {

                    MessageBox.Show("El Plazo Fijo se ha eliminado");
                }
                else
                {
                    MessageBox.Show("El plazo fijo aún se encuentra pendiente de pago, pruebe eliminar el registro en una fecha posterior");

                }
                cargaPlazoFijo();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBoxPlazo.Text != "" && textBoxPlazo.Text != "")
            {
                CajaDeAhorro caja;

                DateTime fecha = new DateTime();
                fecha = DateTime.Now;
                float montoPlazo = float.Parse(textBoxPlazo.Text);
                if (montoPlazo >= 1000)
                {



                    if (banco.AltaPlazoFijo(montoPlazo, comboBoxPlazo.SelectedItem.ToString()))
                    {
                        banco.Retirar(comboBoxPlazo.SelectedItem.ToString(), montoPlazo);
                        MessageBox.Show("El plazo fijo ha sido creado exitosamente");
                    }
                    else
                    {
                        MessageBox.Show("El saldo de la cuenta no es suficiente");

                    }


                }
                else
                {
                    MessageBox.Show("El monto debe ser al menos 1000$");
                }
            }
            else
            {
                MessageBox.Show("Elija una caja de ahorro para realizar el Plazo Fijo y un monto");

            }
            cargaPlazoFijo();
            //textBoxPlazo
            //comboBoxPlazo
            //dataGridPlazo
        }

        private void cargaPlazoFijo()
        {
            int fila;
            dataGridPlazo.Rows.Clear();
            dataGridPlazo.Refresh();
            List<PlazoFijo> listaPlazo = new List<PlazoFijo>();
            listaPlazo = banco.MostrarPfUsuario();
            foreach (PlazoFijo pf in listaPlazo)
            {

                fila = dataGridPlazo.Rows.Add();
                dataGridPlazo.Rows[fila].Cells[0].Value = pf._id;
                dataGridPlazo.Rows[fila].Cells[1].Value = pf._monto;
                dataGridPlazo.Rows[fila].Cells[2].Value = pf._fechaIni;
                dataGridPlazo.Rows[fila].Cells[3].Value = pf._fechaFin;
                dataGridPlazo.Rows[fila].Cells[4].Value = pf._tasa;
                dataGridPlazo.Rows[fila].Cells[5].Value = pf._pagado;

            }
        }

        private void cBox_tarjeta_Click(object sender, EventArgs e)
        {
            cBox_tarjeta.Items.Clear();
            cBox_tarjeta.Refresh();
            List<TarjetaDeCredito> listaTarjetas = new List<TarjetaDeCredito>();
            listaTarjetas = banco.MostrarTarjetasDeCredito();

            foreach (TarjetaDeCredito tc in listaTarjetas)
            {
                cBox_tarjeta.Items.Add(tc._numero);
            }

        }

        private void buttonDesbloquear_Click(object sender, EventArgs e)
        {
            try
            {
                if (banco.desbloquearUsuario(int.Parse(dataGridUsuarios.CurrentCell.Value.ToString())))
                {

                    MessageBox.Show("El Usuario ha sido desbloqueado");
                }
                else
                {
                    MessageBox.Show("El usuario seleccionado no se encuentra bloqueado");

                }
                cargaUsuarios();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }



        private void cargaUsuarios()
        {
            int fila;
            dataGridUsuarios.Rows.Clear();
            dataGridUsuarios.Refresh();
            List<Usuario> listaUsuario = new List<Usuario>();
            listaUsuario = banco.MostrarUsuarios();
            foreach (Usuario Us in listaUsuario)
            {

                fila = dataGridUsuarios.Rows.Add();
                dataGridUsuarios.Rows[fila].Cells[0].Value = Us._id;
                dataGridUsuarios.Rows[fila].Cells[1].Value = Us._dni;
                dataGridUsuarios.Rows[fila].Cells[2].Value = Us._nombre;
                dataGridUsuarios.Rows[fila].Cells[3].Value = Us._apellido;
                dataGridUsuarios.Rows[fila].Cells[4].Value = Us._bloqueado;


            }
        }

        private void comBox_id_usuario_Traslado_Click(object sender, EventArgs e)
        {

            comBox_id_usuario_Traslado.Items.Clear();
            comBox_id_usuario_Traslado.Refresh();
            List<Usuario> listaUsuarios = new List<Usuario>();
            listaUsuarios = banco.MostrarUsuarios();

            foreach (Usuario usuario in listaUsuarios)
            {
                comBox_id_usuario_Traslado.Items.Add(usuario._id);
            }

        }

        private void comBox_cbu_Traslado_Saldo_Click(object sender, EventArgs e)
        {
            comBox_cbu_Traslado_Saldo.Items.Clear();
            comBox_cbu_Traslado_Saldo.Refresh();
            List<CajaDeAhorro> listaCajaAhorro = new List<CajaDeAhorro>();
            listaCajaAhorro = banco.MostrarCajasDeAhorro();

            foreach (CajaDeAhorro caja in listaCajaAhorro)
            {
                comBox_cbu_Traslado_Saldo.Items.Add(caja._cbu);
            }


        }

        private void btn_traslada_Caja_Click(object sender, EventArgs e)
        {
            if (comBox_cbu_Traslado_Saldo.Text != "" && comBox_id_usuario_Traslado.Text != "")
            {

                //Accion 1 es trasladar
             if(banco.ModificarCajaDeAhorro(comBox_cbu_Traslado_Saldo.Text, int.Parse(comBox_id_usuario_Traslado.Text), 1))
                {
                    MessageBox.Show("Se  asoció el usuario a la caja de ahorro");
                }
                else
                {
                    MessageBox.Show("No se pudo asociar la caja");
                }

            }
            else { MessageBox.Show("Usuario y Caja Son Necesarios"); }



        }

        private void btn_elimina_Caja_Click(object sender, EventArgs e)
        {
            if (comBox_cbu_Traslado_Saldo.Text != "" && comBox_id_usuario_Traslado.Text != "")
            {

                //Accion 0 es Quitar
                if (banco.ModificarCajaDeAhorro(comBox_cbu_Traslado_Saldo.Text, int.Parse(comBox_id_usuario_Traslado.Text), 0))
                {
                    MessageBox.Show("Se  quito el usuario a la caja de ahorro");
                }
                else
                {
                    MessageBox.Show("No se pudo quitar la caja");
                }

            }
            else { MessageBox.Show("Usuario y Caja Son Necesarios"); }

        }
    }
}

