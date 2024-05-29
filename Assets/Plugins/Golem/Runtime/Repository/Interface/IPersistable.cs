namespace Golem
{
    public interface IPersistable
    {
        void LoadInternalDataStorage(DataStorage dataStorage);

        DataSettings GetDataSettings();
        DataStorage GetDataStorage();
    }

}
