using System;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Menu : Form
    {


        public Menu()
        {
            InitializeComponent();

        }

        private void button1v1_Click(object sender, EventArgs e)
        {
            Hide();
            Game1v1 game1V1form = new Game1v1();
            game1V1form.ShowDialog();
            game1V1form = null;
            Show();
        }

        private void buttonOnline_Click(object sender, EventArgs e)
        {
            Hide();
            HostYJoin hosyjoin = new HostYJoin();
            hosyjoin.ShowDialog();
            hosyjoin = null;

            //aca prende el web server 1 unica vez
            //aca elije el jugador su color y eso


            Show();
        }

        private void button1vIA_Click(object sender, EventArgs e)
        {
            Hide();
            Game1vIA game1vIAForm = new Game1vIA();
            game1vIAForm.ShowDialog();
            game1vIAForm = null;
            Show();
        }
    }
}
