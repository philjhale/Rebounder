using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ph.Bouncer
{
	public class Level
	{
		public string Name { get; set; }
		
		private bool requiresAspectRatioSpecificScenes;
	
		public Level(string name)
		{
			this.Name = name;
		}
		
		public Level SetRequiresAspectRatioSpecificScenes()
		{
			requiresAspectRatioSpecificScenes = true;
			return this;
		}

		public bool DoesRequireAspectRatioSpecificScenes()
		{
			return requiresAspectRatioSpecificScenes;
		}
	}
}