using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonJSON
{
    class PokemonAPI
    {
        public List<AllResults> results { get; set; }
    }

    public class AllResults
    {
        public string name { get; set; }
        public string url { get; set; }

        public override string ToString()
        {
            return CapitalizeFirstLetterOfName();
        }
        public string CapitalizeFirstLetterOfName()
        {
            return name[0].ToString().ToUpper() + name.Substring(1, name.Length - 1);
        }
    }

    public class PokeInfo
    {
        public int weight { get; set; }
        public int height { get; set; }
        public Sprites sprites { get; set; }
    }

    public class Sprites
    {
        public string front_default { get; set; }
        public string back_default { get; set; }
    }
}
