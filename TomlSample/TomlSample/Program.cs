using System;
using System.IO;
using Tomlyn;
using Tomlyn.Model;

namespace TomlSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream("sample.toml", FileMode.Open))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                var doc = Toml.Parse(buffer);
                var table = doc.ToModel();
                //// title
                Console.WriteLine(@$"""Title"" = {table["title"]}");

                //// host
                TomlArray hosts = table["hosts"] as TomlArray;

                Console.WriteLine(@"""hosts"" = [");
                for(int i = 0; i < hosts.Count; i++)
                {
                    Console.Write($"\t{hosts[i]}");

                    if (i < (hosts.Count - 1))
                    {
                        Console.WriteLine(",");
                    }
                    else
                    {
                        Console.WriteLine("");
                    }
                }
                Console.WriteLine(@"]");

                //// owner table
                Console.WriteLine(@$"""owner"".""name"" = {((TomlTable)table["owner"])["name"]}");
                Console.WriteLine($@"""owner"".""organization"" = {((TomlTable)table["owner"])["organization"]}");
                Console.WriteLine($@"""owner"".""bio"" = {((TomlTable)table["owner"])["bio"]}");
                Console.WriteLine($@"""owner"".""dob"" = {((TomlTable)table["owner"])["dob"]}");

                //// database table
                TomlTable database = table["database"] as TomlTable;
                var server = database["server"];
                Console.WriteLine(@$"""database"".""server"" = {server}");
                TomlArray ports = database["ports"] as TomlArray;
                Console.WriteLine(@"""database"".""ports"" = [");

                for (int i = 0; i < ports.Count; i++)
                {
                    Console.Write($"{ports[i]}");

                    if (i < (ports.Count - 1))
                    {
                        Console.WriteLine(",");
                    }
                    else
                    {
                        Console.WriteLine("");
                    }
                }
                Console.WriteLine("]");
                var connection_max = database["connection_max"];
                Console.WriteLine(@$"""database"".""connection_max"" = {connection_max}");
                var enabled = database["enabled"];
                Console.WriteLine($@"""database"".""enabled"" = {enabled}");

                //// servers table
                TomlTable servers = table["servers"] as TomlTable;
                //// alpha sub table
                TomlTable alpha = servers["alpha"] as TomlTable;
                Console.WriteLine(@$"""servers"".""alpha"".""ip"" = {alpha["ip"]}");
                Console.WriteLine($@"""servers"".""alpha"".""dc"" = {alpha["dc"]}");
                TomlTable beta = servers["beta"] as TomlTable;
                Console.WriteLine($@"""servers"".""beta"".""ip"" = {beta["ip"]}");
                Console.WriteLine(@$"""servers"".""beta"".""dc"" = {beta["dc"]}");

                //// clients table
                TomlTable clients = table["clients"] as TomlTable;
                TomlArray data = clients["data"] as TomlArray;
                Console.WriteLine($@"""clients"".""data"" = [");

                for (int i = 0; i < data.Count; i++)
                {
                    TomlArray array = data[i] as TomlArray;

                    Console.WriteLine("\t[");
                    for (int j = 0; j < array.Count; j++)
                    {
                        Console.Write($"\t\t{array[i]}");

                        if (j < (array.Count - 1))
                        {
                            Console.WriteLine(",");
                        }
                        else
                        {
                            Console.WriteLine("");
                        }
                    }
                    Console.Write("\t]");

                    if (i < (data.Count - 1))
                    {
                        Console.WriteLine(",");
                    }
                    else
                    {
                        Console.WriteLine("");
                    }
                }

                Console.WriteLine("]");
            }
        }
    }
}
