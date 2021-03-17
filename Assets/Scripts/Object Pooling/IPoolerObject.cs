public interface IPoolerObject<TKey> where TKey : ObjectKey
{
	TKey PoolingKey { get; }
	void ResetPooledObject();
}