using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Connect4
{
    public class ConexionTcpHost
    {
        public TcpClient TcpClient;
        public StreamReader StreamReader;
        public StreamWriter StreamWriter;
        public Thread ReadThread;

        public delegate void DataCarrier(string data);
        public event DataCarrier OnDataRecieved;

        public delegate void DisconnectNotify();
        public event DisconnectNotify OnDisconnect;

        public delegate void ErrorCarrier(Exception e);
        public event ErrorCarrier OnError;

        public ConexionTcpHost(TcpClient client)
        {
            var ns = client.GetStream();
            this.StreamReader = new StreamReader(ns);
            this.StreamWriter = new StreamWriter(ns);
            this.TcpClient = client;
        }
        private void EscribirMsj(string mensaje)
        {
            try
            {
                this.StreamWriter.Write(mensaje + "\0");
                this.StreamWriter.Flush();
            }
            catch (Exception e)
            {
                if (OnError != null)
                    OnError(e);
            }
        }
        public void EnviarPaquete(Paquete paquete)
        {
            EscribirMsj(paquete);
        }
    }
}
