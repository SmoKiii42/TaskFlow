using System.ComponentModel.DataAnnotations;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.Models
{
    //потом доделаю
    public enum Role
    {
        Admin,
        User,
        Guest
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
        public string Description { get; set; } = "";
        public string? AvatarPath { get; set; }

        public Role Role { get; set; }
    }

    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTime DueDate { get; set; }

        public Workspace Workspace { get; set; }

        public int WorkspaceId { get; set; }

        public required string Status { get; set; }

        public string Priority { get; set; } = "Medium";

        public bool IsCompleted { get; set; }
    }


    public class FriendRequest
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public required User Sender { get; set; }

        public int ReceiverId { get; set; }

        public required User Receiver { get; set; }


        public required string Status { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class Friend
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public required User User { get; set; }

        public int FriendId { get; set; }

        public required User FriendUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class WorkspaceInvite
    {
        public int Id { get; set; }

        public int WorkspaceId { get; set; }
        public required Workspace Workspace { get; set; }

        public int SenderId { get; set; }
        public required User Sender { get; set; }

        public int ReceiverId { get; set; }
        public required User Receiver { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class WorkspaceMember
    {
        public int Id { get; set; }

        public int WorkspaceId { get; set; }

        public Workspace Workspace { get; set; }


        public int UserId { get; set; }

        public User User { get; set; }

        public string Role { get; set; } = "Member";
    }
}