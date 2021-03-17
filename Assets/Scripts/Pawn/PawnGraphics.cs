using System.Collections;
using UnityEngine;

public class PawnGraphics : MonoBehaviour, IPawnComponent
{
	public Pawn Pawn { get; private set; }

	[Space]
	[SerializeField] private Animator animator;

	[Space]
	[SerializeField] private Material normalMaterial;
	[SerializeField] private Material damageMaterial;

	[Space]
	[SerializeField] private MeshRenderer meshRenderer;

	private Coroutine damageCoroutine;

	public void Setup(Pawn pawn)
	{
		Pawn = pawn;

		Pawn.Health.OnDamageReceived.AddListener(OnDamageReceived);
		Pawn.Health.OnDeath.AddListener(OnDeath);

		ResetState();
	}

	private void OnDamageReceived()
	{
		PlayDamageResponse();
	}

	private void OnDeath()
	{
		PlayDeath();
	}

	private void PlayDamageResponse()
	{
		IEnumerator Sequence()
		{
			yield return null;

			meshRenderer.sharedMaterial = normalMaterial;

			yield return null;

			meshRenderer.sharedMaterial = damageMaterial;

			yield return new WaitForSeconds(0.4f);

			meshRenderer.sharedMaterial = normalMaterial;
		}

		CoroutineExt.Restart(ref damageCoroutine, this, Sequence());
	}

	private void PlayDeath()
	{
		IEnumerator Sequence()
		{
			meshRenderer.sharedMaterial = damageMaterial;

			animator.SetTrigger(AnimHash.Death);

			yield return new WaitForSeconds(3f);

			Pawn.DestroyPawn();
		}

		CoroutineExt.Restart(ref damageCoroutine, this, Sequence());
	}

	public void ResetState()
	{
		meshRenderer.sharedMaterial = normalMaterial;
	}
}