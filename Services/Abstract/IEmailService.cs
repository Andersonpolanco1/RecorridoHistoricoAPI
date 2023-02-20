using AutoMapper.Internal;
using EdecanesV2.Models;

namespace EdecanesV2.Services.Abstract
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(MailRequest mailRequest);
        Task<bool> SendEmailRecorridoCreadoAsync(RecorridoHistorico recorridoHistorico);
    }
}
