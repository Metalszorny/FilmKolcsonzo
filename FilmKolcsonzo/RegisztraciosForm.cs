using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace FilmKolcsonzo
{
    /// <summary>
    /// Interaction logic for RegisztraciosForm.
    /// </summary>
    public partial class RegisztraciosForm : Form
    {
        #region Fields

        // XML adatok
        private int kulcs = 0;

        // 
        private string eleresiut = "felhasznalok.xml";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisztraciosForm"/> class.
        /// </summary>
        public RegisztraciosForm()
        {
            InitializeComponent();
        }
		
		/// <summary>
        /// Destroys the instance of the <see cref="RegisztraciosForm"/> class.
        /// </summary>
        ~RegisztraciosForm()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Handles the Load event of the RegisztraciosForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RegisztraciosForm_Load(object sender, EventArgs e)
        {
            // Preset values
            Torol();

            #region SzuletesiDatum

            for (int i = 1901; i < 2014; i++)
            {
                EvComboBox.Items.Add(i);
            }

            for (int i = 1; i < 13; i++)
            {
                HonapComboBox.Items.Add(i);
            }

            if (((string)HonapComboBox.Text == "1") || ((string)HonapComboBox.Text == "3") || ((string)HonapComboBox.Text == "5") || ((string)HonapComboBox.Text == "7") || ((string)HonapComboBox.Text == "8") || ((string)HonapComboBox.Text == "10") || ((string)HonapComboBox.Text == "12"))
            {
                for (int i = 1; i < 32; i++)
                {
                    NapComboBox.Items.Add(i);
                }
            }
            else if (((string)HonapComboBox.Text == "4") || ((string)HonapComboBox.Text == "6") || ((string)HonapComboBox.Text == "9") || ((string)HonapComboBox.Text == "11"))
            {
                for (int i = 1; i < 31; i++)
                {
                    NapComboBox.Items.Add(i);
                }
            }
            else if (((string)HonapComboBox.Text == "2") && ((int.Parse(EvComboBox.Text) - 1904) % 4 == 0))
            {
                for (int i = 1; i < 30; i++)
                {
                    NapComboBox.Items.Add(i);
                }
            }
            else
            {
                for (int i = 1; i < 29; i++)
                {
                    NapComboBox.Items.Add(i);
                }
            }

            #endregion SzuletesiDatum

            // Manage the files.
            if (File.Exists(eleresiut))
            {
                XDocument doc = XDocument.Load(eleresiut);
                var felhasznalok = doc.Descendants("felhasznalo");

                if (felhasznalok.Count() != 0)
                {
                    kulcs = doc.Descendants("felhasznalo").Max(x => (int)x.Attribute("id")) + 1;
                }
            }
            else
            {
                XElement felhasznalok = new XElement("felhasznalok");
                XDocument doc = new XDocument(felhasznalok);
                doc.Save(eleresiut);
            }
        }

        /// <summary>
        /// Sets the textbox and combobox values to empty.
        /// </summary>
        private void Torol()
        {
            NevTextBox.Text = "";
            NevTextBox.Show();
            JelszoTextBox.Text = "";
            JelszoTextBox.Show();
            EmailTextBox.Text = "";
            EmailTextBox.Show();
            BankTextBox.Text = "";
            BankTextBox.Show();
            EvComboBox.SelectedItem = "";
            EvComboBox.Show();
            HonapComboBox.SelectedItem = "";
            HonapComboBox.Show();
            NapComboBox.SelectedItem = "";
            NapComboBox.Show();
        }

        /// <summary>
        /// Handles the Click event of the RegisztraloButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RegisztraloButton_Click(object sender, EventArgs e)
        {
            // Registration
            string hibaoka = "";

            // The texts must not be empty.
            if ((NevTextBox.Text != "") && (JelszoTextBox.Text != "") && (EmailTextBox.Text != "") && (BankTextBox.Text != ""))
            {
                int BankSzamEllenorzo = int.Parse(BankTextBox.Text);

                // Checking of some fileds.
                bool JelszoEllenorzo = ((JelszoTextBox.Text.Length > 8 && int.Parse(JelszoTextBox.Text) > 0) ? true : false);
                bool NevEllenorzo = (NevTextBox.Text.Split(' ').Count() > 1 ? true : false);
                bool EmailEllenorzo = ((EmailTextBox.Text.Split('@').Count() > 1 && EmailTextBox.Text.Split('@')[1].Split('.').Count() > 1) ? true : false);

                // Egyedi kulcsok ellenőrzése
                XDocument kereses = XDocument.Load(eleresiut);
                var felhasznalok = kereses.Descendants("felhasznalo");
                var gyoztes = from x in felhasznalok
                              where (string)x.Element("email") == EmailTextBox.Text || (string)x.Element("jelszo") == JelszoTextBox.Text
                              select new { Név = x.Element("nev").Value };

                // Settings for the financial fields.
                if ((BankSzamEllenorzo > 0) || (gyoztes.Count() > 0) || (JelszoEllenorzo == true) || (NevEllenorzo == true) || (EmailEllenorzo == true))
                {
                    XDocument mentes = XDocument.Load(eleresiut);
                    XElement nev = new XElement("nev", NevTextBox.Text);
                    XElement jelszo = new XElement("jelszo", JelszoTextBox.Text);
                    XElement email = new XElement("email", EmailTextBox.Text);
                    XElement bankszam = new XElement("bankszam", BankTextBox.Text);
                    XElement szuletes = new XElement("szuletes", EvComboBox.Text.ToString()+"."+HonapComboBox.Text.ToString()+"."+NapComboBox.Text.ToString());
                    string mod;
                    bool befi;
                    int kolt;

                    if (HaviRadioButton.Checked == true)
                    {
                        mod = "havi";
                        befi = false;
                        kolt = 5000;
                    }
                    else
                    {
                        mod = "darab";
                        befi = true;
                        kolt = 0;
                    }

                    XElement fizetesmod = new XElement("fizetesmod", mod);
                    XAttribute id = new XAttribute("id", kulcs);
                    XElement egyenleg = new XElement("egyenleg", "0");
                    XElement befizetve = new XElement("befizetve", befi);
                    XElement aktivfilm = new XElement("aktivfilm", "0");
                    XElement aktiv = new XElement("aktiv", "true");
                    XElement koltsege = new XElement("koltseg", kolt);
                    XElement statusza = new XElement("statusz", "user");
                    kulcs++;
                    XElement felhasznalo = new XElement("felhasznalo", id, nev, jelszo, email, bankszam, szuletes, fizetesmod, egyenleg, befizetve, aktivfilm, aktiv, koltsege, statusza);
                    mentes.Element("felhasznalok").Add(felhasznalo);
                    mentes.Save(eleresiut);

                    MessageBox.Show("A regisztráció sikeres, most már bejelentkezhet.");
                    Torol();
                    this.Close();
                }
                else
                {
                    // A field error occured.
                    if (NevEllenorzo == false)
                    {
                        hibaoka += " Nem adtad meg a teljes neved. ";
                    }

                    if (EmailEllenorzo == false)
                    {
                        hibaoka += " Rosszul adtad meg az e-mail címed. ";
                    }

                    if (JelszoEllenorzo == false)
                    {
                        hibaoka += " A jelszó nem felel meg a követelményeknek. ";
                    }

                    if (gyoztes.Count() == 0)
                    {
                        hibaoka += " Már létezik a megadott jelszóval vagy e-mail címmel regisztráció. ";
                    }

                    if (BankSzamEllenorzo <= 0)
                    {
                        hibaoka += " Nem megfelelő a bankszámla-szám. ";
                    }

                    MessageBox.Show("Hiba történt az adatok megadásakor: " + hibaoka);
                    Torol();
                }
            }
            else
            {
                // A field was empty.
                if (NevTextBox.Text == "")
                {
                    hibaoka += " Üres a név mező. ";
                }

                if (JelszoTextBox.Text == "")
                {
                    hibaoka += " Üres a jelszó mező. "; 
                }

                if (EmailTextBox.Text == "")
                {
                    hibaoka += " Üres az e-mail mező. ";
                }

                if (BankTextBox.Text == "")
                {
                    hibaoka += " Üres a bankszámmla-szám mező. ";
                }

                MessageBox.Show("Hiba történt az adatok megadásakor: " + hibaoka);
                Torol();
            }
        }

        /// <summary>
        /// Handles the Click event of the TorloButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TorloButton_Click(object sender, EventArgs e)
        {
            // Delete
            Torol();
        }

        /// <summary>
        /// Handles the Click event of the MegseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MegseButton_Click(object sender, EventArgs e)
        {
            // Cancel
            this.Close();
        }

        #endregion Methods
    }
}