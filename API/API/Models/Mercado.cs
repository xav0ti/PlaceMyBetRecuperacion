using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Mercado
    {
        public Mercado (double tipo, double infoCuotaOver, double infoCuotaUnder, double dineroApostadoOver, double dineroApostadoUnder, double tipoMercado, int idEvento)
        {
            this.Tipo = tipo;
            this.InfoCuotaOver = infoCuotaOver;
            this.InfoCuotaUnder = infoCuotaUnder;
            this.DineroApostadoOver = dineroApostadoOver;
            this.DineroApostadoUnder = dineroApostadoUnder;
            this.idEvento = idEvento;

        }

        public double Tipo { get; set; }
        public double InfoCuotaOver { get; set; }
        public double InfoCuotaUnder { get; set; }
        public double DineroApostadoOver { get; set; }
        public double DineroApostadoUnder { get; set; }
        public int idEvento { get; set; }

    }



    //Cuando se devuelve el listado de todos los mercados, queremos devolver la información
    //referente al tipo de mercado y las cuotas over y under.

    public class MercadoDTO
    {
        public MercadoDTO(double tipo, double infoCuotaOver, double infoCuotaUnder)
        {
            this.Tipo = tipo;
            this.InfoCuotaOver = infoCuotaOver;
            this.InfoCuotaUnder = infoCuotaUnder;
        }

        public double Tipo { get; set; }
        public double InfoCuotaOver { get; set; }
        public double InfoCuotaUnder { get; set; }


    }


    //Para un evento concreto, recuperar todos sus mercados con los siguientes datos:
    //tipo de mercado y cuotas over/under

    public class MercadoEve
    {
        public MercadoEve(double tipo, double infoCuotaOver, double infoCuotaUnder)
        {
            this.Tipo = tipo;
            this.InfoCuotaOver = infoCuotaOver;
            this.InfoCuotaUnder = infoCuotaUnder;
        }

        public double Tipo { get; set; }
        public double InfoCuotaOver { get; set; }
        public double InfoCuotaUnder { get; set; }

    }





}