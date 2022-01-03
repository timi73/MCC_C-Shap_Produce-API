using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EducationRepository : GeneralRepository<MyContext, Education, string>
    {
        public EducationRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
