using UnityEngine;
using System.Collections;

namespace Ph.Bouncer
{
	public static class OutputDebug
	{
		public static void Format(string formatString, params object[] args)
		{
			Debug.Log(string.Format(formatString, args));
		}

		public static void IsNull(object objectToTest)
		{
			string name = "Object";

			if(objectToTest is GameObject)
				name = (objectToTest as GameObject).name;

			Debug.Log(string.Format("{0} is {1}", name, objectToTest != null ? "NOT null" : "null"));
		}
	}
}