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
        int randomIdx = Random.Range(0, allTheLevelBlocks.Count); //Se crea una variable entera en la que se asignara un numero aleatorio entre 0 y el numero maximo
                                                                  // de level blocks, random.range genera numeros aleatorios donde todos son equiprobables
        LevelBlock block;   // declaro una variable llamada block, que sea de tipo levelblock

        Vector3 spawnPosition = Vector3.zero;   //declaro una variable llamada spawnPosition que es de tipo vector3 y la inicializo en cero

        if (currentLevelBlocks.Count == 0)
        {
            block = Instantiate(allTheLevelBlocks[0]);  //Si no hay ningun levelblock, entonces se instanciara (creará) uno que sera el 0
            spawnPosition = levelStartPosition.position;    //se asignará la posicion del levelstarposition a la variable spawposition
        }   
        else
        {
            block = Instantiate(allTheLevelBlocks[randomIdx]);  //Crea un bloque random
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position; //se asigna la posicion del punto de salida del bloque anterior al actual
        }

        block.transform.SetParent(this.transform, false);   //Se establece con setparent, que todos los bloques creados van a ser hijos de este level manager (this)
                                                            // el false se agrega para que los bloques no tengan las transformaciones del padre (estirados, alargados, etc)

        Vector3 correction = new Vector3(spawnPosition.x - block.startPoint.position.x, spawnPosition.y - block.startPoint.position.y, 0);
        //Se restan las posiciones de cada vector y como es 2D, en z se pone 0  

        block.transform.position = correction;
        currentLevelBlocks.Add(block);
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
