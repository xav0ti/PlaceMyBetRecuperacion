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

    //EJERCICIO 1 EXAMEN PLACEMYBET

        // GET: api/Apuestas?idMercado=mercado

        public IEnumerable<ApuestaMerc> GetApuestasMerc(int idMercado) //Aqui le pasamos idMercado porque es lo que le vamos a pasar en Postman para recuperar datos
        {
            var repo = new ApuestaRepository();
            List<ApuestaMerc> apuestas = repo.RetrievebyMercado(idMercado);
            return apuestas;
        }
         

        public void Post([FromBody] Apuesta apu)
        {
            var repo = new ApuestaRepository();
            repo.Save(apu);
        }


        //EXAMEN EJERCICIO 1 ==> Saca una lista ApuestaExamen donde se obtenga el Nombre de usuario, el mercado, la cuota y el dinero apostado

        // GET: api/ApuestaExamen 
        [Route("api/ApuestaExamen")]

        public IEnumerable<ApuestaExamen> GetEjercico1()
        {
            var repo = new ApuestaRepository();
            List<ApuestaExamen> apuestas = repo.RetrieveEjercicio1();

            return apuestas;
        }



        //EXAMEN EJERCICIO 2 ==> Recupera las apuestas cuyas cuots estan entre un valor minimo y un valor maximo pasados por parámetros

        // GET: api/Apuesta?anyo=valor1&anyofin=valor2
        public IEnumerable<ApuestaValores> GetEjercicio2(int valormin, int valormax)
        {
            var repo = new ApuestaRepository();
            List<ApuestaValores> apuestas = repo.RetrieveEjercicio2(valormin, valormax);

            return apuestas;
        }





    }
}
