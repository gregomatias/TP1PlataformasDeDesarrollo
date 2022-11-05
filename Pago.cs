using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
	internal class Pago
	{
		public Pago(int id, Usuario usuario, double monto, string metodo, string detalle, int id_metodo)
		{
			_id = id;
			_usuario = usuario;
			_monto = monto;
			_pagado = false;
			_metodo = metodo;
			_detalle = detalle;
			_id_metodo = id_metodo;

		}

		public Pago(int id, int id_usuario, double monto,int pagado,String metodo, String detalle, int id_metodo)
		{
			_id = id;
			_id_usuario=id_usuario;
			_monto = monto;
			_metodo = metodo;
			_detalle=detalle;
			_id_metodo = id_metodo;
			if (pagado == 1)
			{
				_pagado = true;
			}
			else
			{
				_pagado = false;
			}
		}

		private int id;

		public int _id
		{
			get { return id; }
			set { id = value; }
		}

		private int id_usuario;

		public int _id_usuario
		{
			get { return id_usuario; }
			set { id_usuario = value; }
		}

		private Usuario usuario;

		public Usuario _usuario
		{
			get { return usuario; }
			set { usuario = value; }
		}

		private double monto;

		public double _monto
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

		private string detalle;

		public string _detalle
		{
			get { return detalle; }
			set { detalle = value; }
		}

		 private int id_metodo;

		public int _id_metodo
		{
			get { return id_metodo; }
			set { id_metodo = value; }
		}
		


	}
}
