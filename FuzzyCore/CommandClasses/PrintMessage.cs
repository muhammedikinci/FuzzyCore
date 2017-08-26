using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace FuzzyCore.Server.Data
{
    class PrintMessage
    {
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

        public void Print_Message_OverAndAfterTime(JsonCommand Comm)
        {
            Command = Comm;
            Thread FormOpen = new Thread(new ThreadStart(OpenForm_PrintMessage));
            FormOpen.Start();
            Thread FormClosingOverTime = new Thread(new ThreadStart(FormClosingOverTime_PrintMessage));
            FormClosingOverTime.Start();
        }
        public void Print_Message(JsonCommand Comm)
        {
            Command = Comm;
            OpenForm_PrintMessage();
        }

        public void Print_Message_AfterTime(JsonCommand Comm)
        {
            Command = Comm;
            Thread FormOpen = new Thread(new ThreadStart(FormOpenAfterTime_PrintMessage));
            FormOpen.Start();
        }

        public void Print_Message_OverTime(JsonCommand Comm)
        {
            Command = Comm;
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
            Thread.Sleep((int)Command.OverTime);
            Application.Run(CommandForm);
        }
        //{ "CommandType":"print_message","Text":"hello","OverTime":5 } 
    }
}
