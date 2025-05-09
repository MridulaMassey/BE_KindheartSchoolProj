﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Online_Learning_App.Domain.Entities;

namespace Online_Learning_App.Domain.Interfaces
{
    public interface IClassGroupSubjectStudentActivityRepository
    {
        Task AddAsync(ClassGroupSubjectStudentActivity classgroupSubjectStudentActivity);
        Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetAllAsync();
        Task UpdateAsync(ClassGroupSubjectStudentActivity classgroupSubjectStudentActivity);
        Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetClassGroupSubjectActivityByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<ClassGroupSubjectStudentActivity> GetActivitySubjectStudentByIdAsync(Guid id, Guid studentID);

        Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetClassGroupSubjectActivityStudentByIdAsync(Guid id, Guid studentID);
        //   Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetClgActivityStudentByIdAsync(Guid id, Guid student);
        //  Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetClgActivityStudentByIdAsync(Guid id, Guid studentId);
        // Task<bool> GetClgActivityStudentByIdAsync(Guid id, Guid student);
        // Task<bool> UpdateAsync(ClassGroupSubjectStudentActivity classgroupSubjectStudentActivity);
        Task<IEnumerable<ClassGroupSubjectStudentActivity>> GetClgActivityStudentByIdAsync(Guid id, Guid studentID);
        Task<bool> UpdateISProcessedAsync(ClassGroupSubjectStudentActivity classgroupSubjectStudentActivity);

    }
}
