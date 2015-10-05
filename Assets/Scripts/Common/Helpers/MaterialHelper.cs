using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public static class MaterialHelper 
	{
		public static void SetAlpha(GameObject obj, float alpha)
		{
			obj.renderer.material.color = obj.renderer.material.color.SetAlpha(alpha);
		}
	}
}