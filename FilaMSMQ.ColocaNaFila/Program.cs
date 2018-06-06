using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FilaMSMQ.ColocaNaFila
{
    public class Program
    {
        static void Main(string[] args)
        {
            string nomeFila = @".\private$\MinhaPrimeiraFila";
            MessageQueue queue = new MessageQueue();
            queue.Path = nomeFila;
            Message message = new Message();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Digite o Nome");
                var nomeDigitado = Console.ReadLine();
                Console.WriteLine("Digite a Idade");
                var idadeDigitada = Console.ReadLine();
                message.Body = JsonConvert.SerializeObject(new Modelo() { Nome = nomeDigitado, Idade = int.Parse(idadeDigitada) });
                queue.Send(message);
            }
            
            Console.WriteLine("FIM");
            Console.ReadKey();

        }
    }
}
