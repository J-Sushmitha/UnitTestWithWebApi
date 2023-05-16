using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestWithWebApi.Models;

namespace xUnitTest.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
            {
                new User{
                    Name = "User 1",
                    Email = "user1@examroom.ai",
                    Address = new Address{
                        City = "Bengaluru",
                        State = "Karnataka",
                        PostalCode = 560034
                    }

                },
                new User{
                    Name = "User 2",
                    Email = "user2@examroom.ai",
                    Address = new Address{
                        City = "Mysore",
                        State = "Karnataka",
                        PostalCode = 730024
                    }

                },
                new User{
                    Name = "User 3",
                    Email = "user3@examroom.ai",
                    Address = new Address{
                        City = "Kolkata",
                        State = "West Bengal",
                        PostalCode = 341042
                    }
                },
                new User{
                    Name = "User 4",
                    Email = "user4@examroom.ai",
                    Address = new Address{
                        City = "Jaipur",
                        State = "Rajasthan",
                        PostalCode = 890234
                    }
                },
                new User{
                    Name = "User 5",
                    Email = "user5@examroom.ai",
                    Address = new Address{
                        City = "Hyderabad",
                        State = "Telangana",
                        PostalCode = 234590
                    }
                }
            };

    }
}
