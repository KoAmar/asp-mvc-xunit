﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MainMVC.Models.Polls;
using MainMVC.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MainMVC.Controllers
{
    public class Auth : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserRepository _userRepository;

        public Auth(ILogger<HomeController> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
        }
        public IActionResult MyPage()
        {
            return NotFound();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            IActionResult result = View(user);
            if (ModelState.IsValid)
            {
                if (_userRepository.Register(user) == null)
                {
                    result = RedirectToAction("MyPage", "Auth");
                }
                else
                {
                    result = RedirectToAction("Index", "Home");
                }
            }
            return result;
        }


    }
}