using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class MercadosController : ApiController
    {

        //GET: api/Mercados
        public IEnumerable<MercadoDTO> Get()
        {
            var repo = new MercadoRepository();
            List<MercadoDTO> mercado = repo.Retrieve();

            return mercado;
        }


        // GET: api/Mercados?idEvento=evento
        public IEnumerable<MercadoEve> GetApuestas(int idEvento)
        {
            var repo = new MercadoRepository();
            List<MercadoEve> mercadoEve = repo.RetrievebyEvento(idEvento);
            return mercadoEve;
        }

    }
}
