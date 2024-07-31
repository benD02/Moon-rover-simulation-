using Npgsql;
using robot_controller_api.Persistence;
using robot_controller_api;
using robot_controller_api.Models;


namespace robot_controller_api.Persistence
{ 

		public class RobotCommandRepository : IRobotCommandDataAccess, IRepository
		{
			private readonly IRepository _repo;

			public RobotCommandRepository(IRepository repo)
			{
				_repo = repo;
			}

			public List<RobotCommand> GetRobotCommands()
			{
				var commands = _repo.ExecuteReader<RobotCommand>("SELECT * FROM public.robotcommand");
				return commands;
			}

			public RobotCommand UpdateRobotCommand(RobotCommand updatedCommand)
			{
				var sqlParams = new NpgsqlParameter[]{
					new("id", updatedCommand.Id),
					new("name", updatedCommand.Name),
					new("description", updatedCommand.Description ?? (object)DBNull.Value),
					new("ismovecommand", updatedCommand.IsMoveCommand)
				};
				var result = _repo.ExecuteReader<RobotCommand>(
					"UPDATE robotcommand SET name=@name, description=@description, ismovecommand=@ismovecommand, modifieddate=current_timestamp WHERE id=@id RETURNING *;",
					sqlParams)
					.Single();
				return result;
			}

			// implementation of IRepository
			public List<T> ExecuteReader<T>(string sqlCommand, NpgsqlParameter[] dbParams = null) where T : class, new()
			{
				return _repo.ExecuteReader<T>(sqlCommand, dbParams);
			}

			public RobotCommand AddRobotCommand(RobotCommand robotCommand)
			{
				var sqlParams = new NpgsqlParameter[]{
					new("name", robotCommand.Name),
					new("description", robotCommand.Description ?? (object)DBNull.Value),
					new("ismovecommand", robotCommand.IsMoveCommand)
				};
				_repo.ExecuteReader<RobotCommand>(
					"INSERT INTO robotcommand (name, description, ismovecommand, createddate, modifieddate) VALUES (@name, @description, @ismovecommand, current_timestamp, current_timestamp);",
					sqlParams
				);

				return robotCommand;
			}

			public RobotCommand DeleteRobotCommand(int id)
			{
				var sqlParams = new NpgsqlParameter[]{
					new("id", id)
				};
				_repo.ExecuteReader<RobotCommand>(
					"DELETE FROM robotcommand WHERE id=@id;",
					sqlParams
				);
				return null;
			}

			public RobotCommand GetRobotCommandById(int id)
			{
				var sqlParams = new NpgsqlParameter[]{
					new("id", id)
				};
				var result = _repo.ExecuteReader<RobotCommand>(
					"SELECT * FROM public.robotcommand WHERE id=@id;",
					sqlParams
				).SingleOrDefault();
				return result;
			}

			public bool IsMoveCommand(int id)
			{
				var sqlParams = new NpgsqlParameter[]{
					new("id", id)
				};
				var result = _repo.ExecuteReader<RobotCommand>(
					"SELECT ismovecommand FROM public.robotcommand WHERE id=@id;",
					sqlParams
				).SingleOrDefault();

				if (result == null )
				{
					return false;
				} 
				return true;
			}

			public RobotCommand UpdateRobotCommand(int id, RobotCommand robotCommand)
			{
				var sqlParams = new NpgsqlParameter[]{
					new("id", id),
					new("name", robotCommand.Name),
					new("description", robotCommand.Description ?? (object)DBNull.Value),
					new("ismovecommand", robotCommand.IsMoveCommand)
				};
				_repo.ExecuteReader<RobotCommand>(
					"UPDATE robotcommand SET name=@name, description=@description, ismovecommand=@ismovecommand, modifieddate=current_timestamp WHERE id=@id;",
					sqlParams
				);
				
				return	robotCommand;
			}
		}


}
