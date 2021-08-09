using System;
using System.Collections.Generic;


namespace StudentManagementBot.Data.ObjectModel
{
    public interface IStudyGroup
    {
        long ChatId { get; }

        string Name { get; set; }

        IReadOnlyList<IStudent> Students { get; }

        IReadOnlyList<IRole> Roles { get; }

        IReadOnlyList<IActivity> Activities { get; }

        IStudent CreateStudent(string surname, string name, string patronymic = null);

        IRole CreateRole(string name, byte priority = 0);

        IActivity CreateActivity(string name);

        IActivity CreateActivity(string name, DateTime pivot, int count);

        bool DestroyStudent(IStudent student);
                
        bool DestroyStudent(int id);

        int DestroyAllStudents(Predicate<IStudent> predicate);

        bool DestroyRole(IRole role);
                
        int DestroyAllRoles(Predicate<IRole> predicate);

        bool DestroyActivity(IActivity activity);
                
        int DestroyAllActivities(Predicate<IActivity> predicate);

        void DestroyAllStudents();

        void DestroyAllRoles();

        void DestroyAllActivities();
    }
}
