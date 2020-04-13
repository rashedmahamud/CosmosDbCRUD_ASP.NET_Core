using cosmosdb_test.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cosmosdb_test.Services
{

    public class CosmosDbService : IconsmosDbService
    {

        private Microsoft.Azure.Cosmos.Container _container;
        public CosmosDbService(
          CosmosClient dbClient,
          string databaseName,
          string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddEmployeeAsync(Employee employee)
        {
            await this._container.CreateItemAsync<Employee>(employee, new PartitionKey(employee.Id));
        }

        public async Task DelateEmployeeAsync(string id)
        {
            await this._container.DeleteItemAsync<Employee>(id, new PartitionKey(id));
        }

        public async Task<Employee> GetEmployeeAsync(string id)
        {
            try
            {
                ItemResponse<Employee> response = await this._container.ReadItemAsync<Employee>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Employee>(new QueryDefinition(queryString));
            List<Employee> results = new List<Employee>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, Employee employee)
        {
             await this._container.UpsertItemAsync<Employee>(employee, new PartitionKey(id));
        }

        public async Task UpdateEmployeeAsync(string id, Employee employee)
        {
            await this._container.UpsertItemAsync<Employee>(employee, new PartitionKey(id));
        }
    }

}
