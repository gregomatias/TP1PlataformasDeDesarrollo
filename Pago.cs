using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    internal class Pago
    {
        public Pago(Usuario usuario, float monto, bool pagado, string metodo)
        {
			_id = _id + 1;
			_usuario = usuario;
			_monto = monto;
			_pagado = pagado;
			_metodo = metodo;
        }

        static private int id;

		public int _id
		{
			get { return id; }
			set { id = value; }
		}

		private Usuario usuario;

		public Usuario _usuario
		{
			get { return usuario; }
			set { usuario = value; }
		}

		private float monto;

		public float _monto
		{
			get { return monto; }
			set { monto = value; }
		}

		private bool pagado;

		public bool _pagado
		{
			get { return pagado; }
			set { pagado = value; }
		}

		private string metodo;

		public string _metodo
		{
			get { return metodo; }
			set { metodo = value; }
		}


	}
}
