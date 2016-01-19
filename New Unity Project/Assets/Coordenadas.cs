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