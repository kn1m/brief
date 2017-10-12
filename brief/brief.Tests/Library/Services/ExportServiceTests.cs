namespace brief.Tests.Library.Services
{
    using System.IO;
    using System.IO.Abstractions;
    using brief.Library.Repositories;
    using brief.Library.Services;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    class ExportServiceTests
    {
        [Test]
        public void ExportNotesTest()
        {
            var noteRepository = new Mock<INoteRepository>();
            var fileSysem = new Mock<IFileSystem>();
            var exportService = new ExportService(fileSysem.Object, noteRepository.Object);

            var noteFile = File.ReadAllText(@"D:\brief\unittestdata\notes.txt");
            


        }
    }
}
