using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
    public class Program
    {
        private static readonly string[] _qualityIncreasingItems = new string[]{ "Aged Brie", "Backstage passes to a TAFKAL80ETC concert" };

        IList<Item> Items;
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
                          {
                              Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

                          };

            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdateItemQuality(item);
            }
        }

        public static void UpdateSulfurasQuality(Item item)
        {
        }

        public static void UpdateAgedBrieQuality(Item item)
        {
            item.SellIn--;

            if (item.SellIn < 0 && item.Quality < 48)
                item.Quality += 2;
            else if (item.Quality < 50)
                item.Quality++;

        }

        private static void UpdateBackstagePassesQuality(Item item)
        {
            item.SellIn--;

            if (item.SellIn < 0)
                item.Quality = 0;
            else if (item.SellIn < 5 && item.Quality < 47)
                item.Quality += 3;
            else if (item.SellIn < 10 && item.Quality < 48)
                item.Quality += 2;
            else if (item.Quality < 50)
                item.Quality++;

        }

        private static void UpdateConjuredItemQuality(Item item)
        {
            item.SellIn--;

            if (item.Quality > 0)
            {
                if (item.SellIn < 0)
                    item.Quality -= 4;
                else
                    item.Quality -= 2;
            }

            if (item.Quality < 0)
                item.Quality = 0;
        }

        private static void UpdateSimpleItemQuality(Item item)
        {
            item.SellIn--;

            if (item.Quality > 0)
            {
                if (item.SellIn < 0)
                    item.Quality -= 2;
                else
                    item.Quality -= 1;
            }

            if (item.Quality < 0)
                item.Quality = 0;
        }

        public static void UpdateItemQuality(Item item)
        {
            if (item.Name == "Sulfuras, Hand of Ragnaros")
            {
                UpdateSulfurasQuality(item);
                return;
            }

            if (item.Name == "Aged Brie")
            {
                UpdateAgedBrieQuality(item);
                return;
            }

            if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                UpdateBackstagePassesQuality(item);
                return;
            }

            if (item.Name.StartsWith("Conjured"))
            {
                UpdateConjuredItemQuality(item);
                return;
            }

            UpdateSimpleItemQuality(item);
        }

    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
