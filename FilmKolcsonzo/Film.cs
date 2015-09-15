using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmKolcsonzo
{
    /// <summary>
    /// Base class for Movie.
    /// </summary>
    class Film
    {
        #region Fields

        // The title field of the Movie class.
        private string cime;

        // The year field of the Movie class.
        private int eve;

        // The director field of the Movie class.
        private string rendezoje;

        // The actors field of the Movie class.
        private string szineszei;

        // The trailerDuration field of the Movie class.
        private int bemutatohossza;

        // The duration field of the Movie class.
        private int hossza;

        // The picture field of the Movie class.
        private string kepe;

        // The summary field of the Movie class.
        private string leirasa;

        // The link field of the Movie class.
        private string linkje;

        // The id field of the Movie class.
        private int idje;

        // The isActive field of the Movie class.
        private bool aktiv;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the cime.
        /// </summary>
        /// <value>
        /// The cime.
        /// </value>
        public string Cime
        {
            get { return cime; }
            set { cime = value; }
        }

        /// <summary>
        /// Gets or sets the eve.
        /// </summary>
        /// <value>
        /// The eve.
        /// </value>
        public int Eve
        {
            get { return eve; }
            set { eve = value; }
        }

        /// <summary>
        /// Gets or sets the rendezoje.
        /// </summary>
        /// <value>
        /// The rendezoje.
        /// </value>
        public string Rendezoje
        {
            get { return rendezoje; }
            set { rendezoje = value; }
        }

        /// <summary>
        /// Gets or sets the szineszei.
        /// </summary>
        /// <value>
        /// The szineszei.
        /// </value>
        public string Szineszei
        {
            get { return szineszei; }
            set { szineszei = value; }
        }

        /// <summary>
        /// Gets or sets the bemutatohossza.
        /// </summary>
        /// <value>
        /// The bemutatohossza.
        /// </value>
        public int Bemutatohossza
        {
            get { return bemutatohossza; }
            set { bemutatohossza = value; }
        }

        /// <summary>
        /// Gets or sets the hossza.
        /// </summary>
        /// <value>
        /// The hossza.
        /// </value>
        public int Hossza
        {
            get { return hossza; }
            set { hossza = value; }
        }

        /// <summary>
        /// Gets or sets the kepe.
        /// </summary>
        /// <value>
        /// The kepe.
        /// </value>
        public string Kepe
        {
            get { return kepe; }
            set { kepe = value; }
        }

        /// <summary>
        /// Gets or sets the leirasa.
        /// </summary>
        /// <value>
        /// The leirasa.
        /// </value>
        public string Leirasa
        {
            get { return leirasa; }
            set { leirasa = value; }
        }

        /// <summary>
        /// Gets or sets the linkje.
        /// </summary>
        /// <value>
        /// The linkje.
        /// </value>
        public string Linkje
        {
            get { return linkje; }
            set { linkje = value; }
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
        public bool Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Film"/> class.
        /// </summary>
        public Film()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Film"/> class.
        /// </summary>
        /// <param name="cime">The input value for cime field.</param>
        /// <param name="eve">The input value for eve field.</param>
        /// <param name="rendezoje">The input value for rendezoje field.</param>
        /// <param name="szineszei">The input value for szineszei field.</param>
        /// <param name="hossza">The input value for hossza field.</param>
        /// <param name="kepe">The input value for kepe field.</param>
        /// <param name="leirasa">The input value for leirasa field.</param>
        public Film(string cime, int eve, string rendezoje, string szineszei, int hossza, string kepe, string leirasa)
        {
            this.cime = cime;
            this.eve = eve;
            this.szineszei = szineszei;
            this.rendezoje = rendezoje;
            this.hossza = hossza;
            this.kepe = kepe;
            this.leirasa = leirasa;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Film"/> class.
        /// </summary>
        /// <param name="cime">The input value for cime field.</param>
        /// <param name="eve">The input value for eve field.</param>
        /// <param name="rendezoje">The input value for rendezoje field.</param>
        /// <param name="szineszei">The input value for szineszei field.</param>
        /// <param name="hossza">The input value for hossza field.</param>
        /// <param name="kepe">The input value for kepe field.</param>
        /// <param name="leirasa">The input value for leirasa field.</param>
        /// <param name="linkje">The input value for linkje field.</param>
        /// <param name="idje">The input value for idje field.</param>
        /// <param name="aktiv">The input value for aktiv field.</param>
        /// <param name="bemutatohossza">The input value for bemutatohossza field.</param>
        public Film(string cime, int eve, string rendezoje, string szineszei, int hossza, string kepe, string leirasa, string linkje, int idje, bool aktiv, int bemutatohossza)
        {
            this.cime = cime;
            this.eve = eve;
            this.szineszei = szineszei;
            this.rendezoje = rendezoje;
            this.hossza = hossza;
            this.kepe = kepe;
            this.leirasa = leirasa;
            this.linkje = linkje;
            this.idje = idje;
            this.aktiv = aktiv;
            this.bemutatohossza = bemutatohossza;
        }

        #endregion Constructors

        #region Methods

        #endregion Methods
    }
}