using System;
using System.Linq;
using System.Reflection.Metadata;
using StackExchange.Redis;

namespace FirstAplicattion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello from Application1 int the chat!");
            var configuration = new ConfigurationOptions();
            configuration.EndPoints.Add("localhost", 6379);
            
            var redis = ConnectionMultiplexer.Connect(configuration);
            var db = redis.GetDatabase();
            
            Console.WriteLine("Вводите сообщения. Для выхода введите /e");
            string str = "";
            
            var channel = redis.GetSubscriber().Subscribe("ChatChannel");
            channel.OnMessage(message =>
            {
                Console.WriteLine($"Application2:{message}");
            });
            
            while (str != "/e")
            {
                //SubScribe
                str = Console.ReadLine();
                //Publish
                db.Publish("ChatChannel2", str);
                
            }

        }
    }
}
