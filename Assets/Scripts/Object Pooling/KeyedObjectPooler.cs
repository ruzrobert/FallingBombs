using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class KeyedObjectPooler<TKey, TObject> : MonoBehaviour where TKey : ObjectKey where TObject : Object, IPoolerObject<TKey, TObject>
{
	protected Dictionary<TObject, Queue<TObject>> pooledObjects = new Dictionary<TObject, Queue<TObject>>();

	public virtual TObject GetObjectFromPool(TKey key)
	{
		(TObject prefab, TObject instance) GetObjectInternal()
		{
			TObject poolPrefab = GetPrefabByKey(key);

			if (pooledObjects.TryGetValue(poolPrefab, out Queue<TObject> objectPool) && objectPool.Count > 0)
			{
				return (poolPrefab, objectPool.Dequeue());
			}
			else
			{
				return (poolPrefab, CreateNewObject(poolPrefab));
			}
		}

		(TObject prefab, TObject instance) = GetObjectInternal();
		instance.ResetPooledObject(prefab);

		return instance;
	}

	protected abstract TObject GetPrefabByKey(TKey key);

	protected virtual TObject CreateNewObject(TObject poolObject)
	{
		return poolObject ? Instantiate(poolObject) : null;
	}

	public virtual void ReturnObjectToPool(TObject poolObject)
	{
		Transform transform = GetTransform(poolObject);

		if (transform)
		{
			transform.gameObject.SetActive(false);
			transform.SetParent(this.transform);
		}

		if (!pooledObjects.TryGetValue(poolObject.PoolerPrefab, out Queue<TObject> objectPool))
		{
			objectPool = pooledObjects[poolObject.PoolerPrefab] = new Queue<TObject>();
		}

		objectPool.Enqueue(poolObject);
	}

	private Transform GetTransform(TObject prefab)
	{
		if (prefab is GameObject gameObject)
		{
			return gameObject.transform;
		}
		else if (prefab is Component component)
		{
			return component.transform;
		}
		else
		{
			return null;
		}
	}
}