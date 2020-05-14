using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationNetLab.Models;

namespace WebApplicationNetLab.DAL
{
    public class EventorerInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EventorerContext>
    {
        delegate void PopulateAccount(string email, string nickname, string creationTime);
        delegate void PopulateEvent(int id, string title, int credits);
        delegate void PopulateEnrollments(int accountId, int eventId, Grade grade);
        protected override void Seed(EventorerContext context)
        {
            var accounts = new List<Account>();

            PopulationAccountData populationAccount = new PopulationAccountData(accounts);
            PopulateAccount pushAccount = populationAccount.PopulateAccount;
            pushAccount("loster@electromail.com", "Loster", "2005-09-01");
            pushAccount("comodo@electromail.com", "Comodo", "2010-10-01");
            pushAccount("kineret@electromail.com", "Kineret", "2016-07-01");
            pushAccount("ashtorer@electromail.com", "Astorer", "2017-09-01");
            pushAccount("compor@electromail.com", "Compor", "2015-04-01");
            pushAccount("indier@electromail.com", "Indier", "2019-12-01");
            pushAccount("poster@electromail.com", "Poster", "2018-09-01");
            pushAccount("fostor@electromail.com", "Fostor", "2009-09-01");

            accounts.ForEach(s => context.Accounts.Add(s));
            context.SaveChanges();

            var events = new List<Event>();

            PopulationEventData populationEvent = new PopulationEventData(events);
            PopulateEvent pushEvent = populationEvent.PopulateEvent;
            pushEvent(1001, "Summer event 1001", 5);
            pushEvent(1002, "Winter event 1002", 5);
            pushEvent(1003, "Fall event 1003", 5);
            pushEvent(1004, "Spring event 1004", 5);
            pushEvent(1005, "Fall event 1005", 5);
            pushEvent(1006, "Lobster event 1006", 5);
            pushEvent(1007, "Kinetic event 1007", 5);

            events.ForEach(s => context.Events.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>();

            PopulationEnrollmentData populationEnrollment = new PopulationEnrollmentData(enrollments);
            PopulateEnrollments pushEnrollment = populationEnrollment.PopulateEnrollment;
            pushEnrollment(1, 1001, Grade.A);
            pushEnrollment(1, 1002, Grade.B);
            pushEnrollment(1, 1003, Grade.C);
            pushEnrollment(2, 1004, Grade.A);
            pushEnrollment(2, 1005, Grade.B);
            pushEnrollment(2, 1006, Grade.C);
            pushEnrollment(3, 1001, Grade.F);
            pushEnrollment(4, 1001, Grade.D);
            pushEnrollment(4, 1002, Grade.C);
            pushEnrollment(5, 1003, Grade.F);
            pushEnrollment(6, 1004, Grade.B);
            pushEnrollment(7, 1005, Grade.A);

            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }

    class PopulationAccountData
    {
        public List<Account> Accounts { get; set; }

        public PopulationAccountData(List<Account> accounts)
        {
            Accounts = accounts;
        }
        public void PopulateAccount(string email, string nickname, string creationTime)
        {
            Accounts.Add(new Account() { Email = email, Nickname = nickname, CreationDate = DateTime.Parse(creationTime) });
        }
    }

    class PopulationEventData
    {
        public List<Event> Events { get; set; }
        public PopulationEventData(List<Event> events)
        {
            Events = events;
        }
        public void PopulateEvent(int id, string title, int credits)
        {
            Events.Add(new Event() { EventID = id, Title = title, Credits = credits });
        }
    }

    class PopulationEnrollmentData
    {
        public List<Enrollment> Enrollments { get; set; }
        public PopulationEnrollmentData(List<Enrollment> enrollments)
        {
            Enrollments = enrollments;
        }
        public void PopulateEnrollment(int accountId, int eventId, Grade grade)
        {
            Enrollments.Add(new Enrollment() { AccountID = accountId, EventID = eventId, Grade = grade });
        }
    }
}