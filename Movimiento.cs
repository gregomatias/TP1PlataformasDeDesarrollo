using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    internal class Movimiento
    {
        public Movimiento(CajaDeAhorro cajaDeAhorro, string detalle, float monto)
        {
			_id = _id + 1;
			_cajaDeAhorro = cajaDeAhorro;
			_detalle = detalle;
			_monto = monto;
			_fecha = DateTime.Now;
        }

        static private int id;

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

		private float monto;

		public float _monto
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
