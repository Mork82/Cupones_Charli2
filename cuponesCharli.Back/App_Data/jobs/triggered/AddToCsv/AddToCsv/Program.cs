using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AddToCsv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lineNumber = 0;
            using (SqlConnection conn = new SqlConnection("Server=tcp:codigoscharli.database.windows.net,1433;Initial Catalog=codigos_bd;Persist Security Info=False;User ID=Charlies4;Password=Charlis@2021;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")) {
                conn.Open();
                //Put your file location here:
                WebClient client = new WebClient();
                Stream stream = client.OpenRead("https://storagecervezas.blob.core.windows.net/codigoscontainer/codigosPremiados.csv");
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (lineNumber != 0)
                        {
                            var values = line.Split(',');

                            var sql = "INSERT INTO codigos VALUES ('" + values[0] + "','" + values[1] + "',NULL,NULL,NULL)";

                            var cmd = new SqlCommand();
                            cmd.CommandText = sql;
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Connection = conn;
                            cmd.ExecuteNonQuery();
                        }
                        lineNumber++;
                    }
                }
                conn.Close();
            }
            Console.WriteLine("Codigos Import Complete");
            Console.ReadLine();



        }
        }
    }

