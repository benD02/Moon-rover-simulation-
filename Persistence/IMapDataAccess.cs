using robot_controller_api.Models;
namespace robot_controller_api.Persistence
{
	public interface IMapDataAccess
	{
		Map AddMap(Map rMap);
		Map DeleteMap(int id);
		Map GetMapById(int id);
		List<Map> GetMaps();
		Map UpdateMap(Map rMap);
	}
}