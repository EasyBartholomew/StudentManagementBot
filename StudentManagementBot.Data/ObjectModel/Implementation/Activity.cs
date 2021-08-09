using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;

// ReSharper disable VirtualMemberCallInConstructor


namespace StudentManagementBot.Data.ObjectModel.Implementation
{
    internal class Activity : GroupObject, IActivity
    {
        private string _name;

        [NotNull]
        [Length(1, 50, Message = "Имя активности должно содержать от одного до пятидесяти символов!")]
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
        [Size(0, 100, Message = "Активность должна содержать от 0 до 100 действий!")]
        public virtual IList<ActivityAction> Actions { get; set; }

        IReadOnlyList<IActivityAction> IActivity.Actions =>
            this.Actions.AsEnumerable().ToList().AsReadOnly();

        public virtual bool Empty => this.Actions.Count == 0;

        public virtual bool Single => this.Actions.Count == 1;


        public Activity(StudyGroup group, string name) : base(group)
        {
            this.Name = name;
            this.Actions = new List<ActivityAction>();
        }

        public Activity() : this(null, nameof(Activity))
        { }


        public virtual IActivityAction CreateAction(DateTime date, string remark = null)
        {
            if (this.Actions.Count(a => a.Date == date) != 0)
                throw new InvalidOperationException();

            var action = new ActivityAction(this, date, remark);
            this.Actions.Add(action);

            return action;
        }

        public virtual bool DestroyAction(IActivityAction action)
        {
            if (action is ActivityAction a)
            {
                a.RemoveAllStudents();
                a.Activity = null;
                return this.Actions.Remove(a);
            }

            throw new ObjectModelException();
        }

        public virtual int DestroyAllActions(Predicate<IActivityAction> predicate)
        {
            return this.Actions
                 .Where(a => predicate(a))
                 .Count(this.DestroyAction);
        }

        public virtual void DestroyAllActions()
        {
            while (this.Actions.Count != 0)
                this.DestroyAction(this.Actions.First());
        }
    }
}
