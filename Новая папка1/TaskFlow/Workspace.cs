using Microsoft.Identity.Client;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Data;
using TaskFlow.Models;
using TaskFlow.Новая_папка1.TaskFlow;

namespace TaskFlow.Новая_папка1.TaskFlow
{
    public class Workspace
    {
        
        public int Id { get; set; }
        public  string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public int OwnerId { get; set; }

        public Workspace() { }       
        

        public Workspace( string name, string description, DateTime createDate, int ownerId)
        {
            Name = name;
            Description = description;
            CreateDate = createDate;
            OwnerId = ownerId;
        }
    }
}
