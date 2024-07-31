using Npgsql;
using System;
using System.Collections.Generic;
using robot_controller_api.Models;

namespace robot_controller_api.Persistence
{
	public class MapAD0 : IMapDataAccess
	{
		public const string CONNECTION_STRING = "Host=localhost;Username=postgres;Password=ben0009;Database=sit331";

		public List<Map> GetMaps()
		{
			var maps = new List<Map>();
			using var conn = new NpgsqlConnection(CONNECTION_STRING);
			conn.Open();
			using var cmd = new NpgsqlCommand("SELECT * FROM rmap", conn);
			using var dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				var id = dr.GetInt32(0);
				var columns = dr.GetInt32(1);
				var rows = dr.GetInt32(2);
				var name = dr.GetString(3);
				var descr = dr.IsDBNull(4) ? null : dr.GetString(4);
				var createdDate = dr.GetDateTime(5);
				var modifiedDate = dr.GetDateTime(6);
				var rMap = new Map(id, columns, rows, name, createdDate, modifiedDate, descr);
				maps.Add(rMap);
			}
			return maps;
		}

		public Map GetMapById(int id)
		{
			Map map = null;
			using var conn = new NpgsqlConnection(CONNECTION_STRING);
			conn.Open();
			using var cmd = new NpgsqlCommand("SELECT * FROM map WHERE id = @Id", conn);
			cmd.Parameters.AddWithValue("Id", id);
			using var dr = cmd.ExecuteReader();
			if (dr.Read())
			{
				var columns = dr.GetInt32(1);
				var rows = dr.GetInt32(2);
				var name = dr.GetString(3);
				var createdDate = dr.GetDateTime(4);
				var modifiedDate = dr.GetDateTime(5);
				var description = dr.IsDBNull(6) ? null : dr.GetString(6);

				map = new Map(id, columns, rows, name, createdDate, modifiedDate, description);
			}
			return map;
		}

		public Map AddMap(Map rMap)
		{
			using var conn = new NpgsqlConnection(CONNECTION_STRING);
			conn.Open();
			using var cmd = new NpgsqlCommand(
				"INSERT INTO rmap (columns, rows, name, description, createddate, modifieddate) " +
				"VALUES (@Columns, @Rows, @Name, @Description, @CreatedDate, @ModifiedDate)", conn);
			cmd.Parameters.AddWithValue("Columns", rMap.Columns);
			cmd.Parameters.AddWithValue("Rows", rMap.Rows);
			cmd.Parameters.AddWithValue("Name", rMap.Name);
			cmd.Parameters.AddWithValue("Description", rMap.Description ?? (object)DBNull.Value);
			cmd.Parameters.AddWithValue("CreatedDate", rMap.CreatedDate);
			cmd.Parameters.AddWithValue("ModifiedDate", rMap.ModifiedDate);
			cmd.ExecuteNonQuery();
			return rMap;
		}

		public Map UpdateMap(Map rMap)
		{
			using var conn = new NpgsqlConnection(CONNECTION_STRING);
			conn.Open();
			using var cmd = new NpgsqlCommand(
				"UPDATE rmap SET columns = @Columns, rows = @Rows, name = @Name, " +
				"description = @Description, modifieddate = @ModifiedDate " +
				"WHERE id = @Id", conn);
			cmd.Parameters.AddWithValue("Columns", rMap.Columns);
			cmd.Parameters.AddWithValue("Rows", rMap.Rows);
			cmd.Parameters.AddWithValue("Name", rMap.Name);
			cmd.Parameters.AddWithValue("Description", rMap.Description ?? (object)DBNull.Value);
			cmd.Parameters.AddWithValue("ModifiedDate", rMap.ModifiedDate);
			cmd.Parameters.AddWithValue("Id", rMap.Id);
			cmd.ExecuteNonQuery();
			return null;
		}

		public Map DeleteMap(int id)
		{
			using var conn = new NpgsqlConnection(CONNECTION_STRING);
			conn.Open();
			using var cmd = new NpgsqlCommand("DELETE FROM rmap WHERE id = @Id", conn);
			cmd.Parameters.AddWithValue("Id", id);
			cmd.ExecuteNonQuery();
			return new Map();
		}

	}

}
