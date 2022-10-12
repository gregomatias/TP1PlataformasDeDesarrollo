using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TP1
{
    internal partial class Form3 : Form
    {
        private object[] argumentos;
        private List<List<string>> datos;
        private Banco banco;
        private TransfDelegadoForm2 transEvento;

        public Form3(Banco banco, TransfDelegadoForm2 transEvento)
        {

            this.transEvento = transEvento;

            this.banco = banco;
            InitializeComponent();
            label2.Text = banco.GetNombreUsuarioLogueado();



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
     
            //agrego lo nuevo
            //   dataGridView1.Rows.Add(user.toArray());
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void btn_crearCajaAhorro_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            banco.CrearCajaDeAhorro();
            List<CajaDeAhorro> listaCajaAhorro = new List<CajaDeAhorro>();
            listaCajaAhorro = banco.MostrarCajasDeAhorro();
            foreach (CajaDeAhorro caja in listaCajaAhorro)
            {

                int fila = dataGridView1.Rows.Add();
                dataGridView1.Rows[fila].Cells[0].Value = caja._cbu;
                dataGridView1.Rows[fila].Cells[1].Value = caja._saldo;

            }

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
            //MessageBox.Show("Selected Item  ");
            //selectedIndex.ToString());
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


                    if (banco.Depositar(comboBox1.SelectedIndex, monto))
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


                    if (banco.Retirar(comboBox1.SelectedIndex, monto))
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
            //comboBox1.Text = "";
            comboBox2.Items.Clear();
            comboBox2.Refresh();
            List<CajaDeAhorro> listaCajaAhorro = new List<CajaDeAhorro>();
            listaCajaAhorro = banco.MostrarCajasDeAhorro();

            foreach (CajaDeAhorro caja in listaCajaAhorro)
            {
                comboBox2.Items.Add(caja._cbu);
            }
        }

        private void btn_transferir_Click(object sender, EventArgs e)
        {



            if (txtb_monto_transferencia.Text != "" && txtb_cbu_destino.Text == "")
            {
                float monto = float.Parse(txtb_monto_transferencia.Text);
                int cbu_destino = int.Parse(txtb_cbu_destino.Text);
                if (monto > 0)
                {


                    if (banco.Transferir(comboBox2.SelectedIndex, cbu_destino, monto))
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
            //comboBox1.Text = "";
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
                    //comboBox3_movimientos.SelectedIndex
                }
                else { montoFiltro = 0; }

                List<Movimiento> listaMovimientos = new List<Movimiento>();
                listaMovimientos = banco.BuscarMovimiento(comboBox3_movimientos.SelectedIndex,
                txtb_filtro_detalle.Text, dateTimePicker_filtro.Value, montoFiltro);

                dataGridView_movimiento.Rows.Clear();
                dataGridView_movimiento.Refresh();


                int fila;
                foreach (Movimiento intem in listaMovimientos)
                {
                    fila = dataGridView_movimiento.Rows.Add();
                    dataGridView_movimiento.Rows[fila].Cells[0].Value = intem._cajaDeAhorro._cbu;
                    dataGridView_movimiento.Rows[fila].Cells[1].Value = intem._detalle;
                    dataGridView_movimiento.Rows[fila].Cells[2].Value = intem._monto;
                    dataGridView_movimiento.Rows[fila].Cells[3].Value = intem._fecha;

                }




            }
            else { MessageBox.Show("La cuenta es obligatoria"); }
        }



        private void tabPage1_Click(object sender, EventArgs e)
        {
           
            

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((sender as TabControl).SelectedIndex)
            {
                case 0:
                    // Do nothing here (let's suppose that TabPage index 0 is the address information which is already loaded.
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                    List<CajaDeAhorro> listaCajaAhorro = new List<CajaDeAhorro>();
                    listaCajaAhorro = banco.MostrarCajasDeAhorro();
                    foreach (CajaDeAhorro caja in listaCajaAhorro)
                    {

                        int fila = dataGridView1.Rows.Add();
                        dataGridView1.Rows[fila].Cells[0].Value = caja._cbu;
                        dataGridView1.Rows[fila].Cells[1].Value = caja._saldo;

                    }
                    break;
                case 1:
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
    }
}
