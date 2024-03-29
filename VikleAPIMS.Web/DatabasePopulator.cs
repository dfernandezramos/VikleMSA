// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Contracts;
using Common.Domain;
using VikleAPIMS.Data;

namespace VikleAPIMS.Web
{
    /// <summary>
    /// This class contains the database populator for the vikle API microservice
    /// </summary>
    public class DatabasePopulator
    {
        private readonly IVikleRepository _repository;
        private readonly ILog _log;

        public DatabasePopulator(IVikleRepository repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        /// <summary>
        /// This method populates the database.
        /// </summary>
        public async Task Seed()
        {
            _log.Info("Populating API database...");

            await PopulateWorkshops();
            await PopulateUsers();
            await PopulateReparations();
            await PopulateVehicles();
            await PopulateDates();

            _log.Info("API database population success");
        }

        async Task PopulateWorkshops()
        {
            var result = await _repository.GetWorkshopById("1");

            if (result == null)
            {
                await _repository.NewWorkshop("1", "Garatge la Salut");
            }
        }

        async Task PopulateReparations()
        {
            var id = "e462fe80-63e3-4130-8128-eed38527dd2f";
            var result = await _repository.GetReparationById(id);

            if (result == null)
            {
                var reparation = new Reparation
                {
                    Id = id,
                    WorkshopId = "1",
                    Date = DateTime.UtcNow.Ticks,
                    PlateNumber = "1234ABC",
                    Type = ReparationType.Reparation,
                    Status = ReparationStatus.Repairing
                };
                await _repository.NewReparation(reparation);
            }
            
            id = "8cee76b3-6ed9-4fd5-af23-8a641c3dac63";
            result = await _repository.GetReparationById(id);

            if (result == null)
            {
                var reparation = new Reparation
                {
                    Id = id,
                    WorkshopId = "1",
                    AirFilter = true,
                    Date = DateTime.UtcNow.AddMonths(-1).Ticks,
                    GasFilter = true,
                    PlateNumber = "1234ABC",
                    Type = ReparationType.Maintenance,
                    Status = ReparationStatus.Completed,
                    Details = new List<string>
                    {
                        "Changed oil filter",
                        "Changed oil",
                        "Changed gas filter",
                        "Other minor reparations"
                    }
                };
                await _repository.NewReparation(reparation);
            }
        }
        
        async Task PopulateDates()
        {
            var result = await _repository.GetDateByPlateNumber("1234ABC");

            if (result == null)
            {
                var date = new Date
                {
                    Id = Guid.NewGuid().ToString(),
                    WorkshopId = "1",
                    PlateNumber = "1234ABC",
                    ReparationDate = DateTime.UtcNow.Ticks,
                    Reason = ReparationType.Reparation,
                    IdClient = "28feef62-08c1-4b14-9ea4-13e007d1f002",
                    Status = ReparationStatus.Repairing
                };
                await _repository.NewDate(date);
            }
        }
        
        async Task PopulateVehicles()
        {
            var result = await _repository.GetVehicleByPlateNumber("1234ABC");

            if (result == null)
            {
                var vehicle = new Vehicle
                {
                    PlateNumber = "1234ABC",
                    IdClient = "28feef62-08c1-4b14-9ea4-13e007d1f002",
                    Model = "A3",
                    VehicleType = VehicleType.Car,
                    Year = 2007
                };
                await _repository.UpdateVehicle(vehicle.PlateNumber, vehicle);
            }
        }

        async Task PopulateUsers()
        {
            var clientEmail = "client@email.com";
            var client = await _repository.GetUserByEmail(clientEmail);

            if (client == null)
            {
                var clientId = "28feef62-08c1-4b14-9ea4-13e007d1f002";
                await _repository.NewUser(new User
                {
                    Id = clientId,
                    Email = clientEmail,
                    Name = "Client",
                    Surname = "Test",
                    Phone = "123456789"
                }, default);
            }
            
            var workerEmail = "worker@email.com";
            var worker = await _repository.GetUserByEmail(workerEmail);
            
            if (worker == null)
            {
                var workerId = "0c0910e9-0ab2-4d70-9ea2-21198dfb36ac";
                await _repository.NewUser(new User
                {
                    Id = workerId,
                    Email = workerEmail,
                    Name = "Worker",
                    Surname = "Test",
                    Phone = "123456789",
                    IsWorker = true,
                    IdWorkshop = "1"
                }, default);
            }
            
            var adminEmail = "admin@email.com";
            var admin = await _repository.GetUserByEmail(adminEmail);
            
            if (admin == null)
            {
                var adminId = "115deaa7-5405-4db4-8ee8-cc8162d67bbb";
                await _repository.NewUser(new User
                {
                    Id = adminId,
                    Email = adminEmail,
                    Name = "Admin",
                    Surname = "Test",
                    Phone = "123456789"
                }, default);
            }
        }
    }
}