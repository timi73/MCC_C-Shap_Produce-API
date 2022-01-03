using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        Employee Get(string Nik);
        int Insert(Employee employee);
        int Update(string Nik,Employee employee);
        int Delete(string Nik);
    }
}
