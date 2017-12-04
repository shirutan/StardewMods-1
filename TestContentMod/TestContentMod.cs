using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;

namespace Pathoschild.Stardew.TestContentMod
{
    /// <summary>The mod entry point.</summary>
    public class TestContentMod : Mod, IAssetEditor, IAssetLoader
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper) { }

        /****
        ** Edit
        ****/
        /// <summary>Get whether this instance can edit the given asset.</summary>
        /// <param name="asset">Basic metadata about the asset being loaded.</param>
        public bool CanEdit<T>(IAssetInfo asset)
        {
            // return typeof(T) == typeof(Texture2D);
            return false;
        }

        /// <summary>Edit a matched asset.</summary>
        /// <param name="asset">A helper which encapsulates metadata about an asset and enables changes to it.</param>
        public void Edit<T>(IAssetData asset)
        {
            // texture
            Texture2D texture = asset.AsImage().Data;
            texture = this.GrayscaleColors(texture);
            asset.ReplaceWith(texture);
        }

        /****
        ** Load
        ****/
        /// <summary>Get whether this instance can load the initial version of the given asset.</summary>
        /// <param name="asset">Basic metadata about the asset being loaded.</param>
        public bool CanLoad<T>(IAssetInfo asset)
        {
            return false;
        }

        /// <summary>Load a matched asset.</summary>
        /// <param name="asset">Basic metadata about the asset being loaded.</param>
        public T Load<T>(IAssetInfo asset)
        {
            return default(T);
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Create a copy of a texture with inverted colors.</summary>
        /// <param name="source">The original texture to copy.</param>
        private Texture2D InvertColors(Texture2D source)
        {
            // get source pixels
            Color[] pixels = new Color[source.Width * source.Height];
            source.GetData(pixels);

            // invert pixel colors
            for (int i = 0; i < pixels.Length; i++)
            {
                Color color = pixels[i];
                if (color.A == 0)
                    continue; // transparent

                pixels[i] = new Color(byte.MaxValue - color.R, byte.MaxValue - color.G, byte.MaxValue - color.B, color.A);
            }

            // create new texture
            Texture2D target = new Texture2D(source.GraphicsDevice, source.Width, source.Height);
            target.SetData(pixels);
            return target;
        }

        /// <summary>Create a copy of a texture with grayscale colors.</summary>
        /// <param name="source">The original texture to copy.</param>
        private Texture2D GrayscaleColors(Texture2D source)
        {
            // get source pixels
            Color[] pixels = new Color[source.Width * source.Height];
            source.GetData(pixels);

            // grayscale pixel colors
            for (int i = 0; i < pixels.Length; i++)
            {
                Color color = pixels[i];
                if (color.A == 0)
                    continue; // not transparent

                int grayscale = (int)((color.R * 0.3) + (color.G * 0.59) + (color.B * 0.11));
                pixels[i] = new Color(grayscale, grayscale, grayscale, color.A);
            }

            // create new texture
            Texture2D target = new Texture2D(source.GraphicsDevice, source.Width, source.Height);
            target.SetData(pixels);
            return target;
        }
    }
}