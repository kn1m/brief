namespace brief.Tests.Library.Services
{
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
            var exportService = new ExportService(noteRepository.Object);

            

        }
    }
}
