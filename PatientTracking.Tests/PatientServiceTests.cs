using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PatientTracking.API.Data;
using PatientTracking.API.Models;
using PatientTracking.API.Services;
using Xunit;

namespace PatientTracking.Tests
{
    public class PatientServiceTests
    {
        private readonly AppDbContext _context;
        private readonly PatientService _patientService;

        public PatientServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "PatientDbTest")
                .Options;

            _context = new AppDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated(); 
            _patientService = new PatientService(_context);
        }

        [Fact]
        public void ShouldAddPatientSuccessfully()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "Ayşe",
                Surname = "Arslan",
                BirthDate = new DateTime(1990, 5, 12)
            };

            // Act
            _patientService.AddPatient(patient);

            // Assert
            var savedPatient = _context.Patients.FirstOrDefault();
            Assert.NotNull(savedPatient);
            Assert.Equal("Ayşe", savedPatient.Name);
            Assert.Equal("Arslan", savedPatient.Surname);
        }

        [Fact]
        public void ShouldDeletePatientSuccessfully()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "Ali",
                Surname = "Yılmaz",
                BirthDate = new DateTime(1985, 8, 23)
            };
            _context.Patients.Add(patient);
            _context.SaveChanges();

            // Act
            _patientService.DeletePatient(patient.Id);

            // Assert
            var deletedPatient = _context.Patients.FirstOrDefault(p => p.Id == patient.Id);
            Assert.Null(deletedPatient);
        }

        [Fact]
        public void ShouldGetAllPatientsSuccessfully()
        {
            // Arrange
            _context.Patients.Add(new Patient { Name = "Ahmet", Surname = "Demir", BirthDate = new DateTime(1992, 3, 15) });
            _context.Patients.Add(new Patient { Name = "Zeynep", Surname = "Kaya", BirthDate = new DateTime(1995, 7, 20) });
            _context.SaveChanges();

            // Act
            var patients = _patientService.GetAllPatients();

            // Assert
            Assert.Equal(2, patients.Count());
        }

        [Fact]
        public void ShouldUpdatePatientSuccessfully()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "Mehmet",
                Surname = "Yıldız",
                BirthDate = new DateTime(1988, 4, 15)
            };
            _context.Patients.Add(patient);
            _context.SaveChanges();

            // Act
            patient.Name = "Ahmet";
            patient.Surname = "Kaya";
            _patientService.UpdatePatient(patient);

            // Assert
            var updatedPatient = _context.Patients.FirstOrDefault(p => p.Id == patient.Id);
            Assert.NotNull(updatedPatient);
            Assert.Equal("Ahmet", updatedPatient.Name);
            Assert.Equal("Kaya", updatedPatient.Surname);
        }

        [Fact]
        public void ShouldGetPatientByIdSuccessfully()
        {
            // Arrange
            var patient = new Patient
            {
                Name = "Zeynep",
                Surname = "Demir",
                BirthDate = new DateTime(1993, 6, 25)
            };
            _context.Patients.Add(patient);
            _context.SaveChanges();

            // Act
            var retrievedPatient = _patientService.GetPatientById(patient.Id);

            // Assert
            Assert.NotNull(retrievedPatient);
            Assert.Equal(patient.Name, retrievedPatient.Name);
            Assert.Equal(patient.Surname, retrievedPatient.Surname);
        }

        [Fact]
        public void ShouldThrowExceptionWhenDeletingNonExistentPatient()
        {
            // Arrange
            var nonExistentId = 999;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _patientService.DeletePatient(nonExistentId));
        }

        [Fact]
        public void ShouldThrowExceptionWhenUpdatingNonExistentPatient()
        {
            // Arrange
            var nonExistentPatient = new Patient
            {
                Id = 999,
                Name = "Test",
                Surname = "Patient",
                BirthDate = DateTime.Now
            };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _patientService.UpdatePatient(nonExistentPatient));
        }

        [Fact]
        public void ShouldGetEmptyListWhenNoPatientsExist()
        {
            // Act
            var patients = _patientService.GetAllPatients();

            // Assert
            Assert.Empty(patients);
        }
    }
}
