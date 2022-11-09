using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    internal class PlazoFijo
    {
		
        public PlazoFijo(Usuario titular, double monto, DateTime fechaFin, double tasa)
        {
			_id = _id + 1;
			_titular = titular;
			_monto = monto;
			_fechaIni= DateTime.Now;
            _fechaFin = fechaFin;
			_tasa = tasa;
			_pagado = 0;
        }
		
		

        public PlazoFijo(int id, int id_usuario, double monto, DateTime fechaFin, double tasa,int pagado)
        {
            _id = id;
            _id_usuario = id_usuario;
            _monto = monto;
            _fechaIni = DateTime.Now;
            _fechaFin = fechaFin;
            _tasa = tasa;
            _pagado = pagado;
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


		private Usuario titular;

		public Usuario _titular
		{
			get { return titular; }
			set { titular = value; }
		}

		private double monto;

		public double _monto
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

		private double tasa;

		public double _tasa
		{
			get { return tasa; }
			set { tasa = value; }
		}

		private int pagado;

		public int _pagado
		{
			get { return pagado; }
			set { pagado = value; }
		}





	}
}
