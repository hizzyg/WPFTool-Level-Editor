using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGFramework;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class LevelImport : MonoBehaviour
{
    public GameObject TilePrefab;
    public Level CurrentLevel;

	// Use this for initialization
	void Start ()
    {
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        LoadLevel(desktop + @"\ManiakLevel\tstlvl.lvl");
	}

    public void LoadLevel(string path)
    {
        if (!File.Exists(path))
        {
            Debug.Log("Error while Loading Level!");
            return;
        }

        //reading from the file
        using (var file = File.OpenRead(path))
        {
            var reader = new BinaryFormatter();
            CurrentLevel = (Level)reader.Deserialize(file);
        }

        foreach(Tile tile in CurrentLevel.Grid.Tiles)
        {
            GameObject NewTile = GameObject.Instantiate(TilePrefab, new Vector3(tile.CoordinateX, 0, tile.CoordinateY), TilePrefab.transform.rotation);
            byte[] TextureRaw = CurrentLevel.Palette.Entries[tile.TextureEntryID].Texture;

            Texture2D Texture = new Texture2D(64, 64);
            Texture.LoadImage(TextureRaw);

            NewTile.GetComponent<Renderer>().material.mainTexture = Texture;

            switch(tile.Type)
            {
                default:
                case TileType.Default:
                    break;
                case TileType.Collision:
                    NewTile.AddComponent<BoxCollider>();
                    break;
                case TileType.Trigger:
                    break;
            }
        }
    }
}
