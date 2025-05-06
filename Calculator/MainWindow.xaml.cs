using System;
using System.Collections.Generic;
using System.Globalization;
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
            string digit = button.Content.ToString();

            if (currentInput == "0" && digit != "0")
            {
                currentInput = digit;
            }
            else if (currentInput == "-0")
            {
                currentInput = "-" + digit;
            }
           
            else
            {
                currentInput += digit;
            }

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

            
            if (operatorSymbol == "-" && string.IsNullOrEmpty(currentInput))
            {
                currentInput = "-";
                numbersTbl.Text = currentInput;
                return;
            }

            if (currentInput == "-" || currentInput == ".")
            {
                return;
            }

          
            if (!string.IsNullOrEmpty(currentInput) && currentInput.EndsWith("."))
            {
                currentInput = currentInput.TrimEnd('.'); 
            }

          
            if (string.IsNullOrEmpty(currentInput) && !firstOperand.HasValue)
            {
                return;
            }

            
            if (!string.IsNullOrEmpty(currentInput))
            {
                try
                {
                    
                    string numberToParse = currentInput.Replace(".",
                        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    firstOperand = double.Parse(numberToParse);
                    operation = operatorSymbol[0];
                    currentInput = ""; 
                }
                catch (FormatException)
                {
                    numbersTbl.Text = "Error";
                    ResetCalculator();
                    return;
                }
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
                try
                {
                  
                    string numberString = currentInput.Replace(".",
                        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    double secondOperand = double.Parse(numberString);
                    double result = 0;
                    bool calculationError = false;

                    switch (operation.Value)
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
                                numbersTbl.Text = "Error: Division by zero";
                                calculationError = true;
                            }
                            else
                            {
                                result = firstOperand.Value / secondOperand;
                            }
                            break;
                        default:
                            numbersTbl.Text = "Error: Invalid operation";
                            calculationError = true;
                            break;
                    }

                    if (!calculationError)
                    {
                        numbersTbl.Text = result.ToString(CultureInfo.InvariantCulture);
                        firstOperand = result;
                        currentInput = "";
                    }
                    else
                    {
                        ResetCalculator();
                    }
                }
                catch (FormatException)
                {
                    numbersTbl.Text = "Error: Invalid number";
                    ResetCalculator();
                }
                catch (OverflowException)
                {
                    numbersTbl.Text = "Error: Too large";
                    ResetCalculator();
                }
            }
        }

        private void ResetCalculator()
        {
            firstOperand = null;
            operation = null;
            currentInput = "";
        }
        private void DecimalPoint_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(currentInput))
            {
                currentInput = "0.";
            }
           
            else if (currentInput == "-")
            {
                currentInput = "-0.";
            }
           
            else if (!currentInput.Contains("."))
            {
                currentInput += ".";
            }

            numbersTbl.Text = currentInput;
        }

    }
}
