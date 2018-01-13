using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace encuesta
{
    public class InitialScript
    {
        public InitialScript(Database database)
        {
            database.CreateTable<Answer>();
            database.CreateTable<Customer>();
            database.CreateTable<CustomerAnswer>();
            database.CreateTable<Moment>();
            database.CreateTable<Question>();
            database.CreateTable<QuestionOption>();
            database.CreateTable<Survey>();
            database.CreateTable<SurveyQuestion>();
            database.CreateTable<User>();

            // Insert username.
            if (database.Query<User>("SELECT * FROM User").FirstOrDefault() == null)
            {
                database.SaveItem(new User("1", "1"));
            }

            // Insert test customers.
            if (database.Query<Customer>("SELECT * FROM Customer").FirstOrDefault() == null)
            {
                database.SaveItem(new Customer("Almacén", "Viñedo 123"));
                database.SaveItem(new Customer("Supermercado", "Arboleda 555"));
                database.SaveItem(new Customer("Restaurant", "Mendoza 1001"));
            }

            // Moment.
            if (database.Query<Customer>("SELECT * FROM 'Moment' ").FirstOrDefault() == null)
            {
                database.SaveItem(new Moment("MAT", "Matinal Diaria"));
                database.SaveItem(new Moment("AIC", "Antes de ingresar al cliente"));
                database.SaveItem(new Moment("PDV", "Revisión Categorización del PDV"));
                database.SaveItem(new Moment("STK", "Revisión stock en depósito"));
                database.SaveItem(new Moment("GLD", "Negociación Plan Gold"));
                database.SaveItem(new Moment("SLD", "Saludo"));
                database.SaveItem(new Moment("RUP", "Repasar último pedido"));
                database.SaveItem(new Moment("CAT", "Categorías"));
                database.SaveItem(new Moment("OCA", "Ofertas, combos, acciones"));
                database.SaveItem(new Moment("OPO", "Oportunidades"));
                database.SaveItem(new Moment("LAN", "Lanzamientos / Incorporaciones"));
                database.SaveItem(new Moment("ACT", "Actualizaciones de precios"));
                database.SaveItem(new Moment("CDV", "Cierre de ventas"));
                database.SaveItem(new Moment("OBJ", "Registro y evaluación del día. Objetivos propuestos Vs Resultados. Ver focos para próximas visitas. (Semanal)"));
                database.SaveItem(new Moment("VOL", "Revisar avance propio de volumen (Diario)"));
            }

            // Insert questions.
            // *********
            // Category: MAT.
            if (database.Query<Customer>("SELECT * FROM Question").FirstOrDefault() == null)
            {
                database.SaveItem(new Question("Revisar avance propio de volúmenes y objetivos de Trade", "MAT", 25));
                database.SaveItem(new Question("Reporte de porcentaje de rechazos por cajas y por clientes. Reporte de entregas logística.", "MAT", 19));
                database.SaveItem(new Question("Incorporaciones de productos y discontinuos (cuando es necesario)", "MAT", 19));
                database.SaveItem(new Question("Revisar listas de precios:  Acciones del mes, combos, etc. (Cuando es necesario si hay cambios)", "MAT", 19));
                database.SaveItem(new Question("Identificar inconvenientes pendientes en la ruta (cambio de mercadería, NC pendiente, etc)", "MAT", 19));
                database.SaveItem(new Question("Saber la ruta del día + Ranking de Clientes (Vol o Fact)", "MAT", 25));
            }

            // Surveys.
            if (database.Query<Survey>("SELECT * FROM Survey").FirstOrDefault() == null)
            {
                // Create first survey.
                database.SaveItem(new Survey("Encuesta general"));

                Survey survey = database.Query<Survey>("SELECT * FROM Survey WHERE Name='Encuesta general'").FirstOrDefault();
                List<Question> questions = database.Query<Question>("SELECT * FROM Question ORDER BY Moment, ID").ToList();
                int i = 0;

                foreach (var q in questions)
                {
                    database.SaveItem(new SurveyQuestion(survey.ID, q.ID, i++));
                }

            }



        }
    }
}