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

        public IEnumerable<Object> UniversityCount()
        {
            var list = from edu in myContext.Educations
                       join uni in myContext.Universities on edu.UniversityId equals uni.Id
                       group uni by new { edu.UniversityId, uni.Name } into Group
                       select new
                       {
                           UniversityId = Group.Key.UniversityId,
                           UniversityName = Group.Key.Name,
                           Count = Group.Count()
                       };
            return list.ToList();
        }
    }
}
