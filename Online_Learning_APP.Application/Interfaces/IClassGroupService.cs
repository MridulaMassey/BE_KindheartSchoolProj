using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_APP.Application.DTO;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_APP.Application.Interfaces
{
    public interface IClassGroupService
    {
        Task<ClassGroup> CreateClassGroupAsync(ClassGroupCreateDto classGroupDto);
        Task<List<ClassGroup>> GetAllClassGroupsAsync();
        Task<IEnumerable<StudentDto>> GetStudentsByClassGroupIdAsync(Guid classGroupId);
        Task<bool> AssignTeacherToClassGroupAsync(Guid classGroupId, Guid teacherId);
        Task<TeacherLoadDto> GetTeacherLoadAsync(Guid teacherId);

    }
}
