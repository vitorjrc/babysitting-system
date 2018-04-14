using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuguDadah.Data;

namespace GuguDadah.Pages {
    public interface IUserService {

        Client AuthenticateClient(string username, string password);
        Professional AuthenticateProfessional(string username, string password);
    }

    public class UserService : IUserService {

        private readonly AppDbContext dbContext;

        public UserService(AppDbContext context) {

            dbContext = context;
        }

        public Client AuthenticateClient(string username, string password) {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            Client client = dbContext.Clients.FirstOrDefault(m => m.userName.Equals(username));
            if (client != null && password.Equals(client.password)) return client;

            return null;
        }


        public Professional AuthenticateProfessional(string username, string password) {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            Professional professional = dbContext.Professionals.FirstOrDefault(m => m.userName.Equals(username));
            if (professional != null && password.Equals(professional.password)) return professional;

            return null;
        }
    }
}