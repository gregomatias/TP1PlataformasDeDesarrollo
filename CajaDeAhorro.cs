using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    internal class CajaDeAhorro
    {
        public CajaDeAhorro(int cbu, List<Usuario> titulares)
        {
			_id = _id + 1;	
			_cbu = cbu;
			_titulares = new List<Usuario>();
			_titulares = titulares;
			_saldo = 0;

        }

        static private int id = 0;

		public int _id
		{
			get { return id; }
			set { id = value; }
		}

		private int cbu;

		public int _cbu
		{
			get { return cbu; }
			set { cbu = value; }
		}

		private List<Usuario> titulares;

		public List<Usuario>  _titulares

		{
			get { return titulares; }
			set { titulares = value; }
		}

		private float saldo;


		public float _saldo
		{
			get { return saldo; }
			set { saldo = value; }
		}


	}
}
