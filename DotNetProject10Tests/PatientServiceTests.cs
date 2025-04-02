using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PatientMicroservice.Entities;
using PatientMicroservice;
using PatientMicroservice.Services;
using Moq.EntityFrameworkCore;
using Moq;
using MockQueryable.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientMicroservice.Models;

namespace DotNetProject10Tests
{
    public class PatientServiceTests
    {
        private readonly List<Patient> _patients;
        private readonly List<Patient>? _patientsEmpty;
        private readonly Mock<ILogger<PatientService>> _logger;

        public PatientServiceTests()
        {
            _logger = new Mock<ILogger<PatientService>>();
            _patients = new()
            {
                new ()
                {
                    Id = 1,
                    GivenName = "name1",
                    FamilyName = "name1",
                    Dob = DateTime.Parse("2025-04-01"),
                    Sex = "F",
                    Address = "111",
                    Phone = "9999999999"
                },
                new ()
                {
                    Id = 2,
                    GivenName = "name2",
                    FamilyName = "name2",
                    Dob = DateTime.Parse("2025-05-01"),
                    Sex = "M",
                    Address = "222",
                    Phone = "8888888888"
                }
            };
            _patientsEmpty = [];
        }

        [Fact]
        public async Task GetPatientsShouldReturnPatients()
        {
            var mockSet = _patients.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<PatientDbContext>();
            mockContext.Setup(m => m.Patients).Returns(mockSet.Object);

            PatientService patientService = new PatientService(mockContext.Object, _logger.Object);
            
            var result = await patientService.GetPatients();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<PatientModel?>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetPatientsEmptyShouldReturnEmpty()
        {
            var mockSet = _patientsEmpty.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<PatientDbContext>();
            mockContext.Setup(m => m.Patients).Returns(mockSet.Object);

            PatientService patientService = new PatientService(mockContext.Object, _logger.Object);

            var result = await patientService.GetPatients();
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetPatientByIdShouldReturnPatient()
        {
            var mockSet = _patients.AsQueryable().BuildMockDbSet();
            mockSet.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(_patients[0]);

            var mockContext = new Mock<PatientDbContext>();
            mockContext.Setup(m => m.Patients).Returns(mockSet.Object);

            PatientService patientService = new PatientService(mockContext.Object, _logger.Object);
            var result = await patientService.GetPatientById(1);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PatientModel>(result);
            Assert.Equal(1, result.Id);
        }
    }
}
