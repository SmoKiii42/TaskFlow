using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Models;

namespace TaskFlow.Новая_папка1.TaskFlow;

public class WorkspaceInviteService
{
    private readonly AppDbContext _context;

    public WorkspaceInviteService()
    {
        _context = new AppDbContext();
    }

    public void SendInvite(int workspaceId, int senderId, int receiverId)
    {
        WorkspaceInvite invite = new WorkspaceInvite()
        {
            WorkspaceId = workspaceId,
            SenderId = senderId,
            ReceiverId = receiverId,
            Status = "Pending",

            Workspace = _context.Workspaces.First(w => w.Id == workspaceId),
            Sender = _context.Users.First(u => u.Id == senderId),
            Receiver = _context.Users.First(u => u.Id == receiverId)
        };

        _context.WorkspaceInvites.Add(invite);

        _context.SaveChanges();
    }

    public List<WorkspaceInvite> GetInvites(int receiverId)
    {
        return _context.WorkspaceInvites
            .Include(i => i.Workspace)
            .Include(i => i.Sender)
            .Where(i => i.ReceiverId == receiverId &&
                        i.Status == "Pending")
            .ToList();
    }


    public void AcceptInvite(int inviteId)
    {
        WorkspaceInvite? invite =
            _context.WorkspaceInvites
            .FirstOrDefault(x => x.Id == inviteId);

        if (invite == null)
            return;


        invite.Status = "Accepted";

        _context.SaveChanges();
    }



    public void DeclineInvite(int inviteId)
    {
        WorkspaceInvite? invite = _context.WorkspaceInvites.FirstOrDefault(x => x.Id == inviteId);

        if (invite == null) return;


        invite.Status = "Declined";

        _context.SaveChanges();
    }


    public void DeleteFriend(int friendId)
    {


    }
}