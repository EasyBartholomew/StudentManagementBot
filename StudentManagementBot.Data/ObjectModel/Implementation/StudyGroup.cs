using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;

// ReSharper disable VirtualMemberCallInConstructor


namespace StudentManagementBot.Data.ObjectModel.Implementation
{
    internal class StudyGroup : ObjectModel, IStudyGroup
    {
        [NotNull]
        public virtual long ChatId { get; set; }

        [Length(1, 50, Message = "Имя группы должно составлять от одного до пятидесяти символов!")]
        public virtual string Name { get; set; }

        [NotNull]
        [Size(0, 50, Message = "Группа не может содержать более пятидесяти студентов!")]
        public virtual IList<Student> Students { get; set; }

        [NotNull]
        [Size(0, 10, Message = "Группа не может содержать более 10 ролей!")]
        public virtual IList<Role> Roles { get; set; }

        [NotNull]
        [Size(0, 50, Message = "Группа не может содержать более 50 активностей!")]
        public virtual IList<Activity> Activities { get; set; }

        IReadOnlyList<IStudent> IStudyGroup.Students =>
            this.Students.AsEnumerable().ToList().AsReadOnly();

        IReadOnlyList<IRole> IStudyGroup.Roles =>
            this.Roles.AsEnumerable().ToList().AsReadOnly();

        IReadOnlyList<IActivity> IStudyGroup.Activities =>
            this.Activities.AsEnumerable().ToList().AsReadOnly();


        public StudyGroup(long chatId, string name = null)
        {
            this.ChatId = chatId;
            this.Name = name;

            this.Students = new List<Student>();
            this.Roles = new List<Role>();
            this.Activities = new List<Activity>();
        }

        public StudyGroup() : this(0)
        { }


        public virtual IActivity CreateActivity(string name)
        {
            var activity = new Activity(this, name);
            this.Activities.Add(activity);

            return activity;
        }

        public virtual IActivity CreateActivity(string name, DateTime pivot, int count)
        {
            var activity = this.CreateActivity(name);

            for (var i = 0; i < count; i++)
            {
                activity.CreateAction(pivot);
                pivot += TimeSpan.FromDays(7);
            }

            return activity;
        }

        public virtual IRole CreateRole(string name, byte priority = 0)
        {
            var role = new Role(this, name, priority);
            this.Roles.Add(role);

            return role;
        }

        public virtual IStudent CreateStudent(string surname, string name, string patronymic = null)
        {
            var student = new Student(this, surname, name, patronymic);
            this.Students.Add(student);

            return student;
        }

        public virtual bool DestroyActivity(IActivity activity)
        {
            if (activity == null)
                throw new ArgumentNullException(nameof(activity));

            if (this != activity.Group)
                throw new ArgumentException();

            if (activity is Activity a)
            {
                a.Group = null;
                a.DestroyAllActions();
                return this.Activities.Remove(a);
            }

            throw new ObjectModelException();
        }

        public virtual int DestroyAllActivities(Predicate<IActivity> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var activities = this.Activities
                .Where(a => predicate(a));

            var result = this.Activities.ToList().RemoveAll(predicate);

            foreach (var activity in activities)
            {
                activity.Group = null;
                activity.DestroyAllActions();
            }

            return result;
        }

        public virtual void DestroyAllActivities()
        {
            foreach (var activity in this.Activities)
            {
                activity.Group = null;
                activity.DestroyAllActions();
            }

            this.Activities.Clear();
        }

        public virtual bool DestroyRole(IRole role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            if (role is Role r)
            {
                foreach (var student in this.Students)
                {
                    student.RemoveRole(r);
                }

                r.Group = null;
                return this.Roles.Remove(r);
            }

            throw new ObjectModelException();
        }

        public virtual int DestroyAllRoles(Predicate<IRole> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var roles = this.Roles
                .Where(r => predicate(r));
            
            foreach (var student in this.Students)
                student.Roles.ToList().RemoveAll(predicate);
            
            return this.Roles.AsEnumerable().ToList().RemoveAll(predicate);
        }

        public virtual void DestroyAllRoles()
        {
            foreach (var student in this.Students)
                student.RemoveAllRoles();
            
            this.Roles.Clear();
        }

        public virtual bool DestroyStudent(IStudent student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            if (student is Student s)
            {
                var actions = this.Activities
                    .SelectMany(a => a.Actions)
                    .Where(a => a.Students.Contains(s));

                foreach (var action in actions)
                    action.Students.Remove(s);

                student.RemoveAllRoles();

                var result = this.Students.Remove(s);
                s.Group = null;
                
                return result;
            }

            throw new ObjectModelException();
        }

        public virtual bool DestroyStudent(int id)
        {
            var student = this.Students.SingleOrDefault(s => s.Id == id);

            return this.DestroyStudent(student);
        }


        public virtual int DestroyAllStudents(Predicate<IStudent> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var students = this.Students
                .Where(s => predicate(s));

            return students.Count(this.DestroyStudent);
        }

        public virtual void DestroyAllStudents()
        {
            while (this.Students.Count != 0)
                this.DestroyStudent(this.Students.First());
        }
    }
}
