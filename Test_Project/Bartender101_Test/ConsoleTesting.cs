using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AssetClient
{
    using Models;
    class ConsoleTesting
    {
        static void Main(string[] args)
        {
            Connector snapshot = new Connector();
            List<Liqueur> liqueur = snapshot.Liqueur();
            List<Cocktail> cocktail = snapshot.Cocktail();
            List<Liquor> liquor = snapshot.Liquor();
            List<Mocktail> mocktails = snapshot.Mocktail();

            List<Cocktail> abs = new List<Cocktail>();
            foreach (Cocktail item in cocktail)
            {
                if (item.pk == 5)
                {
                    abs.Add(item);
                }
            } 

            Liquor whiskey = new Liquor();
            foreach (Liquor item in liquor)
            {
                if (item.name == "Whiskey")
                {
                    whiskey = item;
                }
            }

            //foreach (Liqueur item in liqueur)
            //{
            //    print(item.name);
            //    print(item.url);
            //    print(item.liquor_base.name);
            //    Console.WriteLine(item.abv);
            //    item.special = "None";
            //    item.save();
            ////}

            Liqueur soco = new Liqueur();
            soco.name = "Southern Comfort";
            soco.abv = 35.0f;
            soco.liquor_base = whiskey;
            soco.cocktails = abs;
            soco.origin = "USA";
            soco.flavour = "Dry Peach";
            print(soco._liquor_base);
            var json = JsonConvert.SerializeObject(soco);
            Console.WriteLine(json);
            soco.save();

            //PUT Test
            //foreach (Mocktail item in mocktails)
            //{
            //    print(item.name);
            //    print(item.url);
            //    print(item.garnish);
            //    item.garnish = "Lime";
            //    item.save();
            //    print(item.garnish);
            //}

            ////POST Test
            //Mocktail mock = new Mocktail()
            //{
            //    name = "Virgin Peach Bellini",
            //    garnish = "Apple",
            //    glassware = "Flute",
            //    ice = false
            //};
            //mock.save();

            Console.ReadLine();
        }

        static void print(string word)
        {
            Console.WriteLine(word);
        }
        static void print(int word)
        {
            Console.WriteLine(word);
        }
        static void print(bool word)
        {
            Console.WriteLine(word);
        }
    }
}
