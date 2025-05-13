using System;
using System.Collections.Generic;
using System.Media;

namespace Chatbot
{
    public abstract class ChatbotBase : IChatbot
    {
        protected string UserName { get; set; }
        protected Random Random { get; } = new Random();

        public abstract void GreetUser();
        public abstract void ProcessInput(string input);
        public abstract void DisplayHelp();

        protected void PlayWelcomeSound()
        {
            try
            {
                var soundPlayer = new SoundPlayer("greeting.wav");
                soundPlayer.Play();
            }
            catch
            {
                Console.WriteLine("[Audio greeting could not be played]");
            }
        }

        protected void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
  ____           _        _____                      
 / ___|_ __ ___ | |__    |_   _|____      _____ _ __ 
| |   | '__/ _ \| '_ \     | |/ _ \ \ /\ / / _ \ '__|
| |___| | | (_) | |_) |    | | (_) \ V  V /  __/ |   
 \____|_|  \___/|_.__/     |_|\___/ \_/\_/ \___|_|   
");
            Console.ResetColor();
        }

        protected string GetRandomResponse(List<string> responses)
        {
            return responses[Random.Next(responses.Count)];
        }

        protected void DisplayResponse(string response)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== Cybersecurity Bot ===");
            Console.ResetColor();
            Console.WriteLine(response);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n________________________\n");
            Console.ResetColor();
        }
    }
}
