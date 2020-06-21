using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class EventosController : ApiController
    {

        //Cuando se devuelve el listado de todos los eventos, queremos devolver sólo los nombres de los equipos.

        // GET: api/Eventos
        public IEnumerable<EventoDTO> Get()
        {
            var repo = new EventoRepository();
            List<EventoDTO> eventos = repo.Retrieve();

            return eventos;
        }


    }
}
