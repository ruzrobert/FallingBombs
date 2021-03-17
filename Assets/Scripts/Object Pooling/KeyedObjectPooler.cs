using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public abstract class KeyedObjectPooler<TKey, TObject> : MonoBehaviour where TKey : ObjectKey where TObject : Object, IPoolerObject<TKey>
{
	[Space]
	[FormerlySerializedAs("prefabs")]
	[SerializeField] private TObject[] objects = new TObject[0];

	protected Dictionary<TKey, Queue<TObject>> pooledObjects = new Dictionary<TKey, Queue<TObject>>();

	public virtual TObject GetObjectFromPool(TKey key)
	{
		TObject GetObjectInternal()
		{
			TObject poolObject = objects.FirstOrDefault(x => x.PoolingKey == key);

			if (pooledObjects.TryGetValue(key, out Queue<TObject> objectPool) && objectPool.Count > 0)
			{
				return objectPool.Dequeue();
			}
			else
			{
				return CreateNewObject(poolObject);
			}
		}

		TObject instance = GetObjectInternal();
		instance.ResetPooledObject();

		return instance;
	}

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

		if (pooledObjects.TryGetValue(poolObject.PoolingKey, out Queue<TObject> objectPool) == false)
		{
			objectPool = pooledObjects[poolObject.PoolingKey] = new Queue<TObject>();
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