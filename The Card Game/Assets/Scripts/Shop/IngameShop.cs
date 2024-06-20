using System.Collections.Generic;
using System.Runtime.InteropServices;
namespace Shop
{
	/// <summary>
	/// Does what a shop do
	/// </summary>
	internal static class IngameShop
	{
		#region Item
		/// <summary>
		/// Every item which is inside the shop
		/// </summary>
		private static List<ShopItem> Items = new List<ShopItem>();

		/// <summary>
		/// An item, which can be bought
		/// </summary>
		internal struct ShopItem
		{
			/// <summary>
			/// Name of this item
			/// </summary>
			internal readonly string ItemName;
			/// <summary>
			/// The card 
			/// </summary>
			internal readonly Card StoredCard;
			/// <summary>
			/// The price of this item
			/// </summary>
			internal readonly ulong Price;
			/// <summary>
			/// Makes a new Item
			/// </summary>
			internal ShopItem(string itemname, Card card, ulong price, bool register = true)
			{
				ItemName = itemname;
				StoredCard = card;
				Price = price;

				if (register)
				{ Items.Add(this); }
			}
		}
		#endregion
		#region Basic ShopItem operations

		/// <summary>
		/// Registers an item, so the shop can see it (items are auto registered by default)
		/// </summary>
		/// <returns>The same item, for chained calls</returns>
		internal static ShopItem RegisterItem(ShopItem item)
		{
			Items.Add(item);
			return item;
		}

		/// <summary>
		/// Removes the given Item from the shop
		/// </summary>
		/// <returns>If the operation succeeded or not</returns>
		internal static bool RemoveItem(ShopItem item) => Items.Remove(item);

		/// <summary>
		/// Removes an item, with the given name from the shop
		/// </summary>
		/// <returns>If the operation succeeded or not</returns>
		internal static bool RemoveItem(string itemname) => GetItem(itemname, out ShopItem item) && RemoveItem(item);
		#endregion
		#region ShopItem Helper
		/// <summary>
		/// Gets an item using its name
		/// </summary>
		/// <returns>If the operation succeeded or not</returns>
		internal static bool GetItem(string itemname, out ShopItem item)
		{
			//TODO: _IngameShop: Optimise if possible (Getter)
			for (int i = 0; i < Items.Count; i++)
			{
				if (Items[i].ItemName == itemname)
				{ item = Items[i]; return true; }
			}
			item = default;
			return false;
		}
		#endregion
		#region Purchasing
		/// <summary>
		/// Determines if the user can buy a certain item, or not
		/// </summary>
		/// <returns>If the item can be bought or not</returns>
		internal static bool CanBuy(ShopItem item, ulong usercurrency) => item.Price <= usercurrency;
		/// <summary>
		/// Determines if the user can buy a certain item, or not
		/// </summary>
		/// <returns>If the item can be bought or not</returns>
		internal static bool CanBuy(string itemname, ulong usercurrency) => GetItem(itemname, out ShopItem item) && CanBuy(item, usercurrency);

		/// <summary>
		/// Determines if the user can buy a certain item, and buys it
		/// </summary>
		/// <returns>If the item was bought or not</returns>
		internal static bool TryBuy(ShopItem item, ref ulong usercurrency_PTR)
		{
			//TODO: _IngameShop: Optimise if possible (Buy)
			if (CanBuy(item, usercurrency_PTR))
			{
				usercurrency_PTR -= item.Price;
				return true;
			}
			return false;
		}
		/// <summary>
		/// Determines if the user can buy a certain item, and buys it
		/// </summary>
		/// <returns>If the item was bought or not</returns>
		internal static bool TryBuy(string itemname, ref ulong usercurrency_PTR) => GetItem(itemname, out ShopItem item) && TryBuy(item, ref usercurrency_PTR);
		#endregion
	}
}