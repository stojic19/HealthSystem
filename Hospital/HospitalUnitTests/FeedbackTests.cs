using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.SharedModel.Model.Enumerations;
using HospitalUnitTests.Base;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            ClearDbContext();
            var numOfFeedbackBefore = UoW.GetRepository<IFeedbackReadRepository>().GetAll().Count();
            Context.Feedbacks.Add(new Feedback(1,"dsd",true,false));
            Context.SaveChanges();
            var numOfFeedbackAfter = UoW.GetRepository<IFeedbackReadRepository>().GetAll().Count();
            Assert.StrictEqual(numOfFeedbackBefore + 1, numOfFeedbackAfter);
        }
        [Fact]
        public void Should_not_add_feedback()
        {
            ClearDbContext();
            var numOfFeedbackBefore = UoW.GetRepository<IFeedbackReadRepository>().GetAll().Count();
            try { Context.Feedbacks.Add(new Feedback(1, "   ", true, false)); } catch { }
            Context.SaveChanges();
            var numOfFeedbackAfter = UoW.GetRepository<IFeedbackReadRepository>().GetAll().Count();
            Assert.StrictEqual(numOfFeedbackBefore, numOfFeedbackAfter);
        }
        [Fact]
        public void Should_publish_feedback()
        {
            ClearDbContext();
            Context.Feedbacks.Add(new Feedback(3, "dsd", true, false));
            var feedback = UoW.GetRepository<IFeedbackReadRepository>().GetById(3);
            feedback.Publish();
            Context.SaveChanges();
            var publishedFeedback = UoW.GetRepository<IFeedbackReadRepository>().GetById(3);
            publishedFeedback.FeedbackStatus.ShouldBe(FeedbackStatus.Approved);
        }
        [Fact]
        public void Should_unpublish_feedback()
        {
            ClearDbContext();
            Context.Feedbacks.Add(new Feedback(2, "dsd", true, false));
            var feedback = UoW.GetRepository<IFeedbackReadRepository>().GetById(2);
            feedback.Publish();
            feedback.FeedbackStatus.ShouldBe(FeedbackStatus.Approved);
            feedback.Unpublish();
            Context.SaveChanges();
            var publishedFeedback = UoW.GetRepository<IFeedbackReadRepository>().GetById(2);
            publishedFeedback.FeedbackStatus.ShouldBe(FeedbackStatus.NotApproved);
        }
    }
}
