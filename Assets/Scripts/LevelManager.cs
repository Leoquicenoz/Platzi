using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager sharedInstance;  //Creamos el Singleton.

    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    

    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    public Transform levelStartPosition;


    private void Awake()
    {
        if (sharedInstance == null) //Inicializamos el Singleton
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLevelBlock() //Agregara automaticamente bloques de nivel
    {

    }

    public void RemoveLevelBlock () //Elimina automaticamente bloques de nivel
    {

    }

    public void RemoveAllLevelBlocks () //Elimina automaticamente todos los bloques de nivel cuando el personaje muere
    {

    }

    public void GenerateInitialBlocks ()    //Genera los bloques iniciales
    {
        for (int i=0; i<2; i++)
        {
            AddLevelBlock();
        }
    }
}
