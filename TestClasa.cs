using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expect = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace CargodomProject
{
    [TestFixture]
    internal class TestClasa:BaseClass
    {
        
     
/*1.Navigate to Cargodom.
2.Log in as a user and create a request of your choosing.
- After creating the request, you must confirm that the request is present in ‘My Requests’.
3.Log out.
4.Log in as a transporter, and find the request you created.
5.Make an offer for it.
- After making the offer, you must confirm that the offer was sent, which you can check via the ‘My offers’ page.
6.Log out.
7.Log in as a user and accept the offer.
- Confirm that the offer was accepted and the request is closed.
8.Log out.
9.Confirm that you are logged out.

IMPORTANT:
-Do not use Thread.Sleep() or Task.Delay() and XPath Selectors; IWebElements must be in variables.
-This homework should be in a new solution.
-Use the Random generator in the fields where it can be used.*/



        
        [Test]

        public void FinalHomeworkTest()
        {
            LogMeIn();
                        
            ObjaviBaranje();

            LogMeOut();

            LogMeInTransporter();

            findRequestAndMakeOffer();

            confirmOfferWasSend();

            LogMeOut();

            LogMeIn();

            confirmOffer();

            LogMeOut();

        }
                      
    }
}
