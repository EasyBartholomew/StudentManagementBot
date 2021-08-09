using FluentNHibernate.Mapping;
using StudentManagementBot.Data.ObjectModel.Implementation;


namespace StudentManagementBot.Data.ORM.Mappings
{
    internal class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Table($"{nameof(Role)}s");
            Id(r => r.Id).GeneratedBy.Identity().Not.Nullable();
            Map(r => r.Priority).Not.Nullable();
            Map(r => r.Name).Not.Nullable();
            References(s => s.Group);
        }
    }
}
