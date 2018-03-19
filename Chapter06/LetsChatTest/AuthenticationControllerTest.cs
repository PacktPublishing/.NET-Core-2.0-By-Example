using LetsChat.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LetsChatTest
{
    /// <summary>
    /// Authentication Controller Unit Test - Notice the naming convention {ControllerName}Test
    /// </summary>
    public class AuthenticationControllerTest
    {
        /// <summary>
        /// Mock the dependency needed to initialize the controller.
        /// </summary>
        private Mock<ILogger<AuthenticationController>> mockedLogger = new Mock<ILogger<AuthenticationController>>();

        /// <summary>
        /// Tests the SignIn action.
        /// </summary>
        [Fact]
        public void SignIn_Pass_Test()
        {
            // Arrange - Initialize the controller. Notice the mocked logger object passed as the parameter.
            var controller = new AuthenticationController(mockedLogger.Object);

            // Act - Invoke the method to be tested.
            var actionResult = controller.SignIn();

            // Assert - Make assertions if actual output is same as expected output.
            Assert.NotNull(actionResult);
            Assert.IsType<ChallengeResult>(actionResult);
            Assert.Equal(((ChallengeResult)actionResult).Properties.Items.Count, 1);
        }       
    }
}
