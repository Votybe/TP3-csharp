using System;
using System.Linq;

namespace TP3
{
    internal class Program
    {
        public static void Main(string[] args)
          {
            var collections = new MovieCollection().Movies;
            int choix;
            do
            {
                Console.WriteLine("Veuillez choisir la requette que vous voulez : ");
                Console.WriteLine("1. Compter tous les films");
                Console.WriteLine("2. Comptez tous les films avec la lettre e");
                Console.WriteLine("3. Comptez combien de fois la lettre f se trouve dans tous les titres de cette liste ");
                Console.WriteLine("4. Afficher le titre du film au budget le plus élevé");
                Console.WriteLine("5. Affichez le titre du film dont le box-office est le plus bas");
                Console.WriteLine("6. Classer les films par ordre alphabétique inversé et imprimer les 11 premiers de la liste");
                Console.WriteLine("7. Count all the movies made before 1980");
                Console.WriteLine("8. Affichez la durée moyenne des films dont la première lettre est une voyelle");
                Console.WriteLine("9. Imprimer tous les films dont le titre contient la lettre H ou W, mais pas la lettre I ou T");
                Console.WriteLine("10. Calculez la moyenne de tous les budgets / box office de tous les films de tous les temps");
                Console.WriteLine("0. Quitter le programme !");
                Console.Write("> Votre choix : ");
                
                choix = AskUserForParameter();
                
                switch (choix)
                {
                    case 1:
                        // compter le nombre de films 
                        Console.WriteLine($"Le nombre de films est de : {collections.Count} "   );
                        break;
                    
                    case 2:
                        // compter le nombre de films avec la lettre e
                        Console.WriteLine($"le nombre de films avec la lettre e  {collections.Count(e => e.Title.Contains('e'))} ");
                        break;
                    
                    case 3:
                        
                        int cpt = 0;
                        foreach (var collection in collections) {
                            cpt += collection.Title.Count(f => f  == 'f');
                        }
                        Console.WriteLine($"Le nombre de films est  : {cpt}"); 
                        break;
                    
                    case 4:
                        // chercher le film qui a le plus gros budget 
                        var queryFilmBudget =
                            from collection in collections
                            orderby collection.Budget descending
                            select collection.Title; 
                        Console.WriteLine($"Le film avec le plus gros budget est :  {queryFilmBudget.First()}");
                        break;
         
                    case 5:
                        // chercher le film avec le plus petit box office 
                        var queryFilmBoxOffice =
                            from collection in collections
                            orderby collection.BoxOffice ascending
                            select collection.Title; 
                        Console.WriteLine($"Le film avec le plus petit box office {queryFilmBoxOffice.First()}");
                        break;
                    
                    case 6:
                        // chercher les films par ordre décroissant 
                        var queryOrderByTitle =
                            (from collection
                                    in collections
                                orderby collection.Title
                                    descending
                                select collection
                            ).Take(11);

                        foreach (var collection in queryOrderByTitle)
                        {
                            Console.WriteLine(collection.Title);
                        }
                        break;

                    
                    case 7:
                        // afficher tous les films fait avant 1980 
                        var querySelectAllFilmsBefore1980 =
                            from collection in collections
                            where (collection.ReleaseDate.Year < 1980)
                            select collection.Title; 
                        Console.WriteLine($"Le nombre de films est de : {querySelectAllFilmsBefore1980.Count()} " );
                        break;
                    
                    case 8:
                        var querySelectAverage = (from collection in collections
                            where "aeiou".IndexOf(collection.Title.ToLower()[0]) >= 0
                            select collection.RunningTime).Average();
                            double  test =  Math.Round(querySelectAverage);   
                         Console.WriteLine($"La moyenne (arrondie) : {test}");
                        break;
                    
                    case 9:
                         foreach (var wDMovies in 
                            from collection in collections 
                            where (collection.Title.ToUpper().Contains('H') || collection.Title.ToUpper().Contains('W')) && !(collection.Title.ToUpper().Contains('I') || collection.Title.ToUpper().Contains('T')) 
                            select collection) {
                            Console.WriteLine(wDMovies.Title);
                        }
                        break;
                    case 10:
                        Console.WriteLine($"Budget Moyen par box office Moyen : {(from collection in collections select collection.Budget).Average()/(from collection in collections select collection.BoxOffice).Average()}");
                        break;
                    
                    default:
                        Console.WriteLine("Choix invalide !");
                        break;
                }

            } while (choix != 0);
        }
        
        private static int AskUserForParameter()
        {
            int.TryParse(Console.ReadLine(), out var result);
            return result;
        }
    }
}