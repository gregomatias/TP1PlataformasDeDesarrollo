﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    internal class CajaDeAhorro
    {
        private List<int> titulares = new List<int>();
        private List<Movimiento> movimientos = new List<Movimiento>();

        public CajaDeAhorro(int id,string cbu, int usuario,double saldo)
        {
            _id = id;
            _cbu = cbu;
            _titulares.Add(usuario);
			_saldo = saldo;

        }

     

        public List<int> _titulares

        {
            get { return titulares; }
            set { titulares = value; }
        }

        private int id = 0;

		public int _id
		{
			get { return id; }
			set { id = value; }
		}

		private string cbu;

		public string _cbu
		{
			get { return cbu; }
			set { cbu = value; }
		}


		public List<Movimiento> _movimientos
		{
			get { return movimientos; }
			set { movimientos =value; }
		}
		

		private double saldo;


		public double _saldo
		{
			get { return saldo; }
			set { saldo = value; }
		}

        public override string ToString()
        {
            return "Id: " + _id + " CBU: " + _cbu + " Titulares: " + _titulares.ToString() + " Saldo: " + _saldo;
        }



    }
}
