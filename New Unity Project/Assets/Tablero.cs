using System;

public class Tablero {
    public Ciudad ciudad1;
    public Ciudad ciudad2;
    public Ciudad ciudad3;
    public Ciudad ciudad4;

    int[,] tablero;

    bool exito;

    public Tablero(int tamaño, Ciudad ciudad1, Ciudad ciudad2, Ciudad ciudad3, Ciudad ciudad4) {
        tablero = new int[tamaño, tamaño];
        this.ciudad1 = ciudad1;
        prepararCiudad(ciudad1, Constants.AZUL);
        this.ciudad2 = ciudad2;
        prepararCiudad(ciudad2, Constants.ROJO);
        this.ciudad3 = ciudad3;
        prepararCiudad(ciudad3, Constants.VERDE);
        this.ciudad4 = ciudad4;
        prepararCiudad(ciudad4, Constants.MORADO);
        inicializarTablero();
    }

    public void setExito(bool exito) {
        this.exito = exito;
        
    }


    public void prepararCiudad(Ciudad ciudad, int color) {
        ciudad.setEjercito(0);
        ciudad.setPoblacion(1);
        ciudad.setTamaño(1);
        ciudad.setColor(color);
    }

    public void inicializarTablero() {

        for (int i = 0; i < Constants.TAMAÑO; i++) {
            for (int j = 0; j < Constants.TAMAÑO; j++) {
                tablero[i, j] = 0;
            }
        }

        tablero[0, 0] = Constants.AZUL;
        tablero[Constants.TAMAÑO - 1, 0] = Constants.ROJO;
        tablero[0, Constants.TAMAÑO - 1] = Constants.VERDE;
        tablero[Constants.TAMAÑO - 1, Constants.TAMAÑO - 1] = Constants.MORADO;
    }

    public bool getExito() {
        return this.exito;
    }

    public int[,] getTablero() {
        return tablero;
    }

    public void setTablero(int[,] tablero) {
        this.tablero = tablero;
    }

}
