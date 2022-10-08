using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    internal class TarjetaDeCredito
    {

        public TarjetaDeCredito(Usuario titular, int numero, int codigoV, float limite)
        {
			_id = _id + 1;
			_titular = titular;
			_numero = numero;
			_codigoV = codigoV;
			_limite = limite;
			_consumos = 0;

        }

        static private int id = 0;

		public int _id
		{
			get { return id; }
			set { id = value; }
		}

		private Usuario titular;

		public Usuario _titular
		{
			get { return titular; }
			set { titular = value; }
		}

		private int numero;

		public int _numero
		{
			get { return numero; }
			set { numero = value; }
		}

		private int codigoV;

		public int _codigoV
		{
			get { return codigoV; }
			set { codigoV = value; }
		}

		private float limite;

		public float _limite
		{
			get { return limite; }
			set { limite = value; }
		}

		private float consumos;

		public float _consumos
		{
			get { return consumos; }
			set { consumos = value; }
		}


	}
}
