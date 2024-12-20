using ETL.Entities;
using ETL.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Interfaces
{
    public interface ITaxiTripRepository
    {
        Task<Result<bool>> BulkInsertAsync(IEnumerable<TaxiTrip> trips);
        Task<bool> IsExist(TaxiTrip trip);
        Task<HashSet<string>> GetAllKeysAsync();
    }
}
