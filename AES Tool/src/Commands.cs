using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace AES_Tool.src
{
    class Commands
    {
        CryptoAES aes = new CryptoAES();
        string nl = Environment.NewLine;

        public string Help() => "help";
        public string Encrypt() => "-e";
        public string Decrypt() => "-d";
        
        void Cmds()
        {
            WriteLine("Start with 'aes' command to recall other commands.");
            WriteLine("Encrypt      aes -e [folder|file|text] {aes -e c:\\foldername, aes -e c:\\filename.txt, aes -e 'clear text'}");
            WriteLine("Decrypt      aes -d [folder|file|text] {aes -e c:\\foldername, aes -e c:\\filename.txt, aes -e 'chyper text'}");
        }

        public void HelpCommand() => Cmds();

        public void EncryptCommand(string Args, string Password)
        {
            if (!Args.Contains("'"))
            {
                if (!File.Exists(Args)) 
                {
                    var folder = new DirectoryInfo(Args); 
                    if (folder.GetFiles().Length != 0)
                    {
                        // folder
                    }
                    else 
                    {
                        WriteLine("The directory is empty.");
                    }
                }
                else
                {
                    WriteLine("Start to encrypting file...");
                    aes.EncryptFile(Args, Args, Password);
                    WriteLine($"DONE! Check in {Args}" + nl);
                }
            }
            else
            {
                string plainText = Args.Replace("'", "").Trim();
                string chiperText = aes.EncryptText(plainText, Password);
                WriteLine("----------------- CHIPER TEXT BELOW-----------------");
                WriteLine(chiperText + nl);
            }
        }

        public void DecryptCommand(string Args, string Password)
        {
            if (!Args.Contains("'"))
            {
                if (!File.Exists(Args)) 
                {
                    var folder = new DirectoryInfo(Args);
                    if (folder.GetFiles().Length != 0) 
                    {
                        // folder
                    }
                    else 
                    {
                        WriteLine("The directory is empty.");
                    }
                }
                else
                {
                    WriteLine("Start to decrypting file...");
                    aes.DecryptFile(Args, Args, Password);
                    WriteLine($"DONE! Check in {Args}" + nl);
                }
            }
            else
            {
                string chiperText = Args.Replace("'", "").Trim();
                string plainText = aes.DecryptText(chiperText, Password);
                WriteLine("----------------- PLAIN TEXT BELOW-----------------");
                WriteLine(plainText + nl);
            }
        }
    }
}
