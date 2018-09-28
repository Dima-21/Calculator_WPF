using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public class Calc : INotifyPropertyChanged
    {
        private string history;
        public string History
        {
            get { return history; }
            set
            {
                history = value;
                OnPropertyChanged();
            }
        }

        private string current = "0";
        public string Current
        {
            get { return current; }
            set
            {
                current = value;
                OnPropertyChanged();
            }
        }

        public bool Continue { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        public void EnterNum(string n)
        {
            if (n == ".")
            {
                if (Current.Count(x => x == '.') == 0)
                {
                    Current += ".";
                    Continue = true;
                }
                return;
            }
            else if (n == "<-")
            {
                Current = Current.Remove(Current.Count() - 1);
                if (Current.Count() == 0)
                {
                    Current = "0";
                    Continue = false;
                }
            }
            else if (n == "=")
            {
                Current = Evaluate(History + Current);
                History = "";
                Continue = false;
            }
            else if (n == "C")
            {
                History = "";
                Current = "0";
            }
            else if (n == "CE")
            {
                Current = "0";
                Continue = false;
            }
            else if (("-+*/").Contains(n))
            {
                History += Current;
                Current = Evaluate(History);
                History += n;
                Continue = false;
            }
            else if (Continue)
                Current += n;
            else
            {
                Current = n;
                Continue = true;
            }
        }
        public string Evaluate(string expression)
        {
            expression = expression.Replace(",", ".");
            DataTable table = new DataTable();
            table.Columns.Add("expression", typeof(string), expression);
            DataRow row = table.NewRow();
            table.Rows.Add(row);
            return (string)row["expression"];
        }
    }

}

