using ETL.Entities;
using ETL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Interfaces
{
    public interface ITaxiTripService
    {
        Task<Result<bool>> ProcessTripsAsync(string csvFilePath);

    }
}
