﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace aes_console
{
    class Commands
    {
        EncryptingAES aes = new EncryptingAES();
        string nl = Environment.NewLine;

        public string Help() => "help";
        public string Encrypt() => "-e";
        public string Decrypt() => "-d";
        //public string Remote() => "-r";

        void Cmds()
        {
            WriteLine("Start with 'aes' command to recall other commands." + nl);
            WriteLine("Encrypt      aes -e [folder|file|text] {aes -e c:\\foldername|extension, aes -e c:\\filename.txt, aes -e \"clear text\"}");
            WriteLine("Decrypt      aes -d [folder|file|text] {aes -d c:\\foldername|extension, aes -d c:\\filename.txt, aes -d \"cypher text\"}");
        }

        public void HelpCommand() => Cmds();

        public void EncryptCommand(string Args, string Password)
        {
            try
            {
                if (Args.Contains("\\"))
                {
                    if (!File.Exists(Args))
                    {
                        var check = Args.Split('|');
                        var folder = new DirectoryInfo(check[0]);
                        string ext = check[1];

                        if (folder.GetFiles().Length != 0)
                        {
                            if (check.Length > 1)
                            {
                                var listFile = Directory.GetFiles(folder.FullName, $"*.{ext}", SearchOption.AllDirectories);
                                WriteLine("----------------- STARTING PROCESS -----------------");
                                foreach (string file in listFile)
                                {
                                    System.Threading.Thread.Sleep(500);
                                    aes.EncryptFile(file, file, Password);
                                    WriteLine($"{file} =====> AES Done!");
                                }
                                WriteLine("----------------- END PROCESS -----------------" + nl);
                            }
                            else WriteLine("Warning! Specific the files extension.");

                        }
                        else WriteLine("The directory is empty.");
                    }
                    else
                    {
                        WriteLine("START TO ENCRYPTING FILE...");
                        aes.EncryptFile(Args, Args, Password);
                        WriteLine($"DONE! Check in {Args}" + nl);
                    }
                }
                else
                {
                    string plainText = Args.Replace("\"", "").Trim();
                    string chiperText = aes.EncryptText(plainText, Password);
                    WriteLine("----------------- CYPHER TEXT BELOW -----------------");
                    WriteLine(chiperText + nl);
                }
            }
            catch (Exception ex)
            {
                WriteLine($"ERROR Exception: {ex.Message}");
            }
        }

        public void DecryptCommand(string Args, string Password)
        {
            try
            {
                if (Args.Contains("\\"))
                {
                    if (!File.Exists(Args))
                    {
                        var check = Args.Split('|');
                        var folder = new DirectoryInfo(check[0]);
                        string ext = check[1];

                        if (folder.GetFiles().Length != 0)
                        {
                            if (check.Length > 1)
                            {
                                var listFile = Directory.GetFiles(folder.FullName, $"*.{ext}", SearchOption.AllDirectories);
                                WriteLine("----------------- STARTING PROCESS -----------------");
                                foreach (string file in listFile)
                                {
                                    System.Threading.Thread.Sleep(500);
                                    aes.DecryptFile(file, file, Password);
                                    WriteLine($"{file} =====> DECRYPT Done!");
                                }
                                WriteLine("----------------- END PROCESS -----------------" + nl);
                            }
                            else WriteLine("Warning! Specific the files extension.");

                        }
                        else WriteLine("The directory is empty.");
                    }
                    else
                    {
                        WriteLine("START TO DECRYPTING FILE...");
                        aes.DecryptFile(Args, Args, Password);
                        WriteLine($"DONE! Check in {Args}" + nl);
                    }
                }
                else
                {
                    string chiperText = Args.Replace("'", "").Trim();
                    string plainText = aes.DecryptText(chiperText, Password);
                    WriteLine("----------------- PLAIN TEXT BELOW -----------------");
                    WriteLine(plainText + nl);
                }
            }
            catch (Exception ex)
            {
                WriteLine($"ERROR Exception: {ex.Message}");
            }
        }
    }
}
