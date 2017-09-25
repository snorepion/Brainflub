using System;
using System.IO;
using Brainflub;
namespace BReader
{
    static class Program
    {
        static void Main(string[] args)
        {
            string fp = "";
            string fc = "";
            fp = Console.ReadLine();
            if (File.Exists(fp)) fc = File.ReadAllText(fp);
            else Console.Write("File specified does not exist.\r\n");
            BrainflubParser bf = new BrainflubParser();
            try
            {
                bf.Parse(fc);
            }
            catch
            {
                Console.Write("Code parsing error.\r\n");
            }
            Console.Write(bf.Output + "\r\n");
            Console.Read();
        }
    }
}