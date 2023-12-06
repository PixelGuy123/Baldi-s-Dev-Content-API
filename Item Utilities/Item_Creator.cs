using MTM101BaldAPI;
using UnityEngine;

namespace BaldiDevContentAPI.CustomItems
{
	/// <summary>
	/// A helper to create custom items
	/// </summary>
	public static class ItemCreatorHelper
	{
		/// <summary>
		/// Creates a custom ItemObject with all the data needed from <paramref name="attributes"/>, the Item component is disabled by default, but you can always change the <paramref name="leaveEnabled"/>
		/// </summary>
		/// <typeparam name="I"></typeparam>
		/// <param name="attributes"></param>
		/// <returns></returns>
		public static ItemObject CreateItem<I>(CustomitemAttributes attributes, bool leaveEnabled = false) where I : Item
		{
			var itmObject = ObjectCreatorHandlers.CreateItemObject(attributes.NameKey, attributes.DescKey, attributes.SmallIcon, attributes.NormalIcon, attributes.ItemEnum, attributes.ShopPrice, attributes.RoomCost);
			var item = new GameObject($"Item_{Singleton<LocalizationManager>.Instance.GetLocalizedText(attributes.NameKey)}").AddComponent<Item>(); // The LocalizationManager must be on, there's no way this could cause errors

			item.gameObject.SetActive(leaveEnabled);
			Object.DontDestroyOnLoad(item.gameObject);

			itmObject.item = item;

			return itmObject;
		}
	}
}
