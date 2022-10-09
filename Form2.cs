using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TP1
{
    internal partial class Form2 : Form
    {
         Banco banco;

        public Form2(Banco banco)
        {
            this.banco = banco; 
            InitializeComponent();
        }

        private void btn_AceptaRegistro_Click(object sender, EventArgs e)
        {
            int dni = int.Parse(txtBox_dni.Text);


            banco.AltaUsuario(dni, txtBox_nombres.Text, txtBox_apellidos.Text, txtBox_email.Text, txtBox_contrasena.Text);


        }
    }
}
