using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGFramework
{
    [Serializable]
    public struct TextureEntry
    {
        /// <summary>
        /// String for the name
        /// </summary>
        public string Name;
        /// <summary>
        /// String for the description
        /// </summary>
        public string Description;
        /// <summary>
        /// Ref. to the Shader
        /// </summary>
        public Shader Shader;
        /// <summary>
        /// byte array for the texture
        /// </summary>
        public byte[] Texture;


        /// <summary>
        /// The Texture Entry (name,description and shader)
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_description"></param>
        /// <param name="_shader"></param>
        public TextureEntry(string _name, string _description, Shader _shader)
        {
            Name = _name;
            Description = _description;
            Shader = _shader;
            Texture = new byte[0];
        }

        /// <summary>
        /// The Texture Entry (name,description,shader and texture)
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_description"></param>
        /// <param name="_shader"></param>
        /// <param name="_texture"></param>
        public TextureEntry(string _name, string _description, Shader _shader, byte[] _texture)
        {
            Name = _name;
            Description = _description;
            Shader = _shader;
            Texture = _texture;
        }
    }
}
