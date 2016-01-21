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
    private int numGeneracion = 0;

    void Start() {
        //inicializacion de las variables
        //cada ciudad se corresponde con un numero
        Random.seed = (int)System.DateTime.Now.Millisecond;
        for (int i = 0; i < arr_ciudades.Length; i++) {
            arr_ciudades[i] = new Ciudad();
        }
        numCombate = 0;
        estado = 2;
        graficosIni();

    }

    // Update is called once per frame
    void Update() {

        //estado 1, combatiendo
        if (estado == 1) {
            // Debug.Log("1");

            //ejecutarCombate nos devuelve true si solo queda una ciudad viva
            combate.ejecutarCombate();
            if (combate.combateTerminado()) {
                estado = 2;

            }
            //Debug.Log("hola holita");
            //estado = 3;
        }
        //estado 2, transicion entre combates
        if (estado == 2) {
            //despejar el tablero
            //Debug.Log("estado 2");
            //graficosIni();


            Debug.Log("el numero del combate es: " + numCombate);
            if (numCombate > 15) {
                //reproduce
                estado = 3;
                numGeneracion++;
                numCombate = 0;
            }
            else {
                //combaten
                combate = new Combate(arr_ciudades[4 * numCombate], arr_ciudades[4 * numCombate + 1], arr_ciudades[4 * numCombate + 2], arr_ciudades[4 * numCombate + 3]);

                estado = 1;
                numCombate++;
            }


        }
        //estado 3, reproduccion
        if (estado == 3) {
            //seleccionar pareja a reproducir en funcion de fitness
            Debug.Log("toca reproducir");
            writeFile(numGeneracion);
            //ejecutar reproduccion
            arr_ciudades = Ciudad.ordenaArrCiudades(arr_ciudades);
            int[] fitnessAcumulado = new int[64];
            fitnessAcumulado[0] = arr_ciudades[0].getFitness();
            for (int i = 1; i < 64; i++) {
                fitnessAcumulado[i] = arr_ciudades[i].getFitness() + fitnessAcumulado[i - 1];
            }
            Ciudad[] nuevasCiudades = new Ciudad[16];
            int j;
            int k;
            for (int i = 0; i < 16; i++) {
                int valRandom = Random.Range(0, fitnessAcumulado[63]);
                Debug.Log(valRandom);
                j = 0;
                while (valRandom > fitnessAcumulado[j] && j < 64) {
                    j++;
                }
                k = 0;
                while (valRandom > fitnessAcumulado[k] && k < 64) {
                    k++;
                }
                nuevasCiudades[i] = arr_ciudades[j].reproducir(arr_ciudades[k]);
            }
            for (int i = 0; i < 16; i++) {
                arr_ciudades[i + 48] = nuevasCiudades[i];
            }
            arr_ciudades = randomizar(arr_ciudades);
            estado = 1;
            Debug.Log("acaba una generacion");

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
    private void writeFile(int numGeneracion) {
        Debug.Log("Se ha llamado a writefile con el array de long " + arr_ciudades.Length);
        using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@".\generacion_" + numGeneracion + ".txt", true)) {
            
            file.WriteLine("Generacion " + numGeneracion);
            for (int i = 0; i < arr_ciudades.Length; i++) {
                file.WriteLine("Ciudad " + i);
                file.WriteLine("\tFitness " + arr_ciudades[i].getFitness());
                file.WriteLine("\tCiudadanos " + arr_ciudades[i].getCiudadanos());
                file.WriteLine("\tMilitar " + arr_ciudades[i].getMilitar());
                file.WriteLine("\tMinas " + arr_ciudades[i].getMinas());
                file.WriteLine("\tGranjas " + arr_ciudades[i].getGranjas());
                file.WriteLine("\tValentia " + arr_ciudades[i].getValentia());
                file.WriteLine();
            }
            file.Flush();
            file.Close();
        }
    }
    private Ciudad[] randomizar(Ciudad[] arrCiudades) {
        Ciudad[] resultado = new Ciudad[arrCiudades.Length];
        for (int i = 0; i < resultado.Length; i++) {
            int valRandom = Random.Range(0, resultado.Length - i);
            resultado[i] = arrCiudades[valRandom];
            for (int j = valRandom; j < arrCiudades.Length - 1; j++) {
                arrCiudades[j] = arrCiudades[j + 1];
            }
        }
        return resultado;
    }


}
