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

namespace FilmKolcsonzo
{
    /// <summary>
    /// Interaction logic for BejelentkezoForm.
    /// </summary>
    public partial class BejelentkezoForm : Form
    {
        #region Fields

        // XML adatok
        string eleresiut = "felhasznalok.xml";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BejelentkezoForm"/> class.
        /// </summary>
        public BejelentkezoForm()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Handles the Load event of the BejelentkezoForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BejelentkezoForm_Load(object sender, EventArgs e)
        {
            // Preset values
            TextBoxEmail.Text = "";
            TextBoxJelszo.Text = "";
        }

        /// <summary>
        /// Handles the Click event of the BejelentkezesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BejelentkezesButton_Click(object sender, EventArgs e)
        {
            // Login
            // Search for a user with the specified username and password.
            XDocument kereses = XDocument.Load(eleresiut);
            var felhasznalok = kereses.Descendants("felhasznalo");
            var gyoztes = from x in felhasznalok
                          where (string)x.Element("email") == TextBoxEmail.Text && (string)x.Element("jelszo") == TextBoxJelszo.Text
                          select new { Id = x.Attribute("id").Value };

            // Check if there was any result.
            if (gyoztes.Count() == 1)
            {
                // Add the found user values to the other Form.
                FelhasznaloiFeluletForm felhaszn = new FelhasznaloiFeluletForm();
                var felhasznalo = from x in kereses.Descendants("felhasznalo")
                                  where (string)x.Element("email") == TextBoxEmail.Text && (string)x.Element("jelszo") == TextBoxJelszo.Text
                                  let neve = (string)x.Element("name")
                                  let idje = (int)x.Attribute("id")
                                  let bankszama = (int)x.Element("bankszam")
                                  let jelszava = (string)x.Element("jelszo")
                                  let emailje = (string)x.Element("email")
                                  let aktiv = (string)x.Element("aktiv")
                                  let aktivfilmje = (int)x.Element("aktivfilm")
                                  let szuletese = (string)x.Element("szuletes")
                                  let fizetesmodja = (string)x.Element("fizetesmod")
                                  let egyenlege = (int)x.Element("egyenleg")
                                  let befizetve = (string)x.Element("befizetve")
                                  let koltsege = (int)x.Element("koltseg")
                                  let statusza = (string)x.Element("statusz")
                                  select new Felhasznalo(neve, jelszava, emailje, szuletese, bankszama, fizetesmodja, egyenlege, aktivfilmje, idje, aktiv, befizetve, koltsege, statusza);
                felhaszn.belepo = (Felhasznalo)felhasznalo.Single();
                felhaszn.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sajnos nem találtam a megadott e-mail címmel és jelszóval regisztrációt.");
            }
        }

        /// <summary>
        /// Handles the Click event of the RegisztracioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RegisztracioButton_Click(object sender, EventArgs e)
        {
            // Registration
            RegisztraciosForm RegForm = new RegisztraciosForm();
            RegForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the MegsemButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MegsemButton_Click(object sender, EventArgs e)
        {
            // Cancel
            this.Close();
        }

        #endregion Methods
    }
}