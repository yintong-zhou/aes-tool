using System;
using static System.Console;
using AES_Tool.src;

namespace AES_Tool
{
    class Program
    {
        static Commands cmds = new Commands();
        static Helper h = new Helper();

        static void Main(string[] args)
        {
            h.TitleShow();

            START:
            Write(">>> ");
            string input = ReadLine();
            if (input.StartsWith("aes"))
            {
                string[] cmd = input.Split(' ');
                if(cmd.Length > 1)
                {
                    if(cmd[1] != cmds.Remote())
                    {
                        Cryptography(cmd);
                        goto START;
                    }
                    else
                    {
                        Write(">>> Username: ");
                        string user = ReadLine();
                        Write(">>> Password: ");
                        string pwd = ReadLine();

                        WriteLine($"Connection to {cmd[2]}");
                        goto START;
                    }
                }
                else
                {
                    WriteLine("Select the next command, please check with 'help'.");
                    goto START;
                }
               
            }
            else if (input == "help")
            {
                cmds.HelpCommand();
                goto START;
            }
            else
            {
                WriteLine("Start with 'aes' command or write 'help' to check the commands.");
                goto START;
            }
        }

        static void Cryptography(string[] command)
        {
            if (command[1] == cmds.Encrypt())
            {
                Write("Password Key: ");
                string Password = ReadLine();
                cmds.EncryptCommand(command[2], Password);
            }
            else if (command[1] == cmds.Decrypt())
            {
                Write("Password Key: ");
                string Password = ReadLine();
                cmds.DecryptCommand(command[2], Password);
            }
            else WriteLine("Command is not found, using 'help' to show the commands.");
        }
    }
}
