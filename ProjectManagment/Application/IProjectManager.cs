using MongoDB.Bson;
using ProjectManagment.Entities;

namespace ProjectManagment.Application
{
    public interface IProjectManager
    {
        Project GetProjectById(ObjectId projectId);
        ObjectId CreateProject(Project projectToCreate);
        void GiveMoneyToProject(ObjectId projectId, decimal sum);

        void UpdateProject(Project projectToUpdate);
        void DeleteProject(ObjectId projectIdToDelete);
    }
}