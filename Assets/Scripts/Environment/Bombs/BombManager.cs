using UnityEngine;

public class BombManager : MonoBehaviour
{
	public static BombManager Instance { get; private set; }

	public Collider[] ExplosionCheckResults { get; private set; } = new Collider[10];

	private void Awake()
	{
		Instance = this;

		ClearExplosionCheckResults();
	}

	private void ClearExplosionCheckResults()
	{
		for (int i = 0; i < ExplosionCheckResults.Length; i++)
		{
			ExplosionCheckResults[i] = null;
		}
	}

	private void OnDestroy()
	{
		ClearExplosionCheckResults();
	}
}