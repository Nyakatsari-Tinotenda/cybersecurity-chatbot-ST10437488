using System;
using System.Media;
using System.Media;
namespace Chatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Chatbot";

            var chatbot = new Chatbot();
            chatbot.GreetUser();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nYou: ");
                Console.ResetColor();

                var input = Console.ReadLine();

                if (input?.ToLower() == "exit" || input?.ToLower() == "quit")
                {
                    Console.WriteLine("Goodbye! Stay safe online!");
                    break;
                }

                chatbot.ProcessInput(input);
            }
        }
    }
}