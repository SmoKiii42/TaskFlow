using Microsoft.EntityFrameworkCore;
using System.Windows;
using TaskFlow.Data;


namespace TaskFlow
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                db.Database.Migrate();
            }

            base.OnStartup(e);
        }
    }
}