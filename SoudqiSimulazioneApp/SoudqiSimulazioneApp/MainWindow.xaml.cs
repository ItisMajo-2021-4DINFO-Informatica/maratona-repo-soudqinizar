using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace SoudqiSimulazioneApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Maratona dati;
        public MainWindow()
        {
            InitializeComponent();

            dati = new Maratona();
            DgGiornale.ItemsSource = dati.Collezione;
        }

        
        private void BtnLeggiFile_Click(object sender, RoutedEventArgs e)
        {

            using (FileStream flusso = new FileStream("dati.txt", FileMode.Open, FileAccess.Read))
            {
                StreamReader reader = new StreamReader(flusso);

                while (!reader.EndOfStream)
                {
                    string linea = reader.ReadLine();
                    string[] elementi = linea.Split('%');

                    var unDocumento = new Atleta();
                    unDocumento.nomeArtista = elementi[0];
                    unDocumento.società = elementi[1];
                    unDocumento.tempo = dati.CalcolaTempo(elementi[2]);
                    unDocumento.CittàAppartenenza = elementi[3];

                    dati.AggiungiDati(unDocumento);
                }
            }


            DgGiornale.Items.Refresh();
        }

        private void TxtAtleta_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtAtleta.Text = "";
        }

        private void TxtCittà_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtCittà.Text = "";
        }

        private void BtnTempoImpiegato_Click(object sender, RoutedEventArgs e)
        {
            string atleta = TxtAtleta.Text;
            string città = TxtCittà.Text;
            int minuti = 0;

            foreach (var lista in dati.Collezione)
            {
                if(atleta == lista.nomeArtista && città == lista.CittàAppartenenza)
                {
                    minuti = lista.tempo;
                    LblTempoImpiegato.Content = "Il tempo impiegato è: " + minuti + " minuti";
                }
            }
        }

        

        private void BtnPartecipanti_Click(object sender, RoutedEventArgs e)
        {
            string città = TxtCittà.Text;

            string partecipanti = string.Empty;

            foreach (var lista in dati.Collezione)
            {
                if(città == lista.CittàAppartenenza)
                {
                    partecipanti += lista.nomeArtista + " - ";
                    LblPartecipanti.Content = "I partecipanti di questa città sono: " + partecipanti;
                }
                
            }

        }

        private void TxtNomeArtista2_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtNomeArtista2.Text = "";
        }

        private void TxtSocietà2_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtSocietà2.Text = "";
        }

        private void TxtTempo2_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtTempo2.Text = "";
        }

        private void TxtCittà2_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtCittà2.Text = "";
        }

        private void BtnAggiungi_Click(object sender, RoutedEventArgs e)
        {
            string nomeArtista = TxtNomeArtista2.Text;
            string società = TxtSocietà2.Text;
            int tempo = int.Parse(TxtTempo2.Text);
            string città = TxtCittà2.Text;

            var documento = new Atleta();
            documento.nomeArtista = nomeArtista;
            documento.società = società;
            documento.tempo = tempo;
            documento.CittàAppartenenza = città;

            dati.AggiungiDati(documento);

            DgGiornale.Items.Refresh();

            dati.salva();
        }

        private void TxtAtleta3_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtAtleta3.Text = "";
        }

        private void TxtCittà3_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtCittà3.Text = "";
        }

        private void BtnStampa_Click(object sender, RoutedEventArgs e)
        {
            // da riguardare
            string atleta = TxtAtleta3.Text;
            string città = TxtCittà3.Text;

            string durataMinore = "";

            foreach(var lista in dati.Collezione)
            {
                 if (atleta == lista.nomeArtista && città == lista.CittàAppartenenza)
                 {
                    durataMinore = Convert.ToString(lista.tempo);
                 }  
            }

            string elenco = "";

            foreach (var lista in dati.Collezione)
            {
                if (dati.CalcolaTempo(Convert.ToString(lista.tempo)) < dati.CalcolaTempo(durataMinore) && lista.CittàAppartenenza == città)
                {
                    elenco += lista.nomeArtista + "\n";
                }
            }


            string nomeFile = atleta + "%" + durataMinore + ".txt";

            using (FileStream flusso = new FileStream(nomeFile, FileMode.Create, FileAccess.Write))
            {
                StreamWriter scrittore = new StreamWriter(flusso);

                scrittore.WriteLine(elenco);
                scrittore.Flush();
            }

        }
    }
}
