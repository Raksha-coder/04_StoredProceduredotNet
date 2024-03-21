using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTO;
using WebApplication1.Entities;

namespace WebApplication1.context
{
    public class PersonContext:DbContext
    {

        public PersonContext(DbContextOptions options) : base(options)
        {

        }


        //I have make a method here because , i was not able to use use await in controller
        //parameters are important 
        //and because of getAwaiter , here the return type is int
        public async Task<int> InsertDataAsync(personDTO model)
        {
            var lastname = new SqlParameter("@Param1", model.Lastname);
            var firstname = new SqlParameter("@Param2", model.Firstname);
            var city = new SqlParameter("@Param3", model.City);

            return await Database.ExecuteSqlRawAsync("EXEC spInsertValues @Param1, @Param2,@Param3", lastname, firstname,city);
        }

        public async Task<int> DeleteDataAsync(int id)
        {
            var Id = new SqlParameter("@Param", id);

            return await Database.ExecuteSqlRawAsync("EXEC spDeletePersonById @Param", Id);
        }

        public async Task<int> UpdateFirstnameAsync(int id,string updatedFirstName)
        {
            var Id = new SqlParameter("@Param1", id);
            var name = new SqlParameter("@param2", updatedFirstName);
            return await Database.ExecuteSqlRawAsync("EXEC spUpdatePersonFirstname @Param1,@param2",Id, name);
        }




        public DbSet<Person> Persons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(builder =>
                        {

                            builder.HasKey(p => p.Id);
                        }

            ) ; 
        }

        }
}
