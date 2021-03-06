﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите адрес сервера");
            string hostname = Console.ReadLine();

            Console.WriteLine("Введите имя базы данных на сервере MS SQL : {0}", hostname);
            string database = Console.ReadLine();

            Console.WriteLine("Введите пользователя MS SQL к {0}", database);
            string login = Console.ReadLine();

            Console.WriteLine("Введите пароль пользователя MS SQL : {0}", login);
            string password = Console.ReadLine();

            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
            builder["Data Source"] = hostname;
            builder["integrated Security"] = false;
            builder["Initial Catalog"] = database;
            builder["user id"] = login;
            builder["Password"] = password;
            Console.WriteLine(builder.ConnectionString);

            SqlConnection conn = new SqlConnection(builder.ConnectionString);

            try
            {
                Console.WriteLine("Start connection");
                conn.Open();
                Console.WriteLine("Connection ready");

                // get last userid
                string getUserId = String.Format("SELECT MAX(ID) FROM pList");
                SqlCommand Idcommand = new SqlCommand(getUserId, conn);
                int userid = Convert.ToInt32(Idcommand.ExecuteScalar());

                // get last tabnumber
                string getTabNumber = String.Format("SELECT MAX(ID) FROM pList");
                SqlCommand TabNumbercommand = new SqlCommand(getTabNumber, conn);
                int tabnumber = Convert.ToInt32(TabNumbercommand.ExecuteScalar());

                int status = 8;
                string name_adder = "_";
                string Name = "";
                string StaticName = "Шаблонов";
                string FirstName = "Шаблон";
                string MidName = "Шаблонович";
                int pattern = 1;
                // circle
                for (int i = 0; i < 30000; i++)
                {
                    userid++;
                    tabnumber++;
                    if (i > 0)
                        Name = StaticName + name_adder + userid;
                    else
                        Name = userid > 1 ? StaticName + name_adder + userid : StaticName;
                    // do some queries
                    string sqlExpression = String.Format("INSERT INTO pList (ID, Name, FirstName, Status, MidName, TabNumber, GrStatus) VALUES ({0}, '{1}', '{2}', {3}, '{4}', {5}, {6})", userid, Name, FirstName, status, MidName, tabnumber, pattern);
                    SqlCommand command = new SqlCommand(sqlExpression, conn);
                    command.ExecuteNonQuery();
                    Console.WriteLine("Добавлен: {0}, '{1}', '{2}', '{3}', {4}, {5}", userid, Name, FirstName, MidName, tabnumber, pattern);
                }

                Console.WriteLine("Circle ended");

                // end connections
                conn.Close();
                Console.WriteLine("Connection ended");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
