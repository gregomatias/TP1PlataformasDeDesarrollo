using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    internal class PlazoFijo
    {

        public PlazoFijo(Usuario titular, float monto, DateTime fechaFin, float tasa)
        {
			_id = _id + 1;
			_titular = titular;
			_monto = monto;
			_fechaFin = fechaFin;
			_tasa = tasa;
			_pagado = false;
        }

        static private int id;

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

		private float monto;

		public float _monto
		{
			get { return monto; }
			set { monto = value; }
		}

		private DateTime fechaIni;

		public DateTime _fechaIni
		{
			get { return fechaIni; }
			set { fechaIni = value; }
		}

		private DateTime fechaFin;

		public DateTime _fechaFin
		{
			get { return fechaFin; }
			set { fechaFin = value; }
		}

		private float tasa;

		public float _tasa
		{
			get { return tasa; }
			set { tasa = value; }
		}

		private bool pagado;

		public bool _pagado
		{
			get { return pagado; }
			set { pagado = value; }
		}





	}
}
