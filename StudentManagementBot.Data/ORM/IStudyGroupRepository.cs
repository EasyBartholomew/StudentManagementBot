using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using StudentManagementBot.Data.ObjectModel;


namespace StudentManagementBot.Data.ORM
{
    public interface IStudyGroupRepository : IDisposable
    {
        int Count();

        Task<int> CountAsync();

        IList<IStudyGroup> GetAllStudyGroups();

        Task<IList<IStudyGroup>> GetAllStudyGroupsAsync();

        IStudyGroup Get(int id);

        IStudyGroup Get(long chatId);

        Task<IStudyGroup> GetAsync(int id);

        Task<IStudyGroup> GetAsync(long chatId);

        bool Contains(IStudyGroup group);

        bool Contains(int id);

        bool Contains(long chatId);

        void Save(IStudyGroup group);

        Task SaveAsync(IStudyGroup group);

        bool Destroy(IStudyGroup group);

        bool Destroy(int id);

        bool Destroy(long chatId);

        Task<bool> DestroyAsync(IStudyGroup group);

        Task<bool> DestroyAsync(int id);

        Task<bool> DestroyAsync(long chatId);
    }
}
