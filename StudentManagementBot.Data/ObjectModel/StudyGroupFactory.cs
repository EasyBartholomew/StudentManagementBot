using StudentManagementBot.Data.ObjectModel.Implementation;


namespace StudentManagementBot.Data.ObjectModel
{
    public static class StudyGroupFactory
    {
        public static IStudyGroup Create(long chatId, string name = null)
        {
            return new StudyGroup(chatId, name);
        }
    }
}
