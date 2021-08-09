using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using StudentManagementBot.Data.ObjectModel.Implementation.Helpers;

// ReSharper disable VirtualMemberCallInConstructor


namespace StudentManagementBot.Data.ObjectModel.Implementation
{
    internal class Student : GroupObject, IStudent
    {
        private string _surname;
        private string _name;
        private string _patronymic;

        
        [NotNull]
        [Length(2, 50, Message = "Фамилия должна содержать от двух до пятидесяти символов!")]
        public virtual string Surname
        {
            get => this._surname;

            set => _surname = value.FormatPersonalData();
        }

        [NotNull]
        [Length(2, 50, Message = "Имя должно содержать от двух до пятидесяти символов!")]
        public virtual string Name
        {
            get => this._name;

            set => this._name = value.FormatPersonalData();
        }
                
        [Length(2, 50, Message = "Отчество должно содержать от двух до пятидесяти символов!")]
        public virtual string Patronymic
        {
            get => this._patronymic;

            set => this._patronymic = value?.FormatPersonalData();
        }
        
        public virtual int? UserId { get; set; }
         
        [NotNull]
        [Size(0, 10, Message = "Студент не может иметь более 10 ролей!")]
        public virtual IList<Role> Roles { get; set; }

        IReadOnlyList<IRole> IStudent.Roles => this.Roles.AsEnumerable().ToList();
        
        public Student(StudyGroup group, string surname, string name, string patronymic = null) : base(group)
        {

            this.Surname = surname;
            this.Name = name;
            this.Patronymic = patronymic;
            this.UserId = null;
            this.Roles = new List<Role>();
        }

        public Student() : this(null, "Фамилия студента", "Имя студента")
        { }


        public virtual bool AddRole(IRole studentRole)
        {
            switch (studentRole)
            {
                case null:
                    throw new ArgumentNullException(nameof(studentRole));
                case Role role when this.Group != role.Group:
                    throw new InvalidOperationException();
                case Role role when this.Roles.Any(r => r == role):
                    return false;
                case Role role:
                    this.Roles.Add(role);

                    return true;
                default:
                    throw new ObjectModelException();
            }
        }

        public virtual bool RemoveRole(IRole studentRole)
        {
            switch (studentRole)
            {
                case null:
                    throw new ArgumentNullException(nameof(studentRole));
                case Role role when this.Group != role.Group:
                    throw new InvalidOperationException();
                case Role role:
                    return this.Roles.Remove(role);
                default:
                    throw new ObjectModelException();
            }
        }

        public virtual void RemoveAllRoles()
        {
            this.Roles.Clear();
        }

        public override string ToString()
        {
            return $"{Surname} {Name}";
        }

        public virtual bool RecognizeSelf(string personalData)
        {
            if (personalData == null)
                return false;

            var result = false;

            var splitData = personalData.Split(
                new char[] { ' ', '\t', '\n' },
                StringSplitOptions.RemoveEmptyEntries);


            switch (splitData.Length)
            {
                case 1:

                    if (Int32.TryParse(splitData.First().Trim(), out int id))
                    {
                        result = (this.Id == id) || (this.UserId == id);
                    }
                    else
                    {
                        result = (this.Surname == splitData.First().FormatPersonalData())
                        || (this.Name == splitData.First().FormatPersonalData());
                    }
                    break;

                case 2:
                    result = splitData.Any(d => d.FormatPersonalData() == this.Name)
                        && splitData.Any(d => d.FormatPersonalData() == this.Surname);
                    break;

                case 3:
                    result = splitData.Any(d => d.FormatPersonalData() == this.Name)
                        && splitData.Any(d => d.FormatPersonalData() == this.Surname)
                        && splitData.Any(d => d.FormatPersonalData() == this.Patronymic);
                    break;

                default:
                    break;
            }

            return result;
        }
    }
}