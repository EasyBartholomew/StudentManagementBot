using FluentNHibernate.Mapping;
using StudentManagementBot.Data.ObjectModel.Implementation;


namespace StudentManagementBot.Data.ORM.Mappings
{
    internal class ActivityActionMap : ClassMap<ActivityAction>
    {
        public ActivityActionMap()
        {
            Table($"{nameof(ActivityAction)}s");
            Id(a => a.Id).GeneratedBy.Identity().Not.Nullable();
            Map(a => a.Date).Not.Nullable();
            Map(a => a.Remark).Nullable();
            References(a => a.Activity);
            HasManyToMany(a => a.Students)
                .Cascade.None()
                .Table($"{nameof(Student)}s_{nameof(ActivityAction)}s");
        }
    }
}
