using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;


namespace Connect4
{
    public partial class GameOnlineJugador1 : Form
    {
        internal bool Esmiturno = false;
        internal int player1_points;
        internal int player2_points;
        internal int col_num;
        internal int row_num;
        internal int turn = 1;
        private string posicion;
        internal int[,] board = new int[6, 7];
        private Graphics g;
        private bool Conectado = false;

        private string[] Nombres = new string[2];

        Funcionalidad funcionalidad = new Funcionalidad();
        
        

        public GameOnlineJugador1()
        {
            InitializeComponent();

            
            player1_points = 0;
            player2_points = 0;

            this.col_num = 0;
            this.row_num = 0;
            funcionalidad.Turn_label_Change(labelTurnColor, turn);

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

                    PintarCasilla(this.col_num, this.row_num, 1);

                    //controlar ganador
                    ControlarGanador(1);

                    //controlar empate
                    ControlarEmpate();

                    CambiarDeTurno(1);
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

        public void CambiarDeTurno( int turno)
        {
            if (turno == 1)
            {
                labelTurnColor.ForeColor = Color.DodgerBlue;
                labelTurnColor.Text = "BLUE";
                this.turn = 2;
                Deshabilitar();
                return;

            }
            /*esto esta pensado para que le host sea el jugador 1 en todos los casos
            de modo que ya sabemos que numero de jugador tienen ambos podemos hacer lo siguiente*/
            else if (turno == 2)
            {
                labelTurnColor.ForeColor = Color.Red;
                labelTurnColor.Text = "RED";
                this.turn = 1;
                Habilitar();
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

        private void Deshabilitar()
        {
            this.Esmiturno = false;
        }

        private void Habilitar()
        {
            this.Esmiturno = true;
            //this.Refresh();
        }

        private void GameOnlineJugador1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Salir();
        }

        //con esta clase descifrar mensaje separo el mensaje recibido
        //(posicion) en columnas y filas para enviarlo a mi clase pintar 
        //casillas(), bueno y tambien le llevo el jugador para que pinte con
        //el color correspondiente a cada uno
        //siempre que tenga que descifrar un mensaje, va a ser el otro jugador
        //entonces pongo directamente un 2 en el argumento de la llamada
        private void DescifrarMensaje(string posicion)
        {
            Invoke(new Action(() => Deshabilitar()));
            int[] arregloPos = new int[2];

            string[] posicionesStr = posicion.Split(',');

            arregloPos[0] = int.Parse(posicionesStr[0]);
            arregloPos[1] = int.Parse(posicionesStr[1]);

            PintarCasilla(arregloPos[0], arregloPos[1], 2);
            
            ControlarEmpate();
            
            Invoke(new Action(() => ControlarGanador(2)));
            Invoke(new Action(() => CambiarDeTurno(2))); 
        }
        

        //Necesario para la conexion en modo host
        #region Cosas de la conexion del host


        public delegate void ClientCarrier(ConexionTcpHost conexionTcp);
        public event ClientCarrier OnClientConnected;
        public event ClientCarrier OnClientDisconnected;
        public delegate void DataRecieved(ConexionTcpHost conexionTcp, string data);
        public event DataRecieved OnDataRecieved;
        public ConexionTcpHost conexionTcp;

        private TcpListener _tcpListener;
        private Thread _acceptThread;

        private void GameOnlineJugador1_Load(object sender, EventArgs e)
        {
            //al inicio en el otro programa pide el nombre del jugador
            //nosotros podemos hacerlo como no
            //por ahora voy a colocar en el metodo de carga de ambos jugadores 
            //que su nombre sea "jugador 1" para  el host
            //y "jugador 2" para el cliente, esto es facil de cambiar
            Nombres[0] = "Jugador 1";
            lbl_rojo.Text = Nombres[0];
            OnDataRecieved += MensajeRecibido;
            OnClientConnected += ConexionRecibida;
            OnClientDisconnected += ConexionCerrada;

            EscucharClientes("127.0.0.1", 33456);
        }
        

        private void MensajeRecibido(ConexionTcpHost conexionTcp, string datos)
        {
            //cuando se conecta por primera vez el cliente esto de abajo cambia el bool conectado a true
            //conectado va a ser falso hasta que se conecte por primera vez el cliente
            if (!Conectado)
            {
                this.conexionTcp = conexionTcp;
                //ese mensaje enviado en la primer conexion es el nombre del cliente(datos)
                Nombres[1] = datos;
                Conectado = true;
                //ahora escribimos el nombre del cliente en el form
                Invoke(new Action(() => lbl_azul.Text = Nombres[1]));
                return;
            }

            //aca envia la posicion de la anterior jugada del cliente
            var paquete = new Paquete(datos);
            string posicion = paquete.posicion;

            //el lugar es un numero entero en este caso
            //en el otro programa manda un entero del 0 al 67 y mediante el "#" y el "X"
            //elije si una ficha es roja o azul, en nuestro caso mandamos columnas y filas
            //el sistema de identificacion nosotros lo tenemos como:
            //1:jugador1_rojo
            //2:jugador2_azul
            //entonces en vez de "#" usariamos un "2"
            //y en vez de un "X" usariamos un "1".
            DescifrarMensaje(posicion);

            //la primera vez nuestras fichas ya estan 
            //habilitadas, deberiamos poner que por defecto comiencen desabilitadas
            //cosa que no salte un error hevy cuando iniciamos y no se conecto
            //todavia el pibe cliente
            Invoke(new Action(() => Habilitar()));
        }

        private void ConexionRecibida(ConexionTcpHost conexionTcp)
        {
            MessageBox.Show("CONEXION INICIADA", "JUEGO INICIADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            conexionTcp.EnviarPaquete(new Paquete(Nombres[0]));
            Invoke(new Action(() => Habilitar()));

        }

        private void ConexionCerrada(ConexionTcpHost conexionTcp)
        {
            MessageBox.Show("CONEXION TERMINADA, EL JUEGO SE CERRARÁ", "JUEGO TERMINADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Salir();
        }

        private void EscucharClientes(string ipAddress, int port)
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
                _tcpListener.Start();
                _acceptThread = new Thread(AceptarClientes);
                _acceptThread.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        private void AceptarClientes()
        {
            do
            {
                try
                {

                    //Resumen: AcceptTcpClient();
                    //     Acepta una solicitud de conexión pendiente.
                    //
                    // Devuelve:
                    //     System.Net.Sockets.TcpClient que se utiliza para enviar y recibir datos.
                    //
                    var conexion = _tcpListener.AcceptTcpClient();
                    var srvClient = new ConexionTcpHost(conexion)
                    {
                        ReadThread = new Thread(LeerDatos)
                    };
                    srvClient.ReadThread.Start(srvClient);

                    if (OnClientConnected != null)
                        OnClientConnected(srvClient);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }

            } while (true);
        }

        //esto pasa en un nuevo hilo
        private void LeerDatos(object client)
        {
            var cli = client as ConexionTcpHost;
            var charBuffer = new List<int>();

            do
            {
                try
                {

                    if (cli == null)
                        break;
                    if (cli.StreamReader.EndOfStream)
                        break;
                    int charCode = cli.StreamReader.Read();
                    if (charCode == -1)
                        break;
                    if (charCode != 0)
                    {
                        //lee los caracteres pero los lee hechos enteros
                        charBuffer.Add(charCode);
                        continue;
                    }
                    //se fija si ya no existe una conexion hecha con algun cliente y si no es asi hace lo siguiente
                    //si todavia no recibio ningun mensaje
                    if (OnDataRecieved != null)
                    {
                        //sacamos el largo del mensaje para recorrerlo
                        var chars = new char[charBuffer.Count];
                        //convierto todos los codigos de caracteres a caracteres
                        for (int i = 0; i < charBuffer.Count; i++)
                        {
                            //ACA LOS CONVIERTE AL CARACTER QUE REPRESENTAN
                            chars[i] = Convert.ToChar(charBuffer[i]);
                        }
                        //ACA ARMA UN STRING CON EL ARREGLO DE CARACTERES QUE ANTES ERA UN ARREGLO 
                        //DE ENTEROS QUE REPRESENTABA CODIGOS DE CARACTERES
                        //convierto el arreglo de caracteres a string
                        var message = new string(chars);

                        //invocamos nuestro evento
                        OnDataRecieved(cli, message);
                    }
                    charBuffer.Clear();
                }
                catch (IOException)
                {
                    break;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());

                    break;
                }
            } while (true);

            if (OnClientDisconnected != null)
                OnClientDisconnected(cli);
        }

        #endregion

        
    }
}
