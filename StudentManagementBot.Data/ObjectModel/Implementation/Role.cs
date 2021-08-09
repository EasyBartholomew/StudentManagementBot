using System;
using NHibernate.Validator.Constraints;

// ReSharper disable VirtualMemberCallInConstructor


namespace StudentManagementBot.Data.ObjectModel.Implementation
{
    internal class Role : GroupObject, IRole
    {
        private string _name;

        [NotNull]
        [Length(1, 50, Message = "Имя роли должно содержать от одного до пятидесяти символов!")]
        public virtual string Name
        {
            get => this._name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "Value of name can't be null, empty or whitespace string!",
                        nameof(value));

                this._name = value;
            }
        }

        [NotNull]
        [Range(0, 255)]
        public virtual byte Priority { get; set; }

        
        public Role(StudyGroup group, string name, byte priority = 0) : base(group)
        {
            this.Name = name;
            this.Priority = priority;
        }

        public Role() : this(null, "Role")
        { }


        public virtual int CompareTo(IRole other)
        {
            return other != null ? this.Priority.CompareTo(other.Priority) : 1;
        }
    }
}
