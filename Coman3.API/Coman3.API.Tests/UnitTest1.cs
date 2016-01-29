using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Coman3.API.Champion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coman3.API.Tests
{
    [TestClass]
    public class Square
    {
        [TestMethod]
        public void CreateImage()
        {
            var iconGen = new IconGenerator(IconGenerator.IconType.Square, 128, 128, Color.FromArgb(200, Color.Red), 10);
            var bitmap = iconGen.GetChampionIcon("Ekko");
            var savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Square_CreateImage_Test.png";
            bitmap.Save(savePath, ImageFormat.Png);
        }
        [TestMethod]
        public void CreateErrorImage()
        {
            var iconGen = new IconGenerator(IconGenerator.IconType.Square, 128, 128, Color.FromArgb(200, Color.Red), 5);
            var bitmap = iconGen.GetChampionIcon(""); //Will not be found and error out
            var savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Square_CreateErrorImage_Test.png";
            bitmap.Save(savePath, ImageFormat.Png);
        }
    }

    [TestClass]
    public class Cirle
    {
        [TestMethod]
        public void CreateImage()
        {
            var iconGen = new IconGenerator(IconGenerator.IconType.Circle, 128, 128, Color.FromArgb(200, Color.Red), 20);
            var bitmap = iconGen.GetChampionIcon("Ekko");
            var savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Cirle_CreateImage_Test.png";
            bitmap.Save(savePath, ImageFormat.Png);
        }
        [TestMethod]
        public void CreateErrorImage()
        {
            var iconGen = new IconGenerator(IconGenerator.IconType.Circle, 128, 128, Color.FromArgb(50, Color.Red), 10);
            var bitmap = iconGen.GetChampionIcon(""); //Will not be found and error out
            var savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/Cirle_CreateErrorImage_Test.png";
            bitmap.Save(savePath, ImageFormat.Png);
        }
    }
}
