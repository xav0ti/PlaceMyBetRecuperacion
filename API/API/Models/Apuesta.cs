using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Apuesta
    {

            public Apuesta(int idApuesta, string tipoApuesta, double cuota, double dineroApostado, double Id_Mercado, int idUsuario)
            {
                this.Id_Apuesta = idApuesta;
                this.TipoApuesta = tipoApuesta;
                this.Cuota = cuota;
                this.DineroApostado = dineroApostado;
                this.Id_Mercado = Id_Mercado;
                this.Id_Usuario = idUsuario;
            }

            public int Id_Apuesta { get; set; }
            public string TipoApuesta { get; set; }
            public double Cuota { get; set; }
            public double DineroApostado { get; set; }
            public double Id_Mercado { get; set; }
            public int Id_Usuario { get; set; }

        }


        //Sacame todos los datos de las apuestas

        public class ApuestaDTO
        {
            public ApuestaDTO(int Id_Apuesta, string tipoApuesta, double cuota, double dineroApostado, double Id_Mercado)
            {
                this.Id_Apuesta = Id_Apuesta;
                this.TipoApuesta = tipoApuesta;
                this.Cuota = cuota;
                this.Id_Mercado = Id_Mercado;
                this.DineroApostado = dineroApostado;
            }

            public int Id_Apuesta { get; set; }
            public string TipoApuesta { get; set; }
            public double Cuota { get; set; }
            public double DineroApostado { get; set; }
            public double Id_Mercado { get; set; }

        }


        //Para un email concreto de usuario, recuperar todas sus apuestas.Al menos, con
        //la siguiente información: evento, tipo de mercado, tipo de apuesta cuota y dinero apostado.
        public class ApuestaEmail
        {
            public ApuestaEmail(int idEvento, double tipoMercado, string tipoApuesta, double cuota, double dineroApostado)
            {
                this.IdEvento = idEvento;
                this.TipoMercado = tipoMercado;
                this.TipoApuesta = tipoApuesta;
                this.Cuota = cuota;
                this.DineroApostado = dineroApostado;

            }

            public int IdEvento { get; set; }
            public double TipoMercado { get; }
            public string TipoApuesta { get; }
            public double Cuota { get; }
            public double DineroApostado { get; }
        }


        //Para un mercado concreto, recuperar todas sus apuestas. Al menos, con la siguiente
        //información: email de usuario, tipo de mercado, tipo de apuesta, cuota y dinero apostado.
        public class ApuestaMerc
        {
            public ApuestaMerc(string emailUsu, double tipoMercado, string tipoApuesta, double cuota, double dineroApostado)
            {
                this.EmailUsu = emailUsu;
                this.TipoMercado = tipoMercado;
                this.TipoApuesta = tipoApuesta;
                this.Cuota = cuota;
                this.DineroApostado = dineroApostado;

            }

            public string EmailUsu { get; set; }
            public double TipoMercado { get; }
            public string TipoApuesta { get; }
            public double Cuota { get; }
            public double DineroApostado { get; }
        }

    //EXAMEN EJERCICIO 1 PLACEMYBET

        public class ApuestaExamen
        {
            public ApuestaExamen(string nombre, int mercadoId, double cuota, double dineroApostado)
            {
                this.Nombre = nombre;
                this.MercadoId = mercadoId;
                this.Cuota = cuota;
                this.DineroApostado = dineroApostado;

            }

            public string Nombre { get; set; }
            public int MercadoId { get; }
            public double Cuota { get; }
            public double DineroApostado { get; }
        }










}