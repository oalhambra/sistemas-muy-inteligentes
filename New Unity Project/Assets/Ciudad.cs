
using UnityEngine;

public class Ciudad {
    int color;
    int[] stats = new int[Constants.PUNTOS];
    const int CIUDADANOS = 1;
    const int MILITAR = 2;
    const int MINAS = 3;
    const int GRANJAS = 4;

    int valentia;

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
        valentia = Random.Range(0, 100);
    }
    private static Ciudad reproducir(Ciudad ciudad1, Ciudad ciudad2) {
        int[] stats = new int[Constants.PUNTOS];
        int randomNumber = Random.Range(0, Constants.PUNTOS + 1);
        for (int i = 0; i < randomNumber; i++) {
            stats[i] = ciudad1.stats[i];
        }
        for (int i = randomNumber; i < stats.Length; i++) {
            stats[i] = ciudad2.stats[i];
        }

        return new Ciudad(stats);
    }
    public static Ciudad[] ordenaArrCiudades(Ciudad[] arrCiudades)
    {
        int i, j;
        int N = arrCiudades.Length;

        for (j = N - 1; j > 0; j--)
        {
            for (i = 0; i < j; i++)
            {
                if (arrCiudades[i].getFitness() > arrCiudades[i + 1].getFitness())
                    exchange(arrCiudades, i, i + 1);
            }
        }
        return arrCiudades;
    }
    private static void exchange(Ciudad[] data, int m, int n)
    {
        Ciudad temporary;

        temporary = data[m];
        data[m] = data[n];
        data[n] = temporary;
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



    public bool expandir(Tablero tablero)//devuelve true si se expande correctamente, false en caso contrario, incrementa el tamaño
    {
        bool exito = false;
        //obtener listado de posiciones posibles para la expansion
        //clasificadas en funcion de expansion libre o combate
        //seleccionar una
        //si es expansion libre, exito=true
        //si es combate, realizar combate y aplicar valor a exito

        // movimientosPosibles valors: 0 -> no se puede llegar. 1 -> vacio. 2 -> Ciudad enemiga
        int[,] movimientosPosibles = new int[Constants.TAMAÑO, Constants.TAMAÑO];

        // Crear funcion ponerCosoACeros si eso
        for (int i = 0; i < Constants.TAMAÑO; i++) {
            for (int j = 0; j < Constants.TAMAÑO; j++) {
                movimientosPosibles[i, j] = 0;
            }
        }
        for (int i = 0; i < Constants.TAMAÑO; i++) {
            for (int j = 0; j < Constants.TAMAÑO; j++) {
                // TODO: Este color hay que inicializarlo
                if (tablero.getTablero()[i, j] == this.color) {
                    // Sacar esto a una funcion para comprobar los 4 casos, cada uno con una llamada
                    // Le pasamos el movimientosPosibles y que lo devuelva modificado, a no ser que 
                    // consigamos pasarle un puntero
                    evaluarPosicion(tablero.getTablero(), i + 1, j, movimientosPosibles);
                    evaluarPosicion(tablero.getTablero(), i - 1, j, movimientosPosibles);
                    evaluarPosicion(tablero.getTablero(), i, j + 1, movimientosPosibles);
                    evaluarPosicion(tablero.getTablero(), i, j - 1, movimientosPosibles);
                }
            }
        }
        //hacemos una lista de posiciones libres y de posiciones ocupadas
        Coordenadas[] arrLibres = posicionesCoincidentes(1, movimientosPosibles);
        Coordenadas[] arrOcupadas = posicionesCoincidentes(2, movimientosPosibles);


        if (valentia > Random.Range(0, 100)) {
            Debug.Log("Tenemos un valiente. Valentia: " + valentia);
            if (arrOcupadas.Length > 0) {

                Coordenadas posExpansion = arrOcupadas[Random.Range(0, arrOcupadas.Length)];
                Ciudad ciudadAtacada;
                switch (tablero.getTablero()[posExpansion.getI(), posExpansion.getJ()]) {
                    case 1:
                        ciudadAtacada = tablero.ciudad1;
                        break;
                    case 2:
                        ciudadAtacada = tablero.ciudad2;
                        break;
                    case 3:
                        ciudadAtacada = tablero.ciudad3;
                        break;
                    case 4:
                        ciudadAtacada = tablero.ciudad4;
                        break;
                    default:
                        ciudadAtacada = new Ciudad();
                        break;
                }

                if (this.tamaño + this.ejercito > ciudadAtacada.tamaño + ciudadAtacada.ejercito) {
                    //ganamos nosotros
                    tablero.getTablero()[posExpansion.getI(), posExpansion.getJ()] = this.color;
                    ciudadAtacada.tamaño--;
                    //Perdida de puntos a la enemiga
                    ciudadAtacada.fitness -= 3;
                    ciudadAtacada.setPoblacion(ciudadAtacada.getPoblacion() - 5);
                    exito = true;
                    //Bonus por ataque completado con exito
                    this.fitness += 5;
                }
                else {
                    //Bonus por defensa
                    ciudadAtacada.fitness += 5;
                    exito = false;
                }




            }
            else if (arrLibres.Length > 0) {
                // Se puede hacer un movimiento a un espacio libre
               // Debug.Log("Hay una opcion de desplazamiento. La long del arrLibres es " + arrLibres.Length);
                //seleccionar posicion a random
                exito = true;
                Coordenadas posExpansion = arrLibres[Random.Range(0, arrLibres.Length)];
                tablero.getTablero()[posExpansion.getI(), posExpansion.getJ()] = this.color;

            }
        }

        else {
            Debug.Log("Tenemos un cobarde. Valentia: " + valentia);
            // Falta elegir una random
            if (arrLibres.Length > 0) {
                // Se puede hacer un movimiento a un espacio libre
                //Debug.Log("Hay una opcion de desplazamiento. La long del arrLibres es " + arrLibres.Length);
                //seleccionar posicion a random
                exito = true;
                Coordenadas posExpansion = arrLibres[Random.Range(0, arrLibres.Length)];
                tablero.getTablero()[posExpansion.getI(), posExpansion.getJ()] = this.color;

            }
            else if (arrOcupadas.Length > 0) {
                Coordenadas posExpansion = arrOcupadas[Random.Range(0, arrOcupadas.Length)];
                Ciudad ciudadAtacada;
                switch (tablero.getTablero()[posExpansion.getI(), posExpansion.getJ()]) {
                    case 1:
                        ciudadAtacada = tablero.ciudad1;
                        break;
                    case 2:
                        ciudadAtacada = tablero.ciudad2;
                        break;
                    case 3:
                        ciudadAtacada = tablero.ciudad3;
                        break;
                    case 4:
                        ciudadAtacada = tablero.ciudad4;
                        break;
                    default:
                        ciudadAtacada = new Ciudad();
                        break;
                }

                if (this.tamaño + this.ejercito > ciudadAtacada.tamaño + ciudadAtacada.ejercito) {
                    //ganamos nosotros
                    tablero.getTablero()[posExpansion.getI(), posExpansion.getJ()] = this.color;
                    ciudadAtacada.setPoblacion(ciudadAtacada.getPoblacion() - 5);
                    ciudadAtacada.tamaño--;
                    //perdida de puntos
                    ciudadAtacada.fitness -= 3;
                    exito = true;
                    //Bonus por ataque completado con exito
                    this.fitness += 5;
                }
                else {
                    //Bonus por defensa
                    ciudadAtacada.fitness += 5;
                    exito = false;
                }




            }
        }


        if (exito) {
            tamaño++;
            fitness += 5;
        }


        return exito;

    }
    //evalua si la posicion pasada es posible para la expansion y que tipo de expansion seria
    private int[,] evaluarPosicion(int[,] tablero, int iAEvaluar, int jAEvaluar, int[,] movimientosPosibles) {
        if (Constants.TAMAÑO > iAEvaluar && iAEvaluar >= 0 && Constants.TAMAÑO > jAEvaluar && jAEvaluar >= 0 && tablero[iAEvaluar, jAEvaluar] != this.color) {
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
        for (int i = 0; i < Constants.TAMAÑO; i++) {
            for (int j = 0; j < Constants.TAMAÑO; j++) {
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
    public int getColor() {
        return this.color;
    }

    public int getMinas() {
        int minas = 0;

        foreach (int valor in stats) {
            if (valor == MINAS) {
                minas++;
            }
        }
        return minas;
    }
    public int getGranjas() {
        int granjas = 0;

        foreach (int valor in stats) {
            if (valor == GRANJAS) {
                granjas++;
            }
        }
        return granjas;
    }
    public int getFitness()
    {
        return fitness;
    }
    public int getValentia()
    {
        return valentia;
    }
    public void setFitness(int fitness) {
        this.fitness = fitness;
    }
    
}