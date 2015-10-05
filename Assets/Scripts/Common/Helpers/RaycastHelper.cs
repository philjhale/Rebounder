using UnityEngine;
using System.Collections;
using System.Linq;

namespace Ph.Bouncer
{
	public static class RaycastHelper 
	{
		public static bool HasHitObjectWithTags(out RaycastHit hit, params string[] tags)
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	
			return Physics.Raycast(ray, out hit, 50) &&  tags.Contains(hit.collider.tag);	
		}
		
		public static bool HasHitObjectWithLayerMaskAndTags(out RaycastHit hit, int layerMask, params string[] tags)
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	
			return Physics.Raycast(ray, out hit, 50, layerMask) && tags.Contains(hit.collider.tag);	
		}
	}
}
