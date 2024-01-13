using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace BaldiDevContentAPI.Misc.UI
{

	public class UIBuilder
	{
		private readonly Canvas _canvas; // Private field for the targeted canvas
		public Canvas Canvas => _canvas;

		public UIBuilder(Canvas target) // Requires a canvas for every created element to be child of it
		{
			_canvas = target ?? throw new NullReferenceException("Targeted canvas for UI Builder was null");
		}


		private void SetElementToCanvas(Transform transform, Vector2 position, Vector2 size) // private method just to set the element to the canvas
		{
			transform.SetParent(_canvas.transform);

			transform.localPosition = position;
			transform.localScale = size;
			transform.gameObject.layer = LayerMask.NameToLayer("UI");
		}
		/// <summary>
		/// Creates an image element attached to the canvas
		/// </summary>
		/// <param name="position"></param>
		/// <param name="size"></param>
		/// <param name="color"></param>
		/// <param name="sprite"></param>
		/// <returns>Returns an image element attached to the canvas</returns>
		public Image CreateImage(Vector2 position, Vector2 size, Color? color = null, Sprite sprite = null) // Creates an image
		{
			var img = new GameObject("Image", typeof(Image)).GetComponent<Image>();
			img.sprite = sprite;
			img.color = color ?? Color.white;

			SetElementToCanvas(img.transform, position, size);

			return img;
		}

		/// <summary>
		/// Creates an TextMeshProUGUI element attached to the canvas
		/// </summary>
		/// <param name="position"></param>
		/// <param name="size"></param>
		/// <param name="color"></param>
		/// <param name="display"></param>
		/// <param name="alignment"></param>
		/// <returns>Returns a TextMeshProUGUI element attached to the canvas</returns>
		public TextMeshProUGUI CreateTextMesh(Vector2 position, Vector2 size, Color color, string display = "", TextAlignmentOptions alignment = TextAlignmentOptions.Center)
		{
			var text = new GameObject("TextMeshProUGUI", typeof(TextMeshProUGUI)).GetComponent<TextMeshProUGUI>();

			text.text = display;
			text.alignment = alignment;
			text.color = color;

			SetElementToCanvas(text.transform, position, size);

			return text;
		}

	}
}
