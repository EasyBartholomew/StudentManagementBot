using System;


namespace StudentManagementBot.Data.ObjectModel
{
    public interface IRole : IGroupObject, IComparable<IRole>
    {
        string Name { get; set; }

        byte Priority { get; set; }
    }
}
