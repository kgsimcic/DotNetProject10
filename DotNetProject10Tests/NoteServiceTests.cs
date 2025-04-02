using Microsoft.VisualStudio.TestPlatform.Common.Exceptions;
using Moq;
using System.Runtime.CompilerServices;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using PatientMicroservice.Services;
using PatientMicroservice.Entities;
using PatientMicroservice.Models;
using PatientMicroservice;

namespace DotNetProject10Tests
{
    public class NoteServiceTests
    {
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly Mock<IMongoCollection<DoctorNote>> _mockNotes;
        private readonly Mock<ILogger<NoteService>> _mockLogger;
        private readonly NoteService _noteService;

        public NoteServiceTests() { 
            _mockDatabase = new Mock<IMongoDatabase>();
            _mockNotes = new Mock<IMongoCollection<DoctorNote>>();
            _mockLogger = new Mock<ILogger<NoteService>>();

            _mockDatabase.Setup(db => db.GetCollection<DoctorNote>("DoctorNotes", null)).Returns(_mockNotes.Object);
            _noteService = new NoteService(_mockDatabase.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetNotesShouldReturnNotes()
        {
            string patientId = "1";
            var doctorNotes = new List<DoctorNote>
            {
                new() { PatientId = patientId, Note = "Note 1" },
                new() { PatientId = patientId, Note = "Note 2" }
            };

            var mockCursor = new Mock<IAsyncCursor<DoctorNote>>();
            mockCursor.Setup(c => c.Current).Returns(doctorNotes);
            mockCursor
                .SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .ReturnsAsync(false);

            _mockNotes
            .Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<DoctorNote>>(),
                It.IsAny<FindOptions<DoctorNote, DoctorNote>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCursor.Object);

            var result = await _noteService.GetNotes(patientId);
            List<DoctorNoteModel> noteList = result.ToList();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            Assert.Equal(1, noteList[1].PatientId);
            Assert.Equal("Note 2", noteList[1].Note);
        }

        [Fact]
        public async Task Create_ReturnsOne_WhenInsertIsSuccessful()
        {
            var noteModel = new DoctorNoteModel
            {
                PatientId = 1,
                Note = "Patient progress is good"
            };

            _mockNotes
                .Setup(c => c.InsertOneAsync(
                    It.IsAny<DoctorNote>(),
                    It.IsAny<InsertOneOptions>(),
                    It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var result = await _noteService.Create(noteModel);

            Assert.Equal(1, result);

            _mockNotes.Verify(c => c.InsertOneAsync(
                It.Is<DoctorNote>(n =>
                    n.PatientId == "1" &&
                    n.Note == "Patient progress is good"),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}