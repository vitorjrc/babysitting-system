using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuguDadah.Data;

namespace GuguDadah.Includes {
    public interface ILoginService {

        Client AuthenticateClient(string username, string password);
        Professional AuthenticateProfessional(string username, string password);
    }

    public class LoginService : ILoginService {

        private readonly AppDbContext dbContext;

        public LoginService(AppDbContext context) {

            dbContext = context;
        }

        public Client AuthenticateClient(string username, string password) {

            // retorna null se os argumentos são null
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            // vai buscar o cliente à BD
            Client client = dbContext.Clients.FirstOrDefault(m => m.UserName.Equals(username));;

            // se for diferente de null e se a password corresponder, retorna o cliente
            if (client != null && BCrypt.Net.BCrypt.Verify(password, client.Password)) return client;

            return null;
        }


        public Professional AuthenticateProfessional(string username, string password) {

            // retorna null se os argumentos são null
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            // vai buscar o profissional à BD
            Professional professional = dbContext.Professionals.FirstOrDefault(m => m.UserName.Equals(username));

            // se for diferente de null e se a password corresponder, retorna o profissional
            if (professional != null && BCrypt.Net.BCrypt.Verify(password, professional.Password)) return professional;

            return null;
        }
    }
}