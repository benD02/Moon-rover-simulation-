
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
namespace robot_controller_api.Models;

public class Map
{
	public int Id { get; set; }
	public int Columns { get; set; }
	public int Rows { get; set; }
	public string Name { get; set; }
	public DateTime CreatedDate { get; set; }
	public DateTime ModifiedDate { get; set; }
	public string? Description { get; set; }

	/// Implement <see cref="Map"> here following the task sheet requirements
	/// 
	public Map() { }

	public Map(int id, int columns, int rows, string name,  DateTime createdDate, DateTime modifiedDate, string? description = null)
	{
		// Initialize every property with parameters.

		Id = id;
		Columns = columns ;
		Rows = rows;
		Name = name;
		CreatedDate = createdDate;
		ModifiedDate = modifiedDate;
		Description = description;
	}
}
