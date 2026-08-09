using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using TaskFlow.Core;
using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.ViewsSettings;

namespace TaskFlow.Новая_папка1.TaskFlow
{
    internal class FriendsService
    {
        private readonly AppDbContext _context;


        public FriendsService()
        {
            _context = new AppDbContext();
        }

        public bool RequestExists(int senderId, int receiverId)
        {
            return _context.FriendRequests.Any(r =>
                r.SenderId == senderId &&
                r.ReceiverId == receiverId &&
                r.Status == "Pending");
        }

        public void SendRequest(int senderId, int receiverId)
        {
            FriendRequest request = new FriendRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Status = "Pending",
                CreatedAt = DateTime.Now,

                Sender = _context.Users.First(u => u.Id == senderId),
                Receiver = _context.Users.First(u => u.Id == receiverId)
            };

            _context.FriendRequests.Add(request);

            _context.SaveChanges();

        }



        public List<FriendRequest> GetIncomingRequests(int userId)
        {
            return _context.FriendRequests
                .Include(r => r.Sender)
                .Where(r => r.ReceiverId == userId &&
                            r.Status == "Pending")
                .ToList();
        }

        public void AcceptRequest(int requestId)
        {
            FriendRequest? request = _context.FriendRequests
                .FirstOrDefault(r => r.Id == requestId);

            if (request == null)
                return;

            request.Status = "Accepted";

            Friend friend1 = new Friend()
            {
                UserId = request.SenderId,
                FriendId = request.ReceiverId,
                User = _context.Users.First(u => u.Id == request.SenderId),
                FriendUser = _context.Users.First(u => u.Id == request.ReceiverId),
                CreatedAt = DateTime.Now
            };

            Friend friend2 = new Friend()
            {
                UserId = request.ReceiverId,
                FriendId = request.SenderId,
                User = _context.Users.First(u => u.Id == request.ReceiverId),
                FriendUser = _context.Users.First(u => u.Id == request.SenderId),
                CreatedAt = DateTime.Now
            };

            _context.Friends.Add(friend1);
            _context.Friends.Add(friend2);

            _context.SaveChanges();
        }

        public void RemoveFriend(int userId, int friendId)
        {
            Friend? friendship1 = _context.Friends
                .FirstOrDefault(f => f.UserId == userId && f.FriendId == friendId);
            Friend? friendship2 = _context.Friends
                .FirstOrDefault(f => f.UserId == friendId && f.FriendId == userId);
            if (friendship1 != null)
                _context.Friends.Remove(friendship1);
            if (friendship2 != null)
                _context.Friends.Remove(friendship2);
            _context.SaveChanges();
        }

        public List<User> GetFriends(int userId)
        {
            return _context.Friends
                .Where(f => f.UserId == userId)
                .Select(f => f.FriendUser)
                .ToList();
        }


        public void DeclineRequest(int requestId)
        {
            FriendRequest? request =
                _context.FriendRequests.FirstOrDefault(r => r.Id == requestId);

            if (request == null)
                return;

            request.Status = "Declined";

            _context.SaveChanges();
        }
    }
}
