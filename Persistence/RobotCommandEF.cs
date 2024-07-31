using System.Collections.Generic;
using System.Linq;
using robot_controller_api.Models;

namespace robot_controller_api.Persistence
{
	public class RobotCommandEF : RobotContext, IRobotCommandDataAccess
	{
		private RobotContext _context => this;

		public RobotCommand AddRobotCommand(RobotCommand robotCommand)
		{
			_context.RobotCommands.Add(robotCommand);
			_context.SaveChanges();
			return robotCommand;
		}

		public RobotCommand DeleteRobotCommand(int id)
		{
			var robotCommand = GetRobotCommandById(id);
			if (robotCommand != null)
			{
				_context.RobotCommands.Remove(robotCommand);
				_context.SaveChanges();
			}
			return robotCommand;	
		}

		public RobotCommand GetRobotCommandById(int id)
		{
			return _context.RobotCommands.FirstOrDefault(rc => rc.Id == id);
		}

		public List<RobotCommand> GetRobotCommands()
		{
			return _context.RobotCommands.ToList();
		}

		public bool IsMoveCommand(int id)
		{
			var robotCommand = GetRobotCommandById(id);
			return robotCommand != null && robotCommand.IsMoveCommand;
		}

		public RobotCommand UpdateRobotCommand(int id, RobotCommand robotCommand)
		{
			var existingRobotCommand = GetRobotCommandById(id);
			if (existingRobotCommand != null)
			{
				existingRobotCommand.Name = robotCommand.Name;
				existingRobotCommand.IsMoveCommand = robotCommand.IsMoveCommand;
				existingRobotCommand.Description = robotCommand.Description;
				existingRobotCommand.ModifiedDate = robotCommand.ModifiedDate;
				_context.SaveChanges();
			}
			return existingRobotCommand;
		}
	}

}