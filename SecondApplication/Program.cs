using System;
using System.Threading;
using StackExchange.Redis;

namespace SecondApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello from Application2 int the chat!");
            var configuration = new ConfigurationOptions();
            configuration.EndPoints.Add("localhost", 6379);
            
            var redis = ConnectionMultiplexer.Connect(configuration);
            var db = redis.GetDatabase();
            
            Console.WriteLine("Вводите сообщения. Для выхода введите /e");
            string str = "";
            
            //SubScribe
            var channel = redis.GetSubscriber().Subscribe("ChatChannel2");
            channel.OnMessage(message =>
            {
                Console.WriteLine($"Application1:{message}");
            });
            
            while (str != "/e")
            {
                str = Console.ReadLine();
                //Publish
                db.Publish("ChatChannel", str);
                
            }
        }
        
    }
}