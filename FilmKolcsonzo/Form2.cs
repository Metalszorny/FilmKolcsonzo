using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilmKolcsonzo
{
    /// <summary>
    /// Interaction logic for Form2.
    /// </summary>
    public partial class Form2 : Form
    {
        #region Fields

        // Internal field to pass values from one Form to another.
        internal Film megjeleno;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Form2"/> class.
        /// </summary>
        public Form2()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Load event of the Form2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = megjeleno.Cime.ToString();
            label2.Text = "Gyártás éve: " + megjeleno.Eve.ToString();
            label3.Text = "Rendező: " + megjeleno.Rendezoje.ToString();
            label4.Text = "Szereplők:";
            richTextBox2.Text = megjeleno.Szineszei.ToString();
            label5.Text = "Hossza: " + megjeleno.Hossza.ToString() + " perc";
            pictureBox1.Image = Image.FromFile(megjeleno.Kepe.ToString());
            richTextBox1.Text = megjeleno.Leirasa.ToString();
        }

        #endregion Methods
    }
}