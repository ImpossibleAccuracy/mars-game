using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using MarsGame.Controls;

namespace MarsGame.Texture
{
    public class TestTextureContainer : ITextureContainer
    {
        public Bitmap GetTextureByBlockName(char blockName)
        {
            switch (blockName)
            {
                case '.': return GetEmptyBlock();
                case 'X': return GetWallBlock();
                case 'S': return GetStartBlock();
                case 'Q': return GetQuitBlock();
                default:
                    /*if (char.IsNumber(blockName))
                    {

                    }*/

                    if (char.IsLower(blockName)) return GetKeyTexture(blockName);
                    else return GetDoorTexture(blockName);

                    throw new ArgumentException($"Unknown block type \"{blockName}\"", nameof(blockName));
            };
        }

        public Bitmap GetKeyTexture(char blockName)
        {
            Bitmap bitmap = GetFilledBitmap(
                UIGameControl.CellSize,
                Color.FromArgb(100, 75, 184));
            return DrawText(bitmap, blockName.ToString());
        }

        public Bitmap GetDoorTexture(char blockName)
        {
            Bitmap bitmap = GetFilledBitmap(
                UIGameControl.CellSize,
                Color.FromArgb(184, 75, 171));
            return DrawText(bitmap, blockName.ToString());
        }

        public Bitmap GetPlayerTexture()
        {
            Bitmap bitmap = GetFilledBitmap(
                UIGameControl.CellSize,
                Color.FromArgb(255, 0, 0));
            return DrawText(bitmap, "P");
        }

        private Bitmap GetEmptyBlock()
        {
            return GetFilledBitmap(
                UIGameControl.CellSize,
                Color.FromArgb(255, 255, 0));
        }

        private Bitmap GetWallBlock()
        {
            return GetFilledBitmap(
                UIGameControl.CellSize,
                Color.FromArgb(0, 255, 255));
        }

        private Bitmap GetStartBlock()
        {
            Bitmap bitmap = GetFilledBitmap(
                UIGameControl.CellSize,
                Color.FromArgb(255, 255, 255));
            return DrawText(bitmap, "S");
        }

        private Bitmap GetQuitBlock()
        {
            Bitmap bitmap = GetFilledBitmap(
                UIGameControl.CellSize,
                Color.FromArgb(255, 0, 255));
            return DrawText(bitmap, "Q");
        }

        private Bitmap GetFilledBitmap(int size, Color color)
        {
            Bitmap bitmap = new Bitmap(size, size);
            Graphics gfx = Graphics.FromImage(bitmap);
            SolidBrush brush = new SolidBrush(color);
            gfx.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
            return bitmap;
        }

        private Bitmap DrawText(Bitmap bitmap, string text)
        {
            RectangleF rectf = new RectangleF(0, 0, bitmap.Width, bitmap.Height);

            Graphics g = Graphics.FromImage(bitmap);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawString(text, new Font("Arial", 8), Brushes.Black, rectf);

            g.Flush();

            return bitmap;
        }
    }
}
