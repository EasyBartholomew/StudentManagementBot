using FluentNHibernate.Mapping;
using StudentManagementBot.Data.ObjectModel.Implementation;


namespace StudentManagementBot.Data.ORM.Mappings
{
    internal class StudyGroupMap : ClassMap<StudyGroup>
    {
        public StudyGroupMap()
        {
            Table($"{nameof(StudyGroup)}s");
            Id(g => g.Id).GeneratedBy.Identity().Not.Nullable();
            Map(g => g.ChatId).Unique().Not.Nullable();
            Map(g => g.Name).Nullable();
            HasMany(g => g.Students).Inverse().Cascade.AllDeleteOrphan();
            HasMany(g => g.Activities).Inverse().Cascade.AllDeleteOrphan();
            HasMany(g => g.Roles).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
