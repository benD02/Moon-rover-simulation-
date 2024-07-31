using Microsoft.AspNetCore.Mvc;
using robot_controller_api.Persistence;
using System.Collections.Generic;
using robot_controller_api.Models;
namespace robot_controller_api.Controllers
{
	[ApiController]
	[Route("api/maps")]
	public class MapsController : ControllerBase
	{

		private readonly IMapDataAccess _mapCommandsRepo;
		public MapsController(IMapDataAccess mapCommandsRepo)
		{
			_mapCommandsRepo = mapCommandsRepo;
		}

		[HttpGet("")]
		public IEnumerable<Map> GetAllMaps()
		{
			return _mapCommandsRepo.GetMaps();
		}

		[HttpGet("{id}")]
		public ActionResult<Map> GetMapById(int id)
		{
			var map = _mapCommandsRepo.GetMapById(id);
			if (map == null)
			{
				return NotFound();
			}
			return Ok(map);
		}

		[HttpPost]
		public ActionResult<Map> CreateMap(Map map)
		{
			if (map == null)
			{
				return BadRequest();
			}
			_mapCommandsRepo.AddMap(map);
			return CreatedAtAction(nameof(GetMapById), new { id = map.Id }, map);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateMap(int id, Map map)
		{
			var mapValid = _mapCommandsRepo.GetMapById(id);
			if (mapValid == null)
			{
				return NotFound();
			}
			if (map == null)
			{
				return BadRequest();
			}
			if (id != mapValid.Id)
			{
				return BadRequest();
			}
			_mapCommandsRepo.UpdateMap(map);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteMap(int id)
		{
			var mapValid = _mapCommandsRepo.GetMapById(id);
			if (mapValid == null)
			{
				return NotFound();
			}
			_mapCommandsRepo.DeleteMap(id);
			return NoContent();
		}
	}




}

