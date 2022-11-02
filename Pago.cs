using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
	internal class Pago
	{
		public Pago(int id, Usuario usuario, float monto, string metodo, string detalle, string id_metodo)
		{
			_id = id;
			_usuario = usuario;
			_monto = monto;
			_pagado = false;
			_metodo = metodo;
			_detalle = detalle;
			_id_metodo = id_metodo;

		}

		private int id;

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

		private string detalle;

		public string _detalle
		{
			get { return detalle; }
			set { detalle = value; }
		}

		private string id_metodo;

		public string _id_metodo
		{
			get { return id_metodo; }
			set { id_metodo = value; }
		}
		


	}
}
