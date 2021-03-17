using UnityEngine;

public class PoolingManager : MonoBehaviour
{
	public static PoolingManager Instance { get; private set; }

	[Space]
	[SerializeField] private ExploadableBombPooler bombPooler = null;
	[SerializeField] private EnemyPawnPooler enemyPooler = null;
	[SerializeField] private EffectPooler effectPooler = null;

	public ExploadableBombPooler BombPooler => bombPooler;
	public EnemyPawnPooler EnemyPooler => enemyPooler;
	public EffectPooler EffectPooler => effectPooler;

	private void Awake()
	{
		Instance = this;
	}
}