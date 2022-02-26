using System;
using System.Windows;
using ClarityDLL;

namespace ClarityEmail
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Spit form text into the actual SendEmail method.
        private async void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            if(txtRecipient.Text != "" && txtSubject.Text != "" && txtBody.Text != "")
            {
                try
                {
                    error.Text = await Email.SendEmail(txtRecipient.Text, txtSubject.Text, txtBody.Text);
                }
                catch (System.FormatException)
                {
                    error.Text = "Invalid email format";
                }
            }
            else
            {
                error.Text = "All fields are required";
            }
        }
    }
}
