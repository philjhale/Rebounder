using UnityEngine;
using System.Collections;

// http://answers.unity3d.com/questions/8715/how-do-i-use-layermasks.html
namespace Ph.Bouncer
{
	public static class LayerMaskHelper
	{
		public static int FromInt(int layerNumber)
		{
			return 1 << layerNumber;	
		}
	}
}
