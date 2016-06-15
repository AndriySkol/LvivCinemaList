using Moq;
using MovieDomain.Entities;
using MovieDomain.Repository;
using MovieDomain.UnitOfWork;
using MovieServices.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MovieServices.Service;
using MovieDemo.ApiControllers;
using System.Web.Http.Results;

namespace NUnitTestProject
{
    [TestFixture]
    public class CommentTests : AssertionHelper
    {

        IUnitOfWorkFactory _factory;
        List<User> users;
        List<Comment> comments;
        Mock<IRepository<Comment>> _commentsMock;
        Mock<IRepository<User>> _usersMock;
        Mock<IUnitOfWork> _unitMock;
        Mock<ICommentService> _commentService;

        [SetUp]
        public void Initialise()
        {
            Mock<IUnitOfWorkFactory> _factoryMock = new Mock<IUnitOfWorkFactory>();
            _unitMock = new Mock<IUnitOfWork>();
            _commentsMock = new Mock<IRepository<Comment>>();
            _usersMock = new Mock<IRepository<User>>();
            comments = new List<Comment>();
            users = new List<User>();
            comments.Add(new Comment {Id = 6, CinemaId = 1, MovieId = 1, Text = "SomeText2"});
            comments.Add(new Comment { Id = 5, CinemaId = 1, MovieId = 1, Text = "SomeText" });
            comments.Add(new Comment { Id = 7, CinemaId = 1, MovieId = 1, Text = "SomeText3" });
            users.Add(new User (0)  { LikedComments = new List<Comment> {comments[0]}, UnlikedComments = new List<Comment> {comments[1]} });
            comments[0].Liked = new List<User>{users[0]};
            comments[1].NotLiked = new List<User>{users[0]};
            _usersMock.Setup<User>(m => m.FirstOrDefault(It.IsAny<Expression<Func<User, bool>>>())).Returns<Expression<Func<User, bool>>>(e => users.FirstOrDefault(e.Compile()));
            _commentsMock.Setup<Comment>(m => m.FirstOrDefault(It.IsAny<Expression<Func<Comment, bool>>>())).Returns<Expression<Func<Comment, bool>>>(e => comments.FirstOrDefault(e.Compile()));
  
            _unitMock.SetupGet(p => p.Comments).Returns(_commentsMock.Object);
            _unitMock.SetupGet(p => p.Users).Returns(_usersMock.Object);
            _factoryMock.Setup(m => m.Create()).Returns(_unitMock.Object);
            _factory = _factoryMock.Object;
            _commentService = new Mock<ICommentService>();
        }

        [Test]
        public void AddNewLikeToComment()
        {
            CommentService service = new CommentService(_factory);
            service.Like(7, 0);
            Expect(users[0].LikedComments.Contains(comments[2]));
            _usersMock.Verify(m => m.Update(users[0]), Times.Once);
            _unitMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void AddNewDislikeToComment()
        {
            CommentService service = new CommentService(_factory);
            service.UnLike(7, 0);
            Expect(users[0].UnlikedComments.Contains(comments[2]));
            _usersMock.Verify(m => m.Update(users[0]), Times.Once);
            _unitMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void AddBadLikeToGood()
        {
            CommentService service = new CommentService(_factory);
            service.Like(5, 0);
            Expect(users[0].LikedComments.Contains(comments[1]));
            Expect(!users[0].UnlikedComments.Contains(comments[1]));
            _usersMock.Verify(m => m.Update(users[0]), Times.AtLeastOnce);
            _unitMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void AddGoodLikeToBad()
        {
            CommentService service = new CommentService(_factory);
            service.UnLike(6, 0);
            Expect(!users[0].LikedComments.Contains(comments[0]));
            Expect(users[0].UnlikedComments.Contains(comments[0]));
            _usersMock.Verify(m => m.Update(users[0]), Times.AtLeastOnce);
            _unitMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }


        [Test]
        public void AddGoodLikeToGood()
        {
            CommentService service = new CommentService(_factory);
            service.Like(6, 0);
            Expect(users[0].LikedComments.Contains(comments[0]));
            Expect(!users[0].UnlikedComments.Contains(comments[0]));
            _usersMock.Verify(m => m.Update(users[0]), Times.Never);
            _unitMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void AddBadLikeToBad()
        {
            CommentService service = new CommentService(_factory);
            service.UnLike(5, 0);
            Expect(!users[0].LikedComments.Contains(comments[1]));
            Expect(users[0].UnlikedComments.Contains(comments[1]));
            _usersMock.Verify(m => m.Update(users[0]), Times.Never);
            _unitMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void BanComment()
        {
            CommentService service = new CommentService(_factory);
            service.BanComment(5);
            Expect(comments[1].isBanned, Is.True);
            _commentsMock.Verify(m => m.Update(comments[1]), Times.Once);
            _unitMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void UnBanComment()
        {
            CommentService service = new CommentService(_factory);
            service.UnBanComment(5);
            Expect(comments[1].isBanned, Is.False);
            _commentsMock.Verify(m => m.Update(comments[1]), Times.Once);
            _unitMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void LikeControllerCallsService()
        {
            CommentController controller = new CommentController(_commentService.Object);
            var result = controller.Like(6);
            _commentService.Verify(l => l.Like(6, 0), Times.Once);
           
        }

        [Test]
        public void LikeControllerReturnsOk()
        {
            CommentController controller = new CommentController(_commentService.Object);
            var result = controller.Like(6);
            Expect(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void UnLikeControllerCallsService()
        {
            CommentController controller = new CommentController(_commentService.Object);
            var result = controller.Like(5);
            _commentService.Verify(l => l.Like(5, 0), Times.Once);

        }

        [Test]
        public void UnLikeControllerReturnsOk()
        {
            CommentController controller = new CommentController(_commentService.Object);
            var result = controller.UnLike(5);
            Expect(result, Is.TypeOf<OkResult>());
        }


        [Test]
        public void BanCommentControllerReturnsOk()
        {
            CommentController controller = new CommentController(_commentService.Object);
            var result = controller.BanComment(5);
            Expect(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void UnBanCommentControllerReturnsOk()
        {
            CommentController controller = new CommentController(_commentService.Object);
            var result = controller.UnBanComment(5);
            Expect(result, Is.TypeOf<OkResult>());
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void ExceptionRaisesUnlike()
        {
            CommentService service = new CommentService(_factory);
            service.UnLike(1, 1);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void ExceptionRaisesLike()
        {
            CommentService service = new CommentService(_factory);
            service.Like(1, 1);
        }
    }
}
