using FluentNHibernate.Mapping;
using StudentManagementBot.Data.ObjectModel.Implementation;


namespace StudentManagementBot.Data.ORM.Mappings
{
    internal class ActivityMap : ClassMap<Activity>
    {
        public ActivityMap()
        {
            Table("Activities");
            Id(a => a.Id).GeneratedBy.Identity().Not.Nullable();
            Map(a => a.Name).Not.Nullable();
            References(s => s.Group);
            HasMany(a => a.Actions).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
