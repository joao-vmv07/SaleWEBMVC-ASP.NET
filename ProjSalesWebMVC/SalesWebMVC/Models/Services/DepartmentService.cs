using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMVC.Data;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Models.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _salesWebMVCContext;

        public DepartmentService(SalesWebMVCContext salesWebMVC)
        {
            _salesWebMVCContext = salesWebMVC;

        }
        public async Task<List<Department>> FindAllAsync()
        {
            return await _salesWebMVCContext.Department.OrderBy(p => p.Name).ToListAsync();
        }
    }
}
