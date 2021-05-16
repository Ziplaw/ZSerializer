using ZSave;

[System.Serializable]
public class EnemyZSaver : ZSave.ZSaver<Enemy>
{
    public UnityEngine.Material mat;
    public UnityEngine.Mesh mesh;

    public EnemyZSaver(Enemy EnemyInstance) : base(EnemyInstance.gameObject, EnemyInstance)
    {
         mat = (UnityEngine.Material)typeof(Enemy).GetField("mat").GetValue(EnemyInstance);
         mesh = (UnityEngine.Mesh)typeof(Enemy).GetField("mesh").GetValue(EnemyInstance);
    }
}