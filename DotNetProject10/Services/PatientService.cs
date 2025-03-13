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
        // private readonly PatientMappingProfile _mapper;
        private readonly ILogger<PatientService> _logger;

        public PatientService(PatientDbContext patientDbContext, ILogger<PatientService> logger) {
            _context = patientDbContext;
            // _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PatientModel?>> GetPatients()
        {
            var patients = await _context.Patients.ToListAsync();

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

        public async Task<int> UpdatePatient(PatientModel patientModel)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patientModel.Id)
                ?? throw new KeyNotFoundException($"Patient with ID {patientModel.Id} not found");

            _context.Patients.Update(new Patient
            {
                Id = patientModel.Id,
                GivenName = patientModel.GivenName,
                FamilyName = patientModel.FamilyName,
                Dob = patientModel.Dob,
                Sex = patientModel.Sex,
                Address = patientModel.Address,
                Phone = patientModel.Phone
            });
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
            Console.WriteLine("Hi I am a breaKPOINT");
            await _context.SaveChangesAsync();
            return patient.Id;
        }
    }
}
