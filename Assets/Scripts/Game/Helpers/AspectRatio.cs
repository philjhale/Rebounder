namespace Ph.Bouncer
{
	public class AspectRatio
	{
		public AspectRatio(float aspect, string aspectString)
		{
			this.Aspect = aspect;
			this.AspectString = aspectString;
		}
		
		public float Aspect { get; private set; }
		public string AspectString { get; private set; }
	}
}