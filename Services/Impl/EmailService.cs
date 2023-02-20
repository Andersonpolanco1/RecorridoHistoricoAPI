using Antlr3.ST;
using AutoMapper.Internal;
using EdecanesV2.Models;
using EdecanesV2.Services.Abstract;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace EdecanesV2.Services.Impl
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IWebHostEnvironment _env;
        private readonly string _PathTemplates;

        public EmailService(IOptions<MailSettings> mailSettings, IWebHostEnvironment env)
        {
            _mailSettings = mailSettings.Value;
            _env = env;
            _PathTemplates = Path.Combine(_env.ContentRootPath, "Templates");
        }


        public async Task<bool> SendEmailAsync(MailRequest mailRequest)
        {
            MailMessage mail = new(mailRequest.From, mailRequest.To, mailRequest.Subject, mailRequest.Body);

            SmtpClient client = new()
            {
                Port = _mailSettings.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = _mailSettings.UseDefaultCredentials,
                Host = _mailSettings.Host,
                Credentials = new System.Net.NetworkCredential(_mailSettings.Username, _mailSettings.Password),
                EnableSsl = _mailSettings.EnableSsl
            };

            mail.IsBodyHtml = mailRequest.IsBodyHtml;

            if (!string.IsNullOrEmpty(mailRequest.CC))
                mail.CC.Add(mailRequest.CC);

            mail.BodyEncoding = System.Text.Encoding.UTF8;

            try
            {
                await client.SendMailAsync(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<bool> SendEmailRecorridoCreadoAsync(RecorridoHistorico recorridoHistorico)
        {
            var template = GenerarPlantillaNuevasolicitud(recorridoHistorico);

            var mailRequest = new MailRequest
            {
                From = _mailSettings.From,
                To = recorridoHistorico.Correo,
                Body = template,
                IsBodyHtml = true,
                Subject = "Solicitud de Recorrido Histórico-Cultural"

            };

            return await SendEmailAsync(mailRequest);
        }

        private string GenerarPlantillaNuevasolicitud(RecorridoHistorico recorridoHistorico)
        {
            StringTemplateGroup group = new("myGroup", _PathTemplates);
            StringTemplate stemplate = group.GetInstanceOf("service-mail");
            stemplate.SetAttribute("name", recorridoHistorico.Nombres + " " + recorridoHistorico.Apellidos);
            stemplate.SetAttribute("service", "Solicitud de Recorrido Histórico-Cultural");
            stemplate.SetAttribute("requestNumber", recorridoHistorico.Id);
            stemplate.SetAttribute("daysNumber", 10);
            stemplate.SetAttribute("phoneNumber", "809-695-8000");
            stemplate.SetAttribute("ext", "2074");
            stemplate.SetAttribute("depart", "Edecanes");

            return stemplate.ToString();
        }
    }
}
