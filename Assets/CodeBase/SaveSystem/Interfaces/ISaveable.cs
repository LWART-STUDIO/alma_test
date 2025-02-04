using CodeBase.SaveSystem.Extension;

namespace CodeBase.SaveSystem.Interfaces
{
    public interface ISaveable
    {
        SerializableGuid Id { get; set; }
    }
}