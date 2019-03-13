using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGFramework
{
    [Serializable]
    public struct Tile
    {
        /// <summary>
        /// Integer for the X Coordinate
        /// </summary>
        public int CoordinateX;
        /// <summary>
        /// Integer for the Y Coordinate
        /// </summary>
        public int CoordinateY;
        /// <summary>
        /// Integer for the Layer
        /// </summary>
        public int Layer;
        /// <summary>
        /// Ref. to the TileType
        /// </summary>
        public TileType Type;
        /// <summary>
        /// string for the Trigger Action
        /// </summary>
        public string Attribute_TriggerAction;
        /// <summary>
        /// string for the Portal Destination
        /// </summary>
        public string Attribute_PortalDestination;
        /// <summary>
        /// integer for the NPC ID
        /// </summary>
        public int Attribute_NPCID;
        /// <summary>
        /// integer for the Object ID
        /// </summary>
        public int Attribute_ObjectID;
        /// <summary>
        /// integer for the TextureEntry ID
        /// </summary>
        public int TextureEntryID;

        /// <summary>
        /// Function for the tile which includes the x and y coordinate,layer and the type
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_layer"></param>
        /// <param name="_type"></param>
        public Tile(int _x, int _y, int _layer, TileType _type)
        {
            CoordinateX = _x;
            CoordinateY = _y;
            Layer = _layer;
            Type = _type;
            Attribute_TriggerAction = "";
            Attribute_PortalDestination = "";
            Attribute_NPCID = 0;
            Attribute_ObjectID = 0;
            TextureEntryID = 0;
        }

        /// <summary>
        /// Function for the Tile which inlcudes the x and y coordinate,layer,tiletype,trigger action, portal destination,npc id, object id and texture entry id
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_layer"></param>
        /// <param name="_type"></param>
        /// <param name="_triggerAction"></param>
        /// <param name="_portalDestination"></param>
        /// <param name="_npcID"></param>
        /// <param name="_objectID"></param>
        /// <param name="_textureEntryID"></param>
        public Tile(int _x, int _y, int _layer, TileType _type, string _triggerAction, string _portalDestination, int _npcID, int _objectID, int _textureEntryID)
        {
            CoordinateX = _x;
            CoordinateY = _y;
            Layer = _layer;
            Type = _type;
            Attribute_TriggerAction = _triggerAction;
            Attribute_PortalDestination = _portalDestination;
            Attribute_NPCID = _npcID;
            Attribute_ObjectID = _objectID;
            TextureEntryID = _textureEntryID;
        }
    }
}
