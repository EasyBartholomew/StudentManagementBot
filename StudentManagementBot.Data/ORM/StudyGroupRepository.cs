using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using NHibernate;

using StudentManagementBot.Data.ObjectModel;
using StudentManagementBot.Data.ObjectModel.Implementation;


namespace StudentManagementBot.Data.ORM
{
    internal class StudyGroupRepository : IStudyGroupRepository
    {
        protected ISession Session { get; }

        public int Count() => this.Session
            .CreateCriteria<StudyGroup>()
            .List<StudyGroup>().Count;

        public async Task<int> CountAsync() =>
            (await this.Session
            .CreateCriteria<StudyGroup>()
            .ListAsync<StudyGroup>())
            .Count;

        public StudyGroupRepository(ISession session)
        {
            this.Session = session ??
                           throw new ArgumentNullException(nameof(session));
        }

        public void Dispose()
        {
            this.Session.Dispose();
        }

        public IList<IStudyGroup> GetAllStudyGroups() =>
            this.Session.CreateCriteria<StudyGroup>()
                .List<StudyGroup>()
                .Cast<IStudyGroup>()
                .ToList();

        public async Task<IList<IStudyGroup>> GetAllStudyGroupsAsync() =>
            (await this.Session.CreateCriteria<StudyGroup>()
                .ListAsync<StudyGroup>())
                .Cast<IStudyGroup>()
                .ToList();

        public IStudyGroup Get(int id)
        {
            return this.Session.Get<StudyGroup>(id);
        }

        public IStudyGroup Get(long chatId)
        {
            return this.Session
                .CreateCriteria<StudyGroup>()
                .List<StudyGroup>()
                .SingleOrDefault(g => g.ChatId == chatId);
        }

        public async Task<IStudyGroup> GetAsync(int id)
        {
            return await this.Session.GetAsync<StudyGroup>(id);
        }

        public async Task<IStudyGroup> GetAsync(long chatId)
        {
            var groups = await this.Session
                .CreateCriteria<StudyGroup>()
                .ListAsync<StudyGroup>();

            return groups.SingleOrDefault(g => g.ChatId == chatId);
        }

        public bool Contains(IStudyGroup group)
        {
            return this.Session.Contains(group);
        }

        public bool Contains(int id)
        {
            return this.Session.Get<StudyGroup>(id) != null;
        }

        public bool Contains(long chatId)
        {
            return this.Session.CreateCriteria<StudyGroup>()
                .List<StudyGroup>()
                .FirstOrDefault(g => g.ChatId == chatId) != null;
        }

        public void Save(IStudyGroup group)
        {
            if (group == null)
                throw new ArgumentNullException();

            using (var trx = this.Session.BeginTransaction())
            {
                this.Session.SaveOrUpdate(group);
                trx.Commit();
            }
        }

        public async Task SaveAsync(IStudyGroup group)
        {
            if (group == null)
                throw new ArgumentNullException();

            using (var trx = this.Session.BeginTransaction())
            {
                await this.Session.SaveOrUpdateAsync(group);
                await trx.CommitAsync();
            }
        }

        public bool Destroy(IStudyGroup group)
        {
            if (group == null)
                return false;

            var result = this.Contains(group);

            using (var trx = this.Session.BeginTransaction())
            {
                this.Session.Delete(group);
                trx.Commit();
            }

            return result;
        }

        public bool Destroy(int id)
        {
            var group = this.Get(id);

            return this.Destroy(group);
        }

        public bool Destroy(long chatId)
        {
            var group = this.Get(chatId);

            return this.Destroy(group);
        }

        public async Task<bool> DestroyAsync(IStudyGroup group)
        {
            if (group == null)
                return false;

            var result = this.Contains(group);

            using (var trx = this.Session.BeginTransaction())
            {
                await this.Session.DeleteAsync(group);
                await trx.CommitAsync();
            }

            return result;
        }

        public async Task<bool> DestroyAsync(int id)
        {
            var group = await this.GetAsync(id);

            return await this.DestroyAsync(group);
        }

        public async Task<bool> DestroyAsync(long chatId)
        {
            var group = await this.GetAsync(chatId);

            return await this.DestroyAsync(group);
        }
    }
}
