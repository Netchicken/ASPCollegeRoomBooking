using System;
using System.IO;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using RestSharp;
using RestSharp.Authenticators;

namespace ASPCollegeBooking.Email
{
    public static class SendSMTP
    {
        public static void SendMessageSmtp()
        {
            // Compose a message
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Excited Admin", "mg.go-cycle-touring.com"));
            mail.To.Add(new MailboxAddress("Excited User", "gary.d@visioncollege.ac.nz"));
            mail.Subject = "Hello from SMTP";
            mail.Body = new TextPart("plain")
            {
                Text = @"Testing some Mailgun awesomesauce!",
            };

            // Send it!
            using (var client = new SmtpClient())
            {
                // XXX - Should this be a little different?
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.mailgun.org", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("postmaster@mg.go-cycle-touring.com", "ae6c897b4a0ceed9bf4dcebdaa09fd30-115fe3a6-4e28d913");

                client.Send(mail);
                client.Disconnect(true);
            }
        }




    }
}
