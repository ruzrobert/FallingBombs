using UnityEngine;

public static class LayerMasks
{
	#region Layers

	public static readonly Layer Default = new Layer("Default");
	public static readonly Layer UI = new Layer("UI");

	public static readonly Layer Enemy = new Layer("Enemy");
	public static readonly Layer InnerWall = new Layer("InnerWall");

	#endregion

	#region Data

	public readonly struct Layer
	{
		public readonly string name;
		public readonly LayerMask mask;
		public readonly int index;
		public readonly int raycast;

		public Layer(string name)
		{
			this.name = name;
			this.mask = LayerMask.NameToLayer(this.name);
			this.index = LayerMaskToIndex(this.mask);
			this.raycast = LayerIndexToRaycastMask(this.index);
		}
	}

	#endregion

	#region Helper Methods

	public static LayerMask Combine(LayerMask a, LayerMask b)
	{
		return a | b;
	}

	public static bool IsLayerInLayerMask(int layerIndex, LayerMask layerMask)
	{
		return layerMask == (layerMask | (1 << layerIndex));
	}

	public static int LayerMaskToIndex(LayerMask layerMask)
	{
		return layerMask.value;
	}

	public static int LayerIndexToRaycastMask(int layerIndex)
	{
		return 1 << layerIndex;
	}

	#endregion
}