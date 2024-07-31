using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Npgsql;

namespace robot_controller_api.Persistence
{

	public interface IRepository
	{
		List<T> ExecuteReader<T>(string sqlCommand, NpgsqlParameter[] dbParams = null) where T : class, new();
	}


	public class Repository : IRepository
	{
		public const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=ben0009;Database=sit331";
	
		public List<T> ExecuteReader<T>(string sqlCommand, NpgsqlParameter[]
		dbParams = null) where T : class, new()
		{
			var entities = new List<T>();
			using var conn = new NpgsqlConnection(CONNECTION_STRING);
			conn.Open();
			using var cmd = new NpgsqlCommand(sqlCommand, conn);
			// Some of our SQL commands might have SQL parameters we will need to
			//pass to DB.MS
			if (dbParams is not null)
			{
				// CommandType is unnecessary for PostgreSQL but can be used in
			//	other DB engines like Oracle or SQL Server. MS
				 // cmd.CommandType = CommandType.Text; 
				 cmd.Parameters.AddRange(dbParams.Where(x => x.Value is not
				null).ToArray());
			}
			using var dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				var entity = new T();
				dr.MapTo(entity); // AUTOMATIC ORM WILL HAPPEN HERE. MapTo does
				//not exist yet.MS

				entities.Add(entity);
			}
			return entities;


		}
	}
}

