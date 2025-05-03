using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using PatientMicroservice.Entities;
using PatientMicroservice.Models;
// using PatientMicroservice.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace PatientMicroservice.Services
{
    public class PatientService : IPatientService
    {
        private readonly PatientDbContext _context;
        private readonly ILogger<PatientService> _logger;

        public PatientService(PatientDbContext patientDbContext, ILogger<PatientService> logger) {
            _context = patientDbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<PatientModel?>> GetPatients()
        {
            var patients = await _context.Patients.AsNoTracking().ToListAsync();

            return patients.Select(p => new PatientModel
            {
                Id = p.Id,
                GivenName = p.GivenName,
                FamilyName = p.FamilyName,
                Dob = p.Dob,
                Sex = p.Sex,
                Address = p.Address,
                Phone = p.Phone
            });
        }

        public async Task<PatientModel> GetPatientById(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId)
                ?? throw new KeyNotFoundException($"Patient with ID {patientId} not found");

            return new PatientModel
            {
                Id = patient.Id,
                GivenName = patient.GivenName,
                FamilyName = patient.FamilyName,
                Dob = patient.Dob,
                Sex = patient.Sex,
                Address = patient.Address,
                Phone = patient.Phone
            };
        }

        public async Task<int> UpdatePatient(PatientModel patientModel)
        {
            Patient patientToUpdate = new ()
            {
                Id = patientModel.Id,
                GivenName = patientModel.GivenName,
                FamilyName = patientModel.FamilyName,
                Dob = patientModel.Dob,
                Sex = patientModel.Sex,
                Address = patientModel.Address,
                Phone = patientModel.Phone
            };

            _context.Entry(patientToUpdate).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> CreatePatient(PatientModel patientModel)
        {
            var patient = new Patient
            {
                GivenName = patientModel.GivenName,
                FamilyName = patientModel.FamilyName,
                Dob = patientModel.Dob,
                Sex = patientModel.Sex,
                Address = patientModel.Address,
                Phone = patientModel.Phone
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient.Id;
        }
    }
}
