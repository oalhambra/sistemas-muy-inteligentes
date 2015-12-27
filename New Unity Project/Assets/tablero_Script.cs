using UnityEngine;
using System.Collections;

public class tablero_Script : MonoBehaviour
{

    // Use this for initialization
    public const int AZUL = 1;//#0000FF
    public const int ROJO = 2;//#FF0000
    public const int VERDE = 3;//#00FF00
    public const int MORADO = 4;//#FABADA
    public const int PUNTOS = 20;
    public const int TAMAÑO = 25;  
    public Color colorAzul = new Color(0F, 0F, 1.0F, 1.0F);
    public Color colorRojo = new Color(1.0F, 0F, 0F, 1.0F);
    public Color colorVerde = new Color(0F, 1.0F, 0F, 1.0F);
    public Color colorFabada = new Color(0.9765625F, 0.7265625F, 0.8515625F, 1.0F);
    public Ciudad[] arr_ciudades = new Ciudad[64];

    //tablero =0 -> vacio 
    int[,] tablero = new int[TAMAÑO, TAMAÑO];
    GameObject[,] tableroGrafico = new GameObject[TAMAÑO, TAMAÑO];
    [SerializeField]
    private GameObject ciudad;

    void Start()
    {
        //inicializacion de las variables
        //cada ciudad se corresponde con un numero
        for (int i = 0; i < arr_ciudades.Length; i++)
        {
            arr_ciudades[i] = new Ciudad();
        }
           
        inicializarTablero();
        Debug.Log(tablero.Length);
        graficosIni();
        Random.seed = (int)System.DateTime.Now.Millisecond;
        actualizarGraficos();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 4; i++) {
            //ejecutarcombate(arr_ciudades[i]);
        }


            actualizarGraficos();
    }

    private void inicializarTablero()
    {

        for (int i = 0; i < TAMAÑO; i++)
        {
            for (int j = 0; j < TAMAÑO; j++)
            {
                tablero[i, j] = 0;
            }
        }

        tablero[0, 0] = AZUL;
        tablero[TAMAÑO-1, 0] = ROJO;
        tablero[0, TAMAÑO-1] = VERDE;
        tablero[TAMAÑO - 1, TAMAÑO-1] = MORADO;
    }

    private void graficosIni()
    {
        for (int i = 0; i < TAMAÑO; i++)
        {
            for (int j = 0; j < TAMAÑO; j++)
            {
                tableroGrafico[i, j] = (GameObject)Instantiate(ciudad, new Vector3(3 * i, 0, 3 * j), new Quaternion());
            }
        }

        /*
        GameObject test1 = (GameObject)Instantiate(ciudad, new Vector3(0, 0, 0), new Quaternion());
        test1.GetComponent<Renderer>().material.color=color4;
        Debug.Log("test1 con color");
        GameObject test2 = (GameObject)Instantiate(ciudad, new Vector3(25, 0, 0), new Quaternion());
        test2.GetComponent<Renderer>().material.color = color4;
        Debug.Log("test2 con color"); Debug.Log(tablero.Length);
         */
    }
    private void actualizarGraficos() 
    {
        for (int i = 0; i < TAMAÑO; i++)
        {
            for (int j = 0; j < TAMAÑO; j++)
            {
                pintarCuadro(i, j);
            }
        }
    }

    private void pintarCuadro(int i, int j)
    {
        int aux = tablero[i, j];
        Color color=new Color(1,1,1,1);
        switch (aux)
        {
            case 1:
                color = colorAzul;
                break;
            case 2:
                color = colorRojo;
                break;
            case 3:
                color = colorVerde;
                break;
            case 4:
                color = colorFabada;
                break;
        }
        
        tableroGrafico[i, j].GetComponent<Renderer>().material.color = color;
    }




    public class Ciudad
    {
        int color;
        int[] stats = new int[PUNTOS];
        const int CIUDADANOS = 1;
        const int MILITAR = 2;
        const int MINAS = 3;
        const int GRANJAS = 4;
        //const int VALENTIA = 5;

        int poblacion;
        int ejercito;
        int fitness;

        public Ciudad()
        {
            
            inicializarStats();
            //int randomNumber = Random.Range(1, 5);

        }
        public Ciudad(int[] stats)
        {
            //si te explota en la cara arreglalo
            this.stats = stats;
        }
        private void inicializarStats()
        {
            for (int i = 0; i < stats.Length; i++)
            {
                stats[i] = Random.Range(1, 5);
            }
        }
        private static Ciudad reproducir(Ciudad ciudad1, Ciudad ciudad2)
        {
            int[] stats = new int[PUNTOS];
            int randomNumber = Random.Range(0, PUNTOS + 1);
            for (int i = 0; i < randomNumber; i++)
            {
                stats[i] = ciudad1.stats[i];
            }
            for (int i = randomNumber; i < stats.Length; i++)
            {
                stats[i] = ciudad2.stats[i];
            }

            return new Ciudad(stats);
        }
    }
}
