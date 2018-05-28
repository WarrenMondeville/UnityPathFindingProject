using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapData : MonoBehaviour
{

    public int width = 10;
    public int height = 5; 

    public TextAsset textMap;
    public Texture2D textureMap;
    public string resourcePath = "MapData";


    private void Start()
    {
        if (textureMap == null)
        {
            var levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name+"Texture";
            var path = System.IO.Path.Combine(resourcePath, levelName);
            var obj = Resources.Load<Texture2D>(path);
            textureMap = Resources.Load<Texture2D>(path);
        }

        if (textMap == null)
        {
            var levelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name+".txt";
            textMap = Resources.Load(System.IO.Path.Combine(resourcePath, levelName)) as TextAsset;
        }


        int[,] mapInstance = MakeMap();
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

    public List<string> GetMapFromTexture2D(Texture2D Texture2D)
    {
        List<string> lines = new List<string>();

        if (Texture2D != null)
        {

        }
        for (int y = 0; y < Texture2D.height; y++)
        {
            string newLine = "";
            for (int x = 0; x < Texture2D.width; x++)
            {
                if (Texture2D.GetPixel(x, y) == Color.black)
                {
                    newLine += '1';
                }
                else if (Texture2D.GetPixel(x, y) == Color.white)
                {
                    newLine += '0';
                }
                else
                {
                    newLine += ' ';
                }
            }
            lines.Add(newLine);
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
}
