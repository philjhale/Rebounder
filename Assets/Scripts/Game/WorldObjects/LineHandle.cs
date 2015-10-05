using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ph.Bouncer
{
	public class LineHandle : MonoBehaviour 
	{
		private Line line;
		private tk2dSprite sprite;
		private CapsuleCollider ballCollider;
		private Transform myTransform;

		public string Name { get { return name; } }

		private HashSet<int> collidingObjects;

		public Vector3 Position 
		{ 
			get { return myTransform.position; } 
			set 
			{
				myTransform.position = value;
			}
		}

		public void Start()
		{
			line = (Line)transform.parent.GetComponent<Line>();
			sprite = (tk2dSprite)GetComponent<tk2dSprite>();
			ballCollider = (CapsuleCollider)GetComponent<CapsuleCollider>();

			SetColour(transform.parent.GetComponent<Line>().Colour);

			collidingObjects = new HashSet<int>();

			myTransform = transform;
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

		public void SetPosition(Vector3 newPosition)
		{
			transform.position = newPosition;
		}

		public Line Grab()
		{
			line.GrabHandle(name);
			return line;
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
				sprite.SetSprite("LineCap" + colour.ToString());
			
			SetLayer(colour);
		}
		
		private void SetLayer(Colour colour)
		{
			gameObject.layer = LineHelper.GetLayerForColour(colour);
		}
	}
}