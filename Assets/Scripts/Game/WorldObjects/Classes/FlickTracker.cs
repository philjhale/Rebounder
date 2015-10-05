using System;
using UnityEngine;

namespace Ph.Bouncer
{
	// TODO May need to add time back in. Flick needs to be more sensitive
	public class FlickTracker
	{
		private const float MIN_FLICK_DISTANCE = 2f;
		
		private Vector3 previousPosition, currentPosition;
		
		public void UpdatePosition(float x, float y, float z)
		{
			UpdatePosition(new Vector3(x, y, z));
		}
		
		public void UpdatePosition(Vector3 currentPosition)
		{
			this.previousPosition = this.currentPosition;
			
			this.currentPosition = currentPosition;
		}
		
		public bool IsFlick()
		{
			if(previousPosition == Vector3.zero)
				return false;
			
			//Debug.Log(string.Format("IsFlick distance - {0} {1}", (Vector3.Distance(currentPosition, previousPosition)), IsGreaterThanMinFlickDistance()));
			
			return IsGreaterThanMinFlickDistance();
		}
		
		public Vector3 GetFlickVector()
		{
			return (currentPosition - previousPosition).normalized;
		}
		
		private bool IsGreaterThanMinFlickDistance()
		{
			return Vector3.Distance(currentPosition, previousPosition) > MIN_FLICK_DISTANCE;
		}
	}
}

