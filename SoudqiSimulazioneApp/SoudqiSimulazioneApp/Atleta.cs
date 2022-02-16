using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoudqiSimulazioneApp
{
    class Atleta
    {
        public string nomeArtista { get; set; }
        public string società { get; set; }
        public int tempo { get; set; }
        public string CittàAppartenenza { get; set; }

        public string ConcatenaValori()
        {
            return nomeArtista + "%" + società + "%" + tempo + "%" + CittàAppartenenza;
        }
    }
}
