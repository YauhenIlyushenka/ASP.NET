﻿namespace Pcf.Administration.BLL.Models
{
	public class RoleItemResponseDto
	{
		public int Id { get; set; }
		public required string Name { get; init; }
		public required string Description { get; init; }
	}
}
