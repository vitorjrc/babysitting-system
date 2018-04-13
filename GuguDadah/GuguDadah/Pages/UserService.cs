using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuguDadah.Data;

namespace GuguDadah.Pages
{
    public interface IUserService {

        Client Authenticate(string username, string password);
    }

    public class UserService : IUserService {

        private readonly AppDbContext dbContext;

        public UserService(AppDbContext context) {

            dbContext = context;
        }

        public Client Authenticate(string username, string password) {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            Client client = dbContext.Clients.FirstOrDefault(m => m.userName.Equals(username));

            if (!password.Equals(client.password)) return null;

            return client;
        }
    }
}