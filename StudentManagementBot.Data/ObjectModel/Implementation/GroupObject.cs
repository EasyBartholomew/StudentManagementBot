using NHibernate.Validator.Constraints;

// ReSharper disable VirtualMemberCallInConstructor



namespace StudentManagementBot.Data.ObjectModel.Implementation
{
    internal abstract class GroupObject : ObjectModel, IGroupObject
    {
        public virtual StudyGroup Group { get; set; }

        IStudyGroup IGroupObject.Group => this.Group;

        protected GroupObject(StudyGroup group)
        {
            this.Group = group;
        }
    }
}
