using System;
using NUnit.Framework;
using MovieDomain.UnitOfWork;
using Moq;
using MovieDomain.Repository;
using MovieDomain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using MovieServices.Service;
using MovieDemo.ApiControllers;
using System.Web.Http.Results;
using MovieServices.Interfaces;
namespace NUnitTestProject
{
    [TestFixture]
    public class LikeTests : AssertionHelper
    {
        IUnitOfWorkFactory _factory;
        List<Rate> rates;
        Mock<IRepository<Rate>> _ratesMock;
        Mock<IUnitOfWork> _unitMock;
        Mock<ILikeService> _likeService;

        [SetUp]
        public void Initialise()
        {
            Mock<IUnitOfWorkFactory> _factoryMock = new Mock<IUnitOfWorkFactory>();
            _unitMock = new Mock<IUnitOfWork>();
             _ratesMock = new Mock<IRepository<Rate>>();
            rates = new List<Rate>();
            rates.Add(new Rate{CinemaId = 1, Id = 1, MovieId = 1, UserId = 1, Value = 1});
            _ratesMock.Setup<Rate>(m => m.FirstOrDefault(It.IsAny<Expression<Func<Rate, bool>>>())).Returns<Expression<Func<Rate, bool>>>(e => rates.FirstOrDefault(e.Compile()));
           // _ratesMock.Setup<Rate>>(m => m.FirstOrDefault(It.IsAny<Expression<Func<Rate, bool>>>())).
              //  .Returns<Rate>(p => rates.FirstOrDefault(p.Compile()));
            _ratesMock.Setup(p => p.Insert(It.IsAny<Rate>())).Callback<Rate>(p => rates.Add(p));
            _unitMock.SetupGet(p => p.Likes).Returns(_ratesMock.Object);
            _factoryMock.Setup(m => m.Create()).Returns(_unitMock.Object);
            _factory = _factoryMock.Object;
            _likeService = new Mock<ILikeService>();
           
        }

        [Test]
        public void ChangeExisting()
        {
            LikeService likeService = new LikeService(_factory);
            likeService.SubmitLike(new MovieDemo.ApiControllers.LikeBindingModel { CinemaId = 1, MovieId = 1, Rate = 2 }, 1);
            Expect(rates[0].Value, Is.EqualTo(2));
            _ratesMock.Verify(r => r.Update(rates[0]), Times.AtLeastOnce);
            _unitMock.Verify(r => r.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void InsertNewRate()
        {
            LikeService likeService = new LikeService(_factory);
            likeService.SubmitLike(new MovieDemo.ApiControllers.LikeBindingModel { CinemaId = 1, MovieId = 1, Rate = 5 }, 2);
            Expect(rates[1].Value, Is.EqualTo(5));
            _ratesMock.Verify(r => r.Insert(rates[1]), Times.AtLeastOnce);
            _unitMock.Verify(r => r.Save(), Times.AtLeastOnce);
        }

        [Test]

        public void DoesNotChangeLike()
        {
            LikeService likeService = new LikeService(_factory);
            likeService.SubmitLike(new MovieDemo.ApiControllers.LikeBindingModel { CinemaId = 1, MovieId = 1, Rate = 5 }, 2);
            Expect(rates[0].Value, Is.EqualTo(1));
            _ratesMock.Verify(r => r.Update(rates[0]), Times.Never);

        }

        [Test]
        public void DoesNotInsert()
        {
            LikeService likeService = new LikeService(_factory);
            likeService.SubmitLike(new MovieDemo.ApiControllers.LikeBindingModel { CinemaId = 1, MovieId = 1, Rate = 2 }, 1);
            Expect(rates[0].Value, Is.EqualTo(2));
            Expect(rates.Count, Is.EqualTo(1));
            _ratesMock.Verify(r => r.Insert(It.IsAny<Rate>()), Times.Never);
        }

        [Test]
        public void TestLikeControllerCallsService()
        {
            LikesController controller = new LikesController(_likeService.Object);
            MovieDemo.ApiControllers.LikeBindingModel model = new LikeBindingModel{ CinemaId = 1, MovieId = 1, Rate = 2 };
            var result = controller.Like(model);
            _likeService.Verify(l => l.SubmitLike(model, 0), Times.Once);
            Expect(result, Is.TypeOf <OkResult>());
        }

        [Test]
        public void LikeControllerReturnsOk()
        {
            LikesController controller = new LikesController(_likeService.Object);
            MovieDemo.ApiControllers.LikeBindingModel model = new LikeBindingModel { CinemaId = 1, MovieId = 1, Rate = 2 };
            var result = controller.Like(model);
            Expect(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void LikeControllerReturnsBad()
        {
            LikesController controller = new LikesController(_likeService.Object);
            MovieDemo.ApiControllers.LikeBindingModel model = null;
            var result = controller.Like(model);
            Expect(result, Is.TypeOf<BadRequestResult>());
        }

       
    }
}