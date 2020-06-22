using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace API.Models
{
    public class ApuestaRepository
    {

        //Hacemos la conexión con la bbdd
        private MySqlConnection Connect()
        {
            string conString = "server=127.0.0.1; port=3306; Database=apuestas; Uid=root; Password=''; SslMode=none";
            MySqlConnection con = new MySqlConnection(conString);

            return con;
        }


        //Sacame todos los datos de las apuestas
        internal List<ApuestaDTO> Retrieve()
        {
            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "select * from Apuesta";

            con.Open();
            MySqlDataReader res = command.ExecuteReader();


            List<ApuestaDTO> apuestas = new List<ApuestaDTO>();
            while (res.Read())
            {
                //En caso de querer pasar los datos por consola:
                //Debug.WriteLine(res.GetInt32(0) + " " + res.GetString(1) + " " + res.GetDouble(2) + " " + res.GetDouble(3) + " " + res.GetDouble(4));

                apuestas.Add(new ApuestaDTO(res.GetInt32(0), res.GetString(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4)));
            }
            return apuestas;
        }


        //Para un email concreto de usuario, recuperar todas sus apuestas.Al menos, con
        //la siguiente información: evento, tipo de mercado, tipo de apuesta cuota y dinero apostado.

        internal List<ApuestaEmail> RetrievebyEmail(string email)
        {

            List<ApuestaEmail> apuestasEmail = new List<ApuestaEmail>();

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT M.idEvento, M.TipoMercado, A.TipoApuesta, A.Cuota, A.DineroApostado from apuesta A, usuario U, mercado M WHERE A.Id_Mercado = M.Id AND A.Id_Usuario = U.idUsuario AND U.Email =" + "'" + email + "'";

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            while (res.Read())
            {
                apuestasEmail.Add(new ApuestaEmail(res.GetInt32(0), res.GetDouble(1), res.GetString(2), res.GetDouble(3), res.GetDouble(4)));
            }

            return apuestasEmail;
        }



        //Para un mercado concreto, recuperar todas sus apuestas.Al menos, con la siguiente
        //información: email de usuario, tipo de mercado, tipo de apuesta, cuota y dinero apostado

        internal List<ApuestaMerc> RetrievebyMercado(int idMercado) 
        {
            List<ApuestaMerc> apuestasMerc = new List<ApuestaMerc>();

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT U.Email, M.TipoMercado, A.TipoApuesta, A.Cuota, A.DineroApostado from apuesta A, usuario U, mercado M WHERE A.Id_Mercado = M.Id AND A.Id_Usuario = U.idUsuario AND M.Id =" + idMercado;

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            while (res.Read())
            {
                apuestasMerc.Add(new ApuestaMerc(res.GetString(0), res.GetDouble(1), res.GetString(2), res.GetDouble(3), res.GetDouble(4)));
            }

            return apuestasMerc;

        }







        //-----------------------------------------------------------------------------------------------------  


        //Función para calcular la probabilidad Over.
        public double ProbabilidadOver(Apuesta apu)
        {
            double dineroUnder = 0, dineroOver = 0;

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT DineroApostadoOver, DineroApostadoUnder from Mercado WHERE Id = " + apu.Id_Mercado;

            con.Open();
            MySqlDataReader res = command.ExecuteReader();


            //Mientras que el campo 'TipoApuesta' sea over guarda el valor del campo 'DineroApostado' en una variable, si no en la otra.
            while (res.Read())
            {

                dineroOver = res.GetDouble(0);
                dineroUnder = res.GetDouble(1);

            }

            //Fórmula para generar la probabilidad OVER
            double probabilidadOver = dineroOver / (dineroOver + dineroUnder);

            return probabilidadOver;
        }



        //Función para calcular la probabilidad UNDER
        public double ProbabilidadUnder(Apuesta apu)
        {
            double dineroUnder = 0, dineroOver = 0;

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT DineroApostadoOver, DineroApostadoUnder from Mercado WHERE Id = " + apu.Id_Mercado;

            con.Open();
            MySqlDataReader res = command.ExecuteReader();


            //Mientras que el campo 'TipoApuesta' sea over guarda el valor del campo 'DineroApostado' en una variable, si no en la otra.
            while (res.Read())
            {
                dineroOver = res.GetDouble(0);
                dineroUnder = res.GetDouble(1);

            }

            //Fórmula para generar la probabilidad OVER
            double probabilidadUnder = dineroUnder / (dineroOver + dineroUnder);

            return probabilidadUnder;
        }




        //Función para calcular la cuota OVER
        public double CuotaOver(double probOver)
        {
            double cuotaOver = (1 / probOver) * 0.95;
            return Math.Truncate(cuotaOver * 100) / 100;    //Para coger 2 decimales
        }


        //Función para calcular la cuota UNDER
        public double CuotaUnder(double probUnder)
        {
            double cuotaUnder = (1 / probUnder) * 0.95;
            return Math.Truncate(cuotaUnder * 100) / 100;   //Para coger 2 decimales

        }



//__________________________________________________________________________________________________________________



        //-----Funcion para guardar en la BBDD apuestas nuevas-----
        internal void Save(Apuesta apu)
        {


            //Código para que no de error con las COMAS y los PUNTOS
            CultureInfo culInfo = new System.Globalization.CultureInfo("es-ES");

            culInfo.NumberFormat.NumberDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            culInfo.NumberFormat.PercentDecimalSeparator = ".";
            culInfo.NumberFormat.CurrencyDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = culInfo;


            //----------------------------------------------------------------------------------------------------------------


            //Insertar apuesta nueva

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "INSERT INTO Apuesta (TipoApuesta, Cuota, DineroApostado, Id_Mercado, Id_Usuario) VALUES ('" + apu.TipoApuesta + "' , '" + apu.Cuota + "' , '" + apu.DineroApostado +
                "' , '" + apu.Id_Mercado + "' , '" + apu.Id_Usuario + "'); ";

            Debug.WriteLine("hago el comando " + command.CommandText);

            //Recupera el dinero que haya en el mercado correspondiente
            //Sumarle a la parte que toque, el dinero que se ha apostado
            //Recalcular cuota over y cuota under
            //Hacer el update

            Debug.WriteLine("comando " + command.CommandText);

            try
            {
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexión " + e.Message);
            }



            //----------------------------------------------------------------------------------------------------------------



            //Tomar los datos del DINERO TOTAL OVER y UNDER de la BBDD
            MySqlCommand commandDineroTotalOverUnder = con.CreateCommand();
            commandDineroTotalOverUnder.CommandText = "SELECT DineroApostadoOver, DineroApostadoUnder FROM Mercado WHERE Id = " + apu.Id_Mercado;    //Dinero de ESE mercado

            double dineroTotalOver = 0, dineroTotalUnder = 0;

            con.Open();
            MySqlDataReader res = commandDineroTotalOverUnder.ExecuteReader();

            while (res.Read())
            {
                dineroTotalOver = res.GetDouble(0);             //Aqui me guardo el dinero total de over y under de sql
                dineroTotalUnder = res.GetDouble(1);

            }
            con.Close();




            //----------------------------------------------------------------------------------------------------------------


            //Aquí actualizamos el dinero total apostado a OVER y UNDER
            MySqlCommand commandDineroApostadoOver = con.CreateCommand();
            MySqlCommand commandDineroApostadoUnder = con.CreateCommand();

            //SUMAR dinero total MÁS el que acaba de apostar
            if (apu.TipoApuesta == "over")
            {
                dineroTotalOver += apu.DineroApostado;


                commandDineroApostadoOver.CommandText = "Update mercado Set DineroApostadoOver =" + dineroTotalOver + " where Id=" + apu.Id_Mercado;

                Debug.WriteLine("2222: comando " + commandDineroApostadoOver.CommandText);

                try
                {
                    con.Open();
                    commandDineroApostadoOver.ExecuteNonQuery();
                    con.Close();
                }
                catch (MySqlException e)
                {
                    Debug.WriteLine("Se ha producido un error de conexión " + e.Message);
                }


            }
            else
            {
                dineroTotalUnder += apu.DineroApostado;

                commandDineroApostadoUnder.CommandText = "Update mercado Set DineroApostadoUnder =" + dineroTotalUnder + " where Id=" + apu.Id_Mercado;

                Debug.WriteLine("comando " + commandDineroApostadoUnder.CommandText);

                try
                {
                    con.Open();
                    commandDineroApostadoUnder.ExecuteNonQuery();
                    con.Close();
                }
                catch (MySqlException e)
                {
                    Debug.WriteLine("Se ha producido un error de conexión " + e.Message);
                }

            }





            //----------------------------------------------------------------------------------------------------------------



            //Llamamos a las funciones creadas anteriormente y guardamos los valores en estas variables.
            double propabilidadOver = ProbabilidadOver(apu);
            double cuotaOver = CuotaOver(propabilidadOver);


            double probabilidadUnder = ProbabilidadUnder(apu);
            double cuotaUnder = CuotaUnder(probabilidadUnder);


            MySqlCommand commandCuotaOver = con.CreateCommand();    //Comando creado para modificar la cuota OVER posteriormente.
            commandCuotaOver.CommandText = "Update mercado Set InfoCuotaOver =" + cuotaOver + " where Id=" + apu.Id_Mercado;

            MySqlCommand commandCuotaUnder = con.CreateCommand();    //Comando creado para modificar la cuota OVER posteriormente.
            commandCuotaUnder.CommandText = "Update mercado Set InfoCuotaUnder =" + cuotaUnder + "where Id=" + apu.Id_Mercado;


            //Volvemos a hacer la conexión para modificar la cuota OVER.
            try
            {
                con.Open();
                commandCuotaOver.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexión");
            }


            //Volvemos a hacer la conexión para modificar la cuota UNDER.
            try
            {
                con.Open();
                commandCuotaUnder.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException e)
            {
                Debug.WriteLine("Se ha producido un error de conexión");
            }
        }

        //EXAMEN EJERCICIO 1 ==> Saca una lista ApuestaExamen donde se obtenga el Nombre de usuario, el mercado, la cuota y el dinero apostado

        
        internal List<ApuestaExamen> RetrieveEjercicio1()
        {

            List<ApuestaExamen> apuestasExamen = new List<ApuestaExamen>();

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT U.Nombre, M.Id, A.Cuota, A.DineroApostado from apuesta A, usuario U, mercado M WHERE A.Id_Mercado = M.Id AND A.Id_Usuario = U.idUsuario";

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            while (res.Read())
            {
                apuestasExamen.Add(new ApuestaExamen(res.GetString(0), res.GetInt32(1), res.GetDouble(2), res.GetDouble(3)));
            }

            return apuestasExamen;
        }



        //EXAMEN EJERCICIO 2 ==> Recupera las apuestas cuyas cuots estan entre un valor minimo y un valor maximo pasados por parámetros

        internal List<ApuestaValores> RetrieveEjercicio2(double valormin, double valormax)
        {

            List<ApuestaValores> apuestasValores = new List<ApuestaValores>();

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT Id_Apuesta, TipoApuesta, Cuota, DineroApostado FROM apuesta WHERE Cuota > @A AND Cuota < @B";
            command.Parameters.AddWithValue("@A", valormin);
            command.Parameters.AddWithValue("@B", valormax);

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            while (res.Read())
            {
                apuestasValores.Add(new ApuestaValores(res.GetInt32(0), res.GetString(1), res.GetDouble(2), res.GetDouble(3)));
            }

            return apuestasValores;
        }



        //EJERCICIO 1 EXAMEN RECUPERACION

        internal List<Apuesta> RetrieveRecuperacion(int idMercado)
        {
            List<Apuesta> apuestas = new List<Apuesta>();

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM apuesta WHERE Id_Mercado  =" + idMercado;

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            while (res.Read())
            {
                apuestas.Add(new Apuesta(res.GetInt32(0), res.GetString(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4), res.GetInt32(5)));
            }

            return apuestas;

        }








        //EJERCICIO 2 EXAMEN RECUPERACION

        internal List<Apuesta> RetrieveUsuCuota(int idUsuario, int cuota)
        {

            List<Apuesta> apuestas = new List<Apuesta>();

            MySqlConnection con = Connect();
            MySqlCommand command = con.CreateCommand();
            command.CommandText = "SELECT * FROM apuesta WHERE id_Usuario = " + idUsuario + " && Cuota > " + cuota ;

            con.Open();
            MySqlDataReader res = command.ExecuteReader();

            while (res.Read())
            {
                apuestas.Add(new Apuesta(res.GetInt32(0), res.GetString(1), res.GetDouble(2), res.GetDouble(3), res.GetDouble(4), res.GetInt32(5)));
            }

            return apuestas;





        }











    }
}