using Bogus;
using idTicketsInfrastructure.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Test
{
    public class DataGenerator
    {
        public static List<Ticket> sampleTickets { get; }
        public static Ticket sampleExtraTicket { get; }
        public static User sampleExtraUser { get; }
        public static Comment sampleExtraComment { get; }



        public static List<User> sampleUsers { get; }
        public static List<Comment> sampleComments { get; }

        private static Random _randomizerSeed { get; }

        private static Faker<Ticket> fakeTicketItem;
        private static Faker<User> fakeUserItem;
        private static Faker<Comment> fakeCommentItem;


        static DataGenerator()
        {
            _randomizerSeed = new Random(123);
            
            generateTickets();
            generateUsers();
            generateComments();
            sampleTickets = fakeTicketItem.GenerateBetween(10, 150);
            sampleExtraTicket = fakeTicketItem.Generate();
            

            sampleUsers = fakeUserItem.Generate(50);
            sampleExtraUser = fakeUserItem.Generate();
            sampleExtraComment = fakeCommentItem.Generate();

            sampleComments = fakeCommentItem.GenerateBetween(1, 100);
        }

        private static void generateTickets()
        {
            // set up the rules for ticket items
            Randomizer.Seed = _randomizerSeed;
            fakeTicketItem = new Faker<Ticket>()
                //.RuleFor(t => t.id, f => f.Random.Int(1, 1000))
                .RuleFor(t => t.title, f => f.Lorem.Sentence(10))
                .RuleFor(t => t.details, f => f.Lorem.Paragraph(1))
                .RuleFor(t => t.creationDate, f => f.Date.Recent())
                .RuleFor(t => t.updateDate, f => f.Date.Recent().AddDays(2))
                .RuleFor(t => t.requestorId, f => f.Random.Int(1, 30))
                .RuleFor(t => t.assigneeId, f => f.Random.Int(0, 30))
                .RuleFor(t => t.priority, f => f.PickRandom(new List<string>() { "VERY LOW", "LOW", "MEDIUM", "HIGH", "VERY HIGH" }.AsEnumerable()))
                .RuleFor(t => t.status, f => f.PickRandom(new List<string>() { "ISSUED", "IN PROGRESS", "IN REVIEW", "RESOLVED", "ARCHIVED" }.AsEnumerable()));
        }

        private static void generateUsers()
        {
            Randomizer.Seed = _randomizerSeed;
            fakeUserItem = new Faker<User>()
                .RuleFor(u => u.firstName, f => f.Name.FirstName())
                .RuleFor(u => u.lastName, f => f.Name.LastName())
                .RuleFor(u => u.email, (f, u) => f.Internet.ExampleEmail(u.firstName, u.lastName))
                .RuleFor(u => u.creationDate, (f) => f.Date.Past(5))
                .RuleFor(u => u.updateDate, (f) => f.Date.Recent())
                .RuleFor(u => u.departmentId, (f) => f.Random.Int(1, 4))
                .RuleFor(u => u.isITStaff, (f) => f.Random.Bool())
                .RuleFor(u => u.isSupervisor, (f) => f.Random.Bool())
            ;

        }

        private static void generateComments()
        {
            Randomizer.Seed = _randomizerSeed;
            fakeCommentItem = new Faker<Comment>()
                .RuleFor(c => c.ticketId, f => f.Random.Int(1, 20))
                .RuleFor(c => c.userId, f => f.Random.Int(1, 15))
                .RuleFor(c => c.textContent, f => f.Lorem.Text())
                .RuleFor(t => t.creationDate, f => f.Date.Recent())
                ;
        }

    }
}
