using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using FuzzyCore.Data;

namespace FuzzyCore.Commands
{
    public class PrintMessage
    {
        public static bool Test_StackBoolean = false;
        Form CommandForm = new Form();
        JsonCommand Command;
        private void Create_Form()
        {
            CommandForm.StartPosition = FormStartPosition.CenterScreen;
            CommandForm.Height = 350;
            CommandForm.Width = 800;
            CommandForm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            CommandForm.Text = Command.FormCaption;

            Label Text = new Label();
            Text.Width = 750;
            Text.Height = 300;
            Text.TextAlign = ContentAlignment.MiddleCenter;
            if (Command.FontSize > 0)
            {
                Text.Font = new Font(FontFamily.GenericSansSerif, Command.FontSize);
            }
            else
            {
                Text.Font = new Font(FontFamily.GenericSansSerif, 18);
            }
            Text.Text = Command.Text;
            CommandForm.Controls.Add(Text);
        }

        public PrintMessage(JsonCommand comm)
        {
            this.Command = comm;
            Permissions.MacPermission.PermissionMac Mac = new Permissions.MacPermission.PermissionMac();
            Mac.MacAddress = comm.MacAddress;
            Permissions.MacPermission MacPer = new Permissions.MacPermission();
            MacPer.MacObject = Mac;
            if (MacPer.PermissionControl())
            {
                DataProcess();
            }
            else
            {
                Console.WriteLine("Permission Denied!");
            }
        }

        void DataProcess()
        {
            if (!string.IsNullOrEmpty(Command.Text))
            {
                if (Command.Repeat == true && Command.RepeatStep > 0)
                {
                    //Print_Message_With_Args();
                }
                else if (Command.OverTime > 500 && Command.AfterTime > 500)
                {
                    Print_Message_OverAndAfterTime();
                }
                else if (Command.OverTime > 500)
                {
                    Print_Message_OverTime();
                }
                else if (Command.AfterTime > 500)
                {
                    Print_Message_AfterTime();
                }
                else
                {
                    Print_Message();
                }
            }
            else { }
        }

        void Print_Message_OverAndAfterTime()
        {
            Thread FormOpen = new Thread(new ThreadStart(OpenForm_PrintMessage));
            FormOpen.Start();
            Thread FormClosingOverTime = new Thread(new ThreadStart(FormClosingOverTime_PrintMessage));
            FormClosingOverTime.Start();
        }
        void Print_Message()
        {
            OpenForm_PrintMessage();
        }

        void Print_Message_AfterTime()
        {
            Create_Form();
            Thread FormOpen = new Thread(new ThreadStart(FormOpenAfterTime_PrintMessage));
            FormOpen.Start();
        }

        void Print_Message_OverTime()
        {
            Create_Form();
            Thread FormOpen = new Thread(new ThreadStart(OpenForm_PrintMessage));
            FormOpen.Start();
            Thread FormClosingOverTime = new Thread(new ThreadStart(FormClosingOverTime_PrintMessage));
            FormClosingOverTime.Start();
        }

        //Open Form
        void OpenForm_PrintMessage()
        {
            Create_Form();
            Application.Run(CommandForm);
            Test_StackBoolean = true;

        }
        //Form Closing Over Time
        void FormClosingOverTime_PrintMessage()
        {
            if (Command.OverTime > 500)
            {
                Thread.Sleep((int)Command.OverTime);
                CommandForm.Invoke(new Action(() => CommandForm.Close()));
            }
        }
        //Form Open After Time
        void FormOpenAfterTime_PrintMessage()
        {
            Thread.Sleep((int)Command.AfterTime);
            Application.Run(CommandForm);
        }
        //{ "CommandType":"print_message","Text":"hello","OverTime":5 } 
    }
}
