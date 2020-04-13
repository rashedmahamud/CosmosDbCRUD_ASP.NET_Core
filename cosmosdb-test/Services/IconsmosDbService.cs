using cosmosdb_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cosmosdb_test.Services
{
   public interface IconsmosDbService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(string query);
        Task<Employee> GetEmployeeAsync(string id);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(string id, Employee employee);
        Task DelateEmployeeAsync(string id);

    }
}
