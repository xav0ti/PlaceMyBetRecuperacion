﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Globalization;


namespace API.Models
{
    public class EventoRepository
    {

        //Conexión con la bbdd
        private MySqlConnection Connect()
        {
            string conString = "server=127.0.0.1; port=3306; Database=apuestas; Uid=root; Password=''; SslMode=none";
            MySqlConnection con = new MySqlConnection(conString);

            return con;
        }


        //Cuando se devuelve el listado de todos los eventos, queremos devolver s´olo los nombres de los equipos.

        internal List<EventoDTO> Retrieve()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select nombreLocal, nombreVisitante from Evento";

            con.Open();
            MySqlDataReader res = command.ExecuteReader();


            List<EventoDTO> eventos = new List<EventoDTO>();
            while (res.Read())
            {
                eventos.Add(new EventoDTO(res.GetString(0), res.GetString(1)));
            }
            return eventos;
        } 


        //PLACEMYBET TIPO A EJERCICIO 3 ==> 

        /*
        MySqlConnection conEvento = Connect();
        MySqlCommand commandEvento = conEvento.CreateCommand();
        commandEvento.CommandText = "INSERT INTO Evento (Id, NombreLocal, NombreVisitante) VALUES ('" + eve.Id + "' , '" + eve.NombreLocal + "' , '" + eve.NombreVisitante + "'); ";


            Debug.WriteLine("hago el comando " + commandEvento.CommandText);


            Debug.WriteLine("comando " + commandEvento.CommandText);

            try
            {
                conEvento.Open();
                commandEvento.ExecuteNonQuery();
                conEvento.Close();
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexión " + e.Message);
            }
        */


    }
}