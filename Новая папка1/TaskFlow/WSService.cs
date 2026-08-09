using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.Services
{
    public class WorkspaceService
    {
        private readonly AppDbContext _context;

        public WorkspaceService()
        {
            _context = new AppDbContext();
        }

        public void CreateWorkspace( string name, string description, int ownerId)
        {
            var workspace = new Workspace(name, description, DateTime.Now,ownerId);

            _context.Workspaces.Add(workspace);
            _context.SaveChanges();

            WorkspaceMember owner = new WorkspaceMember
            {
                WorkspaceId = workspace.Id,
                UserId = ownerId,
                Role = "Owner",

                Workspace = workspace,

                User = _context.Users.First(u => u.Id == ownerId)
            };

            _context.WorkspaceMembers.Add(owner);
            _context.SaveChanges();

        }



        public List<Workspace> GetWorkspacesForUser(int userId)
        {
            return _context.Workspaces
                .Where(w =>
                    w.OwnerId == userId ||

                    _context.WorkspaceMembers
                        .Any(m =>
                            m.WorkspaceId == w.Id &&
                            m.UserId == userId)
                )
                .ToList();
        }
        public Workspace? GetWorkspace(int workspaceId)
        {
            return _context.Workspaces.FirstOrDefault(w => w.Id == workspaceId);
        }

        public void RemoveMember(
    int workspaceId,
    int userId)
        {
            var member =
                _context.WorkspaceMembers
                .FirstOrDefault(m =>
                    m.WorkspaceId == workspaceId &&
                    m.UserId == userId);


            if (member == null)
                return;


            _context.WorkspaceMembers.Remove(member);

            _context.SaveChanges();
        }

        public void DeleteWorkspace(int workspaceId)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == workspaceId);
            if (workspace != null)
            {
                _context.Workspaces.Remove(workspace);
                _context.SaveChanges();
            }
        }

        public Workspace? GetWorkspaceByName(string name)
        {
            return _context.Workspaces.FirstOrDefault(w => w.Name == name);
        }

        public List<Workspace> GetWorkspacesByOwner(int ownerId)
        {
            return _context.Workspaces.Where(w => w.OwnerId == ownerId).ToList();
        }

        public void UpdateWorkspace(int workspaceId, string newName, string newDescription)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == workspaceId);
            if (workspace != null)
            {
                workspace.Name = newName;
                workspace.Description = newDescription;
                _context.SaveChanges();
            }
        }


        public bool WorkspaceExists(int workspaceId)
        {
            return _context.Workspaces.Any(w => w.Id == workspaceId);
        }


        public void CreatePersonalWorkspace(User user)
        {
            Workspace personalWorkspace = new Workspace
            {
                Name = user.FirstName + " " + user.LastName + "'s Personal Workspace",
                Description = "Personal workspace for " + user.FirstName + " " + user.LastName,
                CreateDate = DateTime.Now,
                OwnerId = user.Id

            };
            _context.Workspaces.Add(personalWorkspace);
            _context.SaveChanges();
        }


        /*        public Workspace? GetWorkspaceById(int workspaceId)
                {
                    return _context.Workspaces.FirstOrDefault(w => w.Id == workspaceId);
                }*/


    }
}