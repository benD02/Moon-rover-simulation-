using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Collections.Generic;
using robot_controller_api.Models;

namespace robot_controller_api.Persistence
{
	public class RobotCommandAD0 : IRobotCommandDataAccess
	{
		// connection string to the database.
		public const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=ben0009;Database=sit331";

		public List<RobotCommand> GetRobotCommands()
		{
			var robotCommands = new List<RobotCommand>();

			using (var conn = new NpgsqlConnection(CONNECTION_STRING))
			{
				conn.Open();
				using (var cmd = new NpgsqlCommand("SELECT * FROM robotcommand", conn))
				{
					using (var dr = cmd.ExecuteReader())
					{
						while (dr.Read())
						{
							var id = dr.GetInt32(0);
							var name = dr.GetString(1);
							var descr = dr.IsDBNull(2) ? null : dr.GetString(2);
							var isMoveCmd = dr.GetBoolean(3);
							var createdDate = dr.GetDateTime(4);
							var modifiedDate = dr.GetDateTime(5);
							var robotCommand = new RobotCommand(id, name, isMoveCmd, createdDate, modifiedDate, descr);
							robotCommands.Add(robotCommand);
						}
					}
				}
			}

			return robotCommands;
		}

		public RobotCommand AddRobotCommand(RobotCommand robotCommand)
		{
			using (var conn = new NpgsqlConnection(CONNECTION_STRING))
			{
				conn.Open();
				using (var cmd = new NpgsqlCommand("INSERT INTO robotcommand (\"Name\", description, ismovecommand, createddate, modifieddate) VALUES (@Name, @Description, @IsMoveCommand, @CreatedDate, @ModifiedDate)", conn))
				{
					cmd.Parameters.AddWithValue("Name", robotCommand.Name);
					cmd.Parameters.AddWithValue("Description", robotCommand.Description);
					cmd.Parameters.AddWithValue("IsMoveCommand", robotCommand.IsMoveCommand);
					cmd.Parameters.AddWithValue("CreatedDate", robotCommand.CreatedDate);
					cmd.Parameters.AddWithValue("ModifiedDate", robotCommand.ModifiedDate);
					var rowsAffected = cmd.ExecuteNonQuery();
					if (rowsAffected == 0)
					{
						throw new Exception("Error. Unable to insert robot command into the database.");
					}
				}
			}
			return null;
		}

		public RobotCommand UpdateRobotCommand(int id, RobotCommand robotCommand)
		{
			using (var conn = new NpgsqlConnection(CONNECTION_STRING))
			{
				conn.Open();
				using (var cmd = new NpgsqlCommand("UPDATE robotcommand SET \"Name\"=@Name, description=@Description, ismovecommand=@IsMoveCommand, createddate=@CreatedDate, modifieddate=@ModifiedDate WHERE id=@Id", conn))
				{
					cmd.Parameters.AddWithValue("Name", robotCommand.Name);
					cmd.Parameters.AddWithValue("Description", robotCommand.Description);
					cmd.Parameters.AddWithValue("IsMoveCommand", robotCommand.IsMoveCommand);
					cmd.Parameters.AddWithValue("CreatedDate", robotCommand.CreatedDate);
					cmd.Parameters.AddWithValue("ModifiedDate", robotCommand.ModifiedDate);
					cmd.Parameters.AddWithValue("Id", id);
					var rowsAffected = cmd.ExecuteNonQuery();
					if (rowsAffected == 0)
					{
						throw new Exception("Error. Unable to update robot command.");
					}
				}
			}
			return null;
		}

		public RobotCommand DeleteRobotCommand(int id)
		{
			using var conn = new NpgsqlConnection(CONNECTION_STRING);
			conn.Open();
			using var cmd = new NpgsqlCommand("DELETE FROM robotcommand WHERE id=@Id", conn);
			cmd.Parameters.AddWithValue("Id", id);
			var rowsAffected = cmd.ExecuteNonQuery();
			if (rowsAffected == 0)
			{
				throw new Exception("Error. Unable to delete robot command.");
			}
			return null;
		}

		public RobotCommand GetRobotCommandById(int id)
		{
			using var conn = new NpgsqlConnection(CONNECTION_STRING);
			conn.Open();

			using var cmd = new NpgsqlCommand("SELECT * FROM robotcommand WHERE id = @id", conn);
			cmd.Parameters.AddWithValue("id", id);

			using var dr = cmd.ExecuteReader();

			if (!dr.Read())
			{
				return null;
			}

			var name = dr.GetString(1);
			var descr = dr.IsDBNull(2) ? null : dr.GetString(2);
			var isMoveCmd = dr.GetBoolean(3);
			var createdDate = dr.GetDateTime(4);
			var modifiedDate = dr.GetDateTime(5);

			var robotCommand = new RobotCommand(id, name, isMoveCmd, createdDate, modifiedDate, descr);

			return robotCommand;
		}

		public bool IsMoveCommand(int id)
		{
			using var conn = new NpgsqlConnection(CONNECTION_STRING);
			conn.Open();

			using var cmd = new NpgsqlCommand("SELECT ismovecommand FROM robotcommand WHERE id = @id", conn);
			cmd.Parameters.AddWithValue("id", id);

			var result = cmd.ExecuteScalar();

			if (result is DBNull)
			{
				return false;
			}

			return (bool)result;
		}

	}
}
