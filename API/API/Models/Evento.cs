using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Evento
    {
        public Evento(int id, string nombreLocal, string nombreVisitante)
        {
            this.Id_Evento = id;
            this.NombreLocal = nombreLocal;
            this.NombreVisitante = nombreVisitante;

        }

        public int Id_Evento { get; set; }
        public string NombreLocal { get; set; }
        public string NombreVisitante { get; set; }

    }

    //Cuando se devuelve el listado de todos los eventos, queremos devolver sólo los nombres de los equipos.

    public class EventoDTO
    {
        public EventoDTO(string nombreLocal, string nombreVisitante)
        {
            this.NombreLocal = nombreLocal;
            this.NombreVisitante = nombreVisitante;

        }

        public string NombreLocal { get; set; }
        public string NombreVisitante { get; set; }


    }




}