using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;

// ReSharper disable VirtualMemberCallInConstructor


namespace StudentManagementBot.Data.ObjectModel.Implementation
{
    internal class ActivityAction : ObjectModel, IActivityAction
    {
        public virtual DateTime Date { get; set; }
        
        [Length(1, 500, Message = "Заметка должна содержать от одного до пятисот символов!")]
        public virtual string Remark { get; set; }
        
        public virtual Activity Activity { get; set; }
        
        [NotNull]
        public virtual IList<Student> Students { get; set; }

        IReadOnlyList<IStudent> IActivityAction.Students =>
            this.Students.AsEnumerable().ToList().AsReadOnly();

        IActivity IActivityAction.Activity => this.Activity;


        public ActivityAction(Activity activity, DateTime date, string remark = null)
        {
            this.Activity = activity;
            this.Date = date;
            this.Remark = remark;
            this.Students = new List<Student>();
        }

        public ActivityAction() : this(null, DateTime.MinValue)
        { }


        public virtual int CompareTo(IActivityAction other)
        {
            return other != null ? this.Date.CompareTo(other.Date) : 1;
        }

        public virtual void AddStudent(IStudent student)
        {
            if (student is Student s)
            {
                if (this.Activity.Group != s.Group)
                    throw new InvalidOperationException();

                this.Students.Add(s);
            }
            else
                throw new ObjectModelException();
        }

        public virtual void AddStudents(IEnumerable<IStudent> students)
        {
            if (students == null)
                throw new ArgumentNullException();

            try
            {
                var studentsImpl = students.Cast<Student>();

                if (studentsImpl.Any(s => s.Group != this.Activity.Group))
                    throw new InvalidOperationException();

                this.Students.AsEnumerable().ToList().AddRange(studentsImpl);
            }
            catch (InvalidCastException ex)
            {
                throw new ObjectModelException("", ex);
            }
        }

        public virtual bool RemoveStudent(IStudent student)
        {
            if (student is Student s)
            {
                if (this.Activity.Group != s.Group)
                    throw new InvalidOperationException();

                return this.Students.Remove(s);
            }

            throw new ObjectModelException();
        }

        public virtual int RemoveAllStudents(Predicate<IStudent> predicate)
        {
            return this.Students
                .AsEnumerable()
                .ToList()
                .RemoveAll(predicate);
        }

        public virtual int RemoveAllStudents()
        {
            var result = this.Students.Count;
            this.Students.Clear();

            return result;
        }
    }
}
