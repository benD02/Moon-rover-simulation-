using robot_controller_api.Models;
namespace robot_controller_api.Persistence
{
	public interface IRobotCommandDataAccess 
	{
		RobotCommand AddRobotCommand(RobotCommand robotCommand);
		RobotCommand DeleteRobotCommand(int id);
		RobotCommand GetRobotCommandById(int id);
		List<RobotCommand> GetRobotCommands();
		bool IsMoveCommand(int id);
		RobotCommand UpdateRobotCommand(int id, RobotCommand robotCommand);
	}
}