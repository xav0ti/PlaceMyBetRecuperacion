using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class MercadoRepository
    {

        private MySqlConnection Connect()
        {
            string conString = "server=127.0.0.1; port=3306; Database=apuestas; Uid=root; Password=''; SslMode=none";
            MySqlConnection con = new MySqlConnection(conString);

            return con;
        }


        //Cuando se devuelve el listado de todos los mercados, queremos devolver la información
        //referente al tipo de mercado y las cuotas over y under.

        internal List<MercadoDTO> Retrieve()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from Mercado";

            con.Open();
            MySqlDataReader res = command.ExecuteReader();


            List<MercadoDTO> mercados = new List<MercadoDTO>();
            while (res.Read())
            {
                mercados.Add(new MercadoDTO(res.GetDouble(0), res.GetDouble(1), res.GetDouble(2)));
            }
            return mercados;
        }




        //Para un evento concreto, recuperar todos sus mercados con los siguientes datos:
        //tipo de mercado y cuotas over/under

        internal List<MercadoEve> RetrievebyEvento(int idEvento)
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select TipoMercado, InfoCuotaOver, InfoCuotaUnder from Mercado WHERE idEvento = " + idEvento;

            con.Open();
            MySqlDataReader res = command.ExecuteReader();


            List<MercadoEve> mercadosEv = new List<MercadoEve>();
            while (res.Read())
            {
                mercadosEv.Add(new MercadoEve(res.GetDouble(0), res.GetDouble(1), res.GetDouble(2)));
            }
            return mercadosEv;
        }







    }
}