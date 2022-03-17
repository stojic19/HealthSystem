using HospitalUnitTests.Base;

namespace HospitalUnitTests
{
    public class SendingEmailTest : BaseTest
    {
        public SendingEmailTest(BaseFixture fixture) : base(fixture)
        {
        }

        /*[Fact]
        public void Send_Test_Email_Should_Be_True()
        {
            var emailAddress = "testsemailpsw@gmail.com";

            EmailService es = new EmailService();
            var confirmationLink = "Test.";
            bool sent = es.SendEmail(emailAddress, confirmationLink);

            sent.ShouldBe(true);
        }*/
    }
}
