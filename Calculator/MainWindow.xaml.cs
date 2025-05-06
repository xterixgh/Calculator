using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double? firstOperand = null;
        private char? operation = null; 
        private string currentInput = "";

        public MainWindow()
        {
            InitializeComponent();
        }


        private void One_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            currentInput += button.Content.ToString(); 
            numbersTbl.Text = currentInput;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            firstOperand = null;
            operation = null;
            currentInput = "";
            numbersTbl.Text = "";
        }
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string operatorSymbol = button.Content.ToString();

            if (operatorSymbol == "-" && string.IsNullOrEmpty(currentInput) && firstOperand == null)
            {
                currentInput = "-";
                numbersTbl.Text = currentInput;
                return; 
            }

            
            if (!string.IsNullOrEmpty(currentInput))
            {
                firstOperand = double.Parse(currentInput);
                operation = operatorSymbol[0];
                currentInput = "";
                numbersTbl.Text = "";
            }
            
            else if (firstOperand.HasValue)
            {
                operation = operatorSymbol[0];
            }
        }


        private void Equally_Click(object sender, RoutedEventArgs e)
        {
            if (firstOperand.HasValue && operation.HasValue && !string.IsNullOrEmpty(currentInput))
            {
                double secondOperand = double.Parse(currentInput);
                double result = 0;

                switch (operation)
                {
                    case '+':
                        result = firstOperand.Value + secondOperand;
                        break;
                    case '-':
                        result = firstOperand.Value - secondOperand;
                        break;
                    case '*':
                        result = firstOperand.Value * secondOperand;
                        break;
                    case '/':
                        if (secondOperand == 0)
                        {
                            numbersTbl.Text = "Error";
                            return;
                        }
                        result = firstOperand.Value / secondOperand;
                        break;
                }

                numbersTbl.Text = result.ToString();
                firstOperand = result;  
                currentInput = "";
                operation = null;
            }

            }
    }
}
