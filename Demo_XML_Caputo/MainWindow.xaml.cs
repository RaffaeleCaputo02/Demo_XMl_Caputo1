using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml.Linq;

namespace Demo_XML_Caputo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource ct;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Aggiorna_Click(object sender, RoutedEventArgs e)
        {
            ct = new CancellationTokenSource();
            btn_Aggiorna.IsEnabled = false;
            btn_Stop.IsEnabled = true;
            lst_Calciatori.Items.Clear();
            Task.Factory.StartNew(()=>CaricaDati());
        }

        private void CaricaDati()
        { 
            string path = @"Calcio.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmlcalciatori = xmlDoc.Element("calciatori");
            var xmlatleta = xmlcalciatori.Elements("calciatore");
            Thread.Sleep(1000);

            foreach (var item in xmlatleta)
            {
                XElement xmlFirstName = item.Element("nome");
                XElement xmlLastName = item.Element("cognome");
                XElement xmlSquadra = item.Element("squadra");
                XElement xmlNumero = item.Element("numero");
                Calciatore c = new Calciatore();
                {

                    c.Nome = xmlFirstName.Value;
                    c.Cognome = xmlLastName.Value;
                    c.Squadra = xmlSquadra.Value;
                    c.NumeroMaglia = Convert.ToInt32(xmlNumero.Value);

                }
                
                Dispatcher.Invoke(() => lst_Calciatori.Items.Add(c));
                if(ct.Token.IsCancellationRequested)
                {
                    break;
                }
                Thread.Sleep(1000);
                
            }
            Dispatcher.Invoke(() =>
            {
                btn_Aggiorna.IsEnabled = true;
                btn_Stop.IsEnabled = false;
                ct = null;
            });





        }

        private void btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            ct.Cancel();
        }
    }
}
