using System;
using System.IO;
using System.Net;
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
                        LOGIN:
                        Write(">>> Username: ");
                        string user = ReadLine();
                        Write(">>> Password: ");
                        string pwd = ReadLine();

                        bool connStatus = RemoteConnection(cmd[2], user, pwd);
                        if (!connStatus)
                        {
                            WriteLine(">>> Something goes wrong... Retry");
                            goto LOGIN;
                        }
                        else
                        {
                            WriteLine(">>> Now you can using remote path for encryption and decryption");
                            WriteLine("Encrypt      aes -e [remote folder|file|text] {aes -e \\\\192.168.1.100\\foldername|extension, aes -e \\\\192.168.1.100\\filename.txt}");
                            WriteLine("Decrypt      aes -d [remote folder|file|text] {aes -d \\\\192.168.1.100\\foldername|extension, aes -d \\\\192.168.1.100\\filename.txt}" + Environment.NewLine);
                            goto START;
                        }
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

        static bool RemoteConnection(string remote_path, string user, string password)
        {
            bool conn = false;
            NetworkCredential netCredential;
            NetworkConnection netConn;

            string remote = Path.GetDirectoryName(remote_path);
            netCredential = new NetworkCredential(user, password);

            try
            {
                netConn = new NetworkConnection(remote, netCredential);
                conn = true;
            }
            catch(Exception ex)
            {
                WriteLine($"ERROR >>> {ex}");
            }

            return conn;
        }
    }
}
