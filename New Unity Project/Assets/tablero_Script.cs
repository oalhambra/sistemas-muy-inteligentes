using UnityEngine;
using System.Collections;

public class tablero_Script : MonoBehaviour
{

    // Use this for initialization
    public const int AZUL = 1;
    public const int ROJO = 2;
    public const int VERDE = 3;
    public const int MORADO = 4;
    public const int PUNTOS = 20;
    //tablero =0 -> vacio 
    int[,] tablero = new int[25, 25];
    void Start()
    {
        //inicializacion de las variables
        //cada ciudad se corresponde con un numero
        Random.seed = (int)System.DateTime.Now.Millisecond;
        inicializarTablero();


    }

    // Update is called once per frame
    void Update()
    {

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
