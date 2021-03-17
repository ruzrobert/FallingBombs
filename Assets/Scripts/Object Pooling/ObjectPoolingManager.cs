using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
	public static ObjectPoolingManager Instance { get; private set; }

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