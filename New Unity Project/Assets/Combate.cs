﻿using UnityEngine;

public class Combate {

    public Tablero tablero;

    public Combate(Ciudad ciudad1, Ciudad ciudad2, Ciudad ciudad3, Ciudad ciudad4) {
        tablero = new Tablero(Constants.TAMAÑO, ciudad1, ciudad2, ciudad3, ciudad4);
    }
    public void ejecutarCombate() {
        tablero.ciudad1 = ejecutarTurno(tablero.ciudad1);
        tablero.ciudad2 = ejecutarTurno(tablero.ciudad2);
        tablero.ciudad3 = ejecutarTurno(tablero.ciudad3);
        tablero.ciudad4 = ejecutarTurno(tablero.ciudad4);
    }
    private Ciudad ejecutarTurno(Ciudad ciudad) {
        //comprobar si la ciudad sigue activa
        if (ciudad.getTamaño() > 0) {
            //incrementa valores de la ciudad
            ciudad.setFitness(ciudad.getFitness() + 1);
            float poblacionAntigua = ciudad.getPoblacion();

            ciudad.bonusEjercito += ciudad.getMinas() * 3 / 100;
            ciudad.bonusPoblacion += ciudad.getGranjas() * 3 / 100;

            ciudad.setPoblacion(ciudad.getPoblacion() + 1 + ciudad.getCiudadanos() * 3 + ciudad.bonusPoblacion);
            ciudad.setEjercito(ciudad.getEjercito() + 1 + ciudad.getMilitar() * 3 + ciudad.bonusEjercito);
            //comprueba expansion
            int num_veces = (int)((poblacionAntigua % 5) + ciudad.getPoblacion() - poblacionAntigua) / 5;
            for (int i = 0; i < num_veces; i++) {
                //Debug.Log("Se ha llamado a ciudad.expandir, el numero de poblacion ha aumentado " + num_veces + " veces");
                tablero.setExito(ciudad.expandir(this.tablero));
                if (tablero.getExito()) {
                    ciudad.setTamaño(ciudad.getTamaño() + 1);
                }
                //tablero.setTablero(aux.getTablero());
            }
        }
        return ciudad;


    }
    public bool combateTerminado() {
        bool resultado = true;
        int[,] tabAux = tablero.getTablero();
        int ciudad = tabAux[0, 0];
        for (int i = 0; i < Constants.TAMAÑO; i++) {
            for (int j = 0; j < Constants.TAMAÑO; j++) {
                if (tabAux[i, j] != ciudad) {
                    resultado = false;
                }
            }
        }
        return resultado;
    }

}