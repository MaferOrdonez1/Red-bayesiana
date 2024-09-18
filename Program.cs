using System;
using System.Collections.Generic;

class RedBayesiana
{
    // Estructura de la red bayesiana
    private Dictionary<string, double> probabilidadesGenero;
    private Dictionary<string, double> probabilidadesDuracion;
    private Dictionary<(string, string), Dictionary<string, double>> probabilidadesRecomendacion;

    public RedBayesiana()
    {
        // Definir probabilidades para géneros
        probabilidadesGenero = new Dictionary<string, double>
        {
            { "accion", 0.3 },
            { "drama", 0.4 },
            { "comedia", 0.3 }
        };

        // Definir probabilidades para duración
        probabilidadesDuracion = new Dictionary<string, double>
        {
            { "largas", 0.6 },
            { "cortas", 0.4 }
        };

        // Definir probabilidades condicionales para las recomendaciones
        probabilidadesRecomendacion = new Dictionary<(string, string), Dictionary<string, double>>
        {
            { ("accion", "largas"), new Dictionary<string, double> { { "El señor de los anillos", 0.7 }, { "Black Panther", 0.3 } } },
            { ("accion", "cortas"), new Dictionary<string, double> { { "Black Panther", 0.8 }, { "El señor de los anillos", 0.2 } } },
            { ("drama", "largas"), new Dictionary<string, double> { { "Hachiko", 0.9 }, { "Her", 0.1 } } },
            { ("drama", "cortas"), new Dictionary<string, double> { { "Her", 0.8 }, { "Hachiko", 0.2 } } },
            { ("comedia", "largas"), new Dictionary<string, double> { { "Click", 0.6 }, { "Shrek 2", 0.4 } } },
            { ("comedia", "cortas"), new Dictionary<string, double> { { "Shrek 2", 0.7 }, { "Click", 0.3 } } }
        };
    }

    // Función para hacer inferencias
    public string ObtenerRecomendacion(string genero, string duracion)
    {
        if (probabilidadesRecomendacion.ContainsKey((genero, duracion)))
        {
            var recomendaciones = probabilidadesRecomendacion[(genero, duracion)];
            double maxProbabilidad = 0;
            string mejorRecomendacion = "";

            // Buscar la recomendación con mayor probabilidad
            foreach (var recomendacion in recomendaciones)
            {
                if (recomendacion.Value > maxProbabilidad)
                {
                    maxProbabilidad = recomendacion.Value;
                    mejorRecomendacion = recomendacion.Key;
                }
            }

            return mejorRecomendacion;
        }

        return "No hay recomendaciones disponibles.";
    }
}

class Program
{
    static void Main(string[] args)
    {
        RedBayesiana redBayesiana = new RedBayesiana();

        while (true)
        {
            // Obtener preferencias del usuario
            Console.Write("¿Qué género te gusta? (acción, drama, comedia): ");
            string genero = Console.ReadLine().Trim().ToLower();

            Console.Write("¿Prefieres películas largas o cortas?: ");
            string duracion = Console.ReadLine().Trim().ToLower();

            // Obtener la recomendación
            string recomendacion = redBayesiana.ObtenerRecomendacion(genero, duracion);
            Console.WriteLine("Recomendación: " + recomendacion);

            // Preguntar si el usuario quiere continuar
            Console.Write("¿Quieres hacer otra consulta? (sí/no): ");
            string continuar = Console.ReadLine().Trim().ToLower();

            if (continuar != "sí")
            {
                Console.WriteLine("¡Feliz día!");
                break;
            }
        }
    }
}
