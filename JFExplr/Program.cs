using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFExplr
{
    class Program
    {
        static void Main(string[] args)
        {
            for (;;)
            {
                Console.Write("jfexplr-0.2$ ");
                string commandLine = Console.ReadLine();
                Array commandList = commandLine.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);

                string command = (string)commandList.GetValue(0);
                Array restList = Array.CreateInstance(typeof(String), commandList.Length - 1);
                for(int i = 0; i < commandList.Length - 1; i++)
                {
                    restList.SetValue(commandList.GetValue(i + 1), i);
                }

                switch (command)
                {
                    case "--help":
                        Console.WriteLine("--help");
                        break;

                    case "echo":
                        foreach(string echoValue in restList)
                        {
                            Console.Write(echoValue + " ");
                        }
                        Console.WriteLine();
                        break;

                    case "chmod":
                        Console.WriteLine("chmod");
                        break;
                    case "grep":
                        Console.WriteLine("grep");
                        break;
                    case "ps":
                        Console.WriteLine("ps");
                        break;

                    case "who":
                        string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                        Console.WriteLine(userName);
                        break;

                    case "date":
                        Console.WriteLine(DateTime.Now.Date);
                        break;

                    case "df":
                        Console.WriteLine("df");
                        break;

                    case "ls":
                        string path = (string)commandList.GetValue(1);

                        Console.WriteLine("Directory Listing");
                        Array dirList = Directory.GetDirectories(path);
                        foreach (string dir in dirList)
                        {
                            Console.WriteLine(dir.Replace(path, ""));
                        }
                        Array fileList = Directory.GetDirectories(path);
                        foreach (string file in fileList)
                        {
                            Console.WriteLine(file.Replace(path, ""));
                        }

                        break;

                    case "cd":
                        Console.WriteLine("cd");
                        //Directory.CreateDirectory(path);
                        break;
                    case "mkdir":
                        Console.WriteLine("mkdir");
                        break;
                    case "rmdir":
                        Console.WriteLine("rmdir");
                        break;
                    case "cp":
                        Console.WriteLine("cp");
                        break;
                    case "rm":
                        Console.WriteLine("rm");
                        break;
                    case "mv":
                        Console.WriteLine("mv");
                        break;
                    case "more":
                        Console.WriteLine("more");
                        break;
                    case "lpr":
                        Console.WriteLine("lpr");
                        break;
                    case "man":
                        Console.WriteLine("man");
                        break;
                    case "exit":
                    case "quit":
                        return;
                    default:
                        break;
                } //switch
            }
        } // main
    } // class
}
