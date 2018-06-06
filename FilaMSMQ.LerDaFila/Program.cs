using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FilaMSMQ.LerDaFila
{
    public class Program
    {
        private static MessageQueue _queue;


        static void Main(string[] args)
        {
            string nomeFila = @".\private$\MinhaPrimeiraFila";

            _queue = new MessageQueue();
            _queue.Path = nomeFila;
            _queue.ReceiveCompleted += new ReceiveCompletedEventHandler(ProcessaFila);
            _queue.Formatter = new XmlMessageFormatter(new[] { typeof(string) });
            _queue.BeginReceive();
            Console.WriteLine("Tecle algo para sair");
            Console.ReadKey();
        }

        private static void ProcessaFila(object sender, ReceiveCompletedEventArgs e)
        {
            _queue.EndReceive(e.AsyncResult);
            object itemDaFila = e.Message.Body;
            var modelo = JsonConvert.DeserializeObject<Modelo>(itemDaFila.ToString());
            Console.WriteLine($"O Nome e {modelo.Nome} , idade {modelo.Idade}");
            _queue.BeginReceive();//Vai buscar outro item da fila
        }
    }
}
