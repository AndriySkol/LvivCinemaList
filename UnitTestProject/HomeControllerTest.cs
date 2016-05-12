using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieDemo.Services;
using MovieDemo.Models;
using MovieDemo.Controllers;
using System.Web.Mvc;

namespace UnitTestProject
{
    [TestClass]
    public class HomeControllerTest
    {
        ScheduleModel model = new ScheduleModel { CinemaName = "KingCross" };

        [TestMethod]
        public void RightModelReturned()
        {
            Mock<IMovieService> mock = new Mock<IMovieService>();
            mock.Setup(m => m.GetModel()).Returns(model);
            HomeController controller = new HomeController(mock.Object);
            ViewResult view = (ViewResult)controller.Index();
            Assert.AreEqual(view.Model, model);
        }
    }
}
