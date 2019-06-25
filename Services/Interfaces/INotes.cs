using HomeroomRedux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeroomRedux.Services.Interfaces
{
    public interface INotes
    {
        Task<Note> GetAsync(int id);
        Task<List<Note>> GetAllNotesAsync();
        Task<Note> AddAsync(string aspNetId, Note model);
        Task<Note> EditAsync(Note model);
        Task DeleteAsync(Note model);
    }
}