using UnityEngine;
using System.Collections;


public class tablero_Script : MonoBehaviour {

    // Use this for initialization

    private Color colorAzul = new Color(0F, 0F, 1.0F, 1.0F);
    private Color colorRojo = new Color(1.0F, 0F, 0F, 1.0F);
    private Color colorVerde = new Color(0.0F, 1F, 0F, 1.0F);
    private Color colorFabada = new Color(0.9765625F, 0.7265625F, 0.8515625F, 1.0F);
    public Ciudad[] arr_ciudades = new Ciudad[64];
    public Combate combate;


    //tablero = 0 -> vacio 

    GameObject[,] tableroGrafico = new GameObject[Constants.TAMAÑO, Constants.TAMAÑO];
    [SerializeField]
    private GameObject ciudad;
    private int estado;
    private int numCombate;
    private int numGeneracion=0;

    void Start() {
        //inicializacion de las variables
        //cada ciudad se corresponde con un numero
        for (int i = 0; i < arr_ciudades.Length; i++) {
            arr_ciudades[i] = new Ciudad();
        }

        combate = new Combate(arr_ciudades[0], arr_ciudades[1], arr_ciudades[2], arr_ciudades[3]);
        numCombate = 1;
        estado = 3;
        graficosIni();
        Random.seed = (int)System.DateTime.Now.Millisecond;
        actualizarGraficos();

    }

    // Update is called once per frame
    void Update() {

        //estado 1, combatiendo
        if (estado == 1) {
            // Debug.Log("1");

            //ejecutarCombate nos devuelve true si solo queda una ciudad viva
            combate.ejecutarCombate();
            if (combate.combateTerminado())
            {
                estado = 2;
                numGeneracion++;
            }
            //Debug.Log("hola holita");
            //estado = 3;
        }
        //estado 2, transicion entre combates
        if (estado == 2) {
            //despejar el tablero
            //Debug.Log("estado 2");
            //graficosIni();
            combate = new Combate(arr_ciudades[4*numCombate], arr_ciudades[4 * numCombate+1], arr_ciudades[4 * numCombate+2], arr_ciudades[4 * numCombate+3]);
            numCombate++;
            if (numCombate == 64 / 4)
            {
                estado = 3;
            }
            else
            {
                estado = 1;
            }


        }
        //estado 3, reproduccion
        if (estado == 3) {
            //seleccionar pareja a reproducir en funcion de fitness
            Debug.Log("toca reproducir");
            writeFile(numGeneracion);
            //ejecutar reproduccion
            arr_ciudades=Ciudad.ordenaArrCiudades(arr_ciudades);

        }
        if (estado == 0) {
            Debug.Log("estado 0");
            estado = 2;
        }


        actualizarGraficos();
        /*
        for (int i = 0; i < 4; i++) {
            //ejecutarcombate(arr_ciudades[i]);
        }


            actualizarGraficos();*/
    }



    private void graficosIni() {
        for (int i = 0; i < Constants.TAMAÑO; i++) {
            for (int j = 0; j < Constants.TAMAÑO; j++) {
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
    private void actualizarGraficos() {
        for (int i = 0; i < Constants.TAMAÑO; i++) {
            for (int j = 0; j < Constants.TAMAÑO; j++) {
                pintarCuadro(i, j);
            }
        }
    }

    private void pintarCuadro(int i, int j) {
        int[,] tablero = combate.tablero.getTablero();
        int aux = tablero[i, j];
        Color color = new Color(0, 0, 0, 1);
        switch (aux) {
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
    private void writeFile(int numGeneracion)
    {
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@".\generacion_"+numGeneracion+".txt", true))
        {
            file.WriteLine("Generacion "+ numGeneracion);
            for(int i = 0; i < arr_ciudades.Length; i++)
            {
                file.WriteLine("Ciudad " + i);
                file.WriteLine("\tFitness " + arr_ciudades[i].getFitness());
                file.WriteLine("\tCiudadanos " + arr_ciudades[i].getCiudadanos());
                file.WriteLine("\tMilitar " + arr_ciudades[i].getMilitar());
                file.WriteLine("\tMinas " + arr_ciudades[i].getMinas());
                file.WriteLine("\tGranjas " + arr_ciudades[i].getGranjas());
                file.WriteLine("\tValentia " + arr_ciudades[i].getValentia());
                file.WriteLine();
            }
        }
    }
       

}
