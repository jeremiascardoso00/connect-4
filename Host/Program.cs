using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    [ServiceContract]
    public interface IChatClient
    {
        [OperationContract(IsOneWay = true)]
        void RecieveMessage(string user, string message);

        [OperationContract(IsOneWay = true)]
        void RecieveTurno(int turno);


    }

    [ServiceContract(CallbackContract = typeof(IChatClient))]
    public interface IChatService
    {

        [OperationContract(IsOneWay = true)]
        void Join(string username);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);

        [OperationContract(IsOneWay = true)]
        void MandarTurno(int turno);


    }

    [ServiceBehavior(ConcurrencyMode= ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        Dictionary<IChatClient, string> _users = new Dictionary<IChatClient, string>();

        public void Join(string username)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            _users[connection] = username;

        }
        public void MandarTurno(int turno)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            string user;
            //ese out devuelve el nombre del que mando el mensaje
            if (!_users.TryGetValue(connection, out user))
                return;
            foreach (var other in _users.Keys)
            {
                if (other == connection)
                    continue;
                //douuuu aca manda el mensaje al otro
                other.RecieveTurno(turno);
            }
        }

        
        //con esto sube el mensaje al host
        public void SendMessage(string message)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            string user;
            //ese out devuelve el nombre del que mando el mensaje
            if (!_users.TryGetValue(connection, out user)) 
                return;
            foreach(var other in _users.Keys)
            {
                if (other == connection)
                    continue;
                //douuuu aca manda el mensaje al otro
                other.RecieveMessage(user, message);
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ChatService));
            host.Open();
            Console.WriteLine("Servicio listo");
            Console.ReadLine();
            host.Close();
            
        }

        
    }
}
