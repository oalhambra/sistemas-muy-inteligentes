using UnityEngine;
using System.Collections;

public class tablero_Script : MonoBehaviour {

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

    void Start() {
        //inicializacion de las variables
        //cada ciudad se corresponde con un numero
        for (int i = 0; i < arr_ciudades.Length; i++) {
            arr_ciudades[i] = new Ciudad();
        }
        combate = new Combate(arr_ciudades[0], arr_ciudades[0], arr_ciudades[0], arr_ciudades[0]);
        graficosIni();
        Random.seed = (int)System.DateTime.Now.Millisecond;
        actualizarGraficos();

    }

    // Update is called once per frame
    void Update() {

        //estado 1, combatiendo
        if (estado == 1) {
            Debug.Log("1");
            combate.ejecutarCombate();
            Debug.Log("hola holita");
            //estado = 3;
        }
        //estado 2, transicion entre combates
        if (estado == 2) {
            //despejar el tablero
            Debug.Log("estado 2");
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
        for (int i = 0; i < TAMAÑO; i++) {
            for (int j = 0; j < TAMAÑO; j++) {
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
        for (int i = 0; i < TAMAÑO; i++) {
            for (int j = 0; j < TAMAÑO; j++) {
                pintarCuadro(i, j);
            }
        }
    }

    private void pintarCuadro(int i, int j) {
        int[,] tablero = combate.getTablero();
        int aux = tablero[i, j];
        Color color = new Color(1, 1, 1, 1);
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


    public class Ciudad {
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

        public Ciudad() {

            inicializarStats();
            //int randomNumber = Random.Range(1, 5);

        }
        public Ciudad(int[] stats) {
            //si te explota en la cara arreglalo
            this.stats = stats;
        }
        private void inicializarStats() {
            for (int i = 0; i < stats.Length; i++) {
                stats[i] = Random.Range(1, 5);
            }
        }
        private static Ciudad reproducir(Ciudad ciudad1, Ciudad ciudad2) {
            int[] stats = new int[PUNTOS];
            int randomNumber = Random.Range(0, PUNTOS + 1);
            for (int i = 0; i < randomNumber; i++) {
                stats[i] = ciudad1.stats[i];
            }
            for (int i = randomNumber; i < stats.Length; i++) {
                stats[i] = ciudad2.stats[i];
            }

            return new Ciudad(stats);
        }
        public int getPoblacion() {
            return poblacion;
        }
        public int getEjercito() {
            return ejercito;
        }
        public int getTamaño() {
            return tamaño;
        }
        public void setPoblacion(int poblacion) {
            this.poblacion = poblacion;
        }
        public void setEjercito(int ejercito) {
            this.ejercito = ejercito;
        }
        public void setTamaño(int tamaño) {
            this.tamaño = tamaño;
        }

        public tableroYBool expandir(int[,] tablero)//devuelve true si se expande correctamente, false en caso contrario, incrementa el tamaño
        {
            bool exito = false;
            //obtener listado de posiciones posibles para la expansion
            //clasificadas en funcion de expansion libre o combate
            //seleccionar una
            //si es expansion libre, exito=true
            //si es combate, realizar combate y aplicar valor a exito

            // movimientosPosibles valors: 0 -> no se puede llegar. 1 -> vacio. 2 -> Ciudad enemiga
            int[,] movimientosPosibles = new int[TAMAÑO, TAMAÑO];

            // Crear funcion ponerCosoACeros si eso
            for (int i = 0; i < TAMAÑO; i++) {
                for (int j = 0; j < TAMAÑO; j++) {
                    movimientosPosibles[i, j] = 0;
                }
            }
            for (int i = 0; i < TAMAÑO; i++) {
                for (int j = 0; j < TAMAÑO; j++) {
                    // TODO: Este color hay que inicializarlo
                    if (tablero[i, j] == this.color) {
                        // Sacar esto a una funcion para comprobar los 4 casos, cada uno con una llamada
                        // Le pasamos el movimientosPosibles y que lo devuelva modificado, a no ser que 
                        // consigamos pasarle un puntero
                        movimientosPosibles = evaluarPosicion(tablero, i + 1, j, movimientosPosibles);
                        movimientosPosibles = evaluarPosicion(tablero, i - 1, j, movimientosPosibles);
                        movimientosPosibles = evaluarPosicion(tablero, i, j + 1, movimientosPosibles);
                        movimientosPosibles = evaluarPosicion(tablero, i, j - 1, movimientosPosibles);
                    }
                }
            }
            //hacemos una lista de posiciones libres y de posiciones ocupadas
            Coordenadas[] arrLibres = posicionesCoincidentes(1, movimientosPosibles);
            Coordenadas[] arrOcupadas = posicionesCoincidentes(2, movimientosPosibles);
            // Falta elegir una random
            if (arrLibres != null) {
                // Se puede hacer un movimiento a un espacio libre
                Debug.Log("Hay una opcion de desplazamiento. La long del arrLibres es " + arrLibres.Length);
                //seleccionar posicion a random
                exito = true;
                Coordenadas posExpansion = arrLibres[Random.Range(0, arrLibres.Length)];
                tablero[posExpansion.getI(), posExpansion.getJ()] = this.color;

            }
            else {
                // Hay que luchar si o si

                //TODO seleccionar posicion de arrOcupadas a random
                //TODO realizar combate


            }

            if (exito) {
                tamaño++;
            }


            return new tableroYBool(tablero, exito);

        }
        //evalua si la posicion pasada es posible para la expansion y que tipo de expansion seria
        private int[,] evaluarPosicion(int[,] tablero, int iAEvaluar, int jAEvaluar, int[,] movimientosPosibles) {
            if (TAMAÑO > iAEvaluar && iAEvaluar >= 0 && TAMAÑO > jAEvaluar && jAEvaluar >= 0 && tablero[iAEvaluar, jAEvaluar] != this.color) {
                if (tablero[iAEvaluar, jAEvaluar] == 0) {
                    movimientosPosibles[iAEvaluar, jAEvaluar] = 1;
                }
                else {
                    movimientosPosibles[iAEvaluar, jAEvaluar] = 2;
                }
            }
            return movimientosPosibles;
        }
        private Coordenadas[] posicionesCoincidentes(int val, int[,] movimientosPosibles) {
            Coordenadas[] resultado = new Coordenadas[] { };
            Coordenadas aux;
            for (int i = 0; i < TAMAÑO; i++) {
                for (int j = 0; j < TAMAÑO; j++) {
                    if (movimientosPosibles[i, j] == val) {
                        aux = new Coordenadas(i, j);
                        resultado = aux.concat(resultado);
                    }
                }
            }
            return resultado;
        }
        public int getCiudadanos() {
            int ciudadanos = 0;

            foreach (int valor in stats) {
                if (valor == CIUDADANOS) {
                    ciudadanos++;
                }
            }
            return ciudadanos;
        }
        public int getMilitar() {
            int militar = 0;

            foreach (int valor in stats) {
                if (valor == MILITAR) {
                    militar++;
                }
            }
            return militar;
        }
        public void setColor(int color) {
            this.color = color;
        }
    }



    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 


    public class Combate {

        Ciudad ciudad1;
        Ciudad ciudad2;
        Ciudad ciudad3;
        Ciudad ciudad4;
        int[,] tablero = new int[TAMAÑO, TAMAÑO];

        public Combate(Ciudad ciudad1, Ciudad ciudad2, Ciudad ciudad3, Ciudad ciudad4) {
            this.ciudad1 = ciudad1;
            prepararCiudad(ciudad1, AZUL);
            this.ciudad2 = ciudad2;
            prepararCiudad(ciudad2, ROJO);
            this.ciudad3 = ciudad3;
            prepararCiudad(ciudad3, VERDE);
            this.ciudad4 = ciudad4;
            prepararCiudad(ciudad4, MORADO);
            inicializarTablero();

        }
        public void ejecutarCombate() {
            Debug.Log("2");
            ciudad1 = ejecutarTurno(ciudad1);
            Debug.Log("3");
            ciudad2 = ejecutarTurno(ciudad2);
            Debug.Log("4");
            ciudad3 = ejecutarTurno(ciudad3);
            Debug.Log("5");
            ciudad4 = ejecutarTurno(ciudad4);
            Debug.Log("6");


        }
        private Ciudad ejecutarTurno(Ciudad ciudad) {
            //comprobar si la ciudad sigue activa
            if (ciudad.getTamaño() != 0) {
                //incrementa valores de la ciudad
                int poblacionAntigua = ciudad.getPoblacion();
                ciudad.setPoblacion(ciudad.getPoblacion() + 1 + ciudad.getCiudadanos());
                ciudad.setEjercito(ciudad.getEjercito() + 1 + ciudad.getMilitar());
                //comprueba expansion
                int num_veces = ((poblacionAntigua % 5) + ciudad.getPoblacion() - poblacionAntigua) / 5;
                for (int i = 0; i < num_veces; i++) {
                    Debug.Log("Se ha llamado a ciudad.expandir, el numero de poblacion ha aumentado " + num_veces + " veces");
                    tableroYBool aux = ciudad.expandir(this.tablero);
                    if (aux.getExito()) {
                        ciudad.setTamaño(ciudad.getTamaño() + 1);
                    }
                    tablero = aux.getTablero();
                }

            }
            return ciudad;


        }
        public int[,] getTablero() {
            return tablero;
        }
        public void inicializarTablero() {


            for (int i = 0; i < TAMAÑO; i++) {
                for (int j = 0; j < TAMAÑO; j++) {
                    tablero[i, j] = 0;
                }
            }

            tablero[0, 0] = AZUL;
            tablero[TAMAÑO - 1, 0] = ROJO;
            tablero[0, TAMAÑO - 1] = VERDE;
            tablero[TAMAÑO - 1, TAMAÑO - 1] = MORADO;
        }
        public void prepararCiudad(Ciudad ciudad, int color) {
            ciudad.setEjercito(0);
            ciudad.setPoblacion(1);
            ciudad.setTamaño(1);
            ciudad.setColor(color);
        }

    }

    //////////////////////////////////////////////////////////////////////////

    public class Coordenadas {
        int i;
        int j;
        public Coordenadas(int i, int j) {
            this.i = i;
            this.j = j;
        }
        public int getI() {
            return i;
        }
        public int getJ() {
            return j;
        }
        public Coordenadas[] concat(Coordenadas[] aux) {
            if (aux != null) {
                Coordenadas[] devolver = new Coordenadas[aux.Length + 1];
                for (int i = 0; i < aux.Length; i++) {
                    devolver[i] = aux[i];
                }
                devolver[devolver.Length - 1] = this;
                return devolver;
            }
            else {
                Coordenadas[] devolver = { new Coordenadas(i, j) };
                return devolver;
            }

        }
    }

    /////////////////////////////////////////////

    public class tableroYBool {
        int[,] tablero;
        bool exito;
        public tableroYBool(int[,] tablero, bool exito) {
            this.tablero = tablero;
            this.exito = exito;
        }
        public int[,] getTablero() {
            return tablero;
        }
        public bool getExito() {
            return exito;
        }
    }

}
