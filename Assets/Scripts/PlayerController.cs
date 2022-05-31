using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Variables del movimiento del personaje
    public float jumpForce = 8f;        //Public la habilita para trabajar en Unity - Se crea la fuerza con la que va a saltar
    public float runningSpeed = 2f;     //Se crea una variable para determinar la velocidad de movimiento del personaje
    Rigidbody2D rigidBody;              //Se crea una variable llamada rigidbody de propiedades Rigidbody2d
    public LayerMask groundMask;        //se genera una capa que se va a encargar de tener registro quien es parte del suelo
    Animator animator;                  //Se crea una variable animator con propiedades animator.
    Vector3 startPosition;              //Se crea una variable de vector 3d

    const string STATE_ALIVE = "isAlive";   //Se crea una variable que va a ser constante, de tipo string (se pone en mayuscula por convencion, todas las constantes se llamaran asi)
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    private void Awake()                //Awake es el frame numero 0, aqui se pueden iniciailizar variables, aqui es donde todo despierta
    {
        rigidBody = GetComponent<Rigidbody2D>();    //GetComponent : busca otro componente del mismo GameObject componentes: Collider, Rigidbody, etc
                                                    //Getcomponent se finaliza con ()
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()                        //Inicializa el juego
    {
        startPosition = this.transform.position;        //Se guarda la posicion inicial en startposition para llevar el personaje a esta posicion siempre que muera
    }


    public void StartGame()        //Funciomn que se utilizara para reiniciar la partida
    {
        animator.SetBool(STATE_ALIVE, true);    //al iniciar el juego le da los valores de verdadero a que el personaje este vivo
        animator.SetBool(STATE_ON_THE_GROUND, true);

        Invoke("RestartPosition", 0.2f);        //Invoke lo que hace es llamar una función pero con un retardo que yo le quiera dar+++
    }

    void RestartPosition()      //Se crea una funcion para reestablecer la posicion, esto para que no se haga todo en el mismo frame
    {
        this.transform.position = startPosition;        //va a llevar al personaje a la posicion de inicio
        this.rigidBody.velocity = Vector2.zero;         //Pondra la velocidad del personaje en cero para que inicie como siempre
    }
    // Update is called once per frame
    void Update()                       //Se ejecuta hasta 60 veces por segundo (si el juego va a 60 frame)
    {
    if (Input.GetButtonDown("Jump"))   //Input para cuando se requiere una entrada de datos
                                                                        //Getkey cuando se va a verificar que se presiona una tecla 
                                                                        //Keycode.space (tecla barra espaceadora)
                                                                        //Getmousebuttondown cuando se va a verificar que se presiona un boton del mouse
        {
            Jump();
        }

        animator.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());


        //Las clases van con mayuscula al inicio y las variables con minuscula 
        Debug.DrawRay(this.transform.position, Vector2.down * 1.5f, Color.red); //Debug es para entrar en modo de prueba
                                                                                //DrawRay dibuja un rayo
                                                                                //this.transform.position empezar desde el centro del personaje name.transform.position [El name es el nombre que le colocas para hacer la referencia.]
    }

    void FixedUpdate()      // Se ejecuta como un reloj, una cantidad de veces por segundo (se utiliza para que no se vea afectado el rendimiento por los frames)
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)    //Si se esta en estado in game
        {
            if (rigidBody.velocity.x < runningSpeed)
            {
                rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);   //componente en x y componente en y
            }
        } else
        {
            rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);  // Si no estamos dentro de la partida el personaje debe parar solo en el eje X
           
        }
    }

    void Jump()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)    //Solo se puede saltar si el estado del juego es in game
        {

            if (IsTouchingTheGround())
            {

                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);    //Al rigidbody es al que se le aplican las fuerzas
                                                                                    //Vector2 se utiliza para darle la direccion a la fuerza y se multiplica por el valor de la fuerza
                                                                                    //Forcemode2d me determina como estoy aplicando la fuerza, si es constante o un impulso
            }
        }
    }

    bool IsTouchingTheGround()      //funcion que devolvera un bool si se esta tocando el suelo
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1.5f, groundMask)) //invoca el motor de fisicas - raycast para trazar un rayo invisible
                                                                                        //parametros del raycast: el orden importa (1.posicion actual del personaje, 2.direccion hacia donde se traza el rayo, 3.de que longitud, 4. contra que chocara)
        {
            //animator.enabled = true;    //si esta tocando el piso activa la animacion
            return true;
        }
        else
        {
            //animator.enabled = false;   //si no esta tocando el piso desactiva la animacion (la pausa)
            return false;
        }
    }

    public void Die ()      //Funcion de muerte
    {
        this.animator.SetBool(STATE_ALIVE, false);      //Se cambia de estado el statealive a false para activar la animacion de muerte

        GameManager.sharedInstance.GameOver();      //Le decimos al gamemanager que pasamos al estado gameover
    }
}