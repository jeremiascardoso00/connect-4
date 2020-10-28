using Connect4.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Connect4
{
    

    public partial class Game1v1 : Form
    {

        private int player1_points;
        private int player2_points;
        private int turn;
        public int[,] board = new int[6, 7];

        Funcionalidad funcionalidad = new Funcionalidad();
        Random r = new Random();
        public Game1v1()
        {
            InitializeComponent();

            // elije por random que jugador empieza primero, azul o rojo.
            int random = r.Next(1, 3);
            turn = random;
            player1_points = 0;
            player2_points = 0;

            funcionalidad.Turn_label_Change(labelTurnColor, turn);

        }

        // dibujo el tablero en el form/control
        private void Game1v1_Paint(object sender, PaintEventArgs e)
        {

            funcionalidad.draw_board(e);
        }

        private void Game1v1_MouseClick(object sender, MouseEventArgs e)
        {
            game1v1_loop(e);

        }

        private void Game1v1_MouseMove(object sender, MouseEventArgs e)
        {
            //for (int i = 0; i < funcionalidad.col_inv.Length; i++)
            //{
            //    if (funcionalidad.col_inv[i].Contains(e.Location))
            //    {
            //        if (i == 0)
            //            pictureBox1.Show();
            //        else if (i == 1)
            //            pictureBox2.Show();
            //        else if (i == 2)
            //            pictureBox3.Show();
            //        else if (i == 3)
            //            pictureBox4.Show();
            //        else if (i == 4)
            //            pictureBox5.Show();
            //        else if (i == 5)
            //            pictureBox6.Show();
            //        else if (i == 6)
            //            pictureBox7.Show();
            //    }
            //    else
            //    {
            //        if (i == 0)
            //            pictureBox1.Hide();
            //        else if (i == 1)
            //            pictureBox2.Hide();
            //        else if (i == 2)
            //            pictureBox3.Hide();
            //        else if (i == 3)
            //            pictureBox4.Hide();
            //        else if (i == 4)
            //            pictureBox5.Hide();
            //        else if (i == 5)
            //            pictureBox6.Hide();
            //        else if (i == 6)
            //            pictureBox7.Hide();
            //    }
            //}
        }

        // gameloop para el modo 1v1
        private void game1v1_loop(MouseEventArgs e)
        {
            Graphics g = CreateGraphics();

            int col_num = funcionalidad.is_valid_location_click(e);

            if (col_num == -1)
            {
                //Console.WriteLine("no es una columna");

            }
            else if (col_num != -1)
            {
                int row_num = funcionalidad.Empty_valid_Row(board, col_num);
                if (row_num != -1)
                {
                    // Parecido al metodo funcionalidad.Turn_label_Change()
                    // esta invertido ya que al hacer click me va a decir el turno siguiente
                    if (this.turn == 1)
                    {
                        labelTurnColor.ForeColor = Color.DodgerBlue;
                        labelTurnColor.Text = "BLUE";

                    }
                    else if (this.turn == 2)
                    {
                        labelTurnColor.ForeColor = Color.Red;
                        labelTurnColor.Text = "RED";
                    }


                    funcionalidad.drop_coin(board, row_num, col_num, turn);

                    funcionalidad.drop_coin_draw(g, row_num, col_num, turn);

                    // crear un metodo after_win ?
                    if (funcionalidad.winning_con(board, turn, g))
                    {
                        if (turn == 1)
                        {
                            MessageBox.Show("RED WINS!");
                            //contador de puntaje
                            player1_points++;
                            labelRED.Text = player1_points.ToString();
                            // reseteo el tablero
                            board = new int[6, 7];
                            // genero un turn random para la prox ronda
                            int random = r.Next(1, 3);
                            turn = random;

                            funcionalidad.Turn_label_Change(labelTurnColor, turn);

                            // Invalidate() hace que el control se vuelva a armar
                            Invalidate();

                            // salgo del metodo gameloop con el return
                            return;
                        }
                        else if (turn == 2)
                        {
                            MessageBox.Show("BLUE WINS!");
                            player2_points++;
                            labelBLUE.Text = player2_points.ToString();
                            board = new int[6, 7];
                            int random = r.Next(1, 3);
                            turn = random;

                            funcionalidad.Turn_label_Change(labelTurnColor, turn);

                            Invalidate();
                            return;
                        }
                    }

                    if (funcionalidad.draw_con(board))
                    {
                        MessageBox.Show("DRAW!");
                        board = new int[6, 7];
                        int random = r.Next(1, 3);
                        turn = random;

                        funcionalidad.Turn_label_Change(labelTurnColor, turn);

                        Invalidate();
                        return;
                    }

                    if (this.turn == 1)
                        this.turn = 2;
                    else if (this.turn == 2)
                        this.turn = 1;

                }
                else if (row_num == -1)
                {
                    MessageBox.Show("Selected column is FULL");
                }
            }
        }

    }
}
