using CodeBase.SaveSystem.Extension;

namespace CodeBase.SaveSystem.Interfaces
{
    public interface IBind<TData> where TData : ISaveable
    {
        SerializableGuid Id { get; set; }
        void Bind(TData data);
    }
}