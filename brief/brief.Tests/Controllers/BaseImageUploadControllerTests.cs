using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using brief.Controllers;
using brief.Controllers.Helpers;
using brief.Controllers.Models;
using brief.Controllers.Models.BaseEntities;
using NUnit.Framework;

namespace brief.Tests.Controllers
{
    [TestFixture]
    public class BaseImageUploadControllerTests
    {
        class TestingStub : BaseImageUploadController
        {
            public TestingStub(IFileSystem fileSystem) : base(fileSystem) { }

            public async Task<HttpResponseMessage> BaseImageUploadStub<TData>(Func<ImageModel, Task<TData>> strategy,
                                                                              StorageSettings storageSettings,
                                                                              Guid targetId) 
                                                                              where TData : BaseResponseMessage
                => await BaseImageUpload(strategy, storageSettings, targetId);
        }

        private BaseImageUploadController _sut;

        [SetUp]
        public void SetUp()
        {
            
        }

        [TestCaseSource(nameof(GetDataForFail))]
        public async Task BaseImageUpload_sould_fail_if_more_than_one_file_presented_in_request(Guid targetId)
        {
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                { @"c:\demo\image.gif", new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 }) }
            });

            var baseImageUploadController = new TestingStub(fileSystem);

            Func<ImageModel, Task<BaseResponseMessage>> test = async (x) => await Task.FromResult(new BaseResponseMessage());

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/anylink");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "products" } });

            baseImageUploadController.ControllerContext = new HttpControllerContext(config, routeData, request);
            baseImageUploadController.Request = request;
            baseImageUploadController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            var s = await baseImageUploadController.BaseImageUploadStub(test, new StorageSettings(), targetId);
        }

        public static IEnumerable<TestCaseData> GetDataForFail
        {
            get
            {
                yield return new TestCaseData(Guid.NewGuid());
            }
        }

        private BaseImageUploadController CreateBaseImageUploadController(IFileSystem fileSystem)
            => (BaseImageUploadController)typeof(BaseImageUploadController)
                .GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance,
                    null,
                    new[] { typeof(IFileSystem) },
                    null
                )
                .Invoke(new object[] { fileSystem });
    }
}
