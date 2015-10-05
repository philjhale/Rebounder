using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public static class ResolutionHelper 
	{
		private const int IPHONE_3_WIDTH = 480;
		private const int IPHONE_5_WIDTH = 1136;
		// iPhone 4 width 960
		// iPad width 1024

		public static ResolutionSize GetSize()
		{
			// Assumes portrait
			if(Screen.width <= IPHONE_3_WIDTH) // iPhone 3gs and old iPod Touch
				return ResolutionSize.One;
			if(Screen.width <= IPHONE_5_WIDTH) // iPhone 4/5 and iPad 1/2
				return ResolutionSize.Two;

			return ResolutionSize.Four; // iPad Retina
		}
	}
}