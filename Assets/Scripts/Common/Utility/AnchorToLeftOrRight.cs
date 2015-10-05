using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class AnchorToLeftOrRight : MonoBehaviour
	{
		public float WorldCoordinateOffsetX;
		
		void Start() 
		{
			CalculateAnchorSide();
		}
		
		private void CalculateAnchorSide()
		{
			var cachedTransform = gameObject.transform;
			bool addAnchor = false;
			float yOffset = 0, xOffset = 0;
			SimpleAnchor.Sides anchorSide = SimpleAnchor.Sides.Center;
			
			float orthographicSize = 10; // Ha ha ha I've hardcoded the orthographic size. See if I care!
			
			// The object will automatically stick to the top and bottom of the 
			// screen because the orthographic size isn't changed. So we only care
			// about anchoring to the left and right of the screen if the target
			// is positioned there
			
			// Not sure these if statements are ideal. I'm assuming that if the x value is greater
			// than the height of the screen the object is probably stuck to the left or right of the
			// screen
			if(cachedTransform.position.x > orthographicSize)
			{
				addAnchor = true;
				anchorSide = SimpleAnchor.Sides.Right;	
				yOffset = cachedTransform.position.y;
				xOffset = cachedTransform.localScale.x * -1;
			}
			else if(cachedTransform.position.x < orthographicSize * -1)
			{
				addAnchor = true;
				anchorSide = SimpleAnchor.Sides.Left;	
				yOffset = cachedTransform.position.y;
				xOffset = cachedTransform.localScale.x;
			}
			
			if(addAnchor)
			{
				SimpleAnchor anchor = gameObject.AddComponent<SimpleAnchor>();
				anchor.Side = anchorSide;
				anchor.WorldCoordinateOffset.y = yOffset;
				anchor.WorldCoordinateOffset.x = xOffset + WorldCoordinateOffsetX;
			}
		}
	}
}
