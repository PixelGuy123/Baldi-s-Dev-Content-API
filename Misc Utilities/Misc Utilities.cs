using System;

namespace BaldiDevContentAPI.Misc
{
	public static class PosterUtilities
	{
		/// <summary>
		/// Basically just copy a poster text data into another
		/// </summary>
		/// <param name="sources"></param>
		/// <returns>Returns a copied version of an array of PosterTextData</returns>
		public static PosterTextData[] CopyPosterTextData(PosterTextData[] sources) // Gets the Poster Text Data[] and converts it to a new one, so it doesn't get by reference (I hate this)
		{
			var newPosterTextData = new PosterTextData[sources.Length];
			for (int i = 0; i < sources.Length; i++)
			{
				newPosterTextData[i] = CopyPosterTextData(sources[i]);
			}

			return newPosterTextData;
		}
		/// <summary>
		/// Basically just copy a poster text data into another
		/// </summary>
		/// <param name="sources"></param>
		/// <returns>Returns a copied version of PosterTextData</returns>
		public static PosterTextData CopyPosterTextData(PosterTextData source)
		{
			return new PosterTextData()
			{
				position = source.position,
				alignment = source.alignment,
				style = source.style,
				color = source.color,
				font = source.font,
				fontSize = source.fontSize,
				size = source.size
			};
		}
	}

	public static class RoomUtilities
	{
		public static RoomCategory[] AllRooms => (RoomCategory[])Enum.GetValues(typeof(RoomCategory));
	}
}
