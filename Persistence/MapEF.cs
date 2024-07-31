using System.Collections.Generic;
using System.Linq;
using robot_controller_api.Models;

namespace robot_controller_api.Persistence
{

	public class MapEF : RobotContext, IMapDataAccess
	{
		private  RobotContext _context => this;


		public Map AddMap(Map rMap)
		{
			_context.Maps.Add(rMap);
			_context.SaveChanges();
			return rMap;
		}

		public Map DeleteMap(int id)
		{
			var map = _context.Maps.Find(id);

			if (map != null)
			{
				_context.Maps.Remove(map);
				_context.SaveChanges();
			}
			return map;
		}

		public Map GetMapById(int id)
		{
			return _context.Maps.Find(id);
		}

		public List<Map> GetMaps()
		{
			return _context.Maps.ToList();
		}

		public Map UpdateMap(Map rMap)
		{
			var existingMap = _context.Maps.Find(rMap.Id);

			if (existingMap != null)
			{
				existingMap.Name = rMap.Name;
				existingMap.Columns = rMap.Columns;
				existingMap.Rows = rMap.Rows;
				existingMap.ModifiedDate = rMap.ModifiedDate;

				_context.SaveChanges();
			}
			return existingMap;
		}
	}
}

