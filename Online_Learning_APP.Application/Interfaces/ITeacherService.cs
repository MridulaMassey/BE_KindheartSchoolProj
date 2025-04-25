using Online_Learning_APP.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;
using Online_Learning_APP.Application.Interfaces;
using Online_Learning_App.Domain.Entities;
using Online_Learning_App.Domain.Interfaces;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface ITeacherService
    {
        Task<List<ClassGroupWithStudentsDto>> GetTeacherClassStudentsAsync(string teacherUsername);
    }

}