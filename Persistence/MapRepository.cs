using System;
using System.Collections.Generic;
using Npgsql;
using robot_controller_api.Persistence;
using robot_controller_api.Models;

namespace robot_controller_api.Persistence
{
	public class MapRepository : IMapDataAccess, IRepository
	{
		//private IRepository _repo => this;
		private readonly IRepository _repo;

		public MapRepository(IRepository repo)
		{
			_repo = repo;
		}

		public List<T> ExecuteReader<T>(string sqlCommand, NpgsqlParameter[] dbParams = null) where T : class, new()
		{
			return _repo.ExecuteReader<T>(sqlCommand, dbParams);
		}

		public List<Map> GetMaps()
		{
			var maps = _repo.ExecuteReader<Map>("SELECT * FROM public.map");
			return maps;
		}

		public Map GetMapById(int id)
		{
			var map = _repo.ExecuteReader<Map>("SELECT * FROM public.map WHERE id=@id", new NpgsqlParameter[id])
				.FirstOrDefault();
			return map;
		}

		public Map AddMap(Map Map)
		{
			var sqlParams = new NpgsqlParameter[] {
				new("columns", Map.Columns),
				new("rows", Map.Rows),
				new("name", Map.Name),
				new("createddate", Map.CreatedDate),
				new("modifieddate", Map.ModifiedDate),
				new("description", Map.Description ?? (object)DBNull.Value)
			};
			var result = _repo.ExecuteReader<Map>("INSERT INTO public.map (columns, rows, name, createddate, modifieddate, description)" +
				" VALUES (@columns, @rows, @name, @createddate, @modifieddate, @description) RETURNING *",
				sqlParams)
				.Single();
			return result;
		}

		public Map UpdateMap(Map Map)
		{
			var sqlParams = new NpgsqlParameter[] {
				new("id", Map.Id),
				new("columns", Map.Columns),
				new("rows", Map.Rows),
				new("name", Map.Name),
				new("createddate", Map.CreatedDate),
				new("modifieddate", Map.ModifiedDate),
				new("description", Map.Description ?? (object)DBNull.Value)
			};
			var result = _repo.ExecuteReader<Map>("UPDATE public.map SET columns=@columns, rows=@rows, name=@name, " +
				"createddate=@createddate, modifieddate=@modifieddate, description=@description WHERE id=@id RETURNING *",
				sqlParams)
				.Single();
			return result;
		}

		public Map DeleteMap(int id)
		{
			_repo.ExecuteReader<Map>("DELETE FROM public.map WHERE id=@id",
				new NpgsqlParameter[id]);
			return null;
		}
	}





}