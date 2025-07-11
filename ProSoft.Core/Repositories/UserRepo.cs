﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;

        public UserRepo(AppDbContext Context, IMapper mapper) 
        {
            _Context = Context;
            _mapper = mapper;
        }

        // ------------------- Coded By Ayman Saad ------------------- //
        public IEnumerable<SelectListItem> GetAllUsersAsEnumerable()
        {
            return _Context.Users.Select(u => new SelectListItem {Value = u.UserCode.ToString(),Text= u.UserName }).ToList();
        }
        // ------------------- Coded By Ayman Saad ------------------- //
        public async Task<List<AppUser>> GetAllUsersAsync()
        {
            List<AppUser> users = await _Context.Users.ToListAsync();
            return users;
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj=>obj.UserCode == id);
            return user;
        }

        public async Task<Branch> GetUserBranchAsync(int branchID)
        {
            Branch branch = await _Context.Branchs.FirstOrDefaultAsync(obj => obj.BranchId == branchID);
            return branch;
        }

      
    }
}
