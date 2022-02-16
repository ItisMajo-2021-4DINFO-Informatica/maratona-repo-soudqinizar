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
    }
}
