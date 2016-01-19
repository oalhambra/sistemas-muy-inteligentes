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

    void Start() {
        //inicializacion de las variables
        //cada ciudad se corresponde con un numero
        for (int i = 0; i < arr_ciudades.Length; i++) {
            arr_ciudades[i] = new Ciudad();
        }

        combate = new Combate(arr_ciudades[0], arr_ciudades[1], arr_ciudades[2], arr_ciudades[3]);
        graficosIni();
        Random.seed = (int)System.DateTime.Now.Millisecond;
        actualizarGraficos();

    }

    // Update is called once per frame
    void Update() {

        //estado 1, combatiendo
        if (estado == 1) {
           // Debug.Log("1");
            combate.ejecutarCombate();
            //Debug.Log("hola holita");
            //estado = 3;
        }
        //estado 2, transicion entre combates
        if (estado == 2) {
            //despejar el tablero
            //Debug.Log("estado 2");
            estado = 1;
            //reinicializar combate con nuevas ciudades de arr_ciudades

        }
        //estado 3, reproduccion
        if (estado == 3) {
            //seleccionar pareja a reproducir en funcion de fitness

            //ejecutar reproduccion

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
       

}
