using System.Drawing;

namespace MarsGame.Texture
{
    public interface ITextureContainer
    {
        Bitmap GetTextureByBlockName(char blockName);

        Bitmap GetPlayerTexture();
    }
}
