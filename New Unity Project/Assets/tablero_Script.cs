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
    //tablero =0 -> vacio 
    int[,] tablero = new int[25, 25];
    [SerializeField]
    private GameObject ciudad;

    [SerializeField]
    private Material material1;
    [SerializeField]
    private Material material2;
    [SerializeField]
    private Material material3;
    [SerializeField]
    private Material material4;


    void Start()
    {
        //inicializacion de las variables
        //cada ciudad se corresponde con un numero
        graficosIni();
        Random.seed = (int)System.DateTime.Now.Millisecond;
        inicializarTablero();


    }

    // Update is called once per frame
    void Update()
    {
        //Object test1 = Instantiate(ciudad, new Vector3(0, 0, 0), new Quaternion());
    }

    private void inicializarTablero()
    {
        for (int i = 0; i < tablero.Length; i++)
        {
            for (int j = 0; j < tablero.Length; j++)
            {
                tablero[i, j] = 0;
            }
        }

        tablero[0, 0] = AZUL;
        tablero[tablero.Length, 0] = ROJO;
        tablero[0, tablero.Length] = VERDE;
        tablero[tablero.Length, tablero.Length] = MORADO;
    }

    private void graficosIni()
    {
        GameObject test1 = (GameObject)Instantiate(ciudad, new Vector3(0, 0, 0), new Quaternion());
        test1.GetComponent<Renderer>().material.color=material1.color;
        Debug.Log("test1 con color");
        GameObject test2 = (GameObject)Instantiate(ciudad, new Vector3(25, 0, 0), new Quaternion());
        test2.GetComponent<Renderer>().material.color = material2.color;
        Debug.Log("test2 con color");
    }

    private class Ciudad
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
