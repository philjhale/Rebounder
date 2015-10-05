using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public class Ball : MonoBehaviour 
	{
		public Colour Colour;
		
		private const float LIFETIME_SECONDS = 20f;
		
		private float startTime;
		private tk2dSprite sprite;
		float speed;

		void Start()
		{
			startTime = Time.time;
			sprite = GetComponent<tk2dSprite>();
			SetColour(Colour);
		}
		
		void Update()
		{
			if(HasLifeEnded())
			{
				DestroyObject(this.gameObject);
			}

			// Maintain constant speed
			SetVelocity(rigidbody.velocity, speed);
		}
		

		public void SetVelocity(Vector2 velocity, float speed)
		{
			this.speed = speed;
			rigidbody.velocity = velocity.normalized * speed;
		}
		
		
		public void SetColour(Colour colour)
		{
			Colour = colour;
			
			if(sprite != null)
				sprite.SetSprite("Ball" + colour.ToString());

			switch(colour)
			{
				case Colour.Purple:
					gameObject.layer = (int)Layers.PurpleBall;
					break;
				case Colour.Green:
					gameObject.layer = (int)Layers.GreenBall;
					break;
				case Colour.Blue:
					gameObject.layer = (int)Layers.BlueBall;
					break;
				default: gameObject.layer = (int)Layers.Default;
					break;
			}
		}

		private bool HasLifeEnded()
		{
			return Time.time > startTime + LIFETIME_SECONDS;
		}
	}
}