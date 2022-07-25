using Autofac.Extras.Moq;
using idTicketsInfrastructure.Models;
using idTicketsInfrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idTicketsInfrastructure.Test
{
    public class TicketsRepository_Test
    {

        //static Mock<TicketsDbContext> ticketsMockDbContext = new Mock<TicketsDbContext>();
        private TicketsRepository _sut;
        private List<Ticket> expected;
        private Mock<DbSet<Ticket>> mockSet;
        private Mock<TicketsDbContext> mockContext;
        private Ticket nonExistingTicket = new Ticket() { ticketId = 5, description = "Non-existent ticket", ticketStatus = "UNKNOWN" };


        public TicketsRepository_Test() {
            expected = sampleTickets();
            var expectedAsQueryable = expected.AsQueryable();

            // this should be in a constructor somewhere in this class
            mockSet = new Mock<DbSet<Ticket>>();
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(expectedAsQueryable.Provider);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(expectedAsQueryable.Expression);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(expectedAsQueryable.ElementType);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(expectedAsQueryable.GetEnumerator());

            mockContext = new Mock<TicketsDbContext>();
            mockContext.Setup(x => x.tickets).Returns(mockSet.Object);
            _sut = new TicketsRepository(mockContext.Object);
        }

        [Fact]
        public void getAllTickets_Test()
        {

            var actual = _sut.getAll();
            //ticketsMockDbContext.Setup(x => x.tickets.ToList()).Returns(sampleTickets().ToList());
            Assert.True(actual != null);
            Assert.Equal(sampleTickets().Count, actual.Count);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]

        public void getTicketById_Test_ReturnObject(int id)
        {
            Ticket actualFirstTicket = _sut.getById(id);
            Assert.True(actualFirstTicket != null);

        }
        [Fact]
        public void getTicketById_TestReturnValue()
        {
            Ticket actualFirstTicket = _sut.getById(1);
            Assert.Equal(expected.First().ticketId, actualFirstTicket.ticketId);

            // also try getting a ticket that doesn't really exist
            Ticket nonExistentTicket = _sut.getById(10);
            Assert.Equal(-1, nonExistentTicket.ticketId);
        }

        [Fact]
        public void addTicket_Test()
        {
            // add a new ticket
            bool addSuccessful = _sut.addEntry(new Ticket { ticketId = 3, description = "New Urgent Tickets", ticketStatus = "Reopened" });
           // mockSet.Verify(m => m.Add(It.IsAny<Ticket>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.True(addSuccessful);

            addSuccessful = _sut.addEntry(null);
            Assert.False(addSuccessful);

            addSuccessful = _sut.addEntry(sampleTickets().First());
            Assert.True(addSuccessful);


        }

        // try with multiple scenarios

        [Fact]
        public void removeTicket_Test()
        {
            // remove existing item
            Ticket targetItemExisting = sampleTickets().First();
            bool removeSuccess = _sut.deleteEntry(targetItemExisting);
            
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
            Assert.True(removeSuccess);
            //Assert.Equal(sampleTickets().Count - 1, _sut.getAll().Count);

            /*
            // remove non-existing item
            Ticket targetItemNonExisting = new Ticket() { ticketId = 5, description = "Non-existent ticket", ticketStatus = "UNKNOWN" };
            removeSuccess = _sut.deleteEntry(targetItemNonExisting);
            Assert.False(removeSuccess);
            */
            // remove null item
            removeSuccess = _sut.deleteEntry(null);
            Assert.False(removeSuccess);

        }

        [Fact]
        public void removeTicket_Test_Non_Existent()
        {
            //Ticket targetItemNonExisting = new Ticket() { ticketId = 5, description = "Non-existent ticket", ticketStatus = "UNKNOWN" };
            bool removeSuccess = _sut.deleteEntry(nonExistingTicket);
       
            Assert.False(removeSuccess);
        }

        [Fact]
        public void updateTicket_Test()
        {
            // try updating an existing item
            Ticket ticketToUpdate = sampleTickets().First();
            ticketToUpdate.description = "New details have been added";
            ticketToUpdate.ticketStatus = "IN_PROGRESS";

            bool updateSuccess = _sut.updateEntry(ticketToUpdate);
            Assert.True(updateSuccess);
            Assert.Equal(sampleTickets().Count, _sut.getAll().Count);
            
            // then try updating an item that doesn't exist -- don't create a new item and insert into the database
            updateSuccess = _sut.updateEntry(nonExistingTicket);
            Assert.False(updateSuccess);

            // how about the null case
            Assert.False(_sut.updateEntry(null));

        }

        // private method to generate sample tickets

        private List<Ticket> sampleTickets()
        {
            List<Ticket> samples = new List<Ticket>();
            samples.Add(new Ticket { ticketId = 1, description = "First Test Ticket", ticketStatus = "New" });
            samples.Add(new Ticket { ticketId = 2, description = "High Priority Tickets", ticketStatus = "Active" });

            return samples; 
        }

        ~TicketsRepository_Test() { }

    }
}
