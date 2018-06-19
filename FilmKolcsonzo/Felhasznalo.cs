using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmKolcsonzo
{
    /// <summary>
    /// Base class for User.
    /// </summary>
    class Felhasznalo
    {
        #region Fields

        // The name field of the User class.
        private string neve;

        // The password field of the User class.
        private string jelszava;

        // The email field of the User class.
        private string emailje;

        // The birthDate field of the User class.
        private string szuletese;

        // The bankAccount field of the User class.
        private int bankszama;

        // The moneyBalance field of the User class.
        private int egyenlege;

        // The typeOfPayment field of the User class.
        private string fizetesmodja;

        // The numberOfActiveFilms field of the User class.
        private int aktivfilmje;

        // The id field of the User class.
        private int idje;

        // The isActive field of the User class.
        private string aktiv;

        // The deposit field of the User class.
        private string befizetve;

        // The cost field of the User class.
        private int koltsege;

        // The status field of the User class.
        private string statusza;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the neve.
        /// </summary>
        /// <value>
        /// The neve.
        /// </value>
        public string Neve
        {
            get { return neve; }
            set { neve = value; }
        }

        /// <summary>
        /// Gets or sets the jelszava.
        /// </summary>
        /// <value>
        /// The jelszava.
        /// </value>
        public string Jelszava
        {
            get { return jelszava; }
            set { jelszava = value; }
        }

        /// <summary>
        /// Gets or sets the emailje.
        /// </summary>
        /// <value>
        /// The emailje.
        /// </value>
        public string Emailje
        {
            get { return emailje; }
            set { emailje = value; }
        }

        /// <summary>
        /// Gets or sets the szuletese.
        /// </summary>
        /// <value>
        /// The szuletese.
        /// </value>
        public string Szuletese
        {
            get { return szuletese; }
            set { szuletese = value; }
        }

        /// <summary>
        /// Gets or sets the bankszama.
        /// </summary>
        /// <value>
        /// The bankszama.
        /// </value>
        public int Bankszama
        {
            get { return bankszama; }
            set { bankszama = value; }
        }

        /// <summary>
        /// Gets or sets the fizetesmodja.
        /// </summary>
        /// <value>
        /// The fizetesmodja.
        /// </value>
        public string Fizetesmodja
        {
            get { return fizetesmodja; }
            set { fizetesmodja = value; }
        }

        /// <summary>
        /// Gets or sets the egyenlege.
        /// </summary>
        /// <value>
        /// The egyenlege.
        /// </value>
        public int Egyenlege
        {
            get { return egyenlege; }
            set { egyenlege = value; }
        }

        /// <summary>
        /// Gets or sets the aktivfilmje.
        /// </summary>
        /// <value>
        /// The aktivfilmje.
        /// </value>
        public int Aktivfilmje
        {
            get { return aktivfilmje; }
            set { aktivfilmje = value; }
        }

        /// <summary>
        /// Gets or sets the idje.
        /// </summary>
        /// <value>
        /// The idje.
        /// </value>
        public int Idje
        {
            get { return idje; }
            set { idje = value; }
        }

        /// <summary>
        /// Gets or sets the aktiv.
        /// </summary>
        /// <value>
        /// The aktiv.
        /// </value>
        public string Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }

        /// <summary>
        /// Gets or sets the befizetve.
        /// </summary>
        /// <value>
        /// The befizetve.
        /// </value>
        public string Befizetve
        {
            get { return befizetve; }
            set { befizetve = value; }
        }

        /// <summary>
        /// Gets or sets the koltsege.
        /// </summary>
        /// <value>
        /// The koltsege.
        /// </value>
        public int Koltsege
        {
            get { return koltsege; }
            set { koltsege = value; }
        }

        /// <summary>
        /// Gets or sets the statusza.
        /// </summary>
        /// <value>
        /// The statusza.
        /// </value>
        public string Statusza
        {
            get { return statusza; }
            set { statusza = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Felhasznalo"/> class.
        /// </summary>
        public Felhasznalo()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Felhasznalo"/> class.
        /// </summary>
        /// <param name="neve">The input value for neve field.</param>
        /// <param name="jelszava">The input value for jelszava field.</param>
        /// <param name="emailje">The input value for emailje field.</param>
        /// <param name="szuletese">The input value for szuletese field.</param>
        /// <param name="bankszama">The input value for bankszama field.</param>
        /// <param name="fizetesmodja">The input value for fizetesmodja field.</param>
        /// <param name="egyenlege">The input value for egyenlege field.</param>
        /// <param name="aktivfilmje">The input value for aktivfilmje field.</param>
        /// <param name="idje">The input value for idje field.</param>
        /// <param name="aktiv">The input value for aktiv field.</param>
        /// <param name="befizetve">The input value for befizetve field.</param>
        /// <param name="koltsege">The input value for koltsege field.</param>
        /// <param name="statusza">The input value for statusza field.</param>
        public Felhasznalo(string neve, string jelszava, string emailje, string szuletese, int bankszama, string fizetesmodja, int egyenlege, int aktivfilmje, int idje, string aktiv, string befizetve, int koltsege, string statusza)
        {
            this.neve = neve;
            this.idje = idje;
            this.jelszava = jelszava;
            this.szuletese = szuletese;
            this.emailje = emailje;
            this.bankszama = bankszama;
            this.fizetesmodja = fizetesmodja;
            this.egyenlege = egyenlege;
            this.aktivfilmje = aktivfilmje;
            this.aktiv = aktiv;
            this.befizetve = befizetve;
            this.koltsege = koltsege;
            this.statusza = statusza;
        }
		
		/// <summary>
        /// Destroys the instance of the <see cref="Felhasznalo"/> class.
        /// </summary>
        ~Felhasznalo()
        { }

        #endregion Constructors

        #region Methods

        #endregion Methods
    }
}