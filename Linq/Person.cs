using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{/// <summary>
 /// Klasse Person
 /// </summary>
    class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}, {Age}, {City}, {Gender}";
        }
    }
}
