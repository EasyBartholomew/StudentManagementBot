using NHibernate.Validator.Constraints;


namespace StudentManagementBot.Data.ObjectModel.Implementation
{
    internal abstract class ObjectModel
    {
        [NotNull]
        public virtual int Id { get; set; }
    }
}
