//using System.ServiceModel;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using ServiceModelEx;

//namespace Topshelf.Services.UnitTest.Exploratory
//{
//    [ServiceContract]
//    public interface ITestService
//    {
//        [OperationContract]
//        string GetReversedSampleString(string sampleValue);
//    }

//    public class TestService : ITestService
//    {
//        public string GetReversedSampleString(string sampleValue)
//        {
//            return new string(sampleValue.Reverse().ToArray());
//        }
//    }

//    [TestClass]
//    public class AppDomainHostTests
//    {


//        [TestMethod]
//        public void Service_opened_Ok()
//        {
//            AppDomainHost appDomainHost = new AppDomainHost(typeof(TestService));
//            appDomainHost.Open();
//        }
//    }
//}
