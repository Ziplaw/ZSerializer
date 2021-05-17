using ZSave;

[System.Serializable]
public class EnemyZSaver : ZSaver<Enemy>
{

    public EnemyZSaver(Enemy EnemyInstance) : base(EnemyInstance.gameObject, EnemyInstance)
    {
    }
}