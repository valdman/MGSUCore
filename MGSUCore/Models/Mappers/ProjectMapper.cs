using System;
using System.IO;
using Common.Entities;
using MGSUBackend.Models;
using MGSUCore.Models;
using MongoDB.Bson;

namespace MGSUCore.Controllers
{
    public static class ProjectMapper
    {
        public static ProjectModel ProjectToProjectModel(Project project)
        {
            if (project == null) return null;

            return new ProjectModel
            {
                Id = project.Id.ToString(),
                Name = project.Name,
                Direction = project.Direction,
                ShortDescription = project.ShortDescription,
                Content = project.Content,
				Img = project.Img == null
					? null
					: new ImageModel
                {
                    Original = project.Img.Original.ToString(),
                    Small = project.Img.Small.ToString(),
                    Role = project.Img.Role
                },
                Need = project.Need,
                Given = project.Given,
                Public = project.Public,
                CreatingTime = project.CreatingTime.ToString()
            };
        }

        public static Project ProjectModelToProject(ProjectModel projectModel)
        {
            if (projectModel == null) return null;

            return new Project
            {
				Name = projectModel.Name,
				Direction = projectModel.Direction,
				ShortDescription = projectModel.ShortDescription,
				Content = projectModel.Content,
				Img = projectModel.Img == null
					? null
					: new Image
					{
						Original = new FileInfo(projectModel.Img.Original),
						Small = new FileInfo(projectModel.Img.Small),
						Role = projectModel.Img.Role
					},
				Need = projectModel.Need,
				Given = projectModel.Given,
				Public = true,
                CreatingTime = BsonDateTime.Create(projectModel.CreatingTime)
            };
        }

    }
}