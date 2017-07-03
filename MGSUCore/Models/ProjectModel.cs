using System;
using System.ComponentModel.DataAnnotations;
using MGSUBackend.Models;

namespace MGSUCore.Models
{
    public class ProjectModel
    {
        public string Id { get; set; }

        [Required]
		public string Name { get; set; }

        [Required]
		public string Direction { get; set; }

        [Required]
        public decimal Need { get; set; }

		public decimal Given { get; set; }

		[Required]
		[MaxLength(60)]
		public string ShortDescription { get; set; }

		public string Content { get; set; }

        public ImageModel Img { get; set; }

		public bool Public { get; set; }


	}
}
