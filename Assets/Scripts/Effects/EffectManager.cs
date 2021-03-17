using UnityEngine;

public class EffectManager : MonoBehaviour
{
	public static EffectManager Instance { get; private set; } = null;

	private void Awake()
	{
		Instance = this;
	}

	public void SpawnEffectAt(EffectKey effectKey, Vector3 position)
	{
		GameEffect effect = ObjectPoolingManager.Instance.EffectPooler.GetObjectFromPool(effectKey);

		if (effect)
		{
			effect.transform.SetParent(transform);
			effect.transform.position = position;

			effect.gameObject.SetActive(true);

			effect.ForcePlayAllParticleSystems();
		}
	}

	public void FreeEffect(GameEffect effect)
	{
		ObjectPoolingManager.Instance.EffectPooler.ReturnObjectToPool(effect);
	}
}