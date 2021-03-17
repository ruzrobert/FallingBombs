public class EnemyPawn : Pawn, IPoolerObject<EnemyPawnKey, EnemyPawn>
{
	public EnemyPawn PoolerPrefab { get; private set; }

	protected override void OnDeath()
	{
		base.OnDeath();

		EnemyManager.Instance.UnRegisterEnemy(this);
	}

	public override void DestroyPawn()
	{
		gameObject.SetActive(false);

		PoolingManager.Instance.EnemyPooler.ReturnObjectToPool(this);
	}

	public void ResetPooledObject(EnemyPawn originalPoolerPrefab)
	{
		PoolerPrefab = originalPoolerPrefab;

		ResetState();
	}
}