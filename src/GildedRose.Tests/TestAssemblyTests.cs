using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {

        private Item GenerateItem(string name, int sellIn, int quality)
        {
            Item item = new Item()
            {
                Name = name,
                SellIn = sellIn,
                Quality = quality
            };
            return item;
        }
        
        [Fact]
        public void TestTheTruth()
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("+5 Dexterity Vest", 20, 18)]
        [InlineData("+5 Dexterity Vest", 2, 0)]
        [InlineData("+5 Dexterity Vest", 1, 0)]
        [InlineData("+5 Dexterity Vest", 0, 0)]
        [InlineData("Elixir of the Mongoose", 7, 5)]
        public void QualityDegradeAfterExpiration(string name, int quality, int expectedQuality)
        {
            Item item = GenerateItem(name, 0, quality);

            Program.UpdateItemQuality(item);

            Assert.Equal(expectedQuality, item.Quality);
        }

        [Theory]
        [InlineData("+5 Dexterity Vest", 1, 0)]
        [InlineData("Aged Brie", 1, 0)]
        [InlineData("Elixir of the Mongoose", 1, 0)]
        [InlineData("Sulfuras, Hand of Ragnaros", 0, 80)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 1, 0)]
        [InlineData("Conjured Mana Cake", 1, 0)]
        public void QualityNeverNegative(string name, int sellin, int quality)
        {
            int expectedMinimumQuality = 0;
            Item item = GenerateItem(name, sellin, quality);

            Program.UpdateItemQuality(item);

            Assert.True(item.Quality >= expectedMinimumQuality);
        }

        [Theory]
        [InlineData(2, 0, 1)]
        [InlineData(1, 0, 1)]
        [InlineData(1, 1, 2)]
        [InlineData(0, 0, 2)]
        [InlineData(0, 1, 3)]
        public void AgedBrieQualityUpgrade(int sellin, int quality, int expectedQuality)
        {
            Item item = GenerateItem("Aged Brie", sellin, quality);

            Program.UpdateItemQuality(item);

            Assert.Equal(expectedQuality, item.Quality);
        }

        [Theory]
        [InlineData("+5 Dexterity Vest", 1, 50)]
        [InlineData("Aged Brie", 1, 48)]
        [InlineData("Aged Brie", 1, 49)]
        [InlineData("Aged Brie", 1, 50)]
        [InlineData("Aged Brie", 0, 48)]
        [InlineData("Aged Brie", 0, 49)]
        [InlineData("Aged Brie", 0, 50)]
        [InlineData("Elixir of the Mongoose", 1, 50)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 10, 50)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 5, 50)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 1, 50)]
        [InlineData("Conjured Mana Cake", 0, 50)]
        public void NonLegendaryItemQualityNeverMoreThan50(string name, int sellIn, int quality)
        {
            int expectedMaximumQuality = 50;
            Item item = GenerateItem(name, sellIn, quality);

            Program.UpdateItemQuality(item);

            Assert.True(item.Quality <= expectedMaximumQuality);
        }

        [Fact]
        public void SulfurasQualityDegrade()
        {
            int expectedQuality = 80;
            Item item = GenerateItem("Sulfuras, Hand of Ragnaros", 0, 80);

            Program.UpdateItemQuality(item);

            Assert.Equal(expectedQuality, item.Quality);
        }

        [Theory]
        [InlineData(15, 20, 21)]
        [InlineData(14, 21, 22)]
        [InlineData(13, 22, 23)]
        [InlineData(11, 24, 25)]
        [InlineData(10, 25, 27)]
        [InlineData(9, 27, 29)]
        [InlineData(6, 27, 29)]
        [InlineData(5, 30, 33)]
        [InlineData(4, 33, 36)]
        [InlineData(1, 36, 39)]
        [InlineData(0, 39, 0)]
        [InlineData(-1, 0, 0)]
        public void BackstagePassesQualityDegrade(int sellIn, int quality, int expectedQuality)
        {
            Item item = GenerateItem("Backstage passes to a TAFKAL80ETC concert", sellIn, quality);

            Program.UpdateItemQuality(item);

            Assert.Equal(expectedQuality, item.Quality);
        }

        [Theory]
        [InlineData("Conjured Mana Cake", 5, 10, 8)]
        [InlineData("Conjured Mana Cake", 4, 8, 6)]
        [InlineData("Conjured Mana Strudel", 0, 4, 0)]
        [InlineData("Conjured Mana Strudel", 0, 2, 0)]
        [InlineData("Conjured Goblin Bazooka", 2, 50, 48)]
        public void ConjuredItemQualityDegrade(string name, int sellIn, int quality, int expectedQuality)
        {
            Item item = GenerateItem(name, sellIn, quality);

            Program.UpdateItemQuality(item);

            Assert.Equal(expectedQuality, item.Quality);
        }

    }
}