using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mhotivo.Implement
{
    public class Utilities
    {
        public static string GenderToString(bool masculino)
        {
            return masculino ? "Masculino" : "Femenino";
        }

        public static bool IsMasculino(string sex)
        {
            return sex.Equals("Hombre");

        }
    }
}
