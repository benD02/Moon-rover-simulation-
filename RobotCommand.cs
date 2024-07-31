namespace robot_controller_api;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

public class RobotCommand
{
    /// Implement <see cref="RobotCommand"> here following the task sheet requirements
    /// 
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsMoveCommand { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

	public RobotCommand() { }


	public RobotCommand(
 int id, string name, bool isMoveCommand, DateTime createdDate,
DateTime modifiedDate, string? description = null)
    {
        // Initialize every property with parameters.

        Id = id;
        Name = name;
        IsMoveCommand = isMoveCommand;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        Description = description;
    }

}
