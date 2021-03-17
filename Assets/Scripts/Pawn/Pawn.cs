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
		Health.Setup(this);
		Graphics.Setup(this);

		Health.OnDeath.AddListener(OnDeath);
	}

	protected virtual void OnDeath()
	{
		DestroyPawn();
	}

	public virtual void DestroyPawn()
	{
		Destroy(gameObject);
	}

	public virtual void ResetState()
	{
		health.ResetState();
		graphics.ResetState();
	}
}