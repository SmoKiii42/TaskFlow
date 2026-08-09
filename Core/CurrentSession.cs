using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Models;
using TaskFlow.Новая_папка1.TaskFlow;


namespace TaskFlow.Core
{

    public static class  CurrentSession
    {     
        public static User CurrentUser { get; set; }

        public static Workspace? CurrentWorkspace { get; set; }



    }
}
