using Moq;
using PatientTracking.API.Models;
using PatientTracking.API.Data;
using PatientTracking.API.Services;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PatientTracking.Tests
{
    public class PatientServiceTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly PatientService _patientService;

        public PatientServiceTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _patientService = new PatientService(_mockContext.Object);
        }

        [Fact]
        public void ShouldAddPatientSuccessfully()
        {
            // Arrange
            var patient = new Patient { Name = "Ayþe", Surname = "Arslan", BirthDate = new DateTime(1990, 5, 12) };

            // Mock Add method
            _mockContext.Setup(m => m.Patients.Add(It.IsAny<Patient>())).Verifiable();
            _mockContext.Setup(m => m.SaveChanges()).Verifiable();

            // Act
            _patientService.AddPatient(patient);

            // Assert
            _mockContext.Verify(m => m.Patients.Add(It.IsAny<Patient>()), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Fact]
        public void ShouldGetPatientSuccessfully()
        {
            // Arrange
            var patientId = 1;
            var patient = new Patient { Id = patientId, Name = "Ayþe", Surname = "Arslan", BirthDate = new DateTime(1990, 5, 12) };

            // Mock GetPatient method
            _mockContext.Setup(m => m.Patients.FirstOrDefault(p => p.Id == patientId)).Returns(patient);

            // Act
            var result = _patientService.GetPatient(patientId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Ayþe", result.Name);
        }

   
        [Fact]
        public void ShouldDeletePatientSuccessfully()
        {
            // Arrange
            var patientId = 1;
            var patient = new Patient { Id = patientId, Name = "Ayþe", Surname = "Arslan", BirthDate = new DateTime(1990, 5, 12) };

            // Mock Remove method
            _mockContext.Setup(m => m.Patients.FirstOrDefault(p => p.Id == patientId)).Returns(patient);
            _mockContext.Setup(m => m.Patients.Remove(It.IsAny<Patient>())).Verifiable();
            _mockContext.Setup(m => m.SaveChanges()).Verifiable();

            // Act
            _patientService.DeletePatient(patientId);

            // Assert
            _mockContext.Verify(m => m.Patients.Remove(It.IsAny<Patient>()), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
