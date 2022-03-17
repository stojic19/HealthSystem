using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Enumerations;
using HospitalUnitTests.Base;
using Shouldly;
using System.Linq;
using Xunit;

namespace HospitalUnitTests
{
    public class FeedbackTests : BaseTest
    {
        public FeedbackTests(BaseFixture fixture) : base(fixture)
        {

        }
        [Fact]
        public void Should_add_feedback()
        {
            #region Arrange

            ClearDbContext();
            var numOfFeedbackBefore = UoW.GetRepository<IFeedbackReadRepository>().GetAll().Count();
            Context.Feedbacks.Add(new Feedback(1,"dsd",true,false));

            Context.SaveChanges();
            #endregion
            var numOfFeedbackAfter = UoW.GetRepository<IFeedbackReadRepository>().GetAll().Count();
            Assert.StrictEqual(numOfFeedbackBefore + 1, numOfFeedbackAfter);
        }
        [Fact]
        public void Should_not_add_feedback()
        {
            #region Arrange

            ClearDbContext();
            var numOfFeedbackBefore = UoW.GetRepository<IFeedbackReadRepository>().GetAll().Count();
            try { Context.Feedbacks.Add(new Feedback(1, "   ", true, false)); } catch { }
            Context.SaveChanges();

            #endregion

            var numOfFeedbackAfter = UoW.GetRepository<IFeedbackReadRepository>().GetAll().Count();
            Assert.StrictEqual(numOfFeedbackBefore, numOfFeedbackAfter);
        }
        [Fact]
        public void Should_publish_feedback()
        {
            #region Arrange

            ClearDbContext();
            Context.Feedbacks.Add(new Feedback(3, "dsd", true, false));
            var feedback = UoW.GetRepository<IFeedbackReadRepository>().GetById(3);
            feedback.Publish();
            Context.SaveChanges();
            
            #endregion

            var publishedFeedback = UoW.GetRepository<IFeedbackReadRepository>().GetById(3);
            publishedFeedback.FeedbackStatus.ShouldBe(FeedbackStatus.Approved);
        }
        [Fact]
        public void Should_unpublish_feedback()
        {
            #region Arrange

            /**
             * Da li ima potrebe za ovim dobavljenje iz baze . Treba da vjerujemo db contextu ??
             * Ali ne znam da li ovo krsi ista od pravila navedenih 
             * Samo je redundantno
             */
            ClearDbContext();
            Context.Feedbacks.Add(new Feedback(2, "dsd", true, false));
            var feedback = UoW.GetRepository<IFeedbackReadRepository>().GetById(2);
            feedback.Publish();
            feedback.FeedbackStatus.ShouldBe(FeedbackStatus.Approved);
            feedback.Unpublish();
            Context.SaveChanges();

            #endregion

            var publishedFeedback = UoW.GetRepository<IFeedbackReadRepository>().GetById(2);
            publishedFeedback.FeedbackStatus.ShouldBe(FeedbackStatus.NotApproved);
        }
    }
}
