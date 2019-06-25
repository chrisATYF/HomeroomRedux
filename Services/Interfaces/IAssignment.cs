using HomeroomRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeroomRedux.Services.Interfaces
{
    public interface IAssignment
    {
        Task<Assignment> GetAsync(int id);
        Task<List<Assignment>> GetAllAssignmentsAsync();
        Task<Assignment> AddAsync(Assignment model);
        Task<Assignment> EditAsync(Assignment model);
        Task DeleteAsync(Assignment model);
    }
}