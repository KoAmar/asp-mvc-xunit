﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainMVC.Models.Users
{
    public interface IUserRepository
    {

        User Login(string email, string password);
        User Register(string email, string login, string password);
        bool ContainEmail(string email);
        User GetCurrentUser();
        bool IsLogged();
    }
}
