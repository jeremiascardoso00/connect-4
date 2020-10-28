using System;
using System.Drawing;
using System.Windows.Forms;


namespace Connect4
{
    public partial class GameOnlineJugador2 : Form
    {
        internal bool Esmiturno = false;
        internal int player1_points;
        internal int player2_points;
        internal int col_num;
        internal int row_num;
        internal int turn = 2;
        private string posicion;
        internal int[,] board = new int[6, 7];
        private Graphics g;
        private bool Conectado = false;

        private string[] Nombres = new string[2];

        Funcionalidad funcionalidad = new Funcionalidad();

        public GameOnlineJugador2()
        {
            //tiene un par de bugs 
            //anda probandolo con 2 instancias y fijate, por ahi al jugador 2 le deja jugar 2 veces
            //medio raro no se como puta hacer eso

            InitializeComponent();


            player1_points = 0;
            player2_points = 0;

            this.col_num = 0;
            this.row_num = 0;

            //coloco un 1 porque el rojo siempre empieza primero
            funcionalidad.Turn_label_Change(labelTurnColor, 1);

        }

        // dibujo el tablero en el form/control
        private void Game1v1_Paint(object sender, PaintEventArgs e)
        {

            funcionalidad.draw_board(e);
        }

        private void Game1v1_MouseClick(object sender, MouseEventArgs e)
        {
            if (Esmiturno)
            {
                game1v1_loop(e);
            }
            else
            {

            }
        }

        private void Game1v1_MouseMove(object sender, MouseEventArgs e)
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

        // gameloop para el modo online
        private void game1v1_loop(MouseEventArgs e)
        {
            //con esto se fija en que el lugar del click coincida con las coordenadas establecidas
            this.col_num = funcionalidad.is_valid_location_click(e);

            //si el lugar del click no es valido
            if (this.col_num == -1)
            {
                //Console.WriteLine("no es una columna");

            }
            //si el lugar del click es valido
            else if (this.col_num != -1)
            {
                //revisa si la fila en la columna ingresada anteriormente por el click esta vacia
                this.row_num = funcionalidad.Empty_valid_Row(board, this.col_num);

                //si efectivamente esta vacia entonces sucede lo siguiente.
                if (this.row_num != -1)
                {
                    // Parecido al metodo funcionalidad.Turn_label_Change()
                    // esta invertido ya que al hacer click me va a decir el turno siguiente
                    //if (this.turn == 1)
                    //{
                    //    labelTurnColor.ForeColor = Color.DodgerBlue;
                    //    labelTurnColor.Text = "BLUE";

                    //}
                    //else if (this.turn == 2)
                    //{
                    //    labelTurnColor.ForeColor = Color.Red;
                    //    labelTurnColor.Text = "RED";
                    //}

                    //dibuja la ficha, esto debemos llamar cuando actualicemos

                    PintarCasilla(this.col_num, this.row_num, 2);


                    //controlar ganador
                    ControlarGanador(2);

                    //controlar empate
                    ControlarEmpate();

                    CambiarDeTurno(2);
                    //aca tengo que enviar la columna y la fila separadas
                    //por una coma, de esta forma:
                    //3,5

                    posicion = this.col_num.ToString();

                    posicion += "," + this.row_num.ToString();
                    conexionTcp.EnviarPaquete(new Paquete(posicion));
                }
                else if (this.row_num == -1)
                {
                    MessageBox.Show("Selected column is FULL");
                }
            }
        }

        public void CambiarDeTurno(int turno)
        {
            if (turno == 1)
            {
                labelTurnColor.ForeColor = Color.DodgerBlue;
                labelTurnColor.Text = "BLUE";
                this.turn = 2;
                Habilitar();
                return;

            }
            else if (turno == 2)
            {
                labelTurnColor.ForeColor = Color.Red;
                labelTurnColor.Text = "RED";
                this.turn = 1;
                
                Deshabilitar();
                return;
            }
        }

        public void PintarCasilla(int col_num, int row_num, int turn)
        {
            this.g = CreateGraphics();
            funcionalidad.drop_coin(board, row_num, col_num, turn);

            funcionalidad.drop_coin_draw(g, row_num, col_num, turn);
        }

        public void ControlarGanador(int turn)
        {
            Graphics g = CreateGraphics();
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

                    funcionalidad.Turn_label_Change(labelTurnColor, turn);

                    Invalidate();
                    return;
                }
            }
        }

        public void ControlarEmpate()
        {
            if (funcionalidad.draw_con(board))
            {
                MessageBox.Show("DRAW!");
                board = new int[6, 7];
                //int random = r.Next(1, 3);
                //turn = random;

                funcionalidad.Turn_label_Change(labelTurnColor, turn);

                Invalidate();
                return;
            }
        }

        private void Salir()
        {
            Environment.Exit(0);
        }
        private void GameOnlineJugador2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Salir();
        }

        private void Deshabilitar()
        {
            this.Esmiturno = false;
        }

        private void Habilitar()
        {
            this.Esmiturno = true;
            //esto era lo que hacia que se refresque xd
            //this.Refresh();
        }

        //con esta clase descifrar mensaje separo el mensaje recibido
        //(posicion) en columnas y filas para enviarlo a mi clase pintar 
        //casillas(), bueno y tambien le llevo el jugador para que pinte con
        //el color correspondiente a cada uno
        //siempre que tenga que descifrar un mensaje, va a ser el otro jugador
        //entonces pongo directamente un 1 en el argumento de la llamada
        private void DescifrarMensaje(string posicion)
        {
            Invoke(new Action(() => Deshabilitar()));
            int[] arregloPos = new int[2];

            string[] posicionesStr = posicion.Split(',');

            arregloPos[0] = int.Parse(posicionesStr[0]);
            arregloPos[1] = int.Parse(posicionesStr[1]);

            //aca siempre sera el jugador 1
            PintarCasilla(arregloPos[0], arregloPos[1], 1);

            ControlarEmpate();
            
            Invoke(new Action(() => ControlarGanador(1)));
            Invoke(new Action(() => CambiarDeTurno(1)));
        }
        ///////////////////////////conexion cliente - host

        public static ConexionTcpCliente conexionTcp = new ConexionTcpCliente();
        public static string IPADDRESS = "127.0.0.1";
        public const int PORT = 33456;

        private void GameOnlineJugador2_Load(object sender, EventArgs e)
        {
            Nombres[1] = "Jugador 2";
            lbl_azul2.Text = Nombres[1];
            
            conexionTcp.OnDataRecieved += MensajeRecibido;
            conexionTcp.OnDisconnect += ConexionCerrada;

            if (!conexionTcp.Conectar(IPADDRESS, PORT))
            {
                MessageBox.Show("Error conectando con el host", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Conexion establecida con el host, juego iniciado", "Juego iniciado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexionTcp.EnviarPaquete(new Paquete(Nombres[0]));
            }
        }

        private void ConexionCerrada()
        {
            MessageBox.Show("CONEXION TERMINADA, EL JUEGO SE CERRARÁ", "JUEGO TERMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Salir();
        }


        private void MensajeRecibido(string datos)
        {


            if (!Conectado)
            {
                Nombres[0] = datos;
                Conectado = true;
                Invoke(new Action(() => lbl_rojo2.Text = Nombres[0]));
                return;
            }

            var paquete = new Paquete(datos);
            string posicion = paquete.posicion;

            DescifrarMensaje(posicion);
            Invoke(new Action(() => Habilitar()));
        }

        

        
    }
}
