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
using System.Threading;
using System.IO;
using System.Xml;

namespace FilmKolcsonzo
{
    /// <summary>
    /// Interaction logic for FelhasznaloiFeluletForm.
    /// </summary>
    public partial class FelhasznaloiFeluletForm : Form
    {
        #region Fields

        // Internal field to pass values from one Form to another.
        internal Felhasznalo belepo;

        // Fields for functionallity.
        int kulcs = 0;
        int kulcs2 = 0;
        private int getid;
        private int getid2;
        private string box;
        private string box2;
        private string box3;
        private string box5;
        private string url;
        bool seckond = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FelhasznaloiFeluletForm"/> class.
        /// </summary>
        public FelhasznaloiFeluletForm()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Handles the Load event of the FelhasznaloiFeluletForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void FelhasznaloiFeluletForm_Load(object sender, EventArgs e)
        {
            // Preset values.
            #region Preset

            button3.Enabled = false;
            button4.Enabled = false;
            button9.Enabled = false;
            KeresoButton.Enabled = false;

            // Beállítom a bejelentkezéshez rendelt fülek láthatóságát
            Film.TabPages.Remove(AdataimTabPage);
            Film.TabPages.Remove(PenzugyekTabPage);
            Film.TabPages.Remove(LejatszoTabPage);
            Film.TabPages.Remove(ModTabPage);

            // Beállítom a film törlő kereső témáit
            comboBox3.Items.Add("filmcím");
            comboBox3.Items.Add("színészek");
            comboBox3.Items.Add("leírás");
            comboBox3.Items.Add("gyártás éve");
            comboBox3.Items.Add("rendező");
            comboBox3.Items.Add("hossz");

            // Beállítom a film kereső témáit
            KeresoComboBox.Items.Add("leírás");
            KeresoComboBox.Items.Add("színészek");
            KeresoComboBox.Items.Add("rendező");
            KeresoComboBox.Items.Add("gyártás éve");
            KeresoComboBox.Items.Add("filmcím");

            // Beállítom a film másodlagos kereső témáit
            comboBox4.Items.Add("leírás");
            comboBox4.Items.Add("színészek");
            comboBox4.Items.Add("rendező");
            comboBox4.Items.Add("gyártás éve");
            comboBox4.Items.Add("filmcím");
            comboBox1.Items.Add("havidíjas");
            comboBox1.Items.Add("darabonkénti");

            #endregion Preset

            #region WCF_Currency

            ServiceReferenceMNB.MNBArfolyamServiceSoapClient client = new ServiceReferenceMNB.MNBArfolyamServiceSoapClient();
            ServiceReferenceMNB.GetCurrentExchangeRatesResponse info = await client.GetCurrentExchangeRatesAsync();

            XDocument currencies = XDocument.Parse(info.Body.GetCurrentExchangeRatesResult);
            var curr = from x in currencies.Descendants("Rate")
                       select x.Attribute("curr").Value;

            foreach (var c in curr)
            {
                comboBox2.Items.Add(c);
            }

            comboBox2.Items.Add("HUF");
            comboBox2.Text = comboBox2.Items[0].ToString();

            #endregion WCF_Currency

            #region FileExist

            // kölcsönzések kulcsának megszerzése
            if (File.Exists("kolcsonzesek.xml"))
            {
                XDocument doc = XDocument.Load("kolcsonzesek.xml");
                var felhasznalok = doc.Descendants("kolcsonzes");

                if (felhasznalok.Count() != 0)
                {
                    kulcs = doc.Descendants("kolcsonzes").Max(x => (int)x.Attribute("id")) + 1;
                }
            }
            else
            {
                XElement felhasznalok = new XElement("kolcsonzes");
                XDocument doc = new XDocument(felhasznalok);
                doc.Save("kolcsonzesek.xml");
            }

            // filmek kulcsának megszerzése
            if (File.Exists("filmek.xml"))
            {
                XDocument doc2 = XDocument.Load("filmek.xml");
                var felhasznalok2 = doc2.Descendants("film");

                if (felhasznalok2.Count() != 0)
                {
                    kulcs2 = doc2.Descendants("film").Max(x => (int)x.Attribute("id")) + 1;
                }
            }
            else
            {
                XElement felhasznalok2 = new XElement("film");
                XDocument doc2 = new XDocument(felhasznalok2);
                doc2.Save("filmek.xml");
            }

            #endregion FileExist

            #region CheckLogin

            // Ellenőrzöm, hogy van-e felhasználó bejelentkezve
            try
            {
                if (belepo.Neve != "")
                {
                    LoginLabel.Text = "Kilépés";
                    Film.TabPages.Add(AdataimTabPage);

                    if (belepo.Statusza == "user")
                    {
                        Film.TabPages.Remove(ModTabPage);

                        if (belepo.Aktiv != "false")
                        {
                            Film.TabPages.Add(PenzugyekTabPage);

                            if (belepo.Aktivfilmje > 0)
                            {
                                if (belepo.Fizetesmodja == "havi" && belepo.Befizetve == "false")
                                {
                                    Film.TabPages.Remove(LejatszoTabPage);
                                }
                                else
                                {
                                    Film.TabPages.Add(LejatszoTabPage);
                                }
                            }
                            else
                            {
                                Film.TabPages.Remove(LejatszoTabPage);
                            }
                        }
                        else
                        {
                            Film.TabPages.Remove(PenzugyekTabPage);
                        }
                    }
                    else
                    {
                        Film.TabPages.Add(ModTabPage);
                    }
                }
            }
            catch (Exception e6)
            {

            }

            #endregion CheckLogin
        }

        /// <summary>
        /// Handles the Click event of the LoginLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void LoginLabel_Click(object sender, EventArgs e)
        {
            if (LoginLabel.Text == "Bejelentkezés")
            {
                // Login
                BejelentkezoForm bejelentkezes = new BejelentkezoForm();
                bejelentkezes.ShowDialog();

                LoginLabel.Text = "Kilépés";
            }
            else
            {
                // Logout
                LoginLabel.Text = "Bejelentkezés";
                belepo = null;

                Film.TabPages.Remove(AdataimTabPage);
                Film.TabPages.Remove(PenzugyekTabPage);
                Film.TabPages.Remove(LejatszoTabPage);
                Film.TabPages.Remove(ModTabPage);
            }
        }

        /// <summary>
        /// Handles the Click event of the KeresoButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void KeresoButton_Click(object sender, EventArgs e)
        {
            // Search engine for user view.
            if (KeresoTextBox.Text != "")
            {
                switch (KeresoComboBox.Text.ToString())
                {
                    case "filmcím":
                        box = "cim";
                        break;
                    case "leírás":
                        box = "leiras";
                        break;
                    case "színészek":
                        box = "szereplok";
                        break;
                    case "gyártás éve":
                        box = "ev";
                        break;
                    case "rendező":
                        box = "rendezo";
                        break;
                }

                switch (comboBox4.Text.ToString())
                {
                    case "filmcím":
                        box5 = "cim";
                        break;
                    case "leírás":
                        box5 = "leiras";
                        break;
                    case "színészek":
                        box5 = "szereplok";
                        break;
                    case "gyártás éve":
                        box5 = "ev";
                        break;
                    case "rendező":
                        box5 = "rendezo";
                        break;
                }

                bool van = false;
                dataGridView1.Rows.Clear();

                if (seckond == false)
                {
                    XDocument keres = XDocument.Load("filmek.xml");
                    var talalat = from x in keres.Root.Descendants("film")
                                  select x;

                    foreach (var q in talalat)
                    {
                        string keresendo = (string)q.Element(box.ToString());

                        for (int i = 0; i < (KeresoTextBox.Text.Length); i++)
                        {
                            try
                            {
                                if (keresendo.Substring(i, KeresoTextBox.Text.Length).ToUpper() == KeresoTextBox.Text.ToUpper())
                                {
                                    string[] oszlop = new string[6];
                                    oszlop[0] = (string)q.Element("cim");
                                    oszlop[1] = (string)q.Element("ev");
                                    oszlop[2] = (string)q.Element("rendezo");
                                    oszlop[3] = (string)q.Element("szereplok");
                                    oszlop[4] = (string)q.Element("hossz");
                                    oszlop[5] = (string)q.Element("leiras");
                                    dataGridView1.Rows.Add(oszlop);

                                    van = true;
                                }
                            }
                            catch (Exception e2)
                            {

                            }
                        }
                    }
                }
                else
                {
                    if ((textBox14.Text != "") && (comboBox4.Text.ToString() != ""))
                    {
                        XDocument keres = XDocument.Load("filmek.xml");
                        var talalat = from x in keres.Root.Descendants("film")
                                      where (string)x.Element(box5) == textBox14.Text.ToString()
                                      select x;

                        foreach (var q in talalat)
                        {
                            string keresendo = (string)q.Element(box.ToString());

                            for (int i = 0; i < (KeresoTextBox.Text.Length); i++)
                            {
                                try
                                {
                                    if (keresendo.Substring(i, KeresoTextBox.Text.Length).ToUpper() == KeresoTextBox.Text.ToUpper())
                                    {
                                        string[] oszlop = new string[6];
                                        oszlop[0] = (string)q.Element("cim");
                                        oszlop[1] = (string)q.Element("ev");
                                        oszlop[2] = (string)q.Element("rendezo");
                                        oszlop[3] = (string)q.Element("szereplok");
                                        oszlop[4] = (string)q.Element("hossz");
                                        oszlop[5] = (string)q.Element("leiras");
                                        dataGridView1.Rows.Add(oszlop);

                                        van = true;
                                    }
                                }
                                catch (Exception e7)
                                {

                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hiányos másodlagos szűrési feltételek.");
                    }
                }
                if (van == false)
                {
                    MessageBox.Show("Sajnos nem találtam a megadott feltételeknek megfelelő filmet.");
                }
            }
            else
            {
                MessageBox.Show("Sajnos nem találtam a megadott feltételeknek megfelelő filmet.");
            }
        }

        /// <summary>
        /// Handles the Enter event of the KezdolapTabPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void KezdolapTabPage_Enter(object sender, EventArgs e)
        {
            // Preset values.
            label1.Text = "Bosszúállók";
            label2.Text = "Eredet";
            label3.Text = "Avatar";
            label4.Text = "A gyűrűk ura";
            label5.Text = "Üvegtigris";

            pictureBox2.Image = Image.FromFile("..\\..\\kepek\\avengers.JPG");
            pictureBox3.Image = Image.FromFile("..\\..\\kepek\\inception.JPG");
            pictureBox4.Image = Image.FromFile("..\\..\\kepek\\avatar.JPG");
            pictureBox5.Image = Image.FromFile("..\\..\\kepek\\lotr.JPG");
            pictureBox6.Image = Image.FromFile("..\\..\\kepek\\uvegtigris.JPG");

            KeresoButton.Enabled = false;
        }

        /// <summary>
        /// Handles the Enter event of the FilmekTabPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FilmekTabPage_Enter(object sender, EventArgs e)
        {
            // Preset values.
            textBox14.Enabled = false;
            comboBox4.Enabled = false;
            KeresoButton.Enabled = true;
            KeresoTextBox.Text = "";

            FilmekLoad();
        }

        /// <summary>
        /// Handles the Enter event of the LeirasTabPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void LeirasTabPage_Enter(object sender, EventArgs e)
        {
            // Preset values.
            KeresoButton.Enabled = false;
            richTextBox1.Text = "Havi bérlet: 5000 HUF, egy film megrendelési költsége: 500 HUF. \r\n\r\nHavidíjas filmkölcsönzés esetén a kölcsönzés csak a havidíj befizetése után lehetséges.\r\nDarabonkénti kölcsönzés esetén a film megtekintése csak a kölcsönzés után keletkezett költség befizetése után lehetséges.";
        }

        /// <summary>
        /// Handles the Enter event of the AdataimTabPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AdataimTabPage_Enter(object sender, EventArgs e)
        {
            // Preset values.
            KeresoButton.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";

            label8.Text = "Név: " + (string)belepo.Neve;
            label9.Text = "E-mail cím: " + (string)belepo.Emailje;
            label10.Text = "Születési idő:" + (string)belepo.Szuletese;
            label11.Text = "Bankszámla szám: " + belepo.Bankszama.ToString();
            label12.Text = "Jelszó: " + (string)belepo.Jelszava;
            label13.Text = "Fizetési mód: " + (string)belepo.Fizetesmodja;
            label14.Text = "Egyenleg: " + belepo.Egyenlege.ToString();
            label15.Text = "Aktív filmek száma: " + belepo.Aktivfilmje.ToString();
        }

        /// <summary>
        /// Handles the Enter event of the LejatszoTabPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void LejatszoTabPage_Enter(object sender, EventArgs e)
        {
            // Preset values.
            KeresoButton.Enabled = false;
            listBox1.Items.Clear();

            if (belepo.Aktivfilmje > 0)
            {
                XDocument doc = XDocument.Load("kolcsonzesek.xml");
                var kolcsonzesek = doc.Descendants("kolcsonzes");
                var kolcson = from x in kolcsonzesek
                              where int.Parse(x.Element("felhasznaloid").Value) == belepo.Idje && (string)x.Element("megtekintes").Value == "true"
                              select new { Cím = (string)x.Element("filmcim").Value };

                foreach (var q in kolcson)
                {
                    string temp = q.Cím;
                    listBox1.Items.Add(temp);
                }
            }
        }

        /// <summary>
        /// Handles the Enter event of the PenzugyekTabPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PenzugyekTabPage_Enter(object sender, EventArgs e)
        {
            // Preset values.
            KeresoButton.Enabled = false;
            textBox3.Text = "";

            if (int.Parse(belepo.Koltsege.ToString()) != 0)
            {
                button6.Enabled = true;
            }
            else
            {
                button6.Enabled = false;
            }

            label19.Text = "Bankszámla száma: " + belepo.Bankszama.ToString();
            label20.Text = "Egyenlege: " + belepo.Egyenlege.ToString();
            label24.Text = "Fizetés módja: " + belepo.Fizetesmodja.ToString();
            label25.Text = "Befizetendő összeg: " + belepo.Koltsege.ToString();
        }

        /// <summary>
        /// Loads the movies for the user view.
        /// </summary>
        private void FilmekLoad()
        {
            dataGridView1.Rows.Clear();

            XDocument doc = XDocument.Load("filmek.xml");

            foreach (var q in doc.Root.Descendants("film"))
            {
                string[] oszlop = new string[6];
                oszlop[0] = (string)q.Element("cim");
                oszlop[1] = (string)q.Element("ev");
                oszlop[2] = (string)q.Element("rendezo");
                oszlop[3] = (string)q.Element("szereplok");
                oszlop[4] = (string)q.Element("hossz");
                oszlop[5] = (string)q.Element("leiras");
                dataGridView1.Rows.Add(oszlop);
            }
        }

        /// <summary>
        /// Loads the movies for the admin view.
        /// </summary>
        private void FilmekLoad2()
        {
            dataGridView2.Rows.Clear();

            XDocument doc = XDocument.Load("filmek.xml");

            foreach (var q in doc.Root.Descendants("film"))
            {
                string[] oszlop = new string[6];
                oszlop[0] = (string)q.Element("cim");
                oszlop[1] = (string)q.Element("rendezo");
                oszlop[2] = (string)q.Element("szereplok");
                oszlop[3] = (string)q.Element("ev");
                oszlop[4] = (string)q.Element("hossz");
                oszlop[5] = (string)q.Element("leiras");
                dataGridView2.Rows.Add(oszlop);
            }
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            // List
            FilmekLoad();
        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            // Edit user
            int elso = 0;
            string masodik = "";
            string harmadik = "";
            bool[] hiba = new bool[2] { false, false };

            // bankAccount
            // Bankszámla szám
            if (textBox1.Text.ToString() != "")
            {
                if (int.Parse(textBox1.Text) > 0)
                {
                    elso = int.Parse(textBox1.Text);
                }
                else
                {
                    hiba[0] = true;
                }
            }
            else
            {
                elso = belepo.Bankszama;
            }

            // password
            // Jelszó
            if (textBox2.Text.ToString() != "")
            {
                if ((textBox2.Text.Length > 8) && (int.Parse(textBox2.Text) > 0))
                {
                    masodik = textBox2.Text;
                }
                else
                {
                    hiba[1] = true;
                }
            }
            else
            {
                masodik = belepo.Jelszava;
            }

            string temp;

            // typeOfPayment
            // Fizetési mód
            if (comboBox1.Text == "darabonkénti")
            {
                temp = "darab";
            }
            else
            {
                temp = "havi";
            }

            if (belepo.Fizetesmodja.ToString() != temp)
            {
                harmadik = temp;
            }
            else
            {
                harmadik = belepo.Fizetesmodja;
            }

            // Save
            // Mentés
            if (hiba[0] == false && hiba[1] == false)
            {
                belepo.Bankszama = elso;
                belepo.Jelszava = masodik;
                belepo.Fizetesmodja = harmadik;

                XDocument doc = XDocument.Load("felhasznalok.xml");
                var q = from x in doc.Root.Descendants("felhasznalo")
                        where (int)x.Attribute("id") == belepo.Idje
                        select x;

                q.Single().Element("bankszam").Value = belepo.Bankszama.ToString();
                q.Single().Element("jelszo").Value = belepo.Jelszava;
                q.Single().Element("fizetesmod").Value = belepo.Fizetesmodja;
                doc.Save("felhasznalok.xml");

                MessageBox.Show("A mentés megtörtént.");
            }
            else
            {
                MessageBox.Show("Hibás adat, a mentés sikertelen. Kérem próbálja meg újra.");
            }
        }

        /// <summary>
        /// Handles the CellClick event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // An item must be selected.
            // kijelölés esetén engedélyezzük a gombokat
            if (dataGridView1.CurrentCellAddress.X == 0)
            {
                button3.Enabled = true;

                try
                {
                    // Set the button accordingly to the user.
                    if (belepo.Neve != "")
                    {
                        if (belepo.Aktiv != "false")
                        {
                            if (belepo.Statusza != "admin")
                            {
                                if (belepo.Fizetesmodja == "havi")
                                {
                                    if (belepo.Befizetve != "false")
                                    {
                                        button4.Enabled = true;
                                    }
                                    else
                                    {
                                        button4.Enabled = false;
                                    }
                                }
                                else
                                {
                                    button4.Enabled = true;
                                }
                            }
                            else
                            {
                                button4.Enabled = false;
                            }
                        }
                        else
                        {
                            button4.Enabled = false;
                        }
                    }
                }
                catch (Exception e5)
                {

                }
            }
            else
            {
                button3.Enabled = false;
                button4.Enabled = false;
            }

            // Hanyadik sorra lett kattintva
            getid = int.Parse(e.RowIndex.ToString()) + 1;
        }

        /// <summary>
        /// Handles the Click event of the button3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button3_Click(object sender, EventArgs e)
        {
            // Details
            // Átadjuk a film információit az új form-nak
            XDocument doc = XDocument.Load("filmek.xml");
            Form2 innew = new Form2();
            var movie = from x in doc.Descendants("film")
                              where (int)x.Attribute("id") == getid
                              let cime = (string)x.Element("cim")
                              let eve = (int)x.Element("ev")
                              let rendezoje = (string)x.Element("rendezo")
                              let szineszei = (string)x.Element("szereplok")
                              let hossza = (int)x.Element("hossz")
                              let kepe = (string)x.Element("kep")
                              let leirasa = (string)x.Element("leiras")
                              select new Film(cime, eve, rendezoje, szineszei, hossza, kepe, leirasa);
            innew.megjeleno = (Film)movie.Single();
            innew.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the button4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button4_Click(object sender, EventArgs e)
        {
            // Add rent
            // Ellenőrizzük, hogy kikölcsönözte-e már a felhasználó
            XDocument doc = XDocument.Load("kolcsonzesek.xml");
            var vannak = doc.Descendants("kolcsonzes");
            var van = from x in vannak
                          where (string)x.Element("felhasznaloid") == belepo.Idje.ToString() && (int)x.Element("filmid") == getid && (string)x.Element("aktiv") == "true"
                          select x;

            if (van.Count() > 0)
            {
                MessageBox.Show("A filmet már kiválasztotta.");
            }
            // Ha nem, akkor hozzáadjuk a kölcsönzésekhez
            else
            {
                // Megkeresem a kiválasztott film szükséges részleteit
                XDocument doc2 = XDocument.Load("filmek.xml");
                var megvannak = doc2.Descendants("film");
                var megvan = from x in megvannak
                             where (int)x.Attribute("id") == getid
                             select new { Filmcim = (string)x.Element("cim").Value, Link = (string)x.Element("link").Value };

                string[] temp = new string[2];

                foreach (var q3 in megvan)
                {
                    temp[0] = q3.Filmcim;
                    temp[1] = q3.Link;
                }

                // Beírom a film és a felhasználó adatait a kölcsönzésekhez
                XDocument doc1 = XDocument.Load("kolcsonzesek.xml");
                XElement felhasznaloid = new XElement("felhasznaloid", belepo.Idje);
                XElement filmid = new XElement("filmid", getid);
                XElement filmcim = new XElement("filmcim", temp[0]);
                XElement link = new XElement("link", temp[1]);
                XElement aktiv = new XElement("aktiv", "true");

                bool meg;

                if (belepo.Fizetesmodja == "havi")
                {
                    meg = true;
                }
                else
                {
                    meg = false;
                }

                XElement megtekintes = new XElement("megtekintes", meg);
                XAttribute id = new XAttribute("id", kulcs);
                kulcs++;
                XElement jatekos = new XElement("kolcsonzes", id, filmid, felhasznaloid, filmcim, link, aktiv, megtekintes);
                doc1.Element("kolcsonzesek").Add(jatekos);
                doc1.Save("kolcsonzesek.xml");

                // Beírom a felhasználóhoz is a filmet és a szükséges adatokat megváltoztatom
                belepo.Aktivfilmje = int.Parse(belepo.Aktivfilmje.ToString()) + 1;

                if (belepo.Fizetesmodja == "darab")
                {
                    belepo.Koltsege = int.Parse(belepo.Koltsege.ToString()) + 500;
                    belepo.Befizetve = "false";
                }

                XDocument doc3 = XDocument.Load("felhasznalok.xml");
                var q2 = from z in doc3.Root.Descendants("felhasznalo")
                        where (int)z.Attribute("id") == belepo.Idje
                        select z;

                q2.Single().Element("aktivfilm").Value = belepo.Aktivfilmje.ToString();
                q2.Single().Element("koltseg").Value = belepo.Koltsege.ToString();
                q2.Single().Element("befizetve").Value = belepo.Befizetve.ToString();
                doc3.Save("felhasznalok.xml");

                if (belepo.Fizetesmodja == "havi")
                {
                    Film.TabPages.Add(LejatszoTabPage);
                }

                MessageBox.Show("A filmet hozzáadtuk a kölcsönzésekhez.");
            }
        }

        /// <summary>
        /// Handles the Click event of the button5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button5_Click(object sender, EventArgs e)
        {
            // Edit the user financial records.
            int egyenleg = belepo.Egyenlege;
            double text = double.Parse(textBox3.Text);
            double arany = 1;

            if (textBox3.Text != "")
            {
                if (comboBox2.Text != "HUF")
                {
                    #region WCF_Currency

                    ServiceReferenceMNB.MNBArfolyamServiceSoapClient client = new ServiceReferenceMNB.MNBArfolyamServiceSoapClient();
                    string info = client.GetCurrentExchangeRates();
                    var rate = from x in XDocument.Parse(info).Descendants("Rate")
                               where x.Attribute("curr").Value == comboBox2.Text
                               select x.Value;

                    #endregion WCF_Currency

                    try
                    {
                        arany = double.Parse(rate.FirstOrDefault());
                    }
                    catch (Exception e4)
                    {

                    }
                }
                else if ((comboBox2.Text == "HUF") || (comboBox2.Text == ""))
                {
                    arany = 1;
                }

                egyenleg += ((int)(text * arany));
                belepo.Egyenlege = egyenleg;

                XDocument doc = XDocument.Load("felhasznalok.xml");
                var q = from x in doc.Root.Descendants("felhasznalo")
                        where (int)x.Attribute("id") == belepo.Idje
                        select x;

                q.Single().Element("egyenleg").Value = belepo.Egyenlege.ToString();
                doc.Save("felhasznalok.xml");

                MessageBox.Show("Az egyenleg feltöltése megtörtént.");
            }
            else
            {
                MessageBox.Show("Nem írt be összeget.");
            }
        }

        /// <summary>
        /// Handles the Click event of the button6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button6_Click(object sender, EventArgs e)
        {
            // Edit the user financial records.
            if (belepo.Egyenlege >= belepo.Koltsege)
            {
                int egyenleg = belepo.Egyenlege;
                egyenleg -= belepo.Koltsege;
                belepo.Egyenlege = egyenleg;
                belepo.Koltsege = 0;
                belepo.Befizetve = "true";

                XDocument doc = XDocument.Load("felhasznalok.xml");
                var q = from x in doc.Root.Descendants("felhasznalo")
                        where (int)x.Attribute("id") == belepo.Idje
                        select x;

                q.Single().Element("egyenleg").Value = belepo.Egyenlege.ToString();
                q.Single().Element("koltseg").Value = belepo.Koltsege.ToString();
                q.Single().Element("befizetve").Value = belepo.Befizetve.ToString();
                doc.Save("felhasznalok.xml");

                if (belepo.Fizetesmodja == "darab")
                {
                    XDocument doc1 = XDocument.Load("kolcsonzesek.xml");
                    var qq = from x in doc1.Root.Descendants("kolcsonzes")
                             where int.Parse(x.Element("felhasznaloid").Value) == belepo.Idje && (string)x.Element("megtekintes").Value == "false"
                             select x;

                    qq.Single().Element("megtekintes").Value = "true";
                    doc1.Save("kolcsonzesek.xml");
                }

                if (belepo.Aktivfilmje > 0)
                {
                    Film.TabPages.Remove(LejatszoTabPage);
                    Film.TabPages.Add(LejatszoTabPage);
                }

                MessageBox.Show("A befizetés megtörtént, most már megtekintheti a film(ek)et.");
            }
            else
            {
                MessageBox.Show("Az egyenleg összege nem fedezi a költséget.");
            }
        }

        /// <summary>
        /// Handles the Enter event of the ModTabPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ModTabPage_Enter(object sender, EventArgs e)
        {
            // Preset values.
            KeresoButton.Enabled = false;

            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";

            FilmekLoad2();
        }

        /// <summary>
        /// Handles the Click event of the button8 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button8_Click(object sender, EventArgs e)
        {
            // Search engine for admin view.
            if (textBox13.Text != "")
            {
                switch (comboBox3.Text.ToString())
                {
                    case "filmcím":
                        box2 = "cim";
                        break;
                    case "leírás":
                        box2 = "leiras";
                        break;
                    case "színészek":
                        box2 = "szereplok";
                        break;
                    case "gyártás éve":
                        box2 = "ev";
                        break;
                    case "rendező":
                        box2 = "rendezo";
                        break;
                    case "hossz":
                        box2 = "hossz";
                        break;
                }

                bool van = false;
                dataGridView2.Rows.Clear();

                XDocument keres = XDocument.Load("filmek.xml");
                var talalat = from x in keres.Root.Descendants("film")
                            select x;

                foreach (var q in talalat)
                {
                    string keresendo = (string)q.Element(box2.ToString());

                    for (int i = 0; i < (textBox13.Text.Length); i++)
                    {
                        try
                        {
                            if (keresendo.Substring(i, textBox13.Text.Length).ToUpper() == textBox13.Text.ToUpper())
                            {
                                string[] oszlop = new string[6];
                                oszlop[0] = (string)q.Element("cim");
                                oszlop[1] = (string)q.Element("rendezo");
                                oszlop[2] = (string)q.Element("szereplok");
                                oszlop[3] = (string)q.Element("ev");
                                oszlop[4] = (string)q.Element("hossz");
                                oszlop[5] = (string)q.Element("leiras");
                                dataGridView2.Rows.Add(oszlop);

                                van = true;
                            }
                        }
                        catch (Exception e3)
                        {

                        }
                    }
                }

                if (van == false)
                {
                    MessageBox.Show("Sajnos nem találtam a megadott feltételeknek megfelelő filmet.");
                }
            }
            else
            {
                MessageBox.Show("Sajnos nem találtam a megadott feltételeknek megfelelő filmet.");
            }
        }

        /// <summary>
        /// Handles the Click event of the button7 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button7_Click(object sender, EventArgs e)
        {
            // Add movie
            if ((textBox4.Text != "") && (textBox5.Text != "") && (textBox6.Text != "") && (textBox7.Text != "") && (textBox8.Text != "") && (textBox9.Text != "") && (textBox10.Text != "") && (textBox11.Text != "") && (textBox12.Text != ""))
            {
                XDocument mentes = XDocument.Load("filmek.xml");
                XElement cim = new XElement("cim", textBox4.Text);
                XElement ev = new XElement("ev", textBox5.Text);
                XElement rendezo = new XElement("rendezo", textBox6.Text);
                XElement szineszek = new XElement("szereplok", textBox7.Text);
                XElement hossz = new XElement("hossz", textBox8.Text);
                XElement kep = new XElement("kep", textBox9.Text);
                XElement link = new XElement("link", textBox10.Text);
                XElement bemutatohossz = new XElement("bemutatohossz", textBox11.Text);
                XElement leiras = new XElement("leiras", textBox12.Text);
                XAttribute id = new XAttribute("id", kulcs2);
                kulcs2++;
                XElement ujfilm = new XElement("film", id, cim, ev, rendezo, szineszek, hossz, kep, link, bemutatohossz, leiras);
                mentes.Element("filmek").Add(ujfilm);
                mentes.Save("filmek.xml");

                FilmekLoad2();

                MessageBox.Show("A film hozzáadása sikeres.");
            }
            else
            {
                MessageBox.Show("Legalább egy mező üresen maradt.");
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the listBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Select movie
            if (listBox1.Items.Count != 0)
            {
                try
                {
                    url = listBox1.SelectedItem.ToString();
                }
                catch (Exception e1)
                {

                }

                XDocument doc1 = XDocument.Load("kolcsonzesek.xml");
                var kolcsonz = doc1.Descendants("kolcsonzes");
                var kolcs = from y in kolcsonz
                            where (string)y.Element("filmcim").Value == url
                            select new { URL = (string)y.Element("link").Value };

                foreach (var q in kolcs)
                {
                    axShockwaveFlash1.Movie = q.URL.ToString();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the button9 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button9_Click(object sender, EventArgs e)
        {
            // Delete movie
            // Megkeresem a filmet az XML-ben
            XDocument search = XDocument.Load("filmek.xml");
            var result = search.Descendants("film");
            var mod = from x in result
                      where (int)x.Attribute("id") == getid2
                      select new { Cím = (string)x.Element("cim").Value};

            foreach (var q in mod)
            {
                box3 = q.Cím;
            }

            // Törlöm
            bool van = false;

            XmlDocument doc = new XmlDocument();
            doc.Load("filmek.xml");
            XmlNode node = doc.DocumentElement;

            foreach (XmlNode x in node.ChildNodes)
            {
                foreach (XmlNode y in x.ChildNodes)
                {
                    if (y.Name == "cim" && y.InnerText == box3)
                    {
                        node.RemoveChild(x);
                        van = true;
                    }
                }
            }

            doc.Save("filmek.xml");
            FilmekLoad2();

            if (van == true)
            {
                MessageBox.Show("Volt találat és töröltem.");
            }
            else
            {
                MessageBox.Show("Nem találtam meg.");
            }
        }

        /// <summary>
        /// Handles the CellClick event of the dataGridView2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // An item must be selected.
            // kijelölés esetén engedélyezzük a gombokat
            if (dataGridView2.CurrentCellAddress.X == 0)
            {
                button9.Enabled = true;
            }
            else
            {
                button9.Enabled = false;
            }

            // Hanyadik sorra lett kattintva
            getid2 = int.Parse(e.RowIndex.ToString()) + 1;
        }

        /// <summary>
        /// Handles the Click event of the button10 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button10_Click(object sender, EventArgs e)
        {
            // További szűrés
            comboBox4.Enabled = true;
            textBox14.Enabled = true;
            seckond = true;
        }

        #endregion Methods
    }
}