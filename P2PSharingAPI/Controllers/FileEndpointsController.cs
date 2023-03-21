using Microsoft.AspNetCore.Mvc;
using P2PSharingAPI.Managers;
using P2PSharingAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P2PSharingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileEndpointsController : ControllerBase
    {
        private FileEndpointManager _manager = new FileEndpointManager();

        // GET: api/<FileEndpointsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            IEnumerable<string>? fileNames = _manager.GetFileNames()!;
            if (fileNames == null || fileNames!.Count() <= 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(fileNames);
            }
        }

        // GET api/<FileEndpointsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{filename}")]
        public ActionResult<IEnumerable<string>> Get(string filename)
        {
            IEnumerable<FileEndpoint>? endpoints = _manager.GetEndpoints(filename)!;
            if (endpoints == null || endpoints!.Count() <= 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(endpoints);
            }
        }

        // POST api/<FileEndpointsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public ActionResult<FileEndpoint> Post([FromBody] FileEndpoint newFileEndpoint)
        {
            FileEndpoint fileEndpoint = _manager.AddEndpoint(newFileEndpoint);
            return Created($"api/FileEndpoints/{fileEndpoint.FileName}", fileEndpoint);
        }
    }
}
