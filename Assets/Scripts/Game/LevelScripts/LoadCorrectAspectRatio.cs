using UnityEngine;
using System.Collections;

namespace Ph.Bouncer 
{
	public class LoadCorrectAspectRatio : MonoBehaviour 
	{
		void Start() 
		{
			if(!AspectRatioHelper.IsCurrentSceneCorrectAspect())
				LevelLoadHelper.Reload();

			this.enabled = false;
		}
	}
}