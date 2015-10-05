using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ph.Bouncer
{
	public class Target : MonoBehaviour 
	{
		public Colour Colour;

		private const float SECONDS_BETWEEN_DRAIN = 2f;
		
		private float lastHitOrDrainTime;
		private tk2dSprite sprite;
		private int hits = 0;
		private List<TargetHit> targetHitObjects;
		private bool isLevelComplete = false;
		
		
		void Start()
		{
			ResetLastHitOrDrainTime();
			sprite = GetComponent<tk2dSprite>();

			SetColour(Colour);

			targetHitObjects = new List<TargetHit>();
			foreach(var th in GetComponentsInChildren<TargetHit>())
			{
				th.SetColour(Colour);
				th.Hide();
				targetHitObjects.Add(th);
			}
		}
	
		void OnCollisionEnter(Collision collision) 
		{
			if(!IsColliderABallWithMatchingColour(collision))
				return;
				
			RecordHit(collision);
		}
		
		void Update()
		{
			if(!isLevelComplete && hits > 0 && HasDrainTimePassed())
			{
				Drain();
			}
		}

		public void OnLevelComplete()
		{
			isLevelComplete = true;
		}
		
		void OnEnable()
		{
			Messenger.AddListener(Events.LevelComplete, OnLevelComplete);
		}
		
		void OnDisable()
		{
			Messenger.RemoveListener(Events.LevelComplete, OnLevelComplete);
		}

		
		private void Drain()
		{
			ResetLastHitOrDrainTime();
			hits--;
			Messenger<Colour, int>.Broadcast(Events.TargetDrain, Colour, hits);
			HideHitObject();
		}

		private void RecordHit(Collision collision)
		{
			DestroyObject(collision.gameObject);	
			ResetLastHitOrDrainTime();
			hits++;
			Messenger<Colour, int>.Broadcast(Events.TargetHit, Colour, hits);
			ShowHitOject();
		}
			
		private bool IsColliderABallWithMatchingColour(Collision collision)
		{
			var ballScript = collision.collider.GetComponent<Ball>();
			
			return ballScript && ballScript.Colour == this.Colour;
		}
		
		private bool HasDrainTimePassed()
		{
			return Time.time > lastHitOrDrainTime + SECONDS_BETWEEN_DRAIN;
		}
		
		private void ResetLastHitOrDrainTime()
		{
			lastHitOrDrainTime = Time.time;
		}
		
		private void SetColour(Colour colour)
		{
			Colour = colour;
			
			if(sprite != null)
				sprite.SetSprite("Target" + colour.ToString());
		}

		private void ShowHitOject()
		{
			if(hits - 1 < targetHitObjects.Count)
				targetHitObjects[hits-1].Show();
		}

		private void HideHitObject()
		{
			if(hits >= 0 && hits < targetHitObjects.Count)
				targetHitObjects[hits].Hide();
		}
	}
}
