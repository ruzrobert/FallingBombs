using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class KeyedPoolKVP<TKey, TObject> where TKey : ObjectKey where TObject : Object, IPoolerObject<TKey, TObject>
{
	[SerializeField] private TKey key;
	[SerializeField] private TObject prefab;

	public TKey Key => key;
	public TObject Prefab => prefab;
}