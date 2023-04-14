using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

namespace _01_smtp
{
    public partial class MainWindow : Window
    {
        // generate apps password
        // https://stackoverflow.com/questions/72547853/unable-to-send-email-in-c-sharp-less-secure-app-access-not-longer-available
        // https://temp-mail.org/uk/view/6439496dd46994075777780f
        //https://support.google.com/mail/answer/7126229?hl=en#zippy=%2Cstep-check-that-imap-is-turned-on%2Cstep-change-smtp-other-settings-in-your-email-client
        //https://myaccount.google.com/apppasswords?pli=1&rapt=AEjHL4P7BxaI57e3X1AtR_Mtg1-laQpRCtx26qya_VcrDH6f2nun4hiZOyPgV2j_BK34SKzfHuQqY47ot75X9P_eMPJzl2GWBA

        const string myMailAddress = "shrolts@gmail.com";
        const string accountPassword = "jvrsuoowsoikuver";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // create new mail
            MailMessage mail = new MailMessage(myMailAddress, toTxtBox.Text)
            {
                Subject = subjectTxtBox.Text,
                Body = $"<h2>My Mail Message from C#</h2><p>{bodyTxtBox.Text}</p>",
                IsBodyHtml = true,
                Priority = MailPriority.High
            };

            // add attachments
            var result = MessageBox.Show("Do you want to attach a file?", "Attach File", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == true)
                    mail.Attachments.Add(new Attachment(dialog.FileName));
            }

            // send mail message
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(myMailAddress, accountPassword),
                EnableSsl = true
            };

            client.Send(mail);
        }
    }
}
