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
          
            _context.Patients.Add(new Patient { Name = "Ahmet", Surname = "Demir", BirthDate = new DateTime(1992, 3, 15) });
            _context.Patients.Add(new Patient { Name = "Zeynep", Surname = "Kaya", BirthDate = new DateTime(1995, 7, 20) });
            _context.SaveChanges();

         
            var patients = _patientService.GetAllPatients();

         
            Assert.Equal(2, patients.Count());
        }
    }
}
