﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    internal class Movimiento
    {
        public Movimiento(CajaDeAhorro cajaDeAhorro, string detalle, double monto)
        {
			_id = _id + 1;
			_cajaDeAhorro = cajaDeAhorro;
			_detalle = detalle;
			_monto = monto;
			_fecha = DateTime.Now;
        }

		public Movimiento(int id,int id_CajaDeAhorro,String detalle, double monto, DateTime fecha)
		{
			_id = id;
			_id_CajaDeAhorro = id_CajaDeAhorro;
			_detalle = detalle;
			_monto = monto;
			_fecha= fecha;
		}

		private int id_CajaDeAhorro;

		public int _id_CajaDeAhorro
		{
			get { return id_CajaDeAhorro; }
			set { id_CajaDeAhorro = value; }
		}

        private int id;

		public int _id
		{
			get { return id; }
			set { id = value; }
		}

		private CajaDeAhorro cajaDeAhorro;

		public CajaDeAhorro _cajaDeAhorro
		{
			get { return cajaDeAhorro; }
			set { cajaDeAhorro = value; }
		}

		private string detalle;

		public string _detalle
		{
			get { return detalle; }
			set { detalle = value; }
		}

		private double monto;

		public double _monto
		{
			get { return monto; }
			set { monto = value; }
		}

		private DateTime fecha;

		public DateTime _fecha
		{
			get { return fecha; }
			set { fecha = value; }
		}


	}
}
