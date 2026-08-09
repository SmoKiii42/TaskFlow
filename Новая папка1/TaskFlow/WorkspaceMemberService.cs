using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Models;

namespace TaskFlow.Services
{
    public class WorkspaceMemberService
    {
        private readonly AppDbContext _context;


        public WorkspaceMemberService()
        {
            _context = new AppDbContext();
        }



        public void AddMember(
            int workspaceId,
            int userId,
            string role = "Member")
        {

            WorkspaceMember member = new WorkspaceMember()
            {
                WorkspaceId = workspaceId,
                UserId = userId,
                Role = role,

                Workspace = _context.Workspaces
                    .First(w => w.Id == workspaceId),

                User = _context.Users
                    .First(u => u.Id == userId)
            };


            _context.WorkspaceMembers.Add(member);

            _context.SaveChanges();
        }



        public List<WorkspaceMember> GetMembers(int workspaceId)
        {
            return _context.WorkspaceMembers
                .Include(x => x.User)
                .Where(x => x.WorkspaceId == workspaceId)
                .ToList();
        }

        public WorkspaceMember? GetMember(int workspaceId, int userId)
        {
            return _context.WorkspaceMembers
                .FirstOrDefault(x =>
                    x.WorkspaceId == workspaceId &&
                    x.UserId == userId);
        }


        public void RemoveMember(int workspaceId, int userId)
        {

            var member = GetMember(workspaceId, userId);

            if (member == null)return;

            _context.WorkspaceMembers.Remove(member);
            _context.SaveChanges();
        }

        public bool IsMember(
            int workspaceId,
            int userId)
        {
            return _context.WorkspaceMembers
                .Any(x =>
                x.WorkspaceId == workspaceId &&
                x.UserId == userId);
        }

        public bool HasAccess(int workspaceId, int userId)
        {
            return _context.Workspaces.Any(w =>
                w.Id == workspaceId &&
                w.OwnerId == userId)
                ||
                _context.WorkspaceMembers.Any(m =>
                    m.WorkspaceId == workspaceId &&
                    m.UserId == userId);
        }        

    }
}