using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class Game1vIA : Form
    {

        private int player1_points;
        private int playerBOT_points;
        private int turn;
        public int[,] board = new int[6, 7];

        Funcionalidad funcionalidad = new Funcionalidad();
        Random r = new Random();

        public Game1vIA()
        {
            InitializeComponent();

            int random = r.Next(1, 3);
            turn = random;

            player1_points = 0;
            playerBOT_points = 0;

            funcionalidad.Turn_label_Change(labelTurnColor, turn);

        }

        private void Game1vIA_Paint(object sender, PaintEventArgs e)
        {
            funcionalidad.draw_board(e);
        }

        private void Game1vIA_MouseClick(object sender, MouseEventArgs e)
        {
            labelContinuar.Visible = false;
            // Si el primer turno es para el bot muestro un label y genero el primer movimiento random
            // despues el turn tiene como valor 1 y empieza el gameloop normal contra IA
            if (turn == 2)
            {
                Graphics g = CreateGraphics();
                int botRandomPickColumn = r.Next(0, 7);

                int row_num_bot = funcionalidad.Empty_valid_Row(board, botRandomPickColumn);
                if (row_num_bot != -1)
                {
                    labelTurnColor.ForeColor = Color.Red;
                    labelTurnColor.Text = "RED";

                    funcionalidad.drop_coin(board, row_num_bot, botRandomPickColumn, 2);

                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(Brushes.DodgerBlue, 21 + 85 * botRandomPickColumn, 64 + 85 * row_num_bot, 64, 64);
                    turn = 1;
                }
            }
            else
            {
                game1vIA_loop(e);
            }


        }

        private void Game1vIA_MouseMove(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < funcionalidad.col_inv.Length; i++)
            {
                if (funcionalidad.col_inv[i].Contains(e.Location))
                {
                    if (i == 0)
                        pictureBox1.Show();
                    else if (i == 1)
                        pictureBox2.Show();
                    else if (i == 2)
                        pictureBox3.Show();
                    else if (i == 3)
                        pictureBox4.Show();
                    else if (i == 4)
                        pictureBox5.Show();
                    else if (i == 5)
                        pictureBox6.Show();
                    else if (i == 6)
                        pictureBox7.Show();
                }
                else
                {
                    if (i == 0)
                        pictureBox1.Hide();
                    else if (i == 1)
                        pictureBox2.Hide();
                    else if (i == 2)
                        pictureBox3.Hide();
                    else if (i == 3)
                        pictureBox4.Hide();
                    else if (i == 4)
                        pictureBox5.Hide();
                    else if (i == 5)
                        pictureBox6.Hide();
                    else if (i == 6)
                        pictureBox7.Hide();
                }
            }
        }

        // gameloop para el modo 1vIA
        private void game1vIA_loop(MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            int col_num = funcionalidad.is_valid_location_click(e);

            if (col_num == -1)
            {
                // DEBUG
                //Console.WriteLine("no es una columna");

            }
            else if (col_num != -1)
            {
                int row_num = funcionalidad.Empty_valid_Row(board, col_num);
                if (row_num != -1)
                {
                    funcionalidad.drop_coin(board, row_num, col_num, 1);

                    funcionalidad.drop_coin_draw(g, row_num, col_num, 1);

                    if (funcionalidad.winning_con(board, 1, g))
                    {
                        MessageBox.Show("Player 1 (RED) WINS!");
                        //contador de puntaje
                        player1_points++;
                        labelRED.Text = player1_points.ToString();
                        // reseteo el tablero
                        board = new int[6, 7];
                        // genero un turn random para la prox ronda
                        int random = r.Next(1, 3);
                        turn = random;

                        if (this.turn == 1)
                        {
                            labelContinuar.Visible = false;
                            labelTurnColor.ForeColor = Color.Red;
                            labelTurnColor.Text = "RED";

                        }
                        else if (this.turn == 2)
                        {
                            labelContinuar.Show();
                            labelTurnColor.ForeColor = Color.DodgerBlue;
                            labelTurnColor.Text = "BLUE";
                        }

                        Invalidate();
                        return;
                    }

                    if (funcionalidad.draw_con(board))
                    {
                        MessageBox.Show("Empate!");
                        board = new int[6, 7];
                        int random = r.Next(1, 3);
                        turn = random;

                        if (this.turn == 1)
                        {
                            labelContinuar.Visible = false;
                            labelTurnColor.ForeColor = Color.Red;
                            labelTurnColor.Text = "RED";

                        }
                        else if (this.turn == 2)
                        {
                            labelContinuar.Show();
                            labelTurnColor.ForeColor = Color.DodgerBlue;
                            labelTurnColor.Text = "BLUE";
                        }

                        Invalidate();
                        return;
                    }

                    // bot play
                    // delay para mostrar que es el turno del bot. ? thread.sleep / await task.wait

                    bot_play(board);
                }

            }

        }

        // DEBUG 
        //int bad = 0, good = 0;
        private void bot_play(int[,] board)
        {
            Graphics g = CreateGraphics();
            int botRandomPickColumn = r.Next(0, 7);

            int row_num_bot = funcionalidad.Empty_valid_Row(board, botRandomPickColumn);
            if (row_num_bot != -1)
            {
                // DEBUG - para saber si el bot elije un columna full
                //good++;
                //Console.WriteLine(good+"  good bot "+botRandomPickColumn);

                funcionalidad.drop_coin(board, row_num_bot, botRandomPickColumn, 2);

                funcionalidad.drop_coin_draw(g, row_num_bot, botRandomPickColumn, 2);

                // check if bot win
                if (funcionalidad.winning_con(board, 2, g))
                {
                    MessageBox.Show("Player BOT (BLUE) WINS!");
                    playerBOT_points++;
                    labelBLUE.Text = playerBOT_points.ToString();
                    this.board = new int[6, 7];
                    int random = r.Next(1, 3);
                    turn = random;

                    if (this.turn == 1)
                    {
                        labelContinuar.Hide();
                        labelTurnColor.ForeColor = Color.Red;
                        labelTurnColor.Text = "RED";

                    }
                    else if (this.turn == 2)
                    {
                        labelContinuar.Show();
                        labelTurnColor.ForeColor = Color.DodgerBlue;
                        labelTurnColor.Text = "BLUE";
                    }
                    Invalidate();
                    return;
                }

                // check draw by bot move
                if (funcionalidad.draw_con(board))
                {
                    MessageBox.Show("Empate!");
                    board = new int[6, 7];
                    int random = r.Next(1, 3);
                    turn = random;

                    if (this.turn == 1)
                    {
                        labelTurnColor.ForeColor = Color.Red;
                        labelTurnColor.Text = "RED";

                    }
                    else if (this.turn == 2)
                    {
                        labelTurnColor.ForeColor = Color.DodgerBlue;
                        labelTurnColor.Text = "BLUE";
                    }

                    Invalidate();

                    return;
                }

            }
            else if (row_num_bot == -1)
            {
                // DEBUG - para saber si el bot elije un columna full
                //bad++;
                //Console.WriteLine(bad + "  bad bot " + botRandomPickColumn);
                bot_play(board);

            }

        }

    }
}

