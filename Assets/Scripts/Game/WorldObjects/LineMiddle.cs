using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ph.Bouncer
{
	public class LineMiddle : MonoBehaviour 
	{
		private BoxCollider ballCollider;

		//private Vector3 previousPosition;
		private tk2dTiledSprite sprite;

		private HashSet<int> collidingObjects;

		void Start() 
		{
			ballCollider = (BoxCollider)GetComponent<BoxCollider>();
			sprite = (tk2dTiledSprite)GetComponent<tk2dTiledSprite>();

			SetColour(transform.parent.GetComponent<Line>().Colour);

			collidingObjects = new HashSet<int>();
		}


		void OnTriggerEnter(Collider other) 
		{
			collidingObjects.Add(other.gameObject.GetInstanceID());
		}
		
		void OnTriggerExit(Collider other) 
		{
			collidingObjects.Remove(other.gameObject.GetInstanceID());
		}

		public bool AreAnyObjectsCollidingWithTrigger()
		{
			return collidingObjects.Count > 0;
		}

		public bool Stretch(Vector3 from, Vector3 to)
		{
			int lineHandleLength = 1; // Probably shouldn't hardcode but who cares
			float xScale = Vector3.Distance(from, to) - lineHandleLength;

			if(xScale <= 0)
				return false;

			// X scale is set rather than Y because the whole object is rotated by 90 degrees
			transform.localScale = transform.localScale.SetX(xScale);
			// Move to allow for the fact that the scaling happens in both directions and we only want one
			transform.position = Vector3.Lerp(from, to, 0.5f);

			return true;
		}

		public void SetAlpha(float alpha)
		{
			sprite.color = sprite.color.SetAlpha(alpha);
		}

		public void SetAllColours(float value)
		{
			sprite.color = sprite.color.SetAllColours(value);
		}

		public void SetBallsCanCollide()
		{
			ballCollider.isTrigger = false;
		}

		public void SetBallsWillNotCollide()
		{
			ballCollider.isTrigger = true;
		}


		private void SetColour(Colour colour)
		{
			if(sprite != null)
				sprite.SetSprite("LineInner" + colour.ToString());

			SetLayer(colour);
		}
		
		private void SetLayer(Colour colour)
		{
			gameObject.layer = LineHelper.GetLayerForColour(colour);
		}
	}
}