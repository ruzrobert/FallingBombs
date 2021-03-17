using UnityEngine;
using UnityEngine.Events;

public class PawnHealth : MonoBehaviour, IPawnComponent
{
	[Header("Data")]
	[SerializeField] private float maxHealth = 100f;
	[SerializeField] private float health = 0f;

	[Header("Events")]
	[SerializeField] private UnityEvent onDamageReceived = new UnityEvent();
	[SerializeField] private UnityEvent onDeath = new UnityEvent();

	public float Health => health;
	public float MaxHealth => maxHealth;

	public UnityEvent OnDamageReceived => onDamageReceived;
	public UnityEvent OnDeath => onDeath;

	public Pawn Pawn { get; private set; }

	public bool IsAlive { get; private set; } = false;

	public void Setup(Pawn pawn)
	{
		Pawn = pawn;

		ResetState();
	}

	public void TakeDamage(float damage)
	{
		if (IsAlive == false || damage <= 0f)
		{
			return;
		}

		health = Mathf.Max(health - damage, 0f);

		if (health <= 0f)
		{
			IsAlive = false;
			onDeath.Invoke();
		}
		else
		{
			onDamageReceived.Invoke();
		}
	}

	public void ResetState()
	{
		health = maxHealth;
		IsAlive = true;
	}
}