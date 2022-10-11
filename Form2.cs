﻿using System;
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
        private Banco banco;
        private TransfDelegadoForm2 TransEvento;


        public Form2(Banco banco, TransfDelegadoForm2 TransEvento)
        {
            this.banco = banco;
            this.TransEvento = TransEvento;
            InitializeComponent();

        }

        public delegate void TransfDelegadoForm2();

        private void btn_AceptaRegistro_Click(object sender, EventArgs e)
        {
            
            int dni = int.Parse(txtBox_dni.Text);
            banco.AltaUsuario(dni, txtBox_nombres.Text, txtBox_apellidos.Text, txtBox_email.Text, txtBox_contrasena.Text);

            //Traspaso a Form1
            this.TransEvento();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
