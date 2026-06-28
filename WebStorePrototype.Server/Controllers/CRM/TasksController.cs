using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using WebStorePrototype.Server.Models.CRM_API.Data;
using WebStorePrototype.Server.Models.CRM_API.QueryParams;
using WebStorePrototype.Server.Models.CRM_API.RequestBody;
using WebStorePrototype.Server.Services.CRM;


namespace WebStorePrototype.Server.Controllers.CRM
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksAPIService _tasksService;
        public TasksController(CRMSettings settings)
        {
            _tasksService = RestService.For<ITasksAPIService>(settings.Entrypoint);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks([Query] GetTasksQueryParams queryParams)
        {
            var result = await _tasksService.GetTasks(queryParams);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostTasks([Query] PostTasksQueryParams queryParams)
        {
            var result = await _tasksService.PostTasks(queryParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<TaskModel> GetTask(Int64 id)
        {
            TaskModel taskModel = await _tasksService.GetTask(id);
            return taskModel;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTask(Int64 id, [Body] PatchTaskRequestBody body)
        {
            var result = await _tasksService.PatchTask(id, body);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Int64 id)
        {
            var result = await _tasksService.DeleteTask(id);
            return Ok(result);
        }

        [HttpPost("{task_Id}/comments")]
        public async Task<IActionResult> PostComment([AliasAs("task_id")] Int64 id, [Body] PostTaskCommentRequestBody body)
        {
            var result = await _tasksService.PostComment(id, body);
            return Ok(result);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetTaskCategories()
        {
            var result = await _tasksService.GetTaskCategories();
            return Ok(result);
        }

        [HttpGet("statuses")]
        public async Task<IActionResult> GetTaskStatuses()
        {
            var result = await _tasksService.GetTaskStatuses();
            return Ok(result);
        }
    }
}
