using System;
using System.Media;
using System.Threading;

namespace ChatBot
{
    class Program
    {
        static void Main(string[] args)
        {

            // Display ASCII art
            DisplayAsciiArt();

            // Text-based greeting and user interaction
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║  CYBERSECURITY AWARENESS CHATBOT       ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");
            Console.ResetColor();

            Console.Write("Please enter your name: ");
            string userName = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nHello, {userName}! I'm your Cybersecurity Awareness Assistant.");
            Console.ResetColor();

            // Display initial help message
            DisplayHelp();

            // Main conversation loop
            bool continueChat = true;
            while (continueChat)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nYour question: ");
                Console.ResetColor();

                string userInput = Console.ReadLine().ToLower();

                if (userInput == "exit")
                {
                    continueChat = false;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\nGoodbye! Stay safe online!");
                    Console.ResetColor();
                    continue;
                }
                else if (userInput == "help")
                {
                    DisplayHelp();
                    continue;
                }

                // Process user input
                string response = GetResponse(userInput);

                // Simulate typing effect
                Console.ForegroundColor = ConsoleColor.Cyan;
                foreach (char c in response)
                {
                    Console.Write(c);
                    Thread.Sleep(20); // Small delay for typing effect
                }
                Console.WriteLine();
                Console.ResetColor();
            }
        }

        static void DisplayHelp()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nHere are the types of questions I can answer:");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("General Questions:");
            Console.WriteLine("4 or 'how are you?' - Check my status");
            Console.WriteLine("5 or 'what's your purpose?' - Learn about my mission");

            Console.WriteLine("\nPassword Safety (type '1' or 'password'):");
            Console.WriteLine("- How to create a strong password");
            Console.WriteLine("- What makes a good password");
            Console.WriteLine("- Should I use the same password everywhere");
            Console.WriteLine("- What is a password manager");

            Console.WriteLine("\nPhishing Scams (type '2' or 'phishing'):");
            Console.WriteLine("- What is phishing");
            Console.WriteLine("- How to spot a phishing email");
            Console.WriteLine("- What to do if I clicked a phishing link");
            Console.WriteLine("- Examples of phishing scams");

            Console.WriteLine("\nSafe Browsing (type '3' or 'browsing'):");
            Console.WriteLine("- How to browse safely online");
            Console.WriteLine("- What is HTTPS");
            Console.WriteLine("- Is public Wi-Fi safe");
            Console.WriteLine("- What is a VPN");

            Console.WriteLine("\nType 'exit' to end our conversation");
            Console.WriteLine("Type 'help' to see this list again");
            Console.ResetColor();
        }

    
        static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"
   _____ _           _   ____        _   
  / ____| |         | | |  _ \      | |  
 | |    | |__   __ _| |_| |_) | ___ | |_ 
 | |    | '_ \ / _` | __|  _ < / _ \| __|
 | |____| | | | (_| | |_| |_) | (_) | |_ 
  \_____|_| |_|\__,_|\__|____/ \___/ \__|
");
            Console.ResetColor();
        }

        static string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "I didn't quite understand that. Type 'help' to see what you can ask.";
            }

            if (input.Contains("how are you") || input == "4")
            {
                return "I'm just a bot, but I'm functioning perfectly! Ready to help you with cybersecurity questions.";
            }
            else if (input.Contains("purpose") || input == "5")
            {
                return "My purpose is to help South African citizens stay safe online by providing cybersecurity awareness information.";
            }
            else if (input.Contains("password") || input == "1")
            {
                return "Password safety is crucial! Always use:\n" +
                       "- Strong, unique passwords for each account\n" +
                       "- A password manager to keep track\n" +
                       "- Two-factor authentication when available\n" +
                       "- Never share your passwords with anyone\n\n" +
                       "You can ask me more specific questions about passwords (see 'help').";
            }
            else if (input.Contains("phish") || input == "2")
            {
                return "Phishing scams try to trick you into giving personal information.\n" +
                       "⚠️ Warning signs:\n" +
                       "- Unexpected emails asking for details\n" +
                       "- Urgent or threatening language\n" +
                       "- Suspicious links or attachments\n\n" +
                       "Ask me for specific phishing examples or prevention tips (see 'help').";
            }
            else if (input.Contains("brows") || input == "3")
            {
                return "For safe browsing:\n" +
                       "✓ Use HTTPS websites (look for the padlock icon)\n" +
                       "✓ Keep your browser updated\n" +
                       "✓ Avoid public Wi-Fi for sensitive tasks\n" +
                       "✓ Use a VPN if possible\n\n" +
                       "I can provide more details about any of these (see 'help').";
            }
            else
            {
                return "I didn't understand that question. Type 'help' to see the list of questions I can answer.";
            }
        }
    }
}