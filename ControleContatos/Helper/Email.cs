using System.Net;
using System.Net.Mail;

namespace ControleContatos.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool Enviar(string email, string assunto, string mensagem)
        {
            try
            {
                // Obtendo as configurações do SMTP
                string host = _configuration.GetValue<string>("SMTP:Host") ?? throw new ArgumentNullException("Host não configurado.");
                string nome = _configuration.GetValue<string>("SMTP:Nome") ?? "Sistema de Contatos";
                string username = _configuration.GetValue<string>("SMTP:UserName") ?? throw new ArgumentNullException("UserName não configurado.");
                string senha = _configuration.GetValue<string>("SMTP:Senha") ?? throw new ArgumentNullException("Senha não configurada.");
                int porta = _configuration.GetValue<int>("SMTP:Porta");

                // Configurando a mensagem
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(username, nome);
                    mail.To.Add(email); // Endereço de e-mail do destinatário
                    mail.Subject = assunto;
                    mail.Body = mensagem;
                    mail.IsBodyHtml = true; // Define se o corpo é HTML
                    mail.Priority = MailPriority.High;

                    // Configurando o cliente SMTP
                    using (SmtpClient smtp = new SmtpClient(host, porta))
                    {
                        smtp.Credentials = new NetworkCredential(username, senha);
                        smtp.EnableSsl = true; // Ativando SSL para conexão segura
                        smtp.Send(mail); // Enviando o e-mail
                    }
                }

                return true; // Sucesso no envio
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"Erro SMTP: {smtpEx.Message}");
                Console.WriteLine($"Status Code: {smtpEx.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
                return false;
            }
        }
    }
}
