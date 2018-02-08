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
                database.InsertItemWithID(new User(1, "1", "1"));
            }

            // Insert test customers.
            if (database.Query<Customer>("SELECT * FROM Customer").FirstOrDefault() == null)
            {
                database.InsertItemWithID(new Customer(1, "Almacén", "Viñedo 123"));
                database.InsertItemWithID(new Customer(2, "Supermercado", "Arboleda 555"));
                database.InsertItemWithID(new Customer(3, "Restaurant", "Mendoza 1001"));
            }

            // Moment.
            if (database.Query<Customer>("SELECT * FROM 'Moment' ").FirstOrDefault() == null)
            {
                database.InsertItemWithID(new Moment(1, "MAT", "Matinal Diaria"));
                database.InsertItemWithID(new Moment(2, "AIC", "Antes de ingresar al cliente"));
                database.InsertItemWithID(new Moment(3, "PDV", "Revisión Categorización del PDV"));
                database.InsertItemWithID(new Moment(4, "STK", "Revisión stock en depósito"));
                database.InsertItemWithID(new Moment(5, "GLD", "Negociación Plan Gold"));
                database.InsertItemWithID(new Moment(6, "SLD", "Saludo"));
                database.InsertItemWithID(new Moment(7, "RUP", "Repasar último pedido"));
                database.InsertItemWithID(new Moment(8, "CAT", "Categorías"));
                database.InsertItemWithID(new Moment(9, "OCA", "Ofertas, combos, acciones"));
                database.InsertItemWithID(new Moment(10, "OPO", "Oportunidades"));
                database.InsertItemWithID(new Moment(11, "LAN", "Lanzamientos / Incorporaciones"));
                database.InsertItemWithID(new Moment(12, "ACT", "Actualizaciones de precios"));
                database.InsertItemWithID(new Moment(13, "CDV", "Cierre de ventas"));
                database.InsertItemWithID(new Moment(14, "OBJ", "Registro y evaluación del día. Objetivos propuestos Vs Resultados. Ver focos para próximas visitas. (Semanal)"));
                database.InsertItemWithID(new Moment(15, "VOL", "Revisar avance propio de volumen (Diario)"));
            }

            // Insert questions.
            // *********
            // Category: MAT.
            if (database.Query<Customer>("SELECT * FROM Question").FirstOrDefault() == null)
            {
                database.InsertItemWithID(new Question(1, "Revisar avance propio de volúmenes y objetivos de Trade", "MAT", 25, "Volúmenes / Trade"));
                database.InsertItemWithID(new Question(2, "Reporte de porcentaje de rechazos por cajas y por clientes. Reporte de entregas logística.", "MAT", 19, "Rechazos / Entregas"));
                database.InsertItemWithID(new Question(3, "Incorporaciones de productos y discontinuos (cuando es necesario)", "MAT", 19, "Productos y discontinuos"));
                database.InsertItemWithID(new Question(4, "Revisar listas de precios:  Acciones del mes, combos, etc. (Cuando es necesario si hay cambios)", "MAT", 19, "Listas de precios"));
                database.InsertItemWithID(new Question(5, "Identificar inconvenientes pendientes en la ruta (cambio de mercadería, NC pendiente, etc)", "MAT", 19, "Inconvenientes en la ruta"));
                database.InsertItemWithID(new Question(6, "Saber la ruta del día + Ranking de Clientes (Vol o Fact)", "MAT", 25, "Ruta del día / Ranking clientes"));
            }

            // Surveys.
            if (database.Query<Survey>("SELECT * FROM Survey").FirstOrDefault() == null)
            {
                // Create first survey.
                database.InsertItemWithID(new Survey(1, "Encuesta general"));

                Survey survey = database.Query<Survey>("SELECT * FROM Survey WHERE Name='Encuesta general'").FirstOrDefault();
                List<Question> questions = database.Query<Question>("SELECT * FROM Question ORDER BY Moment, ID").ToList();
                int i = 0;

                foreach (var q in questions)
                {
                    i++;
                    database.InsertItemWithID(new SurveyQuestion(i, survey.ID, q.ID, i));
                }

            }



        }
    }
}