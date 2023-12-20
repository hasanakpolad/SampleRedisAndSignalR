using Microsoft.AspNetCore.Mvc;
using SampleRedisandSignalR.RedisBase.Logic;
using SampleRedisandSignalR.RedisBase.Models;

namespace SampleRedisandSignalR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly IIndexManager _indexManager;
        public SampleController(IIndexManager indexManager)
        {
            _indexManager = indexManager;
        }
        [HttpGet("Get")]
        public IActionResult Get(string key)
        {
            var data = _indexManager.Get<UserModel>(key);
            return Ok(data);
        }

        [HttpPost(nameof(Add))]
        public IActionResult Add(string key, object value)
        {
            _indexManager.Set(key, value);
            return Ok("added");
        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(string key)
        {
            _indexManager.Remove(key);
            return Ok("deleted");
        }
    }
}
