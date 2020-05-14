using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationNetLab.Models;

namespace WebApplicationNetLab.DAL
{
    public class EventorerInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EventorerContext>
    {
        protected override void Seed(EventorerContext context)
        {
            var accounts = new List<Account>
            {
                new Account{Email="loster@electromail.com", Nickname="Loster", CreationDate=DateTime.Parse("2005-09-01")},
                new Account{Email="comodo@electromail.com", Nickname="Comodo", CreationDate=DateTime.Parse("2010-10-01")},
                new Account{Email="kineret@electromail.com", Nickname="Kineret", CreationDate=DateTime.Parse("2016-07-01")},
                new Account{Email="ashtorer@electromail.com", Nickname="Astorer", CreationDate=DateTime.Parse("2017-09-01")},
                new Account{Email="compor@electromail.com", Nickname="Compor", CreationDate=DateTime.Parse("2015-04-01")},
                new Account{Email="indier@electromail.com", Nickname="Indier", CreationDate=DateTime.Parse("2019-12-01")},
                new Account{Email="poster@electromail.com", Nickname="Poster", CreationDate=DateTime.Parse("2018-09-01")},
                new Account{Email="fostor@electromail.com", Nickname="Fostor", CreationDate=DateTime.Parse("2009-09-01")}
            };

            accounts.ForEach(s => context.Accounts.Add(s));
            context.SaveChanges();

            var events = new List<Event>
            {
                new Event{ EventID=1001, Title="Summer event 1001", Credits=5},
                new Event{ EventID=1002, Title="Winter event 1002", Credits=5},
                new Event{ EventID=1003, Title="Fall event 1003", Credits=5},
                new Event{ EventID=1004, Title="Spring event 1004", Credits=5},
                new Event{ EventID=1005, Title="Fall event 1005", Credits=5},
                new Event{ EventID=1006, Title="Lobster event 1006", Credits=5},
                new Event{ EventID=1007, Title="Kinetic event 1007", Credits=5},
            };

            events.ForEach(s => context.Events.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment{ AccountID=1, EventID=1001, Grade=Grade.A},
                new Enrollment{ AccountID=1, EventID=1002, Grade=Grade.B},
                new Enrollment{ AccountID=1, EventID=1003, Grade=Grade.C},
                new Enrollment{ AccountID=2, EventID=1004, Grade=Grade.A},
                new Enrollment{ AccountID=2, EventID=1005, Grade=Grade.B},
                new Enrollment{ AccountID=2, EventID=1006, Grade=Grade.C},
                new Enrollment{ AccountID=3, EventID=1001, Grade=Grade.F},
                new Enrollment{ AccountID=4, EventID=1001, Grade=Grade.D},
                new Enrollment{ AccountID=4, EventID=1002, Grade=Grade.C},
                new Enrollment{ AccountID=5, EventID=1003, Grade=Grade.F},
                new Enrollment{ AccountID=6, EventID=1004, Grade=Grade.B},
                new Enrollment{ AccountID=7, EventID=1005, Grade=Grade.A},
            };

            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}