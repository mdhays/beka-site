using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BekaWebsite.Models;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace BekaWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public static void Mailer(string fromAddress, string subject, string body)
        {

            //Configure the smtp server
            const string smtpServer = "smtp.gmail.com";
            const int smtpPort = 587;

            //Get credentials and log in
            string unPath = Directory.GetCurrentDirectory() + "\\un.txt";
            string pwPath = Directory.GetCurrentDirectory() + "\\pw.txt";
            string username = System.IO.File.ReadAllText(unPath);
            string password = System.IO.File.ReadAllText(pwPath);

            var client = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            //Build the emailmessage
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromAddress);
            mailMessage.To.Add("matthew.hays.dev@gmail.com");
            mailMessage.Subject = "hi from beka's website";
            mailMessage.Body = "hi";

            //Send the message
            try
            {
                client.Send(mailMessage);
            }
            catch(Exception E)
            {
                throw;
            }

        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //move this to its own post route eventually
            //temporary code for testing
            string unPath = Directory.GetCurrentDirectory() + "\\un.txt";
            string fromAddress = System.IO.File.ReadAllText(unPath);
            const string subject = "subject";
            const string body = "body";
            Mailer(fromAddress, subject, body);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public  IActionResult PostMessage()
        {


            return RedirectToAction("Index");
        }
    }
}
