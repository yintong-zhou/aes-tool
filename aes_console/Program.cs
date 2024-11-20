using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace aes_console
{
    class Program
    {
        static Commands cmds = new Commands();
        static Title t = new Title();
        static void Main(string[] args)
        {
            t.Show();

        START:
            Write(">>> ");
            string input = ReadLine();
            if (input.StartsWith("aes"))
            {
                string[] cmd = input.Split('"'); 
                if (cmd.Length > 0)
                {
                    if(input.Contains("\"")) Cryptography(cmd, true);
                    else Cryptography(cmd, false);
                    goto START;
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

        static void Cryptography(string[] command, bool text)
        {
            if (command[0].Contains(cmds.Encrypt()))
            {
                Write("Password Key: ");
                string Password = ReadLine();

                if (!text)
                {
                    var value = command[0].Split(' ');
                    if (File.Exists(value[2])) cmds.EncryptCommand(value[2], Password);
                    else cmds.EncryptCommand(value[2], Password);
                }
                else cmds.EncryptCommand(command[1], Password);
            }
            else if (command[0].Contains(cmds.Decrypt()))
            {
                Write("Password Key: ");
                string Password = ReadLine();

                if (!text)
                {
                    var value = command[0].Split(' ');
                    if (File.Exists(value[2])) cmds.DecryptCommand(value[2], Password);
                    else cmds.DecryptCommand(value[2], Password);
                }
                else cmds.DecryptCommand(command[1], Password);
            }
            else WriteLine("Command is not found, using 'help' to show the commands.");
        }
    }
}
