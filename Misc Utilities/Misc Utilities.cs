using System;
using System.Collections.Generic;
using HarmonyLib;
using MTM101BaldAPI;
using MTM101BaldAPI.AssetManager;
using UnityEngine;

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
		/// <summary>
		/// Creates the default poster used by npcs, simplified in a single method for easy usage.
		/// </summary>
		/// <param name="posterNameKey"></param>
		/// <param name="posterDescKey"></param>
		/// <param name="posterVisual"></param>
		/// <returns></returns>
		public static PosterObject CreateDefaultNPCPosterObject(string posterNameKey, string posterDescKey, Texture2D posterVisual) // Yeah, literally that
		{
			var data = CopyPosterTextData(Character.Beans.GetFirstInstance().Poster.textData);
			data[0].textKey = posterNameKey;
			data[1].textKey = posterDescKey;

			return ObjectCreatorHandlers.CreatePosterObject(posterVisual, data);
		}
	}

	public static class RoomUtilities
	{
		public static RoomCategory[] AllRooms => (RoomCategory[])Enum.GetValues(typeof(RoomCategory));
	}

	public static class NPCUtilities
	{
		/// <summary>
		/// A simplified version to call the AssetManager.SpriteFromTexture2D() multiple times to create an animation
		/// </summary>
		/// <param name="pixelsPerUnit"></param>
		/// <returns></returns>
		public static Dictionary<string, Sprite[]> CreateAnimationSprites(string animationName, float pixelsPerUnit, params string[] paths) // Hopefully this makes the code to create these dictionaries shorter
		{
			var sprites = new List<Sprite>();
			foreach (var path in paths)
			{
				sprites.Add(AssetManager.SpriteFromTexture2D(AssetManager.TextureFromFile(path), new Vector2(0.5f, 0.5f), pixelsPerUnit));
			}
			return new Dictionary<string, Sprite[]>() { { animationName, sprites.ToArray() } };
		}
		
	}
}
