using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class UserTransRepo: IUserTransRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public UserTransRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<UserTransDTO> GetUserTransIndexAsync()
        {
            UserTransDTO userTransDTO = new ();
            List<AppUser> users = await _Context.Users.ToListAsync();
            List<GeneralCode> permissions = await _Context.GeneralCodes.ToListAsync();
            return userTransDTO;
        }
    }
}
