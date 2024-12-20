using CsvHelper;
using ETL.Entities;
using ETL.Interfaces;
using ETL.Mappers;
using ETL.Response;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Services
{
    public class TaxiTripService(
        ITaxiTripRepository _taxiRepository,
        ILogger<TaxiTripService> _logger,
        IValidator<TaxiTrip> _validator
    ) : ITaxiTripService
    {
        public async Task<Result<bool>> ProcessTripsAsync(string csvFilePath)
        {
            if (string.IsNullOrEmpty(csvFilePath))
            {
                return Result<bool>.Failure("File path is empty");
            }

            var trips = new List<TaxiTrip>();
            var duplicates = new List<TaxiTrip>();
            var validTrips = new List<TaxiTrip>();

            try
            {
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TaxiTripCsvMapper>();
                    while (csv.Read())
                    {
                        var trip = csv.GetRecord<TaxiTrip>();
                        trips.Add(trip);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error reading CSV file: {ex.Message}");
                return Result<bool>.Failure("Failed to read CSV file.");
            }

            var estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var existingKeys = await _taxiRepository.GetAllKeysAsync();

            var seenKeys = new HashSet<string>();
            foreach (var trip in trips)
            {
                trip.PickupDateTime = TimeZoneInfo.ConvertTimeToUtc(trip.PickupDateTime, estZone);
                trip.DropoffDateTime = TimeZoneInfo.ConvertTimeToUtc(trip.DropoffDateTime, estZone);

                var key = $"{trip.PickupDateTime:O}_{trip.DropoffDateTime:O}_{trip.PassengerCount}";
                if (existingKeys.Contains(key) || seenKeys.Contains(key))
                {
                    duplicates.Add(trip);
                    continue;
                }

                if (trip.DropoffDateTime == default || trip.PickupDateTime == default)
                {
                    _logger.LogWarning($"Invalid date in trip: {trip}");
                    continue;
                }

                trip.StoreAndFwdFlag = trip.StoreAndFwdFlag?.Trim() == "N" ? "No" : "Yes";

                var validationResult = await _validator.ValidateAsync(trip);
                if (validationResult.IsValid)
                {
                    validTrips.Add(trip);
                    seenKeys.Add(key);
                }
            }

            SaveToCsv("duplicates.csv", duplicates);

            var result = await _taxiRepository.BulkInsertAsync(validTrips);

            if (!result.IsSuccess) return Result<bool>.Failure(result.ErrorMessage);

            return Result<bool>.Success(true);
        }


        private void SaveToCsv(string filePath, IEnumerable<TaxiTrip> trips)
        {
            try
            {
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TaxiTripCsvMapper>();
                    csv.WriteRecords(trips);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error writing to CSV file {filePath}: {ex.Message}");
            }
        }
    }
}
