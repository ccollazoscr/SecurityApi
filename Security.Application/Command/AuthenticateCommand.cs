using MediatR;
using Security.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Application.Command
{
    public class AuthenticateCommand : IRequest<AuthenticateToken>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public AuthenticateCommand(string username, string password) {
            Username = username;
            Password = password;
        }
    }
}
