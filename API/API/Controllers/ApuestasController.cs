using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class ApuestasController : ApiController
    {

        //Sacame todos los datos de las apuestas
        // GET: api/Apuestas

        [Authorize]
        public IEnumerable<ApuestaDTO> Get()
        {
            var repo = new ApuestaRepository();
            List<ApuestaDTO> apuestas = repo.Retrieve();

            return apuestas;
        }

        //Para un email concreto de usuario, recuperar todas sus apuestas.Al menos, con
        //la siguiente información: evento, tipo de mercado, tipo de apuesta cuota y dinero apostado.
        // GET: api/Apuestas?email=email
        public IEnumerable<ApuestaEmail> GetApuestas(string email) //Aqui le pasamos email porque es lo que le vamos a pasar en Postman para recuperar datos
        {
            var repo = new ApuestaRepository();
            List<ApuestaEmail> apuestas = repo.RetrievebyEmail(email);
            return apuestas;
        }
         

        public void Post([FromBody] Apuesta apu)
        {
            var repo = new ApuestaRepository();
            repo.Save(apu);
        }


        //EXAMEN EJERCICIO 1 RECUPERACION

        //Get: api/Apuestas?idMercado=mercado

        public IEnumerable<Apuesta> GetApuestaMerca(int idMercado)
        {
            var repo = new ApuestaRepository();
            List<Apuesta> apuesta = repo.RetrieveRecuperacion(idMercado);

            return apuesta;
        }




        //EXAMEN EJERCICIO 2 RECUPERACION

        // GET: api/Apuestas?id_Usuario=idusu&&cuota>cuota

        public IEnumerable<Apuesta> GetApuestaUsu(int idUsuario, int cuota)
        {
            var repo = new ApuestaRepository();
            List<Apuesta> apuesta = repo.RetrieveUsuCuota(idUsuario, cuota);

            return apuesta;
        }





    }
}
