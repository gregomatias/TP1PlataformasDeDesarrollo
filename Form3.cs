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

namespace TP1
{
    internal partial class Form3 : Form
    {
        private object[] argumentos;
        private  List<List<string>> datos;
        private string nombreUsuarioLogueado;
        private Banco banco;
        public Form3(string nombreUsuarioLogueado,Banco banco)
        {
           
            this.nombreUsuarioLogueado = nombreUsuarioLogueado;
            this.banco = banco;
            InitializeComponent();
            label2.Text = this.nombreUsuarioLogueado;
      


        }
        public Form3(object[] args)
        {
            InitializeComponent();
            argumentos = args;
            label2.Text = (string)args[0];
            
            datos = new List<List<string>>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        private void refreshData()
        {
            //borro los datos
            dataGridView1.Rows.Clear();
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

    }
}
