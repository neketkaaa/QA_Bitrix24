using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.TestEntities
{
    public class Account
    {
        public Account(string login, string password, string email, string name, string lastName, string flatNum, string role)
        {
            Name = name;
            LastName = lastName;
            Login = login;
            Password = password;
            Email = email;
            FlatNum = flatNum;
            role = role;
        }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FlatNum { get; set; }
        public string Role { get; set; }
    }
}
