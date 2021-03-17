using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
	[Header("Pawn")]
	[SerializeField] private PawnHealth health;
	[SerializeField] private PawnGraphics graphics;

	public PawnHealth Health => health;
	public PawnGraphics Graphics => graphics;

	protected virtual void Awake()
	{
		if (health) health.Setup(this);
		if (graphics) graphics.Setup(this);

		if (health)
		{
			health.OnDamageReceived.AddListener(OnDamageReceived);
			health.OnDeath.AddListener(OnDeath);
		}
	}

	protected virtual void OnDamageReceived()
	{
		if (graphics) graphics.OnDamageReceived();
	}

	protected virtual void OnDeath()
	{
		if (graphics) graphics.OnDeath();
		else DestroyPawn();
	}

	public virtual void DestroyPawn()
	{
		Destroy(gameObject);
	}

	public virtual void ResetState()
	{
		if (health) health.ResetState();
		if (graphics) graphics.ResetState();
	}
}