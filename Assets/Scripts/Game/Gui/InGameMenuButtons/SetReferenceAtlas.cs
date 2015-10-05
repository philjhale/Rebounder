using UnityEngine;
using System.Collections;

namespace Ph.Bouncer 
{
	public class SetReferenceAtlas : MonoBehaviour 
	{
		public UIAtlas InGameGuiReference;

		public UIAtlas InGameGui;
		public UIAtlas InGameGui2x;
		public UIAtlas InGameGui4x;

		// FirstLoadManager
		void Awake() 
		{	
			ResolutionSize size = ResolutionHelper.GetSize();

			switch (size) 
			{
				case ResolutionSize.One:
					InGameGuiReference.replacement = InGameGui;
					break;
				case ResolutionSize.Two:
					InGameGuiReference.replacement = InGameGui2x;
					break;
				default:
					InGameGuiReference.replacement = InGameGui4x;
					break;
			}
		}
	}
}