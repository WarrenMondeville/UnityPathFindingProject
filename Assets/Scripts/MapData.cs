using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MapData : MonoBehaviour
{

    public int width = 10;
    public int height = 5; 

    public TextAsset textMap;
    public Texture2D textureMap;
    public string resourcePath = "MapData";

    public Color32 openColor=Color.white;
    public Color32 blockedColor = Color.black;
    public Color32 lightTerrainColor = new Color32(124,194,78,255);
    public Color32 mediumTerrainColor = new Color32(252,255,52,255);
    public Color32 heavyTerrainColor = new Color32(255,129,12,255);

    static Dictionary<Color32, NodeType> terrainLookupTable = new Dictionary<Color32, NodeType>();
    private void Awake()
    {
        SetupLookupTable();
    }
    private void Start()
    {
        if (textureMap == null)
        {
            var levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name+"Texture";
            var path = System.IO.Path.Combine(resourcePath, levelName);
            textureMap = Resources.Load<Texture2D>(path);
        }

        if (textMap == null)
        {
            var levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name+".txt";
            textMap = Resources.Load(System.IO.Path.Combine(resourcePath, levelName)) as TextAsset;
        }


        //int[,] mapInstance = MakeMap();
    }

    public List<string> GetMapFromTextFile(TextAsset tAsset)
    {
        List<string> lines = new List<string>();


        string textData = tAsset.text;
        string[] delimiters = { "\r\n", "\n" };
        lines.AddRange(textData.Split(delimiters, System.StringSplitOptions.None));
        lines.Reverse();



        return lines;
    }

    public List<string> GetMapFromFile()
    {
        if (textureMap != null)
        {
            return GetMapFromTexture2D(textureMap);
        }
        else if (textMap != null)
        {
            return GetMapFromTextFile(textMap);
        }
        else
        {
            Debug.LogWarning("MapData GetMapFromfile Error:Invalid MapAsset");
            return null;
        }

    }

    public List<string> GetMapFromTexture2D(Texture2D texture)
    {
        List<string> lines = new List<string>();

        if (texture != null)
        {
            for (int y = 0; y < texture.height; y++)
            {
                string newLine = "";
                for (int x = 0; x < texture.width; x++)
                {
                    Color pixelColor = texture.GetPixel(x,y);
                    if (terrainLookupTable.ContainsKey(pixelColor))
                    {
                        NodeType nodeType = terrainLookupTable[pixelColor];
                        int nodeTypeNum = (int)nodeType;
                        newLine += nodeTypeNum;
                    }
                    else {
                        newLine += '0';
                    }

                 
                    //if (texture.GetPixel(x, y) == Color.black)
                    //{
                    //    newLine += '1';
                    //}
                    //else if (texture.GetPixel(x, y) == Color.white)
                    //{
                    //    newLine += '0';
                    //}
                    //else
                    //{
                    //    newLine += ' ';
                    //}
                }
                //Debug.Log(newLine);
                lines.Add(newLine);
            }

        }


        return lines;
    }

    public void SetDemensions(List<string> textLines)
    {
        height = textLines.Count;
        foreach (var line in textLines)
        {
            if (line.Length > width)
            {
                width = line.Length;
            }
        }
    }

    public int[,] MakeMap()
    {
        var lines = GetMapFromFile();
        SetDemensions(lines);
        int[,] map = new int[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (lines[y].Length > x)
                {
                    map[x, y] = (int)Char.GetNumericValue(lines[y][x]);
                }
            }
        }
        return map;
    }

    void SetupLookupTable()
    {
        terrainLookupTable.Add(openColor, NodeType.Open);
        terrainLookupTable.Add(blockedColor, NodeType.Blocked);
        terrainLookupTable.Add(lightTerrainColor, NodeType.LightTerrain);
        terrainLookupTable.Add(mediumTerrainColor, NodeType.MediumTerrain);
        terrainLookupTable.Add(heavyTerrainColor, NodeType.HeavyTerrain);
    }
    public static Color GetColorFromNodeType(NodeType nodeType)
    {
        if (terrainLookupTable.ContainsValue(nodeType))
        {
            Color colorKey = terrainLookupTable.FirstOrDefault(x=>x.Value==nodeType).Key;
            return colorKey;
        }
        return Color.white;
    }
}
