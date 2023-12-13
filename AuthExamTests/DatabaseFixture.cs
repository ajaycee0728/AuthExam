using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthExam;
using AuthExam.Data;

namespace AuthExamTests
{
    public class DatabaseFixture : IDisposable
    {
        public DataContext Context { get; }

        public DatabaseFixture()
        {
            // Initialize your DbContext
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            Context = new DataContext(options);
        }

        public void Dispose()
        {
            // Cleanup or dispose of any resources if needed
            Context.Dispose();
        }
    }
}
