using System;
using System.ComponentModel.DataAnnotations;

namespace MGSUCore.Models
{
    public class ProjectModel
    {
        public string Id { get; set; }

        [Required]
		public string Name { get; private set; }

        [Required]
		public string Direction { get; private set; }

        [Required]
        public decimal Need { get; private set; }

		public decimal Given { get; private set; }
    }
}
