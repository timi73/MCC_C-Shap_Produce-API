using API.Context;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, string>
    {
        private readonly MyContext myContext;
        public UniversityRepository(MyContext myContext) : base(myContext) {
            this.myContext = myContext;
        }

        public int Check(University university) {
            var name = myContext.Universities.Where(u => u.Name == university.Name).FirstOrDefault();
            if (name != null) {
                return 1;
            }
            return 0;
        }
    }
}
