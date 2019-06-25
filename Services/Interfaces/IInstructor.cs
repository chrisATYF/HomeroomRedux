using HomeroomRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeroomRedux.Services.Interfaces
{
    public interface IInstructor
    {
        Task<Instructor> GetAsync(int instructorId);
        Task AddAboutAsync(Instructor model);
    }
}