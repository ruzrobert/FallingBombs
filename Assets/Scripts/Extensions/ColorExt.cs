using UnityEngine;

public static class ColorExt
{
	public static readonly Color orange = new Color32(209, 139, 33, 255);

	public static Color SetA(this Color color, float a)
	{
		color.a = a;
		return color;
	}
}