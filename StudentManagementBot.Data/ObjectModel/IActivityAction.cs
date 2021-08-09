using System;
using System.Collections.Generic;


namespace StudentManagementBot.Data.ObjectModel
{
    public interface IActivityAction : IComparable<IActivityAction>
    {
        DateTime Date { get; }

        string Remark { get; set; }        

        IActivity Activity { get; }

        IReadOnlyList<IStudent> Students { get; }

        void AddStudent(IStudent student);

        void AddStudents(IEnumerable<IStudent> students);

        bool RemoveStudent(IStudent student);

        int RemoveAllStudents(Predicate<IStudent> predicate);

        int RemoveAllStudents();
    }
}
