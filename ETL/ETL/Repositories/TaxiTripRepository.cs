using Dapper;
using ETL.Entities;
using ETL.Interfaces;
using ETL.Response;
using ETL.SQLScripts;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Repositories
{
    public class TaxiTripRepository : ITaxiTripRepository
    {
        private readonly string _connectionString;

        public TaxiTripRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result<bool>> BulkInsertAsync(IEnumerable<TaxiTrip> trips)
        {
            try
            {
                var dataTable = ConvertToDataTable(trips);

                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using var bulkCopy = new SqlBulkCopy(connection)
                {
                    DestinationTableName = "TaxiTrips",
                    BulkCopyTimeout = 600,
                };

                bulkCopy.ColumnMappings.Add("PickupDateTime", "PickupDateTime");
                bulkCopy.ColumnMappings.Add("DropoffDateTime", "DropoffDateTime");
                bulkCopy.ColumnMappings.Add("PassengerCount", "PassengerCount");
                bulkCopy.ColumnMappings.Add("TripDistance", "TripDistance");
                bulkCopy.ColumnMappings.Add("StoreAndFwdFlag", "StoreAndFwdFlag");
                bulkCopy.ColumnMappings.Add("PULocationId", "PULocationId");
                bulkCopy.ColumnMappings.Add("DOLocationId", "DOLocationId");
                bulkCopy.ColumnMappings.Add("FareAmount", "FareAmount");
                bulkCopy.ColumnMappings.Add("TipAmount", "TipAmount");

                await bulkCopy.WriteToServerAsync(dataTable);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }

        public async Task<HashSet<string>> GetAllKeysAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var query = @"
                SELECT PickupDateTime, DropoffDateTime, PassengerCount
                FROM TaxiTrips";

            var keys = await connection.QueryAsync<(DateTime PickupDateTime, DateTime DropoffDateTime, int PassengerCount)>(query);

            return keys
                .Select(k => $"{k.PickupDateTime:O}_{k.DropoffDateTime:O}_{k.PassengerCount}")
                .ToHashSet();
        }

        public async Task<bool> IsExist(TaxiTrip trip)
        {
            using var connection = new SqlConnection(_connectionString);

            var result = await connection.QueryFirstOrDefaultAsync<TaxiTrip>(Resource.IsExist, trip);

            if(result is null)return false;
            return true;
        }

        private DataTable ConvertToDataTable(IEnumerable<TaxiTrip> trips)
        {
            var table = new DataTable();

            table.Columns.Add("PickupDateTime", typeof(DateTime));
            table.Columns.Add("DropoffDateTime", typeof(DateTime));
            table.Columns.Add("PassengerCount", typeof(int));
            table.Columns.Add("TripDistance", typeof(double));
            table.Columns.Add("StoreAndFwdFlag", typeof(string));
            table.Columns.Add("PULocationId", typeof(int));
            table.Columns.Add("DOLocationId", typeof(int));
            table.Columns.Add("FareAmount", typeof(double));
            table.Columns.Add("TipAmount", typeof(double));

            foreach (var trip in trips)
            {
                table.Rows.Add(
                    trip.PickupDateTime,
                    trip.DropoffDateTime,
                    trip.PassengerCount,
                    trip.TripDistance,
                    trip.StoreAndFwdFlag,
                    trip.PULocationId,
                    trip.DOLocationId,
                    trip.FareAmount,
                    trip.TipAmount
                );
            }

            return table;
        }
    }

}
