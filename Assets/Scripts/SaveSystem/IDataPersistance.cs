using Cysharp.Threading.Tasks;

public interface IDataPersistance
{

    public async UniTask LoadData(SaveData saveData)
    {

    }


    public void SaveData(ref SaveData saveData)
    {

    }
}
