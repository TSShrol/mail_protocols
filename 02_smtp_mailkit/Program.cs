using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MailKit.Security;
using MimeKit;

namespace _02_smtp_mailkit
{
    internal class Program
    {
        // ! change the credentials and addresses
        const string from = "shrolts@gmail.com"; // change here
        const string password = "axatjelehoetummx"; // change here

        static void Main(string[] args)
        {
            ///////////// Send Mails (SMTP)
            Console.Write("Enter address to send an email: ");
            string to = Console.ReadLine();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Tetiana", from));  // change here
            message.To.Add(new MailboxAddress("Test User", to)); // change here
            message.Subject = "Добрий вечір, ми з України!";
            message.Importance = MessageImportance.High;

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Alice,

            What are you up to this weekend? Monica is throwing one of her parties on
            Saturday and I was hoping you could make it.

            Will you be my +1?

            -- Joey 
            "
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);

                client.Authenticate(from, password);

                //var options = FormatOptions.Default.Clone();

                //if (client.Capabilities.HasFlag(SmtpCapabilities.UTF8))
                //    options.International = true;

                client.Send(message);

                client.Disconnect(true);
            }


            //#region IMAP
            /////////// Get Mails (IMAP)
            //Console.OutputEncoding = Encoding.UTF8;

            //using (var client = new ImapClient())
            //{
            //    client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

            //    client.Authenticate(from, password);

            //    // --------------- get all folders
            //    foreach (var item in client.GetFolders(client.PersonalNamespaces[0]))
            //    {
            //        Console.WriteLine("Folder: " + item.Name);
            //    }

            //    // -------------- get all sent messages
            //    var folder = client.GetFolder(SpecialFolder.Sent);
            //    folder.Open(FolderAccess.ReadOnly);

            //    IList<UniqueId> uids = folder.Search(SearchQuery.All);

            //    foreach (var i in uids.Take(5))
            //    {
            //        MimeMessage message1 = folder.GetMessage(i);
            //        Console.WriteLine($"{message1.Date}: {message1.Subject} - {new string(message1.TextBody.Take(10).ToArray())}...");
            //    }

            //    // -------------------- show Inbox 
            //    client.Inbox.Open(FolderAccess.ReadOnly);

            //    foreach (var uid in client.Inbox.Search(SearchQuery.All).Take(5))
            //    {
            //        var m = client.Inbox.GetMessage(uid);
            //        // show message details
            //        Console.WriteLine($"Mail: {m.Subject}");
            //    }

            //    // ---------------------- delete message
            //    client.GetFolder(SpecialFolder.Sent).Open(FolderAccess.ReadWrite);
            //    var id = folder.Search(SearchQuery.All).FirstOrDefault();
            //    var mail = folder.GetMessage(id);

            //    Console.WriteLine(mail.Date + " " + mail.Subject);

            //    folder.MoveTo(id, client.GetFolder(SpecialFolder.Junk)); // move to spam
            //    folder.AddFlags(id, MessageFlags.Deleted, true);

            //    Console.WriteLine("Press to exit!");
            //    Console.ReadKey();

            //    client.Disconnect(true);
            //}
            //#endregion


        }
    }
}