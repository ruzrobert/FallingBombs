using System.Collections;
using UnityEngine;

public class ExploadableBomb : MonoBehaviour, IPoolerObject<ExploadableBombKey>
{
	[Header("Components")]
	[SerializeField] private new Rigidbody rigidbody;

	[Header("Pooling")]
	[SerializeField] private ExploadableBombKey poolingKey;

	[Header("Explosion")]
	[SerializeField] private float explosionRadius = 5f;
	[SerializeField] private AnimationCurve explosionDamageCurve = new AnimationCurve();

	[Header("Damage")]
	[SerializeField] private float explosionCenterDamage = 100f;
	[SerializeField] private bool canDamageThroughWalls = false;

	[Header("Effects")]
	[SerializeField] private EffectKey explosionEffectKey;
	[SerializeField, Min(0)] private float delayAfterEffect = 0f;

	public ExploadableBombKey PoolingKey => poolingKey;

	public bool IsExploded { get; private set; } = false;

	// Object Pooling
	private bool defaultsSaved = false;

	private Vector3 originalScale = Vector3.zero;
	private Quaternion originalLocalRotation = Quaternion.identity;

	private void Awake()
	{
		SaveDefaultsIfNeeded();
	}

	private void Start()
	{
		PlayAppearAnimation();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (IsExploded) return;

		Explode();
	}

	private void Explode()
	{
		PlayExplodeAnimation();
	}

	private void PlayExplodeAnimation()
	{
		IEnumerator Sequence()
		{
			if (explosionEffectKey)
			{
				EffectManager.Instance.SpawnEffectAt(explosionEffectKey, transform.position);

				yield return new WaitForSeconds(delayAfterEffect);
			}

			HandleDamagableObjects();

			Vector3 startScale = transform.localScale;

			float t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / 0.15f;
				transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
				yield return null;
			}

			gameObject.SetActive(false);
			ObjectPoolingManager.Instance.BombPooler.ReturnObjectToPool(this);
		}

		StartCoroutine(Sequence());
	}

	private void HandleDamagableObjects()
	{
		Collider[] results = BombManager.Instance.ExplosionCheckResults;
		int enemyCasts = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius, results, LayerMasks.Enemy.raycast);

		for (int i = 0; i < enemyCasts; i++)
		{
			Collider collider = results[i];
			Rigidbody attachedRigidbody = collider.attachedRigidbody;

			Pawn pawn = attachedRigidbody ? attachedRigidbody.GetComponent<Pawn>() : null;

			if (pawn)
			{
				if (IsPointBlockedByWall(pawn.transform.position + new Vector3(0f, 0.5f, 0f)) == false)
				{
					float distanceToPawn = Vector3.Distance(pawn.transform.position, transform.position);
					float damageT = Mathf.InverseLerp(explosionRadius, 0f, distanceToPawn);
					float damageValue = explosionDamageCurve.Evaluate(damageT) * explosionCenterDamage;

					pawn.Health.TakeDamage(damageValue);
				}
			}
		}
	}

	private bool IsPointBlockedByWall(Vector3 point)
	{
		if (canDamageThroughWalls)
		{
			return false;
		}

		Ray ray = new Ray(transform.position, point - transform.position);
		float distance = Vector3.Distance(ray.origin, point);

		return Physics.Raycast(ray, distance, LayerMasks.InnerWall.raycast);
	}

	private void PlayAppearAnimation()
	{
		IEnumerator Sequence()
		{
			Vector3 targetScale = transform.localScale;

			float t = 0f;
			while (t < 1f)
			{
				t += Time.deltaTime / 0.2f;
				transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);
				yield return null;
			}
		}

		StartCoroutine(Sequence());
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red.SetA(0.5f);
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}

	public void ResetPooledObject()
	{
		SaveDefaultsIfNeeded();

		LoadDefaults();
	}

	private void SaveDefaultsIfNeeded()
	{
		if (defaultsSaved == false)
		{
			defaultsSaved = true;

			originalScale = transform.localScale;
			originalLocalRotation = transform.localRotation;
		}
	}

	private void LoadDefaults()
	{
		transform.localScale = originalScale;
		transform.localRotation = originalLocalRotation;

		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.position = transform.position;
	}
}