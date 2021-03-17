using UnityEngine;

public interface IPoolerObject<TKey, TObject> where TKey : ObjectKey where TObject : Object
{
	TObject PoolerPrefab { get; }

	void ResetPooledObject(TObject originalPoolerPrefab);
}