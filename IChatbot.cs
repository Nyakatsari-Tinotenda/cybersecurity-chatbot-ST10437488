using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot
{
    public interface IChatbot
    {
        void GreetUser();
        void ProcessInput(string input);
        void DisplayHelp();
    }
}
