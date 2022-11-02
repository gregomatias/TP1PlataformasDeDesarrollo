using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    internal class CajaDeAhorro
    {
        private List<int> titulares = new List<int>();

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

        static private int id = 0;

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



		private List<Movimiento> movimientos = new List<Movimiento>();

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


	}
}
