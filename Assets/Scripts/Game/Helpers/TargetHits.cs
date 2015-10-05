using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Ph.Bouncer
{
	public class TargetHits
	{
		private Dictionary<Colour, int> targetHits;
		
		public TargetHits(IEnumerable<Target> targets)
		{
			targetHits = new Dictionary<Colour, int>();
			
			foreach(var target in targets)
			{
				targetHits.Add(target.Colour, 0);
			}
		}
		
		public void UpdateHits(Colour colour, int hits)
		{
			// TODO Error checking? Colour not found
			targetHits[colour] = hits;
		}
		
		public bool AllTargetsHaveHitsGreaterOrEqualTo(int numberOfHits)
		{
			return !(targetHits.Where(x => x.Value < numberOfHits).Count() > 0);
		}
	}
}
