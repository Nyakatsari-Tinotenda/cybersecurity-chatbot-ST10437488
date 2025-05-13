using System;
using System.Collections.Generic;

namespace Chatbot
{
    public class Chatbot : ChatbotBase
    {
        private readonly Dictionary<string, string> _userMemory = new();
        private readonly Dictionary<string, List<string>> _keywordResponses = new();
        private readonly Dictionary<string, int> _keywordResponseIndex = new();
        private readonly List<string> _defaultResponses = new();
        private readonly List<string> _howAreYouResponses = new();
        private readonly List<string> _purposeResponses = new();
        private readonly HashSet<string> _knownUsers = new();
        private string _lastTopic = "";

        public Chatbot()
        {
            InitializeResponses();
        }

        private void InitializeResponses()
        {
            _howAreYouResponses.AddRange(new[]
            {
                "I'm operating at full capacity, scanning threats and educating minds!",
                "I don't have emotions, but I'm secure and functional—just how a bot should be.",
                "All systems are green—ready to tackle any cybersecurity question you throw my way!",
                "Feeling cyber-strong and alert. How can I assist?"
            });

            _purposeResponses.AddRange(new[]
            {
                "My purpose is to empower you with cybersecurity knowledge and awareness.",
                "I'm here to help you stay safe online and prevent digital threats.",
                "Think of me as your cyber-coach—protecting and guiding you in the digital world.",
                "Educating one user at a time to build a more cyber-resilient community."
            });

            _defaultResponses.AddRange(new[]
            {
                "I'm not sure I follow. Could you ask something related to cybersecurity?",
                "That’s outside my training. Try asking about phishing, malware, firewalls, or data protection.",
                "Cybersecurity is my thing—maybe you'd like to learn about passwords or scams?",
                "Let's keep things secure—try asking about digital threats or online safety tips."
            });

            _keywordResponses["password"] = new List<string>
{
    "Passwords should be long (at least 12 characters), include symbols, and be unique for every site. Password safety is a critical aspect of cybersecurity that helps protect your personal and sensitive information from unauthorized access. A strong password should be long, include a mix of upper and lower case letters, numbers, and special characters, and avoid easily guessed words like your name or birthdate. It's important to use different passwords for different accounts so that if one is compromised, the others remain secure.",
    "Using a passphrase like 'TrickyDuck!FlewOver99Hills' is both strong and memorable. Using a reputable password manager can help you generate and store complex passwords safely. Additionally, enabling two-factor authentication (2FA) adds an extra layer of protection, making it significantly harder for attackers to gain access even if they obtain your password.",
    "Never reuse your passwords across services—this is a major risk if one site is breached. Password safety involves creating and maintaining secure passwords to protect your online accounts from cyber threats. A secure password should be at least 12 characters long and should not contain predictable patterns such as '123456' or 'password.' Instead, use a combination of unrelated words, symbols, and numbers to increase complexity.",
    "Password managers like Bitwarden or LastPass help you store and generate strong passwords safely. Avoid reusing the same password across multiple sites, as a single breach could compromise all your accounts. Regularly updating your passwords and enabling features like two-factor authentication can further enhance your security. Remember, your password is often the first line of defense against hackers—treat it like a key to your digital life."
};

            _keywordResponses["phishing"] = new List<string>
{
    "Phishing is a form of cyberattack where attackers impersonate legitimate organizations or individuals to trick you into revealing sensitive information like passwords or credit card numbers. These attempts often use urgent or threatening language, such as claiming your account will be locked if you don’t act quickly. Always pause and verify before responding to such messages.",
    "Phishing emails often appear to come from trusted sources, but a closer inspection reveals minor errors—misspelled URLs, unfamiliar email addresses, or unusual attachments. Always check the sender's full email address and hover over links to view the true destination before clicking. If anything feels off, contact the company directly using verified contact details.",
    "Avoid clicking on links or downloading attachments from emails you weren’t expecting. Even if it seems like a friend or colleague sent it, their account may have been compromised. Phishing isn’t just limited to email—it also occurs via text messages (smishing) and phone calls (vishing). Stay alert across all channels.",
    "Legitimate companies will never ask for sensitive information via email. If you’re unsure about a message, contact the company or person independently. Reporting phishing emails helps security teams and email providers improve their filters, so always flag suspicious messages when possible."
};

            _keywordResponses["firewall"] = new List<string>
{
    "A firewall is your system’s first line of defense against unauthorized access. It acts as a filter between your device and the internet, blocking potentially harmful traffic while allowing legitimate communication through. Firewalls can prevent attackers from exploiting network vulnerabilities and help contain malware if your device becomes infected.",
    "There are two main types of firewalls: hardware firewalls and software firewalls. Hardware firewalls protect entire networks and are typically found in routers or standalone devices, while software firewalls are installed on individual computers. Both types are essential for comprehensive protection against cyber threats.",
    "Firewalls monitor both inbound and outbound traffic. For example, if a piece of malware tries to send data out of your system, the firewall can detect and block this activity. This feature is especially useful in preventing sensitive data leaks and identifying unusual behavior in your network.",
    "Always ensure your firewall is turned on and properly configured, especially when using public Wi-Fi or working remotely. Many operating systems include built-in firewalls, but it's important to review and adjust the settings according to your security needs. Combining a firewall with other protective tools strengthens your cybersecurity posture significantly."
};

            _keywordResponses["malware"] = new List<string>
{
    "Malware, or malicious software, refers to any program or file designed to harm or exploit your computer or network. Common types include viruses, worms, trojans, spyware, adware, and ransomware. Malware can steal personal data, damage systems, or grant unauthorized access to attackers.",
    "To protect against malware, avoid downloading files or software from untrusted websites or clicking on suspicious links. Many malware infections begin with deceptive tactics like fake ads, free software offers, or email attachments. Always verify sources before downloading or installing anything.",
    "Keep your operating system, browser, and applications up to date. Many malware attacks exploit known vulnerabilities that have been patched in newer versions. Regular software updates are a simple but powerful defense mechanism against evolving threats.",
    "Use trusted antivirus and anti-malware software to detect and remove threats in real-time. Schedule regular scans and enable real-time protection. Also, be cautious with USB drives or external devices—they can carry malware even if they seem harmless."
};

            _keywordResponses["vpn"] = new List<string>
{
    "A Virtual Private Network (VPN) creates a secure tunnel between your device and the internet, encrypting all data you send and receive. This is especially useful when using public Wi-Fi, where your data can otherwise be intercepted by cybercriminals. A VPN masks your IP address, enhancing both privacy and security.",
    "By using a VPN, your online activities become much harder to trace. This is beneficial not only for personal privacy but also for avoiding tracking by advertisers or access restrictions based on your geographic location. For remote workers, VPNs are often essential to securely connect to a company’s internal network.",
    "Not all VPNs are created equal. Reputable paid VPNs prioritize security and often have a no-logs policy, meaning they don’t store your browsing history. Free VPNs may log your data or inject ads, which can undermine your privacy goals. Do your research before selecting a provider.",
    "Keep in mind that a VPN is just one part of a broader cybersecurity strategy. It won’t protect you from malware, phishing attacks, or unsafe websites. Combine VPN usage with good browsing habits, antivirus tools, and secure passwords to ensure full protection."
};

            _keywordResponses["social engineering"] = new List<string>
{
    "Social engineering attacks manipulate human psychology rather than technical vulnerabilities. Attackers use persuasion, urgency, and trust to trick people into giving up confidential information or taking unsafe actions. It’s one of the most common and dangerous forms of cyberattack because it targets people, not systems.",
    "Common tactics include impersonating IT support to get passwords, pretending to be a boss requesting urgent fund transfers, or posing as a friend in need of help. These attacks can happen via email, phone calls, messages, or even in person. Always be skeptical of unexpected or urgent requests.",
    "To protect yourself, verify any requests for sensitive information independently. Call the person or organization using official contact information—not the details provided in the suspicious message. Never let emotions like fear or urgency override your judgment.",
    "Training and awareness are key. Educate yourself and others about social engineering tactics. Regular simulated phishing tests in workplaces and ongoing cybersecurity training can significantly reduce the risk of falling victim to social engineering."
};

            _keywordResponses["encryption"] = new List<string>
{
    "Encryption converts data into unreadable code that can only be deciphered with the correct decryption key. It is essential for protecting sensitive information during storage and transmission, ensuring that even if data is intercepted, it cannot be understood by unauthorized users.",
    "Many communication apps use end-to-end encryption, which means that only you and the recipient can read the messages. Not even the service provider can access the content. Always choose platforms that offer strong encryption for your private conversations.",
    "Encrypting your hard drive or files adds an extra layer of protection in case your device is lost or stolen. Tools like BitLocker (Windows) or FileVault (macOS) offer built-in encryption options that are simple to activate.",
    "When browsing online, look for HTTPS in the address bar. The 'S' stands for 'secure' and means that encryption is being used to protect the connection between your browser and the website. Avoid entering personal information on sites that do not use HTTPS."
};


            // Initialize cycling indexes
            foreach (var key in _keywordResponses.Keys)
                _keywordResponseIndex[key] = 0;
        }

        public override void GreetUser()
        {
            PlayWelcomeSound();
            DisplayAsciiArt();

            Console.Write("\nMay I have your name? ");
            UserName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(UserName))
            {
                _userMemory["name"] = UserName;

                if (_knownUsers.Contains(UserName.ToLower()))
                    DisplayResponse($"Welcome back, {UserName}! Ready for more cybersecurity knowledge?");
                else
                {
                    _knownUsers.Add(UserName.ToLower());
                    DisplayResponse($"Nice to meet you, {UserName}. I'm here to help you stay cyber safe!");
                }
            }
            else
            {
                DisplayResponse("Hello! I'm here to help you with cybersecurity. What's your name?");
            }

            Console.WriteLine("(Type 'help' for available commands, or 'exit' to quit)");
        }

        public override void ProcessInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                DisplayResponse("I didn’t catch that. Could you type it again?");
                return;
            }

            var lowerInput = input.ToLower();

            if (lowerInput == "help")
            {
                DisplayHelp();
                return;
            }

            if (lowerInput.StartsWith("my name is "))
            {
                var name = input.Substring("my name is ".Length).Trim();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    UserName = name;
                    _userMemory["name"] = name;
                    _knownUsers.Add(name.ToLower());
                    DisplayResponse($"Got it! I’ll remember your name is {name}.");
                }
                return;
            }

            if (lowerInput.Contains("remember me") || lowerInput.Contains("my name"))
            {
                if (_userMemory.TryGetValue("name", out var name))
                    DisplayResponse($"Of course, {name}. I haven’t forgotten you!");
                else
                    DisplayResponse("I don't seem to have your name yet. What should I call you?");
                return;
            }

            if (lowerInput.Contains("is that all") || lowerInput.Contains("anything else"))
            {
                if (!string.IsNullOrEmpty(_lastTopic) && _keywordResponses.ContainsKey(_lastTopic))
                {
                    var responses = _keywordResponses[_lastTopic];
                    var index = _keywordResponseIndex[_lastTopic];
                    DisplayResponse(responses[index]);
                    _keywordResponseIndex[_lastTopic] = (index + 1) % responses.Count;
                    return;
                }
                else
                {
                    DisplayResponse("Let’s start with a topic! Ask me about malware, phishing, VPNs, and more.");
                    return;
                }
            }

            if (lowerInput.Contains("how are you"))
            {
                DisplayResponse(GetRandomResponse(_howAreYouResponses));
                return;
            }

            if (lowerInput.Contains("purpose") || lowerInput.Contains("what do you do"))
            {
                DisplayResponse(GetRandomResponse(_purposeResponses));
                return;
            }

            foreach (var keyword in _keywordResponses.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    var index = _keywordResponseIndex[keyword];
                    var response = _keywordResponses[keyword][index];
                    DisplayResponse(response);

                    _lastTopic = keyword;
                    _keywordResponseIndex[keyword] = (index + 1) % _keywordResponses[keyword].Count;
                    return;
                }
            }

            DisplayResponse(GetRandomResponse(_defaultResponses));
        }

        public override void DisplayHelp()
        {
            var helpText = @"
You can ask me about the following cybersecurity topics:
- Passwords
- Phishing
- Malware
- Firewalls
- VPN
- Social engineering
- Encryption

Other commands:
- 'my name is [name]' — I'll remember your name
- 'remember me' — Check if I remember you
- 'is that all?' — Get more information on the last topic
- 'help' — Show this help menu
- 'exit' or 'quit' — End the chat
";
            DisplayResponse(helpText);
        }
    }
}
