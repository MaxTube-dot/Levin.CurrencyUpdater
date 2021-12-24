using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsClient.WinCurrencyService;

namespace WinFormsClient
{
    public partial class Form1 : Form
    {
        private string Date { get { return textBox1.Text + "." + textBox2.Text + "." + textBox3.Text; } }

        WinCurrencyService.CurrencyUpdateClient WinCurrencyService;
        public Form1()
        {
            InitializeComponent();

            WinCurrencyService = new WinCurrencyService.CurrencyUpdateClient("BasicHttpBinding_ICurrencyUpdate");

            WinCurrencyService.Open();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            CorrectDate(textBox1, 1, 31);
        }

        /// <summary>
        /// Очистка полей для ввода.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            textBox2.Text = "";

            textBox3.Text = "";

            textBox4.Text = "";
        }

     

        private void button2_Click(object sender, EventArgs e)
        {
 
            DateTime dateTime;

            if (!DateTime.TryParse(Date, out dateTime))
            {
                label5.Text = "Неверно введена дата, такой даты не существует!";

                return;
            }

            double result=0;

            try
            {
                string nameCurrency = textBox4.Text;

                result = WinCurrencyService.GetCurrencyForDayCharCode(dateTime, nameCurrency); 
            }
            catch (FaultException<InvalidDateFault> dateException)
            {
                label5.Text = dateException.Detail.CustomError;

                textBox1.BackColor = Color.Red;

                textBox2.BackColor = Color.Red;

                textBox3.BackColor = Color.Red;

                return;
            }
            catch (FaultException<InvalidCurrencyFault> currencyException)
            {
                label5.Text = currencyException.Detail.CustomError;

                textBox4.BackColor = Color.Red;

                return;
            }
            catch (FaultException<InvalidServerFault> ServerExeption)
            {
                label5.Text = ServerExeption.Detail.CustomError;

                return;
            }

            label5.Text = $"1 {textBox4.Text.ToUpper()} = {result} руб.";

            textBox1.BackColor = Color.White;

            textBox2.BackColor = Color.White;

            textBox3.BackColor = Color.White;

            textBox4.BackColor = Color.White;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CorrectDate(textBox2, 1, 12);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            CorrectDate(textBox3, 1991, DateTime.Now.Year);

        }

        /// <summary>
        /// Подсветка неправильно введенных символов в TextBox.
        /// </summary>
        /// <param name="textBox">TextBox содержимое которого будет контролироваться.</param>
        /// <param name="minValue">Минимальное значение TextBox.</param>
        /// <param name="maxValue">Максимальное значение TextBox.</param>
        private void CorrectDate(TextBox textBox,int minValue,int maxValue)
        {
            var value = textBox.Text;

            int day;

            if (!int.TryParse(value, out day))
            {
                textBox.BackColor = Color.Red;
                return;
            }

            if (day >= minValue && day <= maxValue)
            {
                textBox.BackColor = Color.White;
                return;
            }

            textBox.BackColor = Color.Red;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.Day.ToString();
            textBox2.Text = DateTime.Now.Month.ToString();
            textBox3.Text = DateTime.Now.Year.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
