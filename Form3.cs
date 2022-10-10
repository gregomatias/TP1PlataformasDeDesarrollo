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
        private string usuarioLogueado;
        public Form3(string usuarioLogueado)
        {
            
            this.usuarioLogueado = usuarioLogueado;
            InitializeComponent();
            label2.Text = this.usuarioLogueado;
      


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
    }
}
