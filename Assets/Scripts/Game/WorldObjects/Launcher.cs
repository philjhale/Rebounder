using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	// TODO Ensure balls have DynamicObjects parent
	public class Launcher : MonoBehaviour 
	{
		public Rigidbody BallPrefab;
		public bool Enabled = true;
		public Colour Colour;
		public float OverriddenFireRate;
		public float OverriddenBallSpeed;	
		
	    private float nextFireTime = 0.0F;
		private float defaultSecondsBetweenShots = 1.5F;
		private float defaultBallSpeed = 7;
		
		private float ballSpeed;
		private float secondsBetweenShots;

		private bool tapStarted = false;
		private float tapStartTime;
		private const float LONG_TAP_THRESHOLD = 1f;
		private Launcher[] allShooters;
		private LauncherInner launcherInner;
		
		void Start()
		{
			launcherInner = (LauncherInner)GetComponentInChildren<LauncherInner>();
			launcherInner.SetColour(Colour);

			gameObject.SetActive(Enabled);	
			
			ballSpeed = OverriddenBallSpeed > 0 ? OverriddenBallSpeed : defaultBallSpeed;
			secondsBetweenShots = OverriddenFireRate > 0 ? OverriddenFireRate : defaultSecondsBetweenShots;

			tk2dSprite sprite = GetComponent<tk2dSprite>();
			sprite.SetSprite("Launcher" + Colour.ToString());

			allShooters = (Launcher[])FindObjectsOfType(typeof(Launcher));
		}
		
	    void Update() 
		{
	        FireNext();

			CheckForLongTap();
	    }
		
		public void OnLevelComplete()
		{
			Enabled = false;
		}
		
		void OnEnable()
		{
			Messenger.AddListener(Events.LevelComplete, OnLevelComplete);
		}
		
		void OnDisable()
		{
			Messenger.RemoveListener(Events.LevelComplete, OnLevelComplete);
		}
		
		public void ToggleEnabled()
		{
			Enabled = !Enabled;	
		}
		
		public void StartTap()
		{
			tapStarted = true;
			tapStartTime = Time.time;
		}

		public void EndTap()
		{
			if(IsLongTap())
				ActivateLongTap();
			else
				ActivateShortTap();

			tapStarted = false;
		}


		private void Shoot()
		{
			Rigidbody ballClone = (Rigidbody)Instantiate(BallPrefab, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);	
			//ballClone.velocity = new Vector2(transform.up.x, transform.up.y).normalized * ballSpeed;

			var ballScript = ballClone.GetComponent<Ball>();
			ballScript.SetVelocity(new Vector2(transform.up.x, transform.up.y), ballSpeed);
			ballScript.SetColour(Colour);

			launcherInner.Fire(secondsBetweenShots);
		}
		
		private void FireNext()
		{
			if (Enabled && Time.time > nextFireTime) {
	            nextFireTime = Time.time + secondsBetweenShots;
	            Shoot();
	        }
		}

		private bool IsLongTap()
		{
			return tapStarted && Time.time >= tapStartTime + LONG_TAP_THRESHOLD;	
		}
		
		private bool IsShortTap()
		{
			return tapStarted && Time.time < tapStartTime + LONG_TAP_THRESHOLD;	
		}
		
		private void ActivateShortTap()
		{
			if(!tapStarted) return;

			//Debug.Log("Short tap");
			tapStarted = false;
			
			foreach(var shooter in allShooters)
				shooter.ToggleEnabled();

			GAHelper.LevelLauncherShortTap();
		}
		
		private void ActivateLongTap()
		{
			if(!tapStarted) return;

			//Debug.Log("Long tap");
			tapStarted = false;
			
			Ball[] allBalls = (Ball[])FindObjectsOfType(typeof(Ball));

			foreach(Ball ball in allBalls) 
				DestroyObject(ball.gameObject);

			GAHelper.LevelLauncherLongTap();
		}

		private void CheckForLongTap()
		{
			if(!tapStarted) return;

			if(IsLongTap()) ActivateLongTap();
		}
	}
}

