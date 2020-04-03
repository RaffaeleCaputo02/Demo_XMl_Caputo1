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
        int ctr = 0;

        public MainWindow()
        {
            InitializeComponent();
            txt_Nome.Text = "";
            txt_Cognome.Text = "";
            txt_Squadra.Text = "";
            txt_Numero.Text = "";
        }

        private void btn_Aggiorna_Click(object sender, RoutedEventArgs e)
        {
            ct = new CancellationTokenSource();
            btn_Aggiorna.IsEnabled = false;
            btn_Stop.IsEnabled = true;
            lst_Calciatori.Items.Clear();
            Task.Factory.StartNew(() => CaricaDati());
        }

        private void CaricaDati()
        {
            string path = @"Calcio.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmlcalciatori = xmlDoc.Element("calciatori");
            var xmlcalciatore = xmlcalciatori.Elements("calciatore");
            Thread.Sleep(1000);

            foreach (var item in xmlcalciatore)
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
                if (ct.Token.IsCancellationRequested)
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_Modifica_Click(object sender, RoutedEventArgs e)
        {
            lbl_Nome.IsEnabled = true;
            lbl_Cognome.IsEnabled = true;
            lbl_Numero.IsEnabled = true;
            lbl_Squadra.IsEnabled = true;
            txt_Nome.IsEnabled = true;
            txt_Cognome.IsEnabled = true;
            txt_Numero.IsEnabled = true;
            txt_Squadra.IsEnabled = true;
            btn_Salva.IsEnabled = true;

            ctr = 0;

            string path = @"Calcio.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmlcalciatori = xmlDoc.Element("calciatori");
            var xmlcalciatore = xmlcalciatori.Elements("calciatore");
            foreach (var item in xmlcalciatore)
            {
                XElement xmlFirstName = item.Element("nome");
                XElement xmlLastName = item.Element("cognome");
                XElement xmlSquadra = item.Element("squadra");
                XElement xmlNumero = item.Element("numero");
                Calciatore c = new Calciatore();
                c.Nome = xmlFirstName.Value;
                c.Cognome = xmlLastName.Value;
                c.Squadra = xmlSquadra.Value;
                c.NumeroMaglia = Convert.ToInt32(xmlNumero.Value);

                if (Convert.ToString(lst_Calciatori.SelectedItem) == c.Nome)
                {
                    txt_Nome.Text = c.Nome;
                    txt_Cognome.Text = c.Cognome;
                    txt_Squadra.Text = c.Squadra;
                    txt_Numero.Text = c.NumeroMaglia.ToString();
                    break;
                }
                ctr++;
            }


        }

        private void btn_Salva_Click(object sender, RoutedEventArgs e)
        {
            int ctr2 = 0;
            string path = @"Calcio.xml";
            XDocument xmlDoc = XDocument.Load(path);
            XElement xmlcalciatori = xmlDoc.Element("calciatori");
            var xmlcalciatore = xmlcalciatori.Elements("calciatore");
            foreach (var item in xmlcalciatore)
            {
                XElement xmlFirstName = item.Element("nome");
                XElement xmlLastName = item.Element("cognome");
                XElement xmlSquadra = item.Element("squadra");
                XElement xmlNumero = item.Element("numero");
                if (ctr == ctr2)
                {
                    item.SetElementValue("nome", txt_Nome.Text);
                    item.SetElementValue("cognome", txt_Cognome.Text);
                    item.SetElementValue("squadra", txt_Squadra.Text);
                    item.SetElementValue("numero", txt_Numero.Text);
                    break;
                }
                ctr2++;
            }
            xmlDoc.Save("Calcio.xml");
        }
    }
}
