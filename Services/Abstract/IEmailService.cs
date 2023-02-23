using AutoMapper.Internal;
using RecorridoHistoricoApi.Models;

namespace RecorridoHistoricoApi.Services.Abstract
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(MailRequest mailRequest);
        Task<bool> SendEmailRecorridoCreadoAsync(RecorridoHistorico recorridoHistorico);
    }
}
