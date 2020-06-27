using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.Threading;
using System.Windows.Markup;
using System.Windows.Forms;
using System.Drawing;

namespace copie_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon notifyIcon;
        double memory = 0;
        double copie;
        double number1 ;
        double number2 ;
        char operation = '\n';
        char prevoperation = '\n';
        bool isOperationPerformed=false;
        bool operationPerformed = false;

        public MainWindow()
        {
            InitializeComponent();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon();
            this.notifyIcon.Visible = true;
            this.notifyIcon.Icon = new Icon("icon.ico");
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_DoubleClick);
        }
        
        private void notifyIcon_DoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            if (this.WindowState == WindowState.Minimized)
                {
                    this.WindowState = WindowState.Normal;
                    
                }
           this.Activate();
          
            if (this.WindowState == WindowState.Normal)
            {
                notifyIcon.Visible = false;
            }
          
        }

        private void Number(char symbol)
        {

                if (txtdisplay.Text == "0" && txtdisplay.Text.Length<15)
                    txtdisplay.Text = symbol + "";
                else if (txtdisplay.Text != "0" && txtdisplay.Text.Length<15)
                txtdisplay.Text += symbol;
                isOperationPerformed = false;
            
        }

        private void Operation(char op)
        {
            if (op == '√')
            {
                number1 = double.Parse(txtdisplay.Text);
                txtdisplay.Text = "";
                number1 = Math.Sqrt(number1);
                txtdisplay.Text += number1;
            }
            else if (op == '^')
            {
                number1 = double.Parse(txtdisplay.Text);
                txtdisplay.Text = "";
                number1 = Math.Pow(number1, 2);
                txtdisplay.Text += number1;
            }
            else if (op == '|')
            {
                number1 = double.Parse(txtdisplay.Text);
                txtdisplay.Text = "";
                number1 = 1 / number1;
                txtdisplay.Text += number1;
            }
            else if (op == '%')
            {
                prevoperation = '%';
                if (op != '\n')
                {
                    copie = number1;
                    number2 = double.Parse(txtdisplay.Text);
                    txtdisplay.Text = "";
                    number1 = (number2 * number1)/100;
                    txtdisplay.Text += number1;
                }
            }
            else if (operation == '\n' || operation == '=')
                number1 = double.Parse(txtdisplay.Text);
            else
            {
                if (!string.IsNullOrEmpty(txtdisplay.Text) && !string.IsNullOrEmpty(txtdisplay.Text))
                    number2 = double.Parse(txtdisplay.Text);
                switch (operation)
                {
                    case '+':
                        number1 += number2;
                        break;
                    case '-':
                        number1 -= number2;
                        break;
                    case '*':
                        number1 *= number2;
                        break;
                    case '/':
                        {
                            if (number2 == 0)
                            {
                                number1 = 0;
                                number2 = 0;
                                txtdisplay.Text = "Cannot divide by 0 !";
                            }
                            else
                                number1 /= number2;
                            break;
                        }
                }
            }
            txtdisplay.Text = number1 + "";
            operationPerformed = true;
            operation = op;
        }

        private void Egal()
        {
            if (operation != '=')
            {
                if (txtdisplay.Text == "")
                    number2 = 0;
                else
                    number2 = double.Parse(txtdisplay.Text);
            }
            switch(operation)
            {
                case '+':
                    number1 += number2;
                    break;
                case '-':
                    number1 -= number2;
                    break;
                case '*':
                    number1 *= number2;
                    break;
                case '/':
                    if (number2 == 0)
                    {
                        number1 = 0;
                        number2 = 0;
                        txtdisplay.Text = "Cannot divide by 0 !";
                        operation = '\n';
                        return;
                    }
                    else
                        number1 /= number2;
                    break;
                case '=':
                    if (prevoperation == '%')
                        number1 += copie;
                    else
                    number1 += number2;
                    break;
                    
            }
            if (number1 != 0)
            {
                txtdisplay.Text = number1 + "";
                operation = '=';
            }
            else
                txtdisplay.Text = number2 + "";
           
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            char symbol;
            System.Windows.Controls.Button button = (System.Windows.Controls.Button)sender;
            symbol = Convert.ToChar(button.Content);

                if (operationPerformed == true)
                {
                    txtdisplay.Text = "";
                    operationPerformed = false;
                }
                Number(symbol);
            
        }

       
        private void btnp_Click(object sender, RoutedEventArgs e)
        {
            Operation('+');    
        
        }

        private void btnm_Click(object sender, RoutedEventArgs e)
        {
            Operation('-');
        }

        private void btnx_Click(object sender, RoutedEventArgs e)
        {
            Operation('*');
        }

        private void btndiv_Click(object sender, RoutedEventArgs e)
        {
         if(isOperationPerformed==false)
            {
                Operation('/');
                isOperationPerformed = true;
            }
        }

        private void btnrad_Click(object sender, RoutedEventArgs e)
        {
            Operation('√');
            if (number1 != 0)
            {
                txtdisplay.Text = number1 + "";
                operation = '=';
            }
            else
                txtdisplay.Text = number2 + "";
        }

        private void btnx2_Click(object sender, RoutedEventArgs e)
        {
            Operation('^');
            if (number1 != 0)
            {
                txtdisplay.Text = number1 + "";
                operation = '=';
            }
            else
                txtdisplay.Text = number2 + "";
        }

        private void btn1x_Click(object sender, RoutedEventArgs e)
        {
            Operation('|');
            if (number1 != 0)
            {
                txtdisplay.Text = number1 + "";
                operation = '=';
            }
            else
                txtdisplay.Text = number2 + "";
        }

        private void btnmod_Click(object sender, RoutedEventArgs e)
        {
            Operation('%');
            if (number1 != 0)
            {
                txtdisplay.Text = number1 + "";
                operation = '=';
            }
            else
                txtdisplay.Text = number2 + "";
        }

        private void btnCE_Click(object sender, RoutedEventArgs e)
        {
            if (operation == '\n')
            {
                number1 = 0;
                txtdisplay.Text = "";
            }
            else
            {
                number2 = 0;
                txtdisplay.Text = "";
            }
        }

        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            number1 = 0;
            number2 = 0;
            operation = '\n';
            txtdisplay.Text = "0";
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            if (txtdisplay.Text != String.Empty && txtdisplay.Text!="0")
                txtdisplay.Text = txtdisplay.Text.Remove(txtdisplay.Text.Length - 1);
            
        }

        private void btnMS_Click(object sender, RoutedEventArgs e)
        {
            memory = double.Parse(txtdisplay.Text);
        }

        private void btnMM_Click(object sender, RoutedEventArgs e)
        {
            memory -= double.Parse(txtdisplay.Text);
        }

        private void btnMP_Click(object sender, RoutedEventArgs e)
        {
            memory += double.Parse(txtdisplay.Text);
        }

        private void btnMR_Click(object sender, RoutedEventArgs e)
        {
            txtdisplay.Text = memory.ToString();
        }

        private void btnMC_Click(object sender, RoutedEventArgs e)
        {
            memory = 0;
        }

        private void btnpm_Click(object sender, RoutedEventArgs e)
        {
            if (operation == '\n')
            {
                number1 = double.Parse(txtdisplay.Text);
                number1 *= -1;
                txtdisplay.Text = number1.ToString();
            }
            else
            {
                number2 = double.Parse(txtdisplay.Text);
                number2 *= -1;
                txtdisplay.Text = number2.ToString();
            }

        }

        private void btnpct_Click(object sender, RoutedEventArgs e)
        {
            if ((txtdisplay.Text == "0") || (isOperationPerformed))
                txtdisplay.Text = String.Empty;

            isOperationPerformed = false;
            System.Windows.Controls.Button button = (System.Windows.Controls.Button)sender;
            if (button.Content == ".")
            {
                if (!txtdisplay.Text.Contains("."))
                    txtdisplay.Text = txtdisplay.Text + button.Content;

            }
            else
                if (!txtdisplay.Text.Contains("."))
                    txtdisplay.Text = txtdisplay.Text + button.Content;

        }

        private void btne_Click(object sender, RoutedEventArgs e)
        {
            Egal();
        }

     
        private void KeyInput(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.D0 || e.Key == Key.NumPad0)
            {
                if (operationPerformed == true)
                {
                    txtdisplay.Text = "";
                    operationPerformed = false;
                }
                Number('0');
            }
            else if (e.Key == Key.D1 || e.Key == Key.NumPad1)
            {
                if (operationPerformed == true)
                {
                    txtdisplay.Text = "";
                    operationPerformed = false;
                }
                Number('1');
            }
            else if ((e.Key == Key.D2 && !Keyboard.IsKeyDown(Key.LeftShift)) || e.Key == Key.NumPad2)
            {
                if (operationPerformed == true)
                {
                    txtdisplay.Text = "";
                    operationPerformed = false;
                }
                Number('2');
            }
            else if (e.Key == Key.D3 || e.Key == Key.NumPad3)
            {
                if (operationPerformed == true)
                {
                    txtdisplay.Text = "";
                    operationPerformed = false;
                }
                Number('3');
            }
            else if (e.Key == Key.D4 || e.Key == Key.NumPad4)
            {
                if (operationPerformed == true)
                {
                    txtdisplay.Text = "";
                    operationPerformed = false;
                }
                Number('4');
            }
            else if (e.Key == Key.NumPad5 || (e.Key == Key.D5 && !Keyboard.IsKeyDown(Key.LeftShift)))
            {
                if (operationPerformed == true)
                {
                    txtdisplay.Text = "";
                    operationPerformed = false;
                }
                Number('5');
            }
            else if (e.Key == Key.NumPad6 || (e.Key == Key.D6 && !Keyboard.IsKeyDown(Key.LeftShift)))
            {
                if (operationPerformed == true)
                {
                    txtdisplay.Text = "";
                    operationPerformed = false;
                }
                Number('6');
            }
            else if (e.Key == Key.D7 || e.Key == Key.NumPad7)
            {
                if (operationPerformed == true)
                {
                    txtdisplay.Text = "";
                    operationPerformed = false;
                }
                Number('7');
            }
            else if ((e.Key == Key.D8 && !Keyboard.IsKeyDown(Key.LeftShift)) || e.Key == Key.NumPad8)
            {
                if (operationPerformed == true)
                {
                    txtdisplay.Text = "";
                    operationPerformed = false;
                }
                Number('8');
            }
            else if (e.Key == Key.D9 || e.Key == Key.NumPad9)
            {
                if (operationPerformed == true)
                {
                    txtdisplay.Text = "";
                    operationPerformed = false;
                }
                Number('9');
            }
            else if (e.Key == Key.Add || (e.Key == Key.OemPlus && !Keyboard.IsKeyDown(Key.LeftShift)))
            {
                if (isOperationPerformed == false)
                {
                    Operation('+');
                    isOperationPerformed = true;
                }
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                if (isOperationPerformed == false)
                {
                    Operation('-');
                    isOperationPerformed = true;
                }
            }
            else if (e.Key == Key.Multiply || (e.Key == Key.D8 && Keyboard.IsKeyDown(Key.LeftShift)))
            {
                if (isOperationPerformed == false)
                {
                    Operation('*');
                    isOperationPerformed = true;
                }
            }
            else if (e.Key == Key.Divide || (e.Key == Key.OemQuestion && Keyboard.IsKeyDown(Key.LeftShift)))
            {
                if (isOperationPerformed == false)
                {
                    Operation('/');
                    isOperationPerformed = true;
                }
            }
            else if (e.Key == Key.D5 && Keyboard.IsKeyDown(Key.LeftShift))
            {
                Operation('%');
                if (number1 != 0)
                {
                    txtdisplay.Text = number1 + "";
                    operation = '=';
                }
                else
                    txtdisplay.Text = number2 + "";
            }
            else if (e.Key == Key.D6 && Keyboard.IsKeyDown(Key.LeftShift))
            {
                Operation('^');
                if (number1 != 0)
                {
                    txtdisplay.Text = number1 + "";
                    operation = '=';
                }
                else
                    txtdisplay.Text = number2 + "";
            }
            else if (e.Key == Key.OemPipe)
            {
                Operation('|');
                if (number1 != 0)
                {
                    txtdisplay.Text = number1 + "";
                    operation = '=';
                }
                else
                    txtdisplay.Text = number2 + "";
            }
            else if (e.Key == Key.D2 && Keyboard.IsKeyDown(Key.LeftShift))
            {
                Operation('√');
                if (number1 != 0)
                {
                    txtdisplay.Text = number1 + "";
                    operation = '=';
                }
                else
                    txtdisplay.Text = number2 + "";
            }

            else if (e.Key == Key.Decimal)
            {
                if ((txtdisplay.Text == "0") || (isOperationPerformed))
                    txtdisplay.Text = String.Empty;

                isOperationPerformed = false;
                if (operation == '.')
                {
                    if (!txtdisplay.Text.Contains("."))
                        txtdisplay.Text = txtdisplay.Text + '.';

                }
                else
                    if (!txtdisplay.Text.Contains("."))
                    txtdisplay.Text = txtdisplay.Text + '.';
            }
            else if (e.Key == Key.Back)
            {
                if (txtdisplay.Text != String.Empty && txtdisplay.Text != "0")
                    txtdisplay.Text = txtdisplay.Text.Remove(txtdisplay.Text.Length - 1);
            }
            else if (e.Key == Key.Escape)
            {
                number1 = 0;
                number2 = 0;
                operation = '\n';
                txtdisplay.Text = "0";
            }

            else if (e.Key == Key.Enter || (e.Key == Key.OemPlus && Keyboard.IsKeyDown(Key.LeftShift)))
                Egal();

            else if (e.Key == Key.P && Keyboard.IsKeyDown(Key.M))
            {
                memory += double.Parse(txtdisplay.Text);
            }
            else if (e.Key == Key.N && Keyboard.IsKeyDown(Key.M)) 
            {
                memory -= double.Parse(txtdisplay.Text);
            }
            else if (e.Key == Key.R && Keyboard.IsKeyDown(Key.M))
            {
                txtdisplay.Text = memory.ToString();
            }
            else if (e.Key == Key.C && Keyboard.IsKeyDown(Key.M))
            {
                memory = 0;
            }
            else if (e.Key == Key.S && Keyboard.IsKeyDown(Key.M))
            {
                memory = double.Parse(txtdisplay.Text);
            }

        }

        private void About(object sender, RoutedEventArgs e)
        {
            About objAboutWindow = new About();
            objAboutWindow.Show();

        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Clipboard.SetText(txtdisplay.SelectedText);
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            int ok = 1;
            string s = System.Windows.Clipboard.GetText();
            
            foreach(char c in s)
            {
                if (c!='.'&&(c < '0' || c > '9'))
                    ok = 0;
            }
            if(ok==1)
                txtdisplay.Text = s.ToString();

        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            if (txtdisplay.SelectedText != "")
            {
                System.Windows.Clipboard.SetText(txtdisplay.SelectedText);
                txtdisplay.SelectedText = String.Empty;
            }
        }
    }
}
