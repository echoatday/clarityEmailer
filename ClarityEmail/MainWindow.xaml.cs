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
        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            if(txtRecipient.Text != "" && txtSubject.Text != "" && txtBody.Text != "")
            {
                Email.SendEmail(txtRecipient.Text, txtSubject.Text, txtBody.Text);
                error.Text = "Mail sent";
            }
            else
            {
                error.Text = "All fields are required";
            }
        }
    }
}
