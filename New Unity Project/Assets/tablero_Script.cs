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
    public Combate combate;

    //tablero =0 -> vacio 

    GameObject[,] tableroGrafico = new GameObject[TAMAÑO, TAMAÑO];
    [SerializeField]
    private GameObject ciudad;
    private int estado;

    void Start()
    {
        //inicializacion de las variables
        //cada ciudad se corresponde con un numero
        for (int i = 0; i < arr_ciudades.Length; i++)
        {
            arr_ciudades[i] = new Ciudad();
        }

        combate.inicializarTablero();
        //Debug.Log(tablero.Length);
        graficosIni();
        Random.seed = (int)System.DateTime.Now.Millisecond;
        actualizarGraficos();
    }

    // Update is called once per frame
    void Update()
    {
        //estado 1, combatiendo
        if (estado == 1)
        {
            combate.ejecutarCombate();
        }
        //estado 2, transicion entre combates
        if (estado == 2)
        {
            //despejar el tablero

            //reinicializar combate con nuevas ciudades de arr_ciudades

        }
        //estado 3, reproduccion
        if (estado == 3)
        {
            //seleccionar pareja a reproducir en funcion de fitness

            //ejecutar reproduccion

        }








        /*
        for (int i = 0; i < 4; i++) {
            //ejecutarcombate(arr_ciudades[i]);
        }


            actualizarGraficos();*/
    }



    private void graficosIni()
    {
        for (int i = 0; i < TAMAÑO; i++)
        {
            for (int j = 0; j < TAMAÑO; j++)
            {
                tableroGrafico[i, j] = (GameObject)Instantiate(ciudad, new Vector3(5 * i, 0, 5 * j), new Quaternion());
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
        int[,] tablero = combate.getTablero();
        int aux = tablero[i, j];
        Color color = new Color(1, 1, 1, 1);
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
        /*
        Material aux2 = new Material("");
        aux2.color = color;
        Material[] materiales=tableroGrafico[i, j].GetComponents<Material>();

        foreach(Material a in materiales)
        {
            a.SetColor("", color);
        }
        //tableroGrafico[i, j].;*/
    }

    //////////////////////////////////////////////////////////////////////////////


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

        int tamaño;

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
        public int getPoblacion()
        {
            return poblacion;
        }
        public int getEjercito()
        {
            return ejercito;
        }
        public int getTamaño()
        {
            return tamaño;
        }
        public void setPoblacion(int poblacion)
        {
            this.poblacion = poblacion;
        }
        public void setEjercito(int ejercito)
        {
            this.ejercito = ejercito;
        }
        public void setTamaño(int tamaño)
        {
            this.tamaño = tamaño;
        }
        public void expandir()//devuelve true si se expande correctamente, false en caso contrario, incrementa el tamaño
        {
            bool exito = false;
            //obtener listado de posiciones posibles para la expansion
            //clasificadas en funcion de expansion libre o combate
            //seleccionar una
            //si es expansion libre, exito=true
            //si es combate, realizar combate y aplicar valor a exito


            if (exito)
            {
                tamaño++;
            }
        }
        public int getCiudadanos()
        {
            int ciudadanos = 0;

            foreach (int valor in stats)
            {
                if (valor == CIUDADANOS)
                {
                    ciudadanos++;
                }
            }
            return ciudadanos;
        }
        public int getMilitar()
        {
            int militar = 0;

            foreach (int valor in stats)
            {
                if (valor == MILITAR)
                {
                    militar++;
                }
            }
            return militar;
        }
    }



    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 


    public class Combate
    {
        Ciudad[] ciudades = new Ciudad[4];
        int[,] tablero = new int[TAMAÑO, TAMAÑO];

        public Combate(Ciudad[] ciudades)
        {
            this.ciudades = ciudades;
            inicializarTablero();
            foreach (Ciudad ciudad in ciudades)
            {
                prepararCiudad(ciudad);
            }
        }
        public void ejecutarCombate()
        {
            foreach (Ciudad ciudad in ciudades)
            {
                ejecutarTurno(ciudad);
            }

        }
        private void ejecutarTurno(Ciudad ciudad)
        {
            //comprobar si la ciudad sigue activa
            if (ciudad.getTamaño() != 0)
            {
                //incrementa valores de la ciudad
                ciudad.setPoblacion(ciudad.getPoblacion() + 1 + ciudad.getCiudadanos());
                ciudad.setEjercito(ciudad.getEjercito() + 1 + ciudad.getMilitar());
                //comprueba expansion
                while ((((ciudad.getPoblacion() / 5) + 1) - ciudad.getTamaño()) != 0)
                {
                    ciudad.expandir();
                }
            }


        }
        public int[,] getTablero()
        {
            return tablero;
        }
        public void inicializarTablero()
        {


            for (int i = 0; i < TAMAÑO; i++)
            {
                for (int j = 0; j < TAMAÑO; j++)
                {
                    tablero[i, j] = 0;
                }
            }

            tablero[0, 0] = AZUL;
            tablero[TAMAÑO - 1, 0] = ROJO;
            tablero[0, TAMAÑO - 1] = VERDE;
            tablero[TAMAÑO - 1, TAMAÑO - 1] = MORADO;
        }
        public void prepararCiudad(Ciudad ciudad)
        {
            ciudad.setEjercito(0);
            ciudad.setPoblacion(1);
            ciudad.setTamaño(1);
        }

    }
}
