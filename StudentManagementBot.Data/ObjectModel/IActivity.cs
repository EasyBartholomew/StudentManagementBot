using System;
using System.Collections.Generic;


namespace StudentManagementBot.Data.ObjectModel
{
    public interface IActivity : IGroupObject
    {
        string Name { get; set; }

        bool Empty { get; }

        bool Single { get; }
               
        IReadOnlyList<IActivityAction> Actions { get; }

        IActivityAction CreateAction(DateTime date, string remark = null);

        bool DestroyAction(IActivityAction action);
        
        int DestroyAllActions(Predicate<IActivityAction> predicate);

        void DestroyAllActions();
    }
}
