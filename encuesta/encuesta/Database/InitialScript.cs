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
            database.CreateTable<User>(); // Creates (if does not exist) a table for Users.
            database.CreateTable<Customer>(); // Creates (if does not exist) a table for Customers.
            database.CreateTable<When>(); // Creates (if does not exist) a table for When.
            database.CreateTable<Question>(); // Creates (if does not exist) a table for Questions.

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

            // When.
            if (database.Query<Customer>("SELECT * FROM 'When' ").FirstOrDefault() == null)
            {
                database.SaveItem(new When("MAT", "Matinal Diaria"));
                database.SaveItem(new When("AIC", "Antes de ingresar al cliente"));
                database.SaveItem(new When("PDV", "Revisión Categorización del PDV"));
                database.SaveItem(new When("STK", "Revisión stock en depósito"));
                database.SaveItem(new When("GLD", "Negociación Plan Gold"));
                database.SaveItem(new When("SLD", "Saludo"));
                database.SaveItem(new When("RUP", "Repasar último pedido"));
                database.SaveItem(new When("CAT", "Categorías"));
                database.SaveItem(new When("OCA", "Ofertas, combos, acciones"));
                database.SaveItem(new When("OPO", "Oportunidades"));
                database.SaveItem(new When("LAN", "Lanzamientos / Incorporaciones"));
                database.SaveItem(new When("ACT", "Actualizaciones de precios"));
                database.SaveItem(new When("CDV", "Cierre de ventas"));
                database.SaveItem(new When("OBJ", "Registro y evaluación del día. Objetivos propuestos Vs Resultados. Ver focos para próximas visitas. (Semanal)"));
                database.SaveItem(new When("VOL", "Revisar avance propio de volumen (Diario)"));
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

        }
    }
}