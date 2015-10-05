using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	// It’s like NGUIs UIAnchor but with less code I don’t need, better variable names and the ability to offset by world coordinates
	[ExecuteInEditMode]
	public class SimpleAnchor : MonoBehaviour 
	{
		public enum Sides
		{
			BottomLeft,
			Left,
			TopLeft,
			Top,
			TopRight,
			Right,
			BottomRight,
			Bottom,
			Center,
		}
		
		public Camera AnchorCamera = null;
		
		public Sides Side = Sides.Center;
		
		public bool RunOnlyOnce = false;
		
		public Vector3 WorldCoordinateOffset;
		
		private Transform cachedTransform;
		Rect cameraPixelRect = new Rect();
		
		protected virtual void Awake() 
		{
			cachedTransform = gameObject.transform;
		}
		
		protected virtual void Start()
		{
			if (AnchorCamera == null) 
				AnchorCamera = NGUITools.FindCameraForLayer(gameObject.layer);	
			
			Update();
		}
		
		protected virtual void Update()
		{
			if(AnchorCamera == null)
				return;

			cameraPixelRect = AnchorCamera.pixelRect;
			
			float centreX = (cameraPixelRect.xMin + cameraPixelRect.xMax) * 0.5f;
			float centreY = (cameraPixelRect.yMin + cameraPixelRect.yMax) * 0.5f;
			Vector3 anchorPosition = new Vector3(centreX, centreY, 0f);
	
			if (Side != Sides.Center)
			{
				if (Side == Sides.Right || Side == Sides.TopRight || Side == Sides.BottomRight) anchorPosition.x = cameraPixelRect.xMax;
				else if (Side == Sides.Top || Side == Sides.Center || Side == Sides.Bottom) anchorPosition.x = centreX;
				else anchorPosition.x = cameraPixelRect.xMin;
	
				if (Side == Sides.Top || Side == Sides.TopRight || Side == Sides.TopLeft) anchorPosition.y = cameraPixelRect.yMax;
				else if (Side == Sides.Left || Side == Sides.Center || Side == Sides.Right) anchorPosition.y = centreY;
				else anchorPosition.y = cameraPixelRect.yMin;
			}
	
			if (AnchorCamera.orthographic)
			{
				anchorPosition.x = Mathf.Round(anchorPosition.x);
				anchorPosition.y = Mathf.Round(anchorPosition.y);
			}
	
			anchorPosition.z = AnchorCamera.WorldToScreenPoint(this.transform.position).z;
			anchorPosition = AnchorCamera.ScreenToWorldPoint(anchorPosition);
	
			anchorPosition += WorldCoordinateOffset;
	
			// Wrapped in an 'if' so the scene doesn't get marked as 'edited' every frame
			if (cachedTransform.position != anchorPosition) cachedTransform.position = anchorPosition;
			if (RunOnlyOnce && Application.isPlaying) Destroy(this);
		}
	}
}
