using Microsoft.AspNetCore.Mvc;
using robot_controller_api.Persistence;
using System.Collections.Generic;
using robot_controller_api.Models;
namespace robot_controller_api.Controllers
{
	[ApiController]
	[Route("api/robot-commands")]
	public class RobotCommandsController : ControllerBase
	{
		private readonly IRobotCommandDataAccess _robotCommandsRepo;
		public RobotCommandsController(IRobotCommandDataAccess
	   robotCommandsRepo)
		{
			_robotCommandsRepo = robotCommandsRepo;
		}

		[HttpGet("")]
		//return all commands 
		public IEnumerable<RobotCommand> GetAllRobotCommands()
		{
			return _robotCommandsRepo.GetRobotCommands();
		}

		[HttpGet("move")]
		public IActionResult GetMoveCommandsOnly(int id)
		{
			var isMoveCommand = _robotCommandsRepo.IsMoveCommand(id);
			return Ok(isMoveCommand);
		}

		[HttpGet("{id}", Name = "GetRobotCommand")]
		public IActionResult GetRobotCommandById(int id)
		{
			var robotCommand = _robotCommandsRepo.GetRobotCommandById(id);
			if (robotCommand == null)
			{
				return NotFound();
			}
			return Ok(robotCommand);
		}

		[HttpPost()]
		public IActionResult AddRobotCommand(RobotCommand newCommand)
		{
			if (newCommand == null)
			{
				return BadRequest();
			}

			if (_robotCommandsRepo.GetRobotCommandById(newCommand.Id) != null)
			{
				return Conflict();
			}

			_robotCommandsRepo.AddRobotCommand(newCommand);
			return NoContent();
		}

		[HttpPut("{id}")]
		public IActionResult UpdateRobotCommand(int id, RobotCommand updatedCommand)
		{
			if (id == null || updatedCommand == null)
			{
				return BadRequest();
			}

			var robotCommand = _robotCommandsRepo.GetRobotCommandById(id);
			if (robotCommand == null)
			{
				return NotFound();
			}

			_robotCommandsRepo.UpdateRobotCommand(id, updatedCommand);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteRobotCommand(int id)
		{
			if (id == null)
			{
				return BadRequest();
			}

			var robotCommand = _robotCommandsRepo.GetRobotCommandById(id);
			if (robotCommand == null)
			{
				return NotFound();
			}

			_robotCommandsRepo.DeleteRobotCommand(id);
			return NoContent();
		}
	}
}
