using MainMVC.Controllers;
using MainMVC.Models.Polls;
using MainMVC.Models.Users;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using MainMVC.Models.Polls.Entities;
using MainMVC.Utilities;
using MainMVC.Utilities.Controller;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Xunit;

namespace XUnitTestProject.Controllers
{
    public class HomeControllerTests : IDisposable
    {
        private readonly MockRepository _mockRepository;
        private readonly Mock<ILogger<HomeController>> _mockLogger;
        private readonly Mock<IPollRepository> _mockPollRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;

        public HomeControllerTests()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);

            _mockLogger = _mockRepository.Create<ILogger<HomeController>>();
            _mockPollRepository = _mockRepository.Create<IPollRepository>();
            _mockUserRepository = _mockRepository.Create<IUserRepository>();
        }

        public void Dispose()
        {
            //_mockRepository.VerifyAll();
        }

        private HomeController CreateHomeController()
        {
            var controller = new HomeController(
                _mockLogger.Object,
                _mockPollRepository.Object,
                _mockUserRepository.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { Session = new MockHttpSession() }
                }
            };
            return controller;
        }

        [Fact]
        public void PollsList_IsPollList_FistPollName()
        {
            // Arrange
            var homeController = CreateHomeController();
            _mockPollRepository.Setup(repo => repo.GetPolls())
                .Returns(Data.GetData());

            // Act
            var result = homeController.PollsList();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var polls = Assert.IsType<List<Poll>>(viewResult.Model);
            Assert.Equal("First Poll", polls[0].Name);
        }

        //[Fact]
        //public void PollStatistics_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var homeController = CreateHomeController();
        //    int id = 0;

        //    // Act
        //    //var result = homeController.PollStatistics(
        //    //    id);

        //    // Assert
        //    Assert.True(false);
        //}

        //[Fact]
        //public void CreatePollGET_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var homeController = CreateHomeController();

        //    // Act
        //    var result = homeController.CreatePoll();

        //    // Assert
        //    Assert.True(false);
        //}

        //[Fact]
        //public void CreatePollPOST_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange

        //    var homeController = CreateHomeController();
        //    var poll = new Poll(3, "3", "d3",
        //        "Test", DateTime.Now, new List<Question>());

        //    // Act
        //    var result = homeController.CreatePoll(poll);

        //    // Assert
        //    Assert.IsType<RedirectToActionResult>(result);
        //}

        //[Fact]
        //public void EditPollGET_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var homeController = CreateHomeController();

        //    // Act
        //    var result = homeController.EditPoll();

        //    // Assert
        //    Assert.True(false);
        //}

        //[Fact]
        //public void EditPollPOST_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var homeController = CreateHomeController();
        //    Poll poll = null;

        //    // Act
        //    var result = homeController.EditPoll(
        //        poll);

        //    // Assert
        //    Assert.True(false);
        //}

        [Fact]
        public void DeleteQuestion_RemoveQuestion_TotalQuestions1()
        {
            // Arrange
            var homeController = CreateHomeController();
            _mockPollRepository.Setup(
                    repo => repo.Update(It.IsAny<Poll>()))
                .Returns<Poll>(x => x); ;

            homeController.ControllerContext.HttpContext.Session.Put("poll", Data.GetData()[0]);

            // Act
            homeController.DeleteQuestion(1);

            // Assert

            var result = homeController.HttpContext.Session.Get<Poll>("poll");
            Assert.Equal(1, result.Questions.Count);
        }

        [Fact]
        public void AddQuestion_CreateNewQuestion_TotalQuestions3()
        {
            // Arrange
            var homeController = CreateHomeController();
            _mockPollRepository.Setup(
                    repo => repo.Update(It.IsAny<Poll>()))
                .Returns<Poll>(x => x); ;

            homeController.ControllerContext.HttpContext.Session.Put("poll", Data.GetData()[0]);

            // Act
            homeController.AddQuestion();

            // Assert

            var result = homeController.HttpContext.Session.Get<Poll>("poll");
            Assert.Equal(3, result.Questions.Count);
        }

        [Fact]
        public void PassThePollGET_IsPoll_Poll()
        {
            // Arrange
            var homeController = CreateHomeController();
            _mockPollRepository.Setup(
                    repo => repo.Update(It.IsAny<Poll>()))
                .Returns<Poll>(x => x); ;

            homeController.ControllerContext.HttpContext.Session.Put("passing_poll", Data.GetData()[0]);

            // Act
            var result = homeController.PassThePoll();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var poll = Assert.IsType<Poll>(viewResult.Model);
            Assert.Equal("First Poll", poll.Name);
        }

        [Fact]
        public void AddAnswer_IsPollInSession_NotFound()
        {
            // Arrange
            var homeController = CreateHomeController();
            _mockPollRepository.Setup(
                    repo => repo.Update(It.IsAny<Poll>()))
                .Returns<Poll>(x => x); ;

            // Act
            var result = homeController.AddAnswer(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}