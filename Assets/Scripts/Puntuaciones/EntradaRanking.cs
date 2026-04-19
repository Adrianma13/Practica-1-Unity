using System;
using System.Collections.Generic;

[Serializable]
public class EntradaRanking {
    public string nombre;
    public int puntos;
}

[Serializable]
public class ListaRanking {
    public List<EntradaRanking> lista = new List<EntradaRanking>();
}