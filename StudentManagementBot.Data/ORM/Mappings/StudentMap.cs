using FluentNHibernate.Mapping;
using StudentManagementBot.Data.ObjectModel.Implementation;


namespace StudentManagementBot.Data.ORM.Mappings
{
    internal class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Table($"{nameof(Student)}s");
            Id(s => s.Id).GeneratedBy.Identity().Not.Nullable();
            Map(s => s.UserId).Nullable();
            Map(s => s.Surname).Not.Nullable();
            Map(s => s.Name).Not.Nullable();
            Map(s => s.Patronymic).Nullable();
            References(s => s.Group);
            HasManyToMany(s => s.Roles).Cascade.None()
                .Table($"{nameof(Student)}s_{nameof(Role)}s");
        }
    }
}
