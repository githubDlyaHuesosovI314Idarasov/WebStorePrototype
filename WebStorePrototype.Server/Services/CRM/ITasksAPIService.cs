using Refit;
using WebStorePrototype.Server.Models.Base;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Models.CRM_API.RequestBody;
using WebStorePrototype.Server.Models.CRM_API.Response;
using TaskStatus = WebStorePrototype.Server.Models.CRM_API.Data.TaskStatus;

namespace WebStorePrototype.Server.Services.CRM
{
    public interface ITasksAPIService
    {
        [Get("/tasks")]
        public Task<GetListResponse<TaskModel>> GetTasks([Query] GetTasksQueryParams queryParams);

        [Post("/tasks")]
        public Task<CreatedResponse> PostTasks([Query] PostTasksQueryParams queryParams);

        [Get("/tasks/{id}")]
        public Task<TaskModel> GetTask(Int64 id);

        [Patch("/tasks/{id}")]
        public Task<CreatedResponse> PatchTask(Int64 id, [Body] PatchTaskRequestBody body);

        [Delete("/tasks/{id}")]
        public Task<DeletedResponse> DeleteTask(Int64 id);

        [Post("/tasks/{task_id}/comments")]
        public Task<CreatedResponse> PostComment([AliasAs("task_id")] Int64 id, [Body] PostTaskCommentRequestBody body);

        [Get("/tasks/categories")]
        public Task<GetListResponse<TaskCategory>> GetTaskCategories();

        [Get("/tasks/statuses")]
        public Task<GetListResponse<TaskStatus>> GetTaskStatuses();
    
    }

}
