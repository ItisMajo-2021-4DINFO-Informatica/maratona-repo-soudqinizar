using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SoudqiSimulazioneApp
{
    class Maratona
    {
        public List<Atleta> Collezione { get; set; }

        public Maratona()
        {
            Collezione = new List<Atleta>();
        }

        public void AggiungiDati(Atleta dato)
        {
            Collezione.Add(dato);
        }

        public int CalcolaTempo(string tempo)
        {
            int minuti = 0;

            int ore = int.Parse(tempo.Substring(0, 2));
            int minutiParziali = int.Parse(tempo.Substring(3, 2));

            minuti = (ore * 60) + minutiParziali;

            return minuti;
        }

        public void salva()
        {
            using (FileStream flusso = new FileStream("dati.txt", FileMode.Open, FileAccess.Write))
            {
                StreamWriter scrittore = new StreamWriter(flusso);
                foreach (var documento2 in Collezione)
                {
                    scrittore.WriteLine(documento2.ConcatenaValori());
                }
                scrittore.Flush();
            }
        }



    }
}
