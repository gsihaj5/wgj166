using System;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;

    // Start is called before the first frame update
    void Start() {
        generateLevel();
    }

    void generateLevel(){
        for(int y = 0; y < map.height; y++){
            for(int x = 0; x < map.width; x++){
                generateTile(x,y);
            }
        }
    }

    void generateTile(int x, int y){
        Color pixelColor = map.GetPixel(x,y);

        foreach(ColorToPrefab colorMapping in colorMappings){
            if(colorMapping.color.Equals(pixelColor)){
                Vector2 position = new Vector2(x, y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        } 
    } 
}
