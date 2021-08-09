using NHibernate;


namespace StudentManagementBot.Data.ORM
{
    public static class StudyGroupRepositoryFactory
    {
        public static IStudyGroupRepository Create(ISessionFactory factory)
        {
            return new StudyGroupRepository(factory.OpenSession());
        }
    }
}
