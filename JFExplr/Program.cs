﻿using System;
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
            //Startup
            string rootPath = Directory.GetCurrentDirectory();
            string currentPath = Directory.GetCurrentDirectory();
            string sourcePath = currentPath;
            string destinationPath = currentPath;
            string bashName = "jfexplr";
            string versionNo = "0.2";
            string bashLineEnd = "$";
            string userName = Environment.UserName;
            string machineName = Environment.MachineName;
            
            // Program Core
            for (;;)
            {
                showBash(userName, machineName, currentPath, bashLineEnd);

                string commandLine = Console.ReadLine();
                Array commandList = commandLine.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);

                if (commandLine.Equals(""))
                {
                    continue;
                }

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
                        string fullUserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                        Console.WriteLine(fullUserName);
                        break;

                    case "date":
                        Console.WriteLine(DateTime.Now.Date);
                        break;

                    case "df":
                        Console.WriteLine("df");
                        break;

                    case "ls":
                        Array dirList = Directory.GetDirectories(currentPath);
                        foreach (string dir in dirList)
                        {
                            string dirTemp = dir.Replace(currentPath, "");
                            Console.Write(dirTemp.Replace(@"\", "") + "  ");

                        }
                        Array fileList = Directory.GetFiles(currentPath);
                        foreach (string file in fileList)
                        {
                            string fileTemp = file.Replace(currentPath, "");
                            Console.Write(fileTemp.Replace(@"\", "") + "  ");
                        }
                        Console.WriteLine();

                        break;

                    case "cd":
                        string cdSwitch = (string) restList.GetValue(0); 
                        switch(cdSwitch)
                        {
                            case "..":
                                string checkPath = removeLastDirectory(currentPath);

                                if(Directory.Exists(checkPath))
                                {
                                    currentPath = checkPath;
                                }
                                break;

                            case @"/":
                                currentPath = rootPath;
                                break;

                            default:
                                if(Directory.Exists(currentPath + @"\" + cdSwitch))
                                {
                                    currentPath = currentPath + @"\" + cdSwitch;
                                } else
                                {
                                    Console.WriteLine("{0}: {1}: {2}: No such file or directory", bashName, command, cdSwitch);
                                }

                                break;
                        }
                        break;

                    case "open":
                        Console.WriteLine("open");
                        // Windows.OpenExplorer("c:\");
                        System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", currentPath));
                        break;

                    case "mkdir": //TODO: A lot of checkign is still needed
                        string newFolderName = (string)restList.GetValue(0);

                        if (!Directory.Exists(currentPath + @"\" + newFolderName))
                        {
                            Directory.CreateDirectory(currentPath + @"\" + newFolderName);
                        } else
                        {
                            Console.WriteLine("Directory already exists.");
                        }
                        break;

                    case "rmdir": //TODO: A lot of checking is still needed
                        string deleteFolderName = (string)restList.GetValue(0);

                        if (Directory.Exists(currentPath + @"\" + deleteFolderName))
                        {
                            Directory.Delete(currentPath + @"\" + deleteFolderName);
                        }
                        else
                        {
                            Console.WriteLine("Directory does not exist.");
                        }

                        break;

                    case "cp":
                        string cpFlags = (string)restList.GetValue(0);
                        sourcePath = (string)restList.GetValue(1);
                        destinationPath = (string)restList.GetValue(2);
                        Console.WriteLine("{0}  {1}  {2}", cpFlags, sourcePath, destinationPath);

                        //if(!System.IO.File.Exists(destinationPath)){
                        //   System.IO.File.Create(destinationPath);
                        //}
                        System.IO.File.Copy(sourcePath, destinationPath, true);
                        break;

                    case "rm":
                        Console.WriteLine("rm");
                        break;

                    case "mv":
                        string mvFlags = (string)restList.GetValue(0);
                        sourcePath = (string)restList.GetValue(1);
                        destinationPath = (string)restList.GetValue(2);
                        Console.WriteLine("{0}  {1}  {2}", mvFlags, sourcePath, destinationPath);
                        System.IO.File.Move(sourcePath, destinationPath);
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

                    case "ifconfig":
                        //System.Diagnostics.Process.Start("CMD.exe", "/C ipconfig");
                        break;

                    case "pwd":
                        Console.WriteLine(currentPath);
                        break;
                    
                    case "history":
                        Console.WriteLine("history");
                        break;

                    case "clear":
                        Console.Clear();
                        break;

                    case "restart": //TODO fix the timer
                    case "reboot":
                        Console.WriteLine("The system is going down!");
                        System.Diagnostics.Process.Start("ShutDown", "-r"); //restart
                        break;

                    case "shutdown":
                        Console.WriteLine("The system will shutdown now!");
                        //System.Diagnostics.Process.Start("ShutDown", "-s"); //shutdown
                        System.Diagnostics.Process.Start("shutdown", "/s /t 0");
                        break;

                    case "uptime":
                        TimeSpan elapsedTime = TimeSpan.FromMilliseconds(Environment.TickCount);
                        string upTime = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds);
                        Console.WriteLine("The system has been up for {0}", upTime);
                        break;

                    case "exit":
                    case "quit":
                        return;

                    default:
                        Console.WriteLine("{0}: {1}: command not found", bashName, command); 
                        break;

                } //switch (command)

            } // for (;;)
        } // static void Main(string[] args)

        static string removeLastDirectory(string path)
        {
            int lastInstance = path.LastIndexOf(@"\");

            if(lastInstance == -1) //no '\' found
            {
                return path; 
            }

            if (path.Length == lastInstance + 1) //Check is a trailing '\' is at the end of path
            {
                path = path.Remove(lastInstance, 1);
                lastInstance = path.LastIndexOf(@"\");
            }

            return path.Substring(0, lastInstance);
        } // string removeLastDirectory(string path)

        static string keepLastDirectory(string path)
        {
            int lastInstance = path.LastIndexOf(@"\");

            if (lastInstance == -1)
            {
                return path;
            }

            if (path.Length == lastInstance + 1) //Check is a trailing '\' is at the end of path
            {
                path = path.Remove(lastInstance, 1);
                lastInstance = path.LastIndexOf(@"\");
            }

            lastInstance++; //Eliminate last /
            int lastIndex = path.Length - lastInstance;
            
            return path.Substring(lastInstance, lastIndex);
        }

        static Boolean showBash(string userName, string machineName, string currentPath, string bashLineEnd)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("{0}@{1}", userName, machineName);
            Console.ResetColor();
            Console.Write(":");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("~{0}", keepLastDirectory(currentPath));
            Console.ResetColor();
            Console.Write("{0} ", bashLineEnd);

            return true;
        }

    } // class Program
}
