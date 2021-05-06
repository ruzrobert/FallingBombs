using UnityEngine;

public class BombManager : MonoBehaviour
{
	public static BombManager Instance { get; private set; }

	public Collider[] ExplosionCheckResults { get; private set; } = new Collider[10];

	private void Awake()
	{
		Instance = this;
	}
}