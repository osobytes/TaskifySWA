﻿namespace Taskify.AzureTables.Repositories
{
    using AutoMapper;
    using Azure.Data.Tables;
    using Taskify.AzureTables.Entities;
    using Taskify.Data.Models;
    using Taskify.Data.Repositories;

    internal class TaskStorageRepository : ITaskRepository
    {
        private readonly TableClient Table;
        private readonly IMapper Mapper;
        private const string Tasks = nameof(Tasks);
        public TaskStorageRepository(IAzureTableStorageService storageService, IMapper mapper)
        {
            Table = storageService.GetClient().GetTableClient(Tasks);
            Mapper = mapper;
        }
        public async Task<bool> DeleteAsync(Guid id, Guid? parentId = null)
        {
            var result = await Table.DeleteEntityAsync(parentId?.ToString() ?? "root", id.ToString());
            return !result.IsError;
        }

        public static void CreateTableIfNotExists(IAzureTableStorageService service)
        {
            var client = service.GetClient();
            var table = client.GetTableClient(Tasks);
            table.CreateIfNotExists();
        }

        public async Task<TaskModel?> GetAsync(Guid id, Guid? parentId = null)
        {
            var partitionKey = "root";
            if (parentId != null)
            {
                partitionKey = parentId.ToString();
            }
            try
            {
                var result = await Table.GetEntityAsync<TaskEntity>(partitionKey, id.ToString());
                return Mapper.Map<TaskModel>(result);
            }
            catch
            {
                // When the entity is not found an exception is thrown. We want to return null in this situation.
                return null;
            }
        }

        public async Task<TaskModel> UpsertAsync(TaskModel task)
        {
            var entity = Mapper.Map<TaskEntity>(task);
            var response = await Table.UpsertEntityAsync(entity, TableUpdateMode.Replace);
            if (response.IsError)
            {
                throw new Exception($"Error ocured while upserting element: {response.Content}");
            }
            return task;
        }

        public async Task<TaskModel[]> GetRootAsync()
        {
            List<TaskModel> results = new();
            var query = Table.QueryAsync<TaskEntity>("PartitionKey eq 'root'");
            await foreach (var item in query)
            {
                results.Add(Mapper.Map<TaskModel>(item));
            }
            return results.ToArray();
        }

        public async Task<TaskModel[]> GetChildrenAsync(Guid parent)
        {
            List<TaskModel> results = new();
            var query = Table.QueryAsync<TaskEntity>($"PartitionKey eq '{parent}'");
            await foreach (var item in query)
            {
                results.Add(Mapper.Map<TaskModel>(item));
            }
            return results.ToArray();
        }
    }
}
