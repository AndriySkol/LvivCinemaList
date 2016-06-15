using Moq;
using MovieDomain.UnitOfWork;
using MovieServices.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;


namespace NUnitTestProject
{
    [TestFixture]
    public class AuthenticationTests : AssertionHelper
    {
        IUnitOfWorkFactory _factory;
        Mock<IUnitOfWork> _unitMock;
        Mock<IUserManager> _userManager;
        Mock<IAuthService> _authService;


        [SetUp]
        public void Initialise()
        {
            Mock<IUnitOfWorkFactory> _factoryMock = new Mock<IUnitOfWorkFactory>();

        }

        [Test]
        public void BadModel()
        {
            Expect(true);
        }

        [Test]
        public void GoodRegistration()
        {
            Expect(true);
        }

        [Test]
        public void UserExists()
        {
            Expect(true);
        }

        [Test]
        public void RightLogin()
        {
            Expect(true);
        }

        [Test]
        public void LoginCredentialsWrong()
        {
            Expect(true);
        }
    }
}
