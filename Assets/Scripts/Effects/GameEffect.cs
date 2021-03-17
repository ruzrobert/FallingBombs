using UnityEngine;

public class GameEffect : MonoBehaviour, IPoolerObject<EffectKey, GameEffect>
{
    [Header("Components")]
    [SerializeField] private ParticleSystem[] particleSystems = new ParticleSystem[0];

	[Header("Life")]
	[SerializeField] private bool freeAfterTime = false;
	[SerializeField, Min(0)] private float destroyAfterTimeValue = 0f;

    [Header("Auto")]
    [SerializeField] private bool getChildParticleSystems = false; // inspector stuff
	
	public GameEffect PoolerPrefab { get; private set; }

	private float startTime = 0f;

	private void Awake()
	{
		ResetPooledObject(null);
	}

	private void Update()
	{
		if (freeAfterTime)
		{
			if (Time.time >= startTime + destroyAfterTimeValue)
			{
				EffectManager.Instance.FreeEffect(this);
			}
		}
	}

	private void OnValidate()
	{
		if (getChildParticleSystems)
		{
			getChildParticleSystems = false;
			particleSystems = GetComponentsInChildren<ParticleSystem>(includeInactive: true);
		}
	}

	public void ForcePlayAllParticleSystems()
	{
		for (int i = 0; i < particleSystems.Length; i++)
		{
			ParticleSystem ps = particleSystems[i];
			ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
			ps.Play(true);
		}
	}

	public void ResetPooledObject(GameEffect originalPoolerPrefab)
	{
		PoolerPrefab = originalPoolerPrefab;

		startTime = Time.time;
	}
}