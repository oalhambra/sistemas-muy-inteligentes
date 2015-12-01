using UnityEngine;
using System.Collections;

public class tablero_Script : MonoBehaviour {

	// Use this for initialization
    public const int AZUL = 1;
    public const int ROJO = 2;
    public const int VERDE = 3;
    public const int MORADO = 4;
    public const int PUNTOS = 20;
    //tablero =0 -> vacio 
    int[,] tablero=new int[25,25];
	void Start () {
        //inicializacion de las variables
        //cada ciudad se corresponde con un numero

        inicializarTablero();
	    
        
	}
	
	// Update is called once per frame
	void Update () {
	
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
    private Ciudad reproducir(Ciudad c1, Ciudad c2){
        Ciudad res=null;
        
        return res;

    }
    private class Ciudad{
        int color;
        int[] stats = new int[PUNTOS];
        const int CIUDADANOS = 1;
        const int MILITAR = 2;
        const int MINAS = 3;
        const int GRANJAS = 4;
        int poblacion;
        int ejercito;
        int fitness;
        


        public Ciudad()
        {
            Random.seed=(int)System.DateTime.Now.Millisecond;
            System.Threading.Thread.Sleep(1);
            inicializarStats();
            int randomNumber = Random.Range(1, 5);

        }
        public Ciudad(int[] stats){
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

       


    }

}
