﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    internal class Usuario
    {
        static private int id;
        private int dni;
        private string nombre;
        private string apellido;
        private string mail;
        private string password;
        private int intentosFallidos;
        private bool bloqueado;

        public List<PlazoFijo> pfs = new List<PlazoFijo>();
        public List<TarjetaDeCredito> tarjetas = new List<TarjetaDeCredito>();
        public List<Pago> pagos = new List<Pago>();
        public List<CajaDeAhorro> cajas = new List<CajaDeAhorro>();

        public Usuario(int dni, string nombre, string apellido, string mail, string password)
        {
            _id = _id + 1;
            _dni = dni;
            _nombre = nombre;
            _apellido = apellido;
            _mail = mail;
            _password = password;
            _intentosFallidos = 0;
            _bloqueado = false;

        }

        public int _id
        {
            get { return id; }
            set { id = value; }
        }

        public int _dni
        {
            get { return dni; }
            set { dni = value; }
        }

        public string _nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string _apellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        public string _mail
        {
            get { return mail; }
            set { mail = value; }
        }

        public string _password
        {
            get { return password; }
            set { password = value; }
        }

        public int _intentosFallidos
        {
            get { return intentosFallidos; }
            set { intentosFallidos = value; }
        }

        public bool _bloqueado
        {
            get { return bloqueado; }
            set { bloqueado = value; }
        }

        public List<CajaDeAhorro> _Cajas
        {
            get { return cajas; }
            set { cajas = value; }
        }


    }
}
