using System.Collections.Generic;

namespace CodeBase.SaveSystem.Interfaces
{
    public interface IDataService
    {
        void Save(Data data, bool overwrite = true);
        Data Load(string name);
        void Delete(string name);
        void DeleteAll();
        IEnumerable<string> ListSaves();
    }
}