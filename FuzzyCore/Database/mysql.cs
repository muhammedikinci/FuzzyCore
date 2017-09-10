using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using FuzzyCore.Server;
using System.Drawing;

namespace FuzzyCore.Database
{
    public class mysql
    {
        ConsoleMessage Message = new ConsoleMessage();
        MySqlConnection MySql;
        public bool MySqlInıt
        {
            get
            {
                return MySqlInıt_Pri;
            }
        }
        private bool MySqlInıt_Pri = false;
        const string defaultConnectionString = "Server=localhost;Database=test;Uid=root;Pwd='';";
        public mysql(string DatabaseStr)
        {
            try
            {
                MySql = new MySqlConnection("Server=localhost;Database=" + DatabaseStr + ";Uid=root;Pwd='';");
                MySql.Open();
                if (MySql.State == System.Data.ConnectionState.Open)
                {
                    Message.Write("Connected MySql!", ConsoleMessage.MessageType.SUCCESS);
                    MySqlInıt_Pri = true;
                }
                else
                {
                    Message.Write("Not Connected MySql!", ConsoleMessage.MessageType.ERROR);
                    MySqlInıt_Pri = false;
                }
            }
            catch (Exception ex)
            {
                Message.Write(ex.Message.ToString(), ConsoleMessage.MessageType.ERROR);
            }
        }
        public mysql(string DatabaseStr, string ServerStr, string UserStr, string passwordStr)
        {
            try
            {
                MySql = new MySqlConnection("Server=" + ServerStr + ";Database=" + DatabaseStr + ";Uid=" + UserStr + ";Pwd='" + passwordStr + "';");
                MySql.Open();
                if (MySql.State == System.Data.ConnectionState.Open)
                {
                    Message.Write("Connected MySql!", ConsoleMessage.MessageType.SUCCESS);
                    MySqlInıt_Pri = true;
                }
                else
                {
                    Message.Write("Not Connected MySql!", ConsoleMessage.MessageType.ERROR);
                    MySqlInıt_Pri = false;
                }
            }
            catch (Exception ex)
            {
                Message.Write(ex.Message.ToString(), ConsoleMessage.MessageType.ERROR);
            }
        }
        public mysql(string DatabaseStr, string ServerStr)
        {
            try
            {
                MySql = new MySqlConnection("Server=" + ServerStr + ";Database=" + DatabaseStr + ";Uid=root;Pwd='';");
                MySql.Open();
                if (MySql.State == System.Data.ConnectionState.Open)
                {
                    Message.Write("Connected MySql!", ConsoleMessage.MessageType.SUCCESS);
                    MySqlInıt_Pri = true;
                }
                else
                {
                    Message.Write("Not Connected MySql!", ConsoleMessage.MessageType.ERROR);
                    MySqlInıt_Pri = false;
                }
            }
            catch (Exception ex)
            {
                Message.Write(ex.Message.ToString(), ConsoleMessage.MessageType.ERROR);
            }
        }
    }
}
