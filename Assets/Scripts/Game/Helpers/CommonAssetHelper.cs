using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public static class CommonAssetHelper
	{
		public static void Load()
		{
			DestroyAll("CommonAssets");
			DestroyAll("DynamicObjects");

			Application.LoadLevelAdditive("InGameCommonAssets");
		}

		private static void DestroyAll(string name)
		{
			GameObject objectToDestroy = null;

			do
			{
				objectToDestroy = GameObject.Find(name);

				if(objectToDestroy != null)
					GameObject.DestroyImmediate(objectToDestroy);
			}
			while(objectToDestroy != null);
		}
	}
}