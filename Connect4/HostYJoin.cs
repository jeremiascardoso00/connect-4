using System;
using System.Windows.Forms;
using System.ServiceModel;


namespace Connect4
{
    public partial class HostYJoin : Form
    {
        public HostYJoin()
        {
            InitializeComponent();
            
            
        }

        private void btn_Host_Click(object sender, EventArgs e)
        {
            Hide();
            GameOnlineJugador1 gameOnlineForm = new GameOnlineJugador1();
            gameOnlineForm.ShowDialog();
            gameOnlineForm = null;
            Show();
        }

        private void btn_Join_Click(object sender, EventArgs e)
        {
            Hide();
            
            GameOnlineJugador2 gameOnlineForm = new GameOnlineJugador2();
            gameOnlineForm.ShowDialog();
            gameOnlineForm = null;
            Show();
        }
    }
}
