﻿using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Models.Services
{
    public class SaleRecordsServices
    {
        private readonly SalesWebMVCContext _context;
    
        public SaleRecordsServices(SalesWebMVCContext context)
        {
            _context = context;
        }
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj; //Selecionar obj no BD (IQueryable)
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate);
            }
            return await result.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();
        }
        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj; //Selecionar obj no BD (IQueryable)
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate);
            }
            return await result.Include(x => x.Seller).Include(x => x.Seller.Department).
                OrderByDescending(x => x.Date).GroupBy(x => x.Seller.Department)
                .ToListAsync();
        }
        
    }
    
}

