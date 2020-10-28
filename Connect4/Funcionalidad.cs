using Connect4.Properties;
using System.Drawing;
using System.Windows.Forms;


namespace Connect4
{
    public class Funcionalidad : Control
    {
        ///<summary> Columnas invisibles: me ayudan a saber en que columna hice click, estan hardcodeadas 
        ///ya que el tablero no cambia de tamaño.
        ///Es un pseudo hitbox
        ///Es una ayuda mas que algo necesario ya que el valor de las "x" va de 100 en 100
        ///</summary>
        public Rectangle[] col_inv = new Rectangle[] {  new Rectangle(13 + 85 * 0, 48, 82, 520) ,
                                                        new Rectangle(13 + 85 * 1, 48, 82, 520) ,
                                                        new Rectangle(13 + 85 * 2, 48, 82, 520) ,
                                                        new Rectangle(13 + 85 * 3, 48, 82, 520) ,
                                                        new Rectangle(13 + 85 * 4, 48, 82, 520) ,
                                                        new Rectangle(13 + 85 * 5, 48, 82, 520) ,
                                                        new Rectangle(13 + 85 * 6, 48, 82, 520) ,};

        const int ROW = 6;
        const int COL = 7;

        /// <summary> Dibuja el tablero utilizando la API de windows GDI+ 
        /// La base del tablero es un rectangulo con un tamaño fijo.
        /// Los slots donde "caen" la fichas son circulos con un tamaño fijo y tienen una division arbitraria
        /// Este metodo esta relacionado con con el array de rectangulos cool_inv 
        /// </summary>
        public void draw_board(PaintEventArgs e)
        {
            // dibujo la base del tablero
            Bitmap image1 = (Bitmap)Resources.wood;

            TextureBrush texture = new TextureBrush(image1);
            texture.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;

            Pen pen2 = new Pen(Color.Black, 7);
            e.Graphics.DrawRectangle(pen2, 5, 48, 610, 520);
            e.Graphics.FillRectangle(Brushes.MidnightBlue, 5, 48, 610, 520);

            // Metodo similar a la represntacion de una matriz en consola.
            for (int row = 0; row < ROW; row++)
            {
                for (int col = 0; col < COL; col++)
                {
                    // sombreado del slot
                    //e.Graphics.FillEllipse(Brushes.Black, 21 + 85 * j, 64 + 85 * i, 68, 68);

                    // dibujo los slots donde van a caer las fichas 
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.FillEllipse(texture, 21 + col * 85, 64 + row * 85, 64, 64);
                    Pen pen1 = new Pen(Color.Goldenrod, 2);
                    e.Graphics.DrawEllipse(pen1, 21 + col * 85, 64 + row * 85, 64, 64);
                }
            }

            // hitbox de las columnas - DEBUG
            //Pen pen3 = new Pen(Color.Black, 2);

            //for (int i = 0; i < col_inv.Length; i++)
            //{
            //    e.Graphics.DrawRectangle(pen3, 13 + 85 * i, 48, 82, 520);
            //}
        }

        ///<summary>
        /// Me indica si hice click en alguna de las columnas "invisibles"(col_inv), si el .location del mouse coincide con el contenido de col_inv[i]
        /// entonces me devuelve el indice "i" es decir el numero de columna.
        /// Si clickeamos afuera de los valores declarados en el array "col_inv" significa que clickeamos afuera del tablero. Devuelve -1
        /// </summary>
        public int is_valid_location_click(MouseEventArgs mouse)
        {
            for (int col = 0; col < this.col_inv.Length; col++)
            {
                if (this.col_inv[col].Contains(mouse.Location))
                {
                    //DEBUG
                    //Console.WriteLine(col);
                    //Console.WriteLine(col_inv[i].ToString());
                    return col;
                }
            }

            int afuera = -1;
            // debug
            // Console.WriteLine(afuera);
            return afuera;

        }

        /// <summary> Nos devuelve la primera instancia donde en la posicion [i,col] es igual a cero
        /// es decir esta vacio. Si es diferente de 0 significa que la columna esta llena y
        /// se devuelve un indice inalcanzable (-1).
        /// </summary>
        public int Empty_valid_Row(int[,] board, int col)
        {
            /* int row = ROW(6) seria la ultima fila y desde ahi empezamos a *subir* (basicamente rotamos el array)
             * Nos fijamos la "primera" fila de una columna y preguntamos si tiene un valor.
             * Si esta con un valor diferente de 0 (1 o 2) significa que esa columna esta llena
             */
            for (int row = ROW - 1; row >= 0; row--)
            {
                // == 0 es que no hay una ficha en ese index
                if (board[row, col] == 0)
                {
                    return row;
                }
            }

            // -1 significa que la fila esta llena
            return -1;
        }

        /// <summary> Soltamos una ficha en el tablero.
        /// Asignamos el valor del turn (1 o 2) a la posicion [row,col] en un array multidimensional
        /// </summary>
        public void drop_coin(int[,] board, int row, int col, int turn)
        {
            board[row, col] = turn;

            //DEBUG - Muestra un array por consola.
            //for (int i = 0; i < board.GetLength(0); i++)
            //{
            //    for (int j = 0; j < board.GetLength(1); j++)
            //    {
            //        Console.Write(string.Format("{0} ", board[i, j]));
            //    }
            //    Console.Write(Environment.NewLine + Environment.NewLine);
            //}

        }

        ///<summary> Nos dice la primera instancia donde se dan 4 fichas seguidas.
        ///puede ser vertical, horizontal, diagonal positiva "\" y diagonal negativa "/"
        /// se dan cuando el valor de "turn" se da 4 veces seguidas en posiciones adyacentes en el arreglo multidimensional "board".
        /// Tambien hago un remarco a las fichas ganadoras usango Graphics (mover a otro metodo aparte).
        /// </summary>
        public bool winning_con(int[,] board, int turn, Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // vertical
            for (int row = 0; row < ROW - 3; row++)
            {
                for (int col = 0; col < COL; col++)
                {
                    if (board[row, col] == turn && board[row + 1, col] == turn && board[row + 2, col] == turn && board[row + 3, col] == turn)
                    {
                        g.FillEllipse(Brushes.Green, 37 + col * 85, 80 + (row) * 85, 32, 32);
                        g.FillEllipse(Brushes.Green, 37 + col * 85, 80 + (row + 1) * 85, 32, 32);
                        g.FillEllipse(Brushes.Green, 37 + col * 85, 80 + (row + 2) * 85, 32, 32);
                        g.FillEllipse(Brushes.Green, 37 + col * 85, 80 + (row + 3) * 85, 32, 32);
                        return true;
                    }
                }
            }

            // horizontal
            for (int row = 0; row < ROW; row++)
            {
                for (int col = 0; col < COL - 3; col++)
                {
                    if (board[row, col] == turn && board[row, col + 1] == turn && board[row, col + 2] == turn && board[row, col + 3] == turn)
                    {
                        // Dibujo un circulo en medio de las fichas para señalar fichas ganadoras
                        // circulos pequeños 64/2 = 32 => 32/2 = 16
                        // tengo que sumar 16 a los padings de "x" e "y"

                        g.FillEllipse(Brushes.Green, 37 + col * 85, 80 + row * 85, 32, 32);
                        g.FillEllipse(Brushes.Green, 37 + (col + 1) * 85, 80 + row * 85, 32, 32);
                        g.FillEllipse(Brushes.Green, 37 + (col + 2) * 85, 80 + row * 85, 32, 32);
                        g.FillEllipse(Brushes.Green, 37 + (col + 3) * 85, 80 + row * 85, 32, 32);
                        return true;
                    }
                }
            }

            // diagonal positiva "\"
            for (int row = 0; row < ROW - 3; row++)
            {
                for (int col = 0; col < COL - 3; col++)
                {
                    if (board[row, col] == turn && board[row + 1, col + 1] == turn && board[row + 2, col + 2] == turn && board[row + 3, col + 3] == turn)
                    {
                        g.FillEllipse(Brushes.Green, 37 + (col) * 85, 80 + (row) * 85, 32, 32);
                        g.FillEllipse(Brushes.Green, 37 + (col + 1) * 85, 80 + (row + 1) * 85, 32, 32);
                        g.FillEllipse(Brushes.Green, 37 + (col + 2) * 85, 80 + (row + 2) * 85, 32, 32);
                        g.FillEllipse(Brushes.Green, 37 + (col + 3) * 85, 80 + (row + 3) * 85, 32, 32);
                        return true;
                    }
                }
            }

            // digonal negativa "/"
            // empieza por el 3 por que de otra manera se va del index
            for (int row = 3; row < ROW; row++)
            {
                for (int col = 0; col < COL - 3; col++)
                {
                    if (board[row, col] == turn && board[row - 1, col + 1] == turn && board[row - 2, col + 2] == turn && board[row - 3, col + 3] == turn)
                    {
                        g.FillEllipse(Brushes.Green, 37 + (col) * 85, 80 + (row) * 85, 32, 32);
                        g.FillEllipse(Brushes.Green, 37 + (col + 1) * 85, 80 + (row - 1) * 85, 32, 32);
                        g.FillEllipse(Brushes.Green, 37 + (col + 2) * 85, 80 + (row - 2) * 85, 32, 32);
                        g.FillEllipse(Brushes.Green, 37 + (col + 3) * 85, 80 + (row - 3) * 85, 32, 32);
                        return true;
                    }
                }
            }

            return false;
        }

        ///<summary> Checkea en cada columna si la ultima fila esta llena (fila superior graficamente)
        ///</summary>
        public bool draw_con(int[,] board)
        {

            if (Empty_valid_Row(board, 0) == -1 && Empty_valid_Row(board, 1) == -1 && Empty_valid_Row(board, 2) == -1 && Empty_valid_Row(board, 3) == -1 && Empty_valid_Row(board, 4) == -1 && Empty_valid_Row(board, 5) == -1 && Empty_valid_Row(board, 6) == -1)
            {
                return true;
            }
            return false;
        }

        /// <summary> Cambia el label indicador del turno segun el valor turn
        /// 
        /// </summary>
        public void Turn_label_Change(Label label_turn, int turn)
        {
            if (turn == 1)
            {
                label_turn.ForeColor = Color.Red;
                label_turn.Text = "RED";

            }
            else if (turn == 2)
            {
                label_turn.ForeColor = Color.DodgerBlue;
                label_turn.Text = "BLUE";
            }
        }

        /// <summary> Dibuja la ficha en la posicion que corresponde.
        ///  El color de la ficha va a depender del turno actual
        /// </summary>
        public void drop_coin_draw(Graphics g, int row_num, int col_num, int turn)
        {
            if (turn == 1)
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(Brushes.Red, 21 + col_num * 85, 64 + row_num * 85, 64, 64);
            }
            else if (turn == 2)
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(Brushes.DodgerBlue, 21 + col_num * 85, 64 + row_num * 85, 64, 64);
            }
        }
    }


}
