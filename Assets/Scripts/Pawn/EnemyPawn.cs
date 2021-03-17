using UnityEngine;

public class EnemyPawn : Pawn, IPoolerObject<EnemyPawnKey>
{
	[Header("Pooling"), Header("Enemy")]
	[SerializeField] private EnemyPawnKey poolingKey;

	public EnemyPawnKey PoolingKey => poolingKey;

	protected override void OnDeath()
	{
		base.OnDeath();

		EnemyManager.Instance.UnRegisterEnemy(this);
	}

	public override void DestroyPawn()
	{
		gameObject.SetActive(false);

		ObjectPoolingManager.Instance.EnemyPooler.ReturnObjectToPool(this);
	}

	public void ResetPooledObject()
	{
		ResetState();
	}
}