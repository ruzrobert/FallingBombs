using System;
using System.Linq;
using UnityEngine;

public class EffectPooler : KeyedObjectPooler<EffectKey, GameEffect>
{
	[Serializable]
	public class KVP : KeyedPoolKVP<EffectKey, GameEffect> { }

	[Space]
	[SerializeField] private KVP[] prefabs = new KVP[0];

	protected override GameEffect GetPrefabByKey(EffectKey key)
	{
		return prefabs.FirstOrDefault(x => x.Key == key).Prefab;
	}
}