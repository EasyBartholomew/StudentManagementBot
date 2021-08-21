using System;
using System.Collections.Generic;


namespace StudentManagementBot.Data.ObjectModel
{
    public interface IStudent : IGroupObject
    {
        int Id { get; }

        string Surname { get; set; }

        string Name { get; set; }

        string Patronymic { get; set; }

        long? UserId { get; set; }

        IReadOnlyList<IRole> Roles { get; }

        bool AddRole(IRole studentRole);

        bool RemoveRole(IRole studentRole);

        void RemoveAllRoles();

        bool RecognizeSelf(string personalData);
    }
}
