using UnityEngine;
using System;

namespace BaldiDevContentAPI.CustomItems
{
	/// <summary>
	/// The attributes for the custom item
	/// </summary>
	public struct CustomitemAttributes
	{
		public CustomitemAttributes(string nameKey, Sprite smallIcon, Sprite normalIcon, Items itemEnum, string descKey = "", int shopPrice = 0, int roomCost = 20)
		{
			NameKey = nameKey;
			SmallIcon = smallIcon;
			NormalIcon = normalIcon;
			ItemEnum = itemEnum;
			// Optionals
			DescKey = descKey;
			this.shopPrice = shopPrice;
			this.roomCost = roomCost;
		}
		/// <summary>
		/// Name of the item (use the LocalizedText Key)
		/// </summary>
		public string NameKey { get; set; }
		/// <summary>
		/// The description of the item for the shop
		/// </summary>
		public string DescKey { get; set; }
		/// <summary>
		/// Small Icon of the Item (appears in the inventory)
		/// </summary>
		public Sprite SmallIcon { get; set; }
		/// <summary>
		/// Big Icon of the Item (appears outside the inventory)
		/// </summary>
		public Sprite NormalIcon { get; set; }
		/// <summary>
		/// The enum of the Item
		/// </summary>
		public Items ItemEnum { get; set; }
		/// <summary>
		/// The price of the item in the shop
		/// </summary>
		public int ShopPrice { get => shopPrice; set => ShopPrice = Mathf.Max(0, value); }

		private int shopPrice;
		/// <summary>
		/// Specific value for how common is the item gonna be based on the room, for example, grappling hooks have a high room cost, making them rarer and only appear in faculties that aren't connected to any room.
		/// </summary>
		public int RoomCost { get => roomCost; set => roomCost = Mathf.Max(0, value); }
		private int roomCost;

	}
}
